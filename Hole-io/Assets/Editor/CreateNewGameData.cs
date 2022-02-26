using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateNewGameData : EditorWindow
{
    private SerializedObject serializedObject;
    private SerializedProperty serializedProperty;

    protected Hole[] holes;
    public Hole newHole;
    // Start is called before the first frame update
    private void OnGUI()
    {

        serializedObject = new SerializedObject(newHole);
        serializedProperty = serializedObject.GetIterator();
        serializedProperty.NextVisible(true);
        DrawProperties(serializedProperty);
        if (GUILayout.Button("save"))
        {
            holes = GetAllInstances<Hole>();
            if (newHole.playerName == null)
            {
                newHole.playerName = "Hole" + (holes.Length + 1);
            }
            AssetDatabase.CreateAsset(newHole, "Assets/Scriptable Object/Hole" + (holes.Length + 1) + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Close();
        }

        Apply();
    }

    protected void DrawProperties(SerializedProperty p)
    {

        while (p.NextVisible(false))
        {
            EditorGUILayout.PropertyField(p, true);

        }
    }


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
