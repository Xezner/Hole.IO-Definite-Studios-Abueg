using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public Collider GroundCollider;
    public HoleManager onChangePosition;
    // Start is called before the first frame update
    private void Start()
    {
        GameObject[] Obstacles = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (var GameObj in Obstacles)
        {
            if (GameObj.layer == LayerMask.NameToLayer("Obstacles"))
            {
                Physics.IgnoreCollision(GameObj.GetComponent<Collider>(), onChangePosition.GeneratedMeshCollider, true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //in theory the ground mesh collider is the hole itself while the Hole that you see is just a sprite
        //when the ground collider is ignored, make the mesh collider active
        Physics.IgnoreCollision(other, GroundCollider, true);
        Physics.IgnoreCollision(other, onChangePosition.GeneratedMeshCollider, false);
    }

    private void OnTriggerExit(Collider other)
    {
        //resets collision properties
        Physics.IgnoreCollision(other, GroundCollider, false);
        Physics.IgnoreCollision(other, onChangePosition.GeneratedMeshCollider, true);
    }
}
