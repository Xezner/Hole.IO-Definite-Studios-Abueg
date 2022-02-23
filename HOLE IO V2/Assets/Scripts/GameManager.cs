using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] HoleManager holeManager;
    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;
    private int score = 0;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update

    private void Start()
    {
    }
    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.isGameActive:
                break;
            case GameState.playerScore:
                handlePlayerScore();
                break;
            case GameState.holeSize:
                handleHoleSize();
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

    private void handleHoleSize()
    {

        StartCoroutine(holeManager.ScaleHole());
    }

    private void handlePlayerScore()
    {
        score++;
        Debug.Log(score);
        if(score % 10 == 0)
        {
            UpdateGameState(GameState.holeSize);
        }
    }

    private void selectHoleColor()
    {

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
