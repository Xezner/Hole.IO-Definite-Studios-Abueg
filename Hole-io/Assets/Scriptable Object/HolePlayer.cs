using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hole")]
public class HolePlayer : ScriptableObject
{
    public string playerName;
    public float moveSpeed;
    public float pointsToGrow;  
    public float pointsToGrowMultiplier;
    public Material holeMaterial;
    public Material arrowMaterial;

}
