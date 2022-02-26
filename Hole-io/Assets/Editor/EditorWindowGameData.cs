using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class EditorWindowGameData : EditorWindow
{
    protected SerializedObject serializedObject;
    protected SerializedProperty serializedProperty;

    protected HolePlayer[] hole;
    protected Obstacles[] obstacles;
    protected Obstacles[] obsOld;
    protected string selectedPropertyPach;
    protected string selectedProperty;

    //creates a new menu item called Tools and window called game data
    [MenuItem("Tools/GameData")]
    protected static void ShowWindow()
    {
        //creates a window called Game Data
        GetWindow<EditorWindowGameData>("Game Data");
    }

    private void OnGUI()
    {
       
        hole = GetAllInstancesHole<HolePlayer>();
        obstacles = GetAllInstancesObstacles<Obstacles>();
        //EditorGUILayout.LabelField("Player Data");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));
        DrawSliderBarHole(hole);
        DrawSliderBarObstacles(obstacles);
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));

        if (selectedProperty != null)
        {

            for (int i = 0; i < hole.Length; i++)
            {
                if ("Player Data" == selectedProperty)
                {
                    serializedObject = new SerializedObject(hole[i]);
                    serializedProperty = serializedObject.GetIterator();
                    serializedProperty.NextVisible(true);
                    DrawProperties(serializedProperty);
                    Apply();
                }
            }
            
            for (int i = 0; i < obstacles.Length; i++)
            {
                if (obstacles[i].obstacleName == selectedProperty)
                {
                    ForObstacle(i);
                }
            }
        }
        else
        {
            EditorGUILayout.LabelField("Select an item from the list.");
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }

    protected void ForObstacle(int i)
    {
        obsOld = GetAllInstancesObstacles<Obstacles>();
        serializedObject = new SerializedObject(obstacles[i]);
        serializedProperty = serializedObject.GetIterator();
        serializedProperty.NextVisible(true);
        DrawProperties(serializedProperty);
        EditorGUILayout.LabelField("*New obstacle name must be copy & pasted due to an existing bug");
        Apply();
        
    }

    protected void DrawProperties(SerializedProperty p)
    {
        while(p.NextVisible(false))
        {
            EditorGUILayout.PropertyField(p, true);
        }
    }

    //gets all instances of hole to add to the editor window
    public static T[] GetAllInstancesHole<T>() where T : HolePlayer
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
        T[] a = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
        }

        return a;
    }

    public static T[] GetAllInstancesObstacles<T>() where T : Obstacles
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
        T[] a = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
        }

        return a;
    }


    protected void DrawSliderBarHole(HolePlayer[] allHoles)
    {
        foreach (HolePlayer h in allHoles)
        {
            if (GUILayout.Button("Player Data"))
            {
                selectedPropertyPach = "Player Data";
            }
        }

        if (!string.IsNullOrEmpty(selectedPropertyPach))
        {
            selectedProperty = selectedPropertyPach;
        }
    }

    protected void DrawSliderBarObstacles(Obstacles[] allObstacles)
    {
        foreach (Obstacles obs in allObstacles)
        {
            if (GUILayout.Button(obs.obstacleName))
            {
                selectedPropertyPach = obs.obstacleName;
            }
        }

        if (!string.IsNullOrEmpty(selectedPropertyPach))
        {
            selectedProperty = selectedPropertyPach;
        }

        if (GUILayout.Button("New Obstacle"))
        {
            Obstacles newObs = ScriptableObject.CreateInstance<Obstacles>();
            CreateNewObstacle newObsWindow = GetWindow<CreateNewObstacle>("New Obstacle");
            newObsWindow.newObs = newObs;

        }
    }

    protected void Apply()
    {
        serializedObject.ApplyModifiedProperties();
    }
}
