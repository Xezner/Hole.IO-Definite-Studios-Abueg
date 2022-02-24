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

    private void setTopFiveScores()
    {
        listStr = PlayerPrefsExtra.GetList<string>("highScoreName");
        var myComparer = new CustomComparer();
        listStr.Sort(myComparer);
        listStr.Reverse();
        if (listStr.Count > 5) listStr.RemoveRange(5, (listStr.Count - 5));
        leaderboardsText.text = "Leaderboards\n" + "1. " + listStr[0] + "\n2. " + listStr[1] + "\n3. " + listStr[2] + "\n4. " + listStr[3] +"\n5. " + listStr[4];
    }


    public void SkinsButtonOnClick()
    {
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

    public void ClosedByMenu()
    {
        fcp.gameObject.SetActive(false);
        isSkinsClicked = false;
    }
    
}
