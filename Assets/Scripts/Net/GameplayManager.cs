using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class GameplayManager : NetworkBehaviour
{

    public override void OnStartServer()
    {
        LobbyManager.OnGameStateUpdated.AddListener(GameStateUpdated);

        if (LobbyManager.Instance.GameState == GameState.Warmup)
        {
            StartCoroutine(WarmUpPhase());
        }
    }

    public override void OnStopServer()
    {
        LobbyManager.OnGameStateUpdated.RemoveListener(GameStateUpdated);
    }

    void GameStateUpdated(GameState gameState)
    {
        if (LobbyManager.Instance.GameState == GameState.Gameplay)
        {
            StartCoroutine(GameplayPhase());
        }
        else if (LobbyManager.Instance.GameState == GameState.Summary)
        {
            StartCoroutine(SummaryPhase());
        }
    }

    IEnumerator WarmUpPhase()
    {
        Debug.Log($"Gameplay Phase: WarmUpPhase");
        yield return new WaitForSeconds(3);
        LobbyManager.Instance.NextGameState();
    }

    IEnumerator GameplayPhase()
    {
        Debug.Log($"Gameplay Phase: GameplayPhase");
        yield return new WaitForSeconds(30);
        LobbyManager.Instance.NextGameState();
    }

    IEnumerator SummaryPhase()
    {
        Debug.Log($"Gameplay Phase: SummaryPhase");
        yield return new WaitForSeconds(2);
        LobbyManager.Instance.NextGameState();
    }

}