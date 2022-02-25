using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePropertyAttribute : PropertyAttribute
{
    public float movementSpeed;

    public GamePropertyAttribute(float movementSpeed)
    {
        this.movementSpeed = movementSpeed;
    }
}
