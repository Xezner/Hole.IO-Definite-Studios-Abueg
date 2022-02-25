using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    
    [SerializeField] GameObject fcp = null;
    public bool isSkinsClicked;
    public TextMeshProUGUI leaderboardsText;
    public List<string> listStr;
    // Start is called before the first frame update
    void Start()
    {
        setTopFiveScores();
        fcp.gameObject.SetActive(false);
        isSkinsClicked = false;
    }

    //gets the top scores list and sets it on our leaderboards menu
    private void setTopFiveScores()
    {
        listStr = PlayerPrefsExtra.GetList<string>("highScoreName");
        var myComparer = new CustomComparer();
        listStr.Sort(myComparer);
        listStr.Reverse();

        if (listStr.Count > 5) listStr.RemoveRange(5, (listStr.Count - 5));
        leaderboardsText.text = "Leaderboards";

        int j = 1;
        foreach(string hiScore in listStr)
        {
            leaderboardsText.text = leaderboardsText.text + " \n" + j + ". " + hiScore;
            j++;
        }
    }


    public void SkinsButtonOnClick()
    {
        //button for the color picker that we have created
        if (isSkinsClicked)
        {
            fcp.gameObject.SetActive(false);
            isSkinsClicked = false;
        }
        else
        {
            fcp.gameObject.SetActive(true);
            isSkinsClicked = true;
        }
    }

}
