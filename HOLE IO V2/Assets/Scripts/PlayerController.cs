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
    public GameObject arrowIndicator;
    Vector3 previousPosition;
    Vector3 lastMoveDirection;
    // Start is called before the first frame update
    void Start()
    {
        arrowIndicator = GameObject.FindGameObjectWithTag("Player");
        previousPosition = transform.position;
        lastMoveDirection = Vector3.zero;
        
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
        transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 100f);
        }
    }


}
