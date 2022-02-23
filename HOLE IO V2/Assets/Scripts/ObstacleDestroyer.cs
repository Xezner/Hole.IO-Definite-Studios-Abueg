using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDestroyer : MonoBehaviour
{
    [SerializeField] private GameObject ground;
    private void Start()
    {
        ground = GameObject.FindWithTag("Ground");
        ScaleCollider();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }

    private void ScaleCollider()
    {
        transform.localScale = new Vector3(ground.transform.localScale.x, 1f, ground.transform.localScale.z);
    }
}
