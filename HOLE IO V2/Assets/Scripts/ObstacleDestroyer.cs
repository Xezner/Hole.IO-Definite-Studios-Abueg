using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDestroyer : MonoBehaviour
{
    
    public static ObstacleDestroyer Instance;
    [SerializeField] private GameObject ground;

    private void Start()
    {
        ground = GameObject.FindWithTag("Ground");
        ScaleCollider();
    }

    
    //destroys the obstacles once it hits the "Dead zone"
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        GameManager.Instance.UpdateGameState(GameManager.GameState.playerScore);
    }

    //rescales the "Dead Zone" to the size of our ground
    private void ScaleCollider()
    {
        transform.localScale = new Vector3(ground.transform.localScale.x, 1f, ground.transform.localScale.z);
    }
}
