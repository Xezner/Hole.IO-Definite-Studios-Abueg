using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [GameProperty(10f)]


    public static PlayerController Instance;
    public Hole hole; 
    public float movementSpeed = 2f;
    public float verticalInput;
    public float horizontalInput;
    [SerializeField] GameObject Ground;
    float boundaryX = 0f;
    float boundaryZ = 0f;
    private bool isMoving;

    [SerializeField] GameObject pause = null;
    private bool isMenuOpen;

    public TextMeshPro playerName;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Ground = GameObject.FindGameObjectWithTag("Ground");
        boundaryX = Ground.transform.localScale.x / 2;
        boundaryZ = Ground.transform.localScale.z / 2;
        SpawnPoint();
        isMoving = false;
        playerName.text = hole.playerName;
        pause.gameObject.SetActive(false);
        isMenuOpen = false;

    }
    private void PauseScreen()
    {
        //opens the pause menu if it's not opened
        if (!isMenuOpen)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                pause.gameObject.SetActive(true);
                isMenuOpen = true;
            }
        }
        //closes the pause menu if it's opened
        else
        {
            //allows to continue the game by playing any key
            if (Input.anyKeyDown && !(Input.GetKeyDown(KeyCode.Escape)))
            {
                Debug.Log("HERE");
                Time.timeScale = 1;
                pause.gameObject.SetActive(false);
                isMenuOpen = false;
            }
            //goes back to the title screen if escaped is pressed another time
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("Title Scene", LoadSceneMode.Single);
            }
        }
    }
    private void Update()
    {
        PauseScreen();
    }

    private void FixedUpdate()
    {
        movementSpeed = hole.moveSpeed;
        MouseMovement();
        PlayerMovement();
        /*if (Input.GetMouseButton(0) && ((Input.GetAxis("Horizontal") == 0) && (Input.GetAxis("Vertical") == 0)))
        {
            MouseMovement();
        }
        else if ( ( Input.GetAxis("Horizontal") != 0 ) || ( Input.GetAxis("Vertical") != 0 ) && !(Input.GetMouseButton(0)) )
        {
            PlayerMovement();
        }
        else
        {
            isMoving = false;
        }*/
        
    }
    public void SpawnPoint()
    {
        //randomizes the spawn point of the player on start
        transform.position = new Vector3(Random.Range(-boundaryX, boundaryX), 0, Random.Range(-boundaryZ, boundaryZ));
    }
    public void MouseMovement()
    {
        //created a raycast inorder to get the position of the mouse relative to the worldpoint
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            Vector3 rayHit = raycastHit.point;
            // subtracting the ray hit from the hole's position would tell us the direction of where our mouse is
            Vector3 direction = rayHit - transform.position;
            //setting the y value of direction to 0 since we are only moving from x to z
            direction = new Vector3(direction.x, 0f, direction.z);
            

            // if mouse Button is pressed
            if (Input.GetMouseButton(0))
            {
                //change the state to isMoving
                isMoving = true;
                //converts the direction or position of the mouse to an Angle with reference to the X axis as it's 0 point
                float angle = Vector3.Angle(new Vector3(0.01f, 0.0f, 0.0f), direction);
                //anything on the 3rd and fourth quadrant should return a negative angle and because...
                //and because Vector3.Angle only shows up to 180 degrees, we can fix that by using the..
                //.. the condition below
                if (direction.z < 0) angle = -angle;

                //converts the converted angle back to a Vector, in order to transform it to something similar to..
                //.. to a horizontal and vertical Input, which allows us to cap the movementSpeed to a specific number..
                //.. number, otherwise we wouldn't be able to control the Hole's speed if the mouse cursor is very far away
                Vector2 MyVector = new Vector2((float)Mathf.Cos(angle * Mathf.PI / 180), (float)Mathf.Sin(angle * Mathf.PI / 180));

                //sets the direction of the mouse to the x and y value of our vector, once again leaving y value of the direction to 0
                direction = new Vector3(MyVector.x, 0f, MyVector.y);
                //allows us to move the hole
                transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);
                if (direction != Vector3.zero)
                {
                    //rotates the body of the Player for the arrow indicator to state where we are moving
                    Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 100f);

                }
            }
            else
            {
                //if the mouse button is not pressed, state back to not moving
                isMoving = false;
            }
        }
    }
    public void PlayerMovement()
    {
        //sets boundaries for the hole to move on the horizontal axis but allows to move on the opposite direction
        horizontalInput = Input.GetAxis("Horizontal");

        //makes sure that you can no longer move if you are out of bounds
        if (transform.position.x >= -boundaryX && horizontalInput < 0) ;
        else if (transform.position.x <= boundaryX && horizontalInput > 0) ;
        else horizontalInput = 0f;

        //sets boundaries for the hole to move on the horizontal axis but allows to move on the opposite direction
        verticalInput = Input.GetAxis("Vertical");

        //makes sure that you can no longer move if you are out of bounds
        if (transform.position.z >= -boundaryZ && verticalInput < 0) ;
        else if (transform.position.z <= boundaryZ && verticalInput > 0) ;
        else verticalInput = 0f;

        //saves our horizontal and vertical input in a vector3 called direction        
        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput);

        //translates the direction to move the object based on the movementSpeed in deltatime on the world
        if (horizontalInput != 0 || verticalInput != 0)
        {
            //moves our hole
            if(!isMoving)transform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);
        }

        //rotates the arrow indicator towards where the player is moving
        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            if (!isMoving) transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 100f);
        }
    }


}
