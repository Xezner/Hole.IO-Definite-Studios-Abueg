using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;
    public List<string> listStr;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        listStr = PlayerPrefsExtra.GetList<string>("highScoreName");
    }

    // Update is called once per frame
    public void highScore()
    {
        listStr.Add(GameManager.Instance.score + " - " + PlayerController.Instance.hole.playerName);
        getTopFiveScores();
        PlayerPrefsExtra.SetList("highScoreName", listStr);
    }

    public void clearList()
    {
        listStr.Clear();
        PlayerPrefsExtra.SetList("highScoreName", listStr);
    }


    public void getTopFiveScores()
    {
        var myComparer = new CustomComparer();
        listStr.Sort(myComparer);
        listStr.Reverse();
        if(listStr.Count > 5) listStr.RemoveRange(5, (listStr.Count-5));
    }

}
