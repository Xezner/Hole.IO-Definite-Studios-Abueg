using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateNewObstacle : EditorWindow
{
    private SerializedObject serializedObject;
    private SerializedProperty serializedProperty;

    protected Obstacles[] obs;
    public Obstacles newObs;
    // Start is called before the first frame update
    private void OnGUI()
    {

        serializedObject = new SerializedObject(newObs);
        serializedProperty = serializedObject.GetIterator();
        serializedProperty.NextVisible(true);
        DrawProperties(serializedProperty);
        if (GUILayout.Button("save"))
        {
            obs = GetAllInstances<Obstacles>();
            if (newObs.obstacleName == null)
            {
                newObs.obstacleName = "Building" + (obs.Length + 1);
            }
            AssetDatabase.CreateAsset(newObs, "Assets/Scriptable Object/" + newObs.obstacleName + ".asset");
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


    public static T[] GetAllInstances<T>() where T : Obstacles
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