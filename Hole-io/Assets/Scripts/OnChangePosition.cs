using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnChangePosition : MonoBehaviour
{
    public PolygonCollider2D hole2dCollider;
    public PolygonCollider2D ground2DCollider;
    public MeshCollider GeneratedMeshCollider;
    public GameObject GroundObject;
    public float initialScale = 0.5f;
    Mesh GenMesh;
    // Start is called before the first frame update


    //FixedUpdate is used whenever there is physics involved.
    private void FixedUpdate()
    {
        //makes sure that there's a change in the transform
        if (transform.hasChanged == true)
        {
            transform.hasChanged = false;
            SetHoleCollider2D();
            SetGroundCollider2D();
            MakeHole2D();
            MakeMeshCollider();
        }
    }

    private void SetHoleCollider2D()
    {
        //initialScale = transform.localScale.x / 2f; <- didn't work
        //sets the position of the hole collider to the position of the hole
        hole2dCollider.transform.position = new Vector2(transform.position.x, transform.position.z);
        //rescales the hole collider to the scale of the hole's radius
        //hole2dCollider.transform.localScale = transform.localScale * initialScale;
        hole2dCollider.transform.localScale = new Vector2(transform.localScale.x * initialScale, transform.localScale.z * initialScale);

    }

    private void SetGroundCollider2D()
    {
        //gets the GameObject for Ground
        GroundObject = GameObject.FindGameObjectWithTag("Ground");
        float elemX, elemZ;
        //sets the X, and Z, points for Ground Collider to allow the Ground Collider to resize on the scale of the Ground Object
        elemX = GroundObject.transform.localScale.x / 2;
        elemZ = GroundObject.transform.localScale.z / 2;
        Vector2 elem0 = new Vector2(elemX, elemZ);
        Vector2 elem1 = new Vector2(-elemX, elemZ);
        Vector2 elem2 = new Vector2(-elemX, -elemZ);
        Vector2 elem3 = new Vector2(elemX, -elemZ);
        Vector2[] PointPositions = { elem0, elem1, elem2, elem3 };
        ground2DCollider.SetPath(0, PointPositions);

    }

    //Makes a Hole on the Ground Collider
    private void MakeHole2D()
    {
        //gets the points of the element 0 of the hole collider in the first path which is index 0, and saves it into an array
        Vector2[] PointPositions = hole2dCollider.GetPath(0);

        for ( int i = 0; i < PointPositions.Length; i++)
        {
            PointPositions[i] = hole2dCollider.transform.TransformPoint(PointPositions[i]);
        }
        //set the path to 2 in order to add an element to be able to set the 
        ///coordinates of where the hole of the ground is supposed to be
        ground2DCollider.pathCount = 2;
        //basically set the coordinates of the hole of the ground to the coordinates of the hole of the player
        ground2DCollider.SetPath(1, PointPositions);
    }

    //creates the mesh collider for the ground 
    private void MakeMeshCollider()
    {
        //makes sure that there's no generated mesh before generating a new mesh based off of the ground collider
        if (GenMesh != null) Destroy(GenMesh);
        GenMesh = ground2DCollider.CreateMesh(true, true);
        GeneratedMeshCollider.sharedMesh = GenMesh;
    }
}
