using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float movementSpeed = 2f;
    public float verticalInput;
    public float horizontalInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    public void PlayerMovement()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput);

        if (direction.magnitude >= 0.1f)
        {
            transform.position += (direction * movementSpeed * Time.deltaTime);
            //controller.Move(direction * movementSpeed * Time.deltaTime);
        }
    }
}
