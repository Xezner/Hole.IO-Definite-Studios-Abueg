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
    private int growth = 0;

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

    
    //called when the game is started from the main menu
    public void GameStart()
    {
        //if there is not input for the player name, defaults the name to ANON
        if (inputPlayerName != null)
        {
            if (inputPlayerName.text.Length <= 1)
            {
                hole.playerName = "ANON";
            }
            else hole.playerName = inputPlayerName.text;
        }
        SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
    }

    //restarts the game from the play again button
    public void GameRestart()
    {
        SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
    }

    //called when we go back to the title scene from the main scene
    public void BackToStart()
    {
        SceneManager.LoadScene("Title Scene", LoadSceneMode.Single);
    }

    //closes the application
    public void ExitApplication()
    {
        Application.Quit();
    }
    public void Update()
    {

    }

   
    //updates what state of the game we are in
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
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }


    // does the animation for the hole size growth
    private void handleHoleSize()
    {
        StartCoroutine(holeManager.ScaleHole());
    }

    //adds up the points of the player and displays it on the UI
    private void handlePlayerScore()
    {
        score += points;
        scoreText.text = "Score: " + score;
        //if the score reaches a certain number updates the size of the hole
        float pointsToGrow = hole.pointsToGrow;
        float multiplier = growth * hole.pointsToGrowMultiplier;
        if (score >= (pointsToGrow + pointsToGrow*multiplier*growth))
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
