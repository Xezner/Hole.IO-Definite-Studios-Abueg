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
        timeScoreMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        SetTimer();
    }
    private void SetTimer()
    {
        isTimerGoing = true;
        timeLeft = gm.timeToEnd;
        timeLeftPlaying = TimeSpan.FromSeconds(timeLeft);
        timerText.text = "Time Left: " + timeLeftPlaying.ToString("mm':'ss'.'ff");
        BeginTimer();
    }

    public void BeginTimer()
    {
            timeLeft -= Time.deltaTime;
            timeLeftPlaying = TimeSpan.FromSeconds(timeLeft);
            timerText.text = "Time Left: " + timeLeftPlaying.ToString("mm':'ss'.'ff");
            if (timeLeft <= 0.001f) EndTimer();
    }

    public void EndTimer()
    {
        isTimerGoing = false;
        PlayerController.Instance.enabled = false;
        int myScore = GameManager.Instance.score;
        Debug.Log(myScore);
        timeScoreMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        
    }


    private void Update()
    {
        if(isTimerGoing) BeginTimer();
    }
}
