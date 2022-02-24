using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] HoleManager holeManager;
    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;
    private int score = 0;
    public int points = 0;
    private int growth = 0;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update

    private void Start()
    {

    }

    public void Update()
    {

    }

    public void GameStart()
    {
        SceneManager.LoadScene("Main Scene");
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
        score += points;
        Debug.Log(score);
        if(score % (10 + (growth*10)) == 0)
        {
            UpdateGameState(GameState.holeSize);
            growth++;
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
