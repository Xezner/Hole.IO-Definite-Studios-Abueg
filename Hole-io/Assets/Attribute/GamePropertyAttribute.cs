using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePropertyAttribute : PropertyAttribute
{
    public float holeScale;

    public GamePropertyAttribute(float holeScale)
    {
        this.holeScale = holeScale;
    }
}
