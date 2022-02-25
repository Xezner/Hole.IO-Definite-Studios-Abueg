using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject timeScoreMenu;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI nameText;
    public static GameTimer instance;
    public TextMeshProUGUI timerText;
    public GameManagement gm;
    private float timeLeft;
    private TimeSpan timeLeftPlaying;
    private bool isTimerGoing;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        //enables Player Controller once game timer starts
        PlayerController.Instance.enabled = true;
        //sets the time and score ui active while sets the game overmenu enactive
        timeScoreMenu.SetActive(true);
        gameOverMenu.SetActive(false);

        //initializes our timer
        SetTimer();
    }
    private void SetTimer()
    {
        
        //converts our int for the coundown timer to a usable timespan
        isTimerGoing = true;
        timeLeft = gm.timeToEnd;
        timeLeftPlaying = TimeSpan.FromSeconds(timeLeft);
        timerText.text = "Time Left: " + timeLeftPlaying.ToString("mm':'ss'.'ff");
        //starts timer
        BeginTimer();
    }

    public void BeginTimer()
    {
        //subtract the timeleft by delta time to normalize 1second
            timeLeft -= Time.deltaTime;
            timeLeftPlaying = TimeSpan.FromSeconds(timeLeft);
            timerText.text = "Time Left: " + timeLeftPlaying.ToString("mm':'ss'.'ff");
        //given that it doesn't reach 0seconds we find the closes thing to 0 second and end the timer there
            if (timeLeft <= 0.001f) EndTimer();
    }

    public void EndTimer()
    {
        //disables the player controller to above movement
        isTimerGoing = false;
        PlayerController.Instance.enabled = false;
        //gets the score the player ended with
        int myScore = GameManager.Instance.score;
        //displays the score on the score ui
        scoreText.text = "Your score: " + myScore.ToString();
        //gets the name of the player
        string myName = PlayerController.Instance.hole.playerName;
        //displays the name of the player
        nameText.text = "Your name: " + myName;
        //saves the name and score of the player if it's high score or not
        SaveSystem.Instance.highScore();
        //disables time and score ui and enables game over screen
        timeScoreMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        
    }

    private void Update()
    {
        //repeated run the timer
        if(isTimerGoing) BeginTimer();
    }
}
