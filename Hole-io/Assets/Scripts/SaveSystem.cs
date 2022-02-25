using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;
    //name of our list for the score and playerName
    public List<string> listStr;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //gets the current list of highscores and names
        listStr = PlayerPrefsExtra.GetList<string>("highScoreName");
    }

    // Update is called once per frame
    public void highScore()
    {
        //adds the current highscores and names to our list
        listStr.Add(GameManager.Instance.score + " - " + PlayerController.Instance.hole.playerName);
        //calculates and arranges the scores in descending order
        getTopFiveScores();
        PlayerPrefsExtra.SetList("highScoreName", listStr);
    }

    //function to clear all the top scores in the game
    public void clearList()
    {
        listStr.Clear();
        PlayerPrefsExtra.SetList("highScoreName", listStr);
    }


    public void getTopFiveScores()
    {
        //this comparer is used to sort the list that we have
        var myComparer = new CustomComparer();
        listStr.Sort(myComparer);
        //comparer is ascending in default, so reversing it would change it to descending order (from highest to lowest)
        listStr.Reverse();
        //once the list is sorted from highest to lowest, only get the top 5(five), delete the rest to save memory
        if(listStr.Count > 5) listStr.RemoveRange(5, (listStr.Count-5));
    }

}
