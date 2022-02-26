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
    protected Ground[] ground;
    protected GameManagement[] gm;
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
        ground = GetAllInstancesGround<Ground>();
        gm = GetAllInstancesGM<GameManagement>();

        EditorGUILayout.BeginHorizontal();
        //GUI for the sliders with a max width of 150
        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));

        //adds the slider bars on the left side of the editor
        DrawSliderBarHole(hole);
        DrawSliderBarObstacles(obstacles);
        DrawSliderBarGround(ground);
        DrawSliderBarGM(gm);
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));

        //if a tab is selected proceed here
        if (selectedProperty != null)
        {

            for (int i = 0; i < hole.Length; i++)
            {
                //checks if the tab being selected is the player data, and saves any changes made to the properties of this serializedObject
                if ("Player Data" == selectedProperty)
                {
                    serializedObject = new SerializedObject(hole[i]);
                    serializedProperty = serializedObject.GetIterator();
                    serializedProperty.NextVisible(true);
                    DrawProperties(serializedProperty);
                    Apply();
                }
            }

            //loops through all the different obstacles we have and basically do the same thing as bove
            for (int i = 0; i < obstacles.Length; i++)
            {
                if (obstacles[i].obstacleName == selectedProperty)
                {
                    ForObstacle(i);
                }
            }

            for (int i = 0; i < ground.Length; i++)
            {
                //checks if the tab being selected is the player data, and saves any changes made to the properties of this serializedObject
                if ("Ground Scale" == selectedProperty)
                {
                    serializedObject = new SerializedObject(ground[i]);
                    serializedProperty = serializedObject.GetIterator();
                    serializedProperty.NextVisible(true);
                    DrawProperties(serializedProperty);
                    Apply();
                }
            }
            for (int i = 0; i < gm.Length; i++)
            {
                //checks if the tab being selected is the player data, and saves any changes made to the properties of this serializedObject
                if ("Countdown Timer" == selectedProperty)
                {
                    serializedObject = new SerializedObject(gm[i]);
                    serializedProperty = serializedObject.GetIterator();
                    serializedProperty.NextVisible(true);
                    DrawProperties(serializedProperty);
                    Apply();
                }
            }
        }
        //if no tab was selected in the drawer then output the following
        else
        {
            EditorGUILayout.LabelField("Select an item from the list.");
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }

    protected void ForObstacle(int i)
    {
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
        //basically find game assets of type HolePlayer with the name it needs
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
        //basically find game assets of type Obstacles with the name it needs
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
        T[] a = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
        }

        return a;
    }
    public static T[] GetAllInstancesGround<T>() where T : Ground
    {
        //basically find game assets of type Ground with the name it needs
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
        T[] a = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
        }

        return a;
    }
    public static T[] GetAllInstancesGM<T>() where T : GameManagement
    {
        //basically find game assets of type Ground with the name it needs
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
        //sets the name of the button to the name of the property
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

    protected void DrawSliderBarGround(Ground[] grnd)
    {
        //sets the name of the button to the name of the property
            if (GUILayout.Button("Ground Scale"))
            {
                selectedPropertyPach = "Ground Scale";
            }

        if (!string.IsNullOrEmpty(selectedPropertyPach))
        {
            selectedProperty = selectedPropertyPach;
        }
    }

    protected void DrawSliderBarGM(GameManagement[] gameMgmt)
    {
        //sets the name of the button to the name of the property
        if (GUILayout.Button("Countdown Timer"))
        {
            selectedPropertyPach = "Countdown Timer";
        }

        if (!string.IsNullOrEmpty(selectedPropertyPach))
        {
            selectedProperty = selectedPropertyPach;
        }
    }
    protected void Apply()
    {
        serializedObject.ApplyModifiedProperties();
    }
}
