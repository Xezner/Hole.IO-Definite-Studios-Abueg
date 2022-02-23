using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnChangePosition : MonoBehaviour
{
    public PolygonCollider2D hole2DCollider;
    public PolygonCollider2D ground2DCollider;
    public MeshCollider GeneratedMeshCollider;
    //
    public GameObject GroundObject;
    public float initialScale = 0.5f;
    Mesh GeneratedMesh;

    private void Start()
    {
        
    }
    private void FixedUpdate()
    {
        
        if (transform.hasChanged == true)
        {
            transform.hasChanged = false;
            SetGroundCollider2D();
            SetHoleCollider2D();
            MakeHole2D();
            Make3DMeshCollider();
        }
    }

    private void SetHoleCollider2D()
    {
        //initialScale = transform.localScale.x /2f; <- didn't work
        hole2DCollider.transform.position = new Vector2(transform.position.x, transform.position.z);
        //hole2DCollider.transform.localScale = transform.localScale * initialScale;
        hole2DCollider.transform.localScale = new Vector2(transform.localScale.x * initialScale, transform.localScale.z * initialScale);
    }

    //
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
        //ground2DCollider.pathCount = 1;
        ground2DCollider.SetPath(0, PointPositions);
    }

    private void MakeHole2D()
    {
        Vector2[] PointPositions = hole2DCollider.GetPath(0);

        for(int i = 0; i < PointPositions.Length; i++)
        {
            PointPositions[i] = hole2DCollider.transform.TransformPoint(PointPositions[i]);
        }
        ground2DCollider.pathCount = 2;
        ground2DCollider.SetPath(1, PointPositions);
    }

    private void Make3DMeshCollider()
    {
        if (GeneratedMeshCollider != null) Destroy(GeneratedMesh);
        GeneratedMesh = ground2DCollider.CreateMesh(true, true);
        GeneratedMeshCollider.sharedMesh = GeneratedMesh;
    }
}
