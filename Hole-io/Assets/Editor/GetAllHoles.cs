using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class GetAllHoles : MonoBehaviour
{
    private HolePlayer[] holes;
    // Start is called before the first frame update
    void Start()
    {
        holes = GetAllInstances<HolePlayer>();
    }

    public static T[] GetAllInstances<T>() where T : HolePlayer
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
