using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

public class Player : NetworkBehaviour
{

    public static Player local;

    public static UnityEvent<GameState> OnGameStateUpdated = new UnityEvent<GameState>();
    public static UnityEvent<int> OnCountdownUpdated = new UnityEvent<int>();

    [Header("Prefabs")]
    [SerializeField] GameObject playerCharacterPrefab;

    [Header("Debug")]
    public Tank playerCharacter;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public override void OnStartClient()
    {
        if (hasAuthority)
        {
            NetworkRoomManager.Instance.autoCreatePlayer = false;

            if (local != null)
            {
                DestroyImmediate(local.gameObject);
            }
            local = this;

            LobbyManager.OnGameStateUpdated.AddListener(GameStateUpdated);
            GameStateUpdated(LobbyManager.Instance.GameState);

            LobbyManager.OnMatchReadyCountdownUpdated.AddListener(CountdownUpdated);
        }
    }

    public override void OnStopClient()
    {
        if (hasAuthority)
        {
            LobbyManager.OnGameStateUpdated.RemoveListener(GameStateUpdated);

            LobbyManager.OnMatchReadyCountdownUpdated.RemoveListener(CountdownUpdated);
        }
    }

    public override void OnStartServer()
    {
        LobbyManager.Instance?.AddPlayer(this);
    }

    public override void OnStopServer()
    {
        LobbyManager.Instance?.RemovePlayer(this);
    }

    /* 
        LOBBY
    */

    [Client]
    public void ReadyPlayer(bool ready)
    {
        Debug.Log($"Player ready [{ready}]");
        CmdReadyPlayer(ready);
    }

    [Command]
    void CmdReadyPlayer(bool ready)
    {
        LobbyManager.Instance.PlayerReady(this, ready);
    }

    /* 
        GAMESTATE
    */

    [Client]
    void GameStateUpdated(GameState gameState)
    {
        Debug.Log($"Game State Updated: {gameState}");
        OnGameStateUpdated.Invoke(gameState);
    }

    [Client]
    void CountdownUpdated(int time)
    {
        Debug.Log($"Countdown Updated: {time}");
        OnCountdownUpdated.Invoke(time);
    }

    /* 
        SPAWN CHARACTER
    */

    [Server]
    public void SpawnCharacter(UnityAction OnComplete)
    {
        StartCoroutine(SpawnInCharacter(OnComplete));
    }

    IEnumerator SpawnInCharacter(UnityAction OnComplete)
    {
        yield return null;

        playerCharacter = Instantiate(playerCharacterPrefab).GetComponent<Tank>();
        //Find spawn point
        NetworkServer.ReplacePlayerForConnection(connectionToClient, playerCharacter.gameObject, true);

        yield return null;
        OnComplete.Invoke();
    }

    /* 
        TRANSITIONS
    */

    [Server]
    public void Fade(bool fadeIn)
    {
        TargetFade(fadeIn);
    }

    [TargetRpc]
    void TargetFade(bool fadeIn)
    {
        Debug.Log($"Fading {(fadeIn ? "In" : "Out")}");
    }

}