using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Obstacles")]
public class Obstacles : ScriptableObject
{
    public string obstacleName;
    public int pointsGiven;
    public GameObject obstaclePrefab;
}
