using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    
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
                break;
            case GameState.playerRank:
                break;
            case GameState.gameTimer;
                break;
            default:
                break;
        }
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
