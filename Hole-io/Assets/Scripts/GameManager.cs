using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] HoleManager holeManager;
    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;
    public Hole hole;
    public int score = 0;
    public int points = 0;
    private int growth = 1;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI inputPlayerName;
    
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update

    private void Start()
    {   
    }

    
    public void GameStart()
    {

        if(inputPlayerName.text.Length <=1)
        {
            hole.playerName = "ANON";
        }
        else hole.playerName = inputPlayerName.text;
        SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
    }

    public void Update()
    {

    }

   

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        { 
            case GameState.playerScore:
                handlePlayerScore();
                break;
            case GameState.holeSize:
                handleHoleSize();
                break;
            case GameState.playerRank:
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
        scoreText.text = "Score: " + score;
        if (score % (growth*hole.pointsNeededToGrowMultipler) == 0)
        {
            UpdateGameState(GameState.holeSize);
            growth++;
        }
    }
    public enum GameState
    {
        isGameActive,
        playerScore,
        holeSize,
        holeColor,
        playerRank,
        gameTimer,
        gameOver
    }
}
