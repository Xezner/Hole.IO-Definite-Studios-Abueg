using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DeletePlayerPref : EditorWindow
{
    [MenuItem("Tools/Reset Player Pref")]

    public static void ResetPlayerPref()
    {
        PlayerPrefs.DeleteAll();

    }
}
