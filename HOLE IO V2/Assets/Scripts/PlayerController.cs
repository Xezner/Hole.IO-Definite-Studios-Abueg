using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    //public CharacterController controller;
    public float movementSpeed = 2f;
    public float verticalInput;
    public float horizontalInput;
    float verticalMouseInput;
    float horizontalMouseInput;
    public GameObject arrowIndicator;
    GameObject Ground;
    float boundaryX = 0f;
    float boundaryZ = 0f;
    private bool isMoving;
    Vector3 newPosition;
    // Start is called before the first frame update
    void Start()
    {
        Ground = GameObject.FindGameObjectWithTag("Ground");
        arrowIndicator = GameObject.FindGameObjectWithTag("Player");
        boundaryX = Ground.transform.localScale.x / 2;
        boundaryZ = Ground.transform.localScale.z / 2;
        newPosition = transform.position;
        isMoving = false;
    }

    public void Move(BaseEventData myEvent)
    {
        if(((PointerEventData)myEvent).pointerCurrentRaycast.isValid)
        {
            transform.position = (((PointerEventData)myEvent).pointerCurrentRaycast.worldPosition);
        }
    }
    private void FixedUpdate()
    {
        PlayerMovement();
        MouseMovement();
    }

    public void MouseMovement()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            Vector3 rayHit = raycastHit.point;
            Vector3 direction = rayHit - transform.position;
            direction = new Vector3(direction.x, 0f, direction.z);
            

            if (Input.GetMouseButton(0))
            {
                isMoving = true;
                float angle = Vector3.Angle(new Vector3(0.01f, 0.0f, 0.0f), direction);
                if (direction.z < 0) angle = -angle;
                Vector2 MyVector = new Vector2((float)Mathf.Cos(angle * Mathf.PI / 180), (float)Mathf.Sin(angle * Mathf.PI / 180));
                direction = new Vector3(MyVector.x, 0f, MyVector.y);
                transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);
                if (direction != Vector3.zero)
                {
                    Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 100f);

                }
            }
            else
            {
                isMoving = false;
            }
        }
    }
    public void PlayerMovement()
    {
        //sets boundaries for the hole to move on the horizontal axis but allows to move on the opposite direction
        horizontalInput = Input.GetAxis("Horizontal");
        if (transform.position.x >= -boundaryX && horizontalInput < 0) ;
        else if (transform.position.x <= boundaryX && horizontalInput > 0) ;
        else horizontalInput = 0f;

        //sets boundaries for the hole to move on the horizontal axis but allows to move on the opposite direction
        verticalInput = Input.GetAxis("Vertical");
        if (transform.position.z >= -boundaryZ && verticalInput < 0) ;
        else if (transform.position.z <= boundaryZ && verticalInput > 0) ;
        else verticalInput = 0f;

        //saves our horizontal and vertical input in a vector3 called direction        
        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput);

        //translates the direction to move the object based on the movementSpeed in deltatime on the world
        if (horizontalInput != 0 || verticalInput != 0)
        {
            if(!isMoving)transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);
        }
        /*if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                newPosition = hit.point;
                newPosition = new Vector3(newPosition.x, 0f, newPosition.z);
                transform.Translate(newPosition * movementSpeed * Time.deltaTime, Space.World);
            }
            
        }*/

        //rotates the arrow indicator towards where the player is moving
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 100f);
        }
    }


}
