using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class EditorWindowGameData : EditorWindow
{
    protected SerializedObject serializedObject;
    protected SerializedProperty serializedProperty;

    protected Hole[] hole;
    //creates a new menu item called Tools and window called game data
    [MenuItem("Tools/GameData")]
    protected static void ShowWindow()
    {
        //creates a window called Game Data
        GetWindow<EditorWindowGameData>("Game Data");
    }

    private void OnGUI()
    {
        hole = GetAllInstances<Hole>();
        serializedObject = new SerializedObject(hole[0]);
        EditorGUILayout.LabelField("Player Data");
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));

        for (int i = 0; i < hole.Length; i++)
        {
            serializedObject = new SerializedObject(hole[i]);
            serializedProperty = serializedObject.GetIterator();
            serializedProperty.NextVisible(true);
            DrawProperties(serializedProperty);
        }

        EditorGUILayout.EndVertical();

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
    public static T[] GetAllInstances<T>() where T : Hole
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

    protected void Apply()
    {
        serializedObject.ApplyModifiedProperties();
    }
}
