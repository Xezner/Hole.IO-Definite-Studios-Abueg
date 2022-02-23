using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update

    private void Start()
    {
        UpdateGameState(GameState.holeColor);
    }
    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.isGameActive:
                break;
            case GameState.playerScore:
                break;
            case GameState.holeSize:
                break; 
            case GameState.holeColor:
                selectHoleColor();
                break;
            case GameState.playerRank:
                break;
            case GameState.gameTimer:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void selectHoleColor()
    {
        throw new NotImplementedException();
    }

    public enum GameState
    {
        isGameActive,
        playerScore,
        holeSize,
        holeColor,
        playerRank,
        gameTimer
    }
}
