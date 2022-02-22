using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnChangePosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PolygonCollider2D hole2dCollider;
    public PolygonCollider2D ground2DCollider;
    public MeshCollider GeneratedMeshCollider;
    public float initialScale = 0.5f;
    Mesh GenMesh;
    private void FixedUpdate()
    {

        if(transform.hasChanged == true)
        {
            transform.hasChanged = false;
            hole2dCollider.transform.position = new Vector2(transform.position.x, transform.position.z);
            hole2dCollider.transform.localScale = transform.localScale * initialScale;
            MakeHole2D();
            MakeMeshCollider();
        }
    }

    private void MakeHole2D()
    {
        Vector2[] PointPositions = hole2dCollider.GetPath(0);

        for ( int i = 0; i < PointPositions.Length; i++)
        {
            PointPositions[i] = hole2dCollider.transform.TransformPoint(PointPositions[i]);
        }

        ground2DCollider.pathCount = 2;
        ground2DCollider.SetPath(1, PointPositions);
    }


    private void MakeMeshCollider()
    {
        if (GenMesh != null) Destroy(GenMesh);
        GenMesh = ground2DCollider.CreateMesh(true, true);
        GeneratedMeshCollider.sharedMesh = GenMesh;
    }
}
