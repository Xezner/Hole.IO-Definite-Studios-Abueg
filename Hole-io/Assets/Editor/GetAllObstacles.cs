using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GetAllObstacles : MonoBehaviour
{
    private Obstacles[] obstacles;
    // Start is called before the first frame update
    void Start()
    {
        
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
}
