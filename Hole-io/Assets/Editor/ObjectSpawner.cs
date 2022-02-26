using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class ObjectSpawner : EditorWindow
{
    protected string objectName = "";
    protected int objectID = 1;
    protected GameObject objectToSpawn;
    protected float minPosX;
    protected float maxPosX;
    protected float minPosY;
    protected float maxPosY;

    [MenuItem("Tools/Object Spawner")]
    public static void ShowWindow()
    {
        GetWindow<ObjectSpawner>("Object Spawner");
    }

    private void OnGUI()
    {
        GUILayout.Label("Spawn New Object", EditorStyles.boldLabel);

        objectName = EditorGUILayout.TextField("Object Name", objectName);
        objectID = EditorGUILayout.IntField("Object ID", objectID);
        objectToSpawn = EditorGUILayout.ObjectField("Prefab to Spawn", objectToSpawn, typeof(GameObject), false) as GameObject;
        if(objectToSpawn != null) objectName = objectToSpawn.name;

        if (GUILayout.Button("Spawn Object"))
        {
            
            SpawnObject();
        }

    }

    private void SpawnObject()
    {
        
        if(objectToSpawn == null)
        {
            Debug.LogError("Error: Please assign a prefab to be spawned");
            return;
        }
        if (objectName == string.Empty)
        {
            objectName = objectToSpawn.name;
        }
            float posX = GameObject.Find("Ground").transform.localScale.x/2f - objectToSpawn.transform.localScale.x/2f;
            minPosX = -posX;
            maxPosX = posX;
            float posZ = GameObject.Find("Ground").transform.localScale.y / 2f - objectToSpawn.transform.localScale.y / 2f;
            minPosY = -posZ;
            maxPosY = posZ;
            
            Vector3 halfExtents = (objectToSpawn.transform.localScale / 2f);
            Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(minPosX, maxPosX), 0.5f  , UnityEngine.Random.Range(minPosY, maxPosY));            
            Quaternion yRotate =  Quaternion.AngleAxis(UnityEngine.Random.Range(0, 180), Vector3.up);

        /*Collider[] hitColliders = Physics.OverlapBox(spawnPos, ((objectToSpawn.transform.localScale)*1.1f), yRotate);
    Debug.Log(yRotate);
    int i = 0;
    if (hitColliders.Length > i)
    {
        Debug.LogError("Spawn Point Occupied by: " + hitColliders[i].name);
        if (hitColliders[i].name == "Player" || hitColliders[i].name == "Ground")
        {
            GameObject newObject = Instantiate(objectToSpawn, spawnPos, yRotate, GameObject.Find("Obstacles").transform);
            Debug.Log(yRotate);
            newObject.name = objectName + objectID;
            objectID++;
        }
        else
        {
            
            //DestroyImmediate(newObject);
        }

    }*/
        Debug.Log("Half: " + halfExtents);
        Debug.Log("Spawn.Y: " + spawnPos.y);
        Debug.Log("Spawn point: " + spawnPos);
        Debug.Log("y"+yRotate);
        if (!Physics.CheckBox(spawnPos, halfExtents , yRotate))
        {
            Debug.Log("y"+yRotate);
            GameObject newObject = Instantiate(objectToSpawn, spawnPos, yRotate, GameObject.Find("Obstacles").transform);
            newObject.name = objectName + objectID;
            objectID++;
        }
        else
        {
            Debug.LogError("Destroyed Object");
        }  


    }
}
