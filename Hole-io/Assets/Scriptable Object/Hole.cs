using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hole")]
public class Hole : ScriptableObject
{
    public string playerName;
    public float moveSpeed;
    public float holeScale;
    public Material holeMaterial;
    public Material arrowMaterial;
}
