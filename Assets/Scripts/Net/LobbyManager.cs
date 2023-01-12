using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

public class LobbyManager : NetworkBehaviour
{

    public static LobbyManager Instance;

    public static UnityEvent<GameState> OnGameStateUpdated = new UnityEvent<GameState>();
    public static UnityEvent<int> OnMatchReadyCountdownUpdated = new UnityEvent<int>();

    [Header("References")]
    [SerializeField] GameplayManager gameplayManagerPrefab;

    [Header("Settings")]
    [SerializeField] int minPlayers = 2;
    [SerializeField] int maxPlayers = 4;
    [SerializeField] float matchReadyCountdown = 3;

    [Header("Data")]
    [SerializeField] LevelObject lobbyScene;
    [SerializeField] List<LevelObject> levels = new List<LevelObject>();
    [SerializeField] List<LevelObject> levelsPlayed = new List<LevelObject>();

    [Header("Debug")]
    [SerializeField] List<Player> playersConnected = new List<Player>();
    [SerializeField] List<Player> playersReady = new List<Player>();
    [SerializeField] bool matchReady = false;
    [SerializeField, SyncVar(hook = nameof(GameStateUpdated))] GameState gameState;

    public GameState GameState { get { return gameState; } }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    /* 
        PLAYERS
    */

    [Server]
    public void AddPlayer(Player player)
    {
        if (!playersConnected.Contains(player))
        {
            playersConnected.Add(player);
            Debug.Log($"Player connected. [{playersConnected.Count}]");
        }
    }

    [Server]
    public void RemovePlayer(Player player)
    {
        if (playersConnected.Contains(player))
        {
            playersConnected.Remove(player);
            Debug.Log($"Player disconnected. [{playersConnected.Count}]");
        }
    }

    /* 
        MATCHMAKING
    */

    public void PlayerReady(Player player, bool ready)
    {
        if (ready)
        {
            if (!playersReady.Contains(player))
            {
                playersReady.Add(player);
                Debug.Log($"Player ready: [{playersReady.Count}/{minPlayers}]");
            }
        }
        else
        {
            if (playersReady.Contains(player))
            {
                playersReady.Remove(player);
                Debug.Log($"Player ready: [{playersReady.Count}/{minPlayers}]");
            }
        }
        CheckMatchReady();
    }

    void CheckMatchReady()
    {
        if (gameState == GameState.Waiting)
        {
            if (playersReady.Count >= minPlayers)
            {
                Debug.Log($"Game Starting");

                if (!matchReady)
                {
                    StartCoroutine(MatchReadyCountdown());
                }
            }
        }
    }

    IEnumerator MatchReadyCountdown()
    {
        matchReady = true;
        float countDownTime = matchReadyCountdown;
        int currentSecond = (int)matchReadyCountdown;
        RpcCountDownTimeUpdated(currentSecond);

        while (countDownTime > 0)
        {
            countDownTime -= Time.deltaTime;
            if (currentSecond - countDownTime >= 1)
            {
                currentSecond--;
                RpcCountDownTimeUpdated(currentSecond);
            }

            if (playersReady.Count < minPlayers)
            {
                break;
            }

            //Check for max players then start game immediately
            if (playersReady.Count == maxPlayers)
            {
                break;
            }

            yield return null;
        }
        RpcCountDownTimeUpdated(0);

        if (playersReady.Count >= minPlayers)
        {
            List<Player> _playersReady = new List<Player>();
            playersReady.ForEach(x => _playersReady.Add(x));

            LevelObject selectedLevel = SelectLevel();

            NetworkRoomManager.Instance.SwitchScene(selectedLevel.levelName, () => {
                for (var i = 0; i < _playersReady.Count; i++)
                {
                    StartCoroutine(SpawnPlayerCharacterDelayed(_playersReady[i]));
                }

                _playersReady.ForEach(x => {
                    playersReady.Remove(x);
                });

                NextGameState();

                GameplayManager gameplayManager = Instantiate(gameplayManagerPrefab);
                NetworkServer.Spawn(gameplayManager.gameObject);
            });
        }
        else
        {
            gameState = 0;
        }
        matchReady = false;
    }

    LevelObject SelectLevel()
    {
        LevelObject selectedLevel = levels[Random.Range(0, levels.Count)];
        levelsPlayed.Add(selectedLevel);
        levels.Remove(selectedLevel);
        if (levels.Count == 0)
        {
            levelsPlayed.ForEach(x => levels.Add(x));
            levelsPlayed.Clear();
        }
        return selectedLevel;
    }

    [ClientRpc]
    void RpcCountDownTimeUpdated(int time)
    {
        OnMatchReadyCountdownUpdated.Invoke(time);
    }

    /* 
        GAMEPLAY
    */

    [Server]
    [ContextMenu("Next Game State")]
    public void NextGameState()
    {
        gameState += 1;

        if ((int)gameState >= System.Enum.GetNames(typeof(GameState)).Length)
        {
            NetworkRoomManager.Instance.SwitchScene(lobbyScene.levelName, () => {
                for (int i = 0; i < playersConnected.Count; i++)
                {
                    Debug.Log($"Reassigning authority for {playersConnected[i].netId}");
                    NetworkServer.ReplacePlayerForConnection(playersConnected[i].connectionToClient, playersConnected[i].gameObject, true);
                }
            });
            gameState = 0;
        }

        Debug.Log($"GameState: {gameState}");
    }

    void GameStateUpdated(GameState oldValue, GameState newValue)
    {
        Debug.Log($"GameState Updated: {gameState}");
        OnGameStateUpdated.Invoke(newValue);
    }

    IEnumerator SpawnPlayerCharacterDelayed(Player player)
    {
        player.Fade(false);

        yield return new WaitForSeconds(1);
        player.SpawnCharacter(() => {
            player.Fade(true);
        });
    }

}

public enum GameState
{
    Waiting = 0,
    Warmup = 1,
    Gameplay = 2,
    Summary = 3
}