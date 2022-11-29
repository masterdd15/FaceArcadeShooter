using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/**
 *This class will handle the logic for the Player's Cursor
 * We will use it to track the players input (mouse or face detection)
 * And translate the input into movin the cursor on the screen
 * If the player shoots (either mouse click or by smiling), the cursor will handle creating the laser
 **/
public class CursorLogic : MonoBehaviour
{
    //The player's Camera
    public Camera cam;
    //Canvas transform so we scale locally
    [SerializeField] private RectTransform canvasRectTransform;
  
    //Cursor UI Sprite Transform
    [SerializeField] RectTransform cursorTransform;

    //This will
    [SerializeField] Transform gunAim;
    [SerializeField] LineRenderer lineRend;

    //This is for the keyboard test
    [SerializeField] float keySpeed = 150f;
    Vector2 movement;

    //The number of lives a player starts with
    [SerializeField] int startLives = 5;
    [SerializeField] public int curLives;
    [SerializeField] TextMeshProUGUI livesTracker;

    //Public float turns on and off the shooting
    bool isShooting;


    private void Awake()
    {
        isShooting = false;
        curLives = startLives;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //SpawnLaserMouse();
        TestCursorToWorld();

        //use mouse
        MoveUIObjectMouse();

        //Use keyboard
        //MoveUIObjectKeypad();

        //Update player lives
        HandlePlayerLives();
    }

    public void MoveUIObjectMouse()
    {
        //Vector2 alteredMousePos = new Vector2(Input.mousePosition.x - (cursorTransform.rect.width / 2), Input.mousePosition.y - (cursorTransform.rect.height / 2));
        cursorTransform.anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;
        if (Input.GetMouseButton(0))
        {
            isShooting = true;
        }
        else
        {
            isShooting = false;
        }
    }

    public void MoveUIObjectKeypad()
    {
        movement.x = Input.GetAxisRaw("Horizontal") * keySpeed;
        movement.y = Input.GetAxisRaw("Vertical") * keySpeed;

        if(movement.x != 0 || movement.y != 0)
        {
            cursorTransform.anchoredPosition += new Vector2(movement.x * Time.deltaTime, movement.y * Time.deltaTime) / canvasRectTransform.localScale.x;
        }
        else
        {
            //CursorTransform.anchoredPosition += new Vector2(movement.x * Time.deltaTime, movement.y * Time.deltaTime)
        }

        if (Input.GetKey(KeyCode.Space))
        {
            isShooting = true;
        }
        else
        {
            isShooting = false;
        }

    }


    //This method allows for us to raycast based on the placement of our UI Cursor
    //The cursor is being moved right now with our Mouse
    //But once we implement face tracking, we can use the same raycast to play the game
    public void TestCursorToWorld()
    {
        Vector3 screenPosition = cursorTransform.position;
        screenPosition.z = 10;
        screenPosition = cam.ScreenToWorldPoint(screenPosition);
        //Ray ray = cam.ScreenPointToRay(screenPosition);
        Debug.DrawRay(transform.position, screenPosition - transform.position, Color.blue);


        if (isShooting)
        {
            Ray ray = cam.ScreenPointToRay(cam.WorldToScreenPoint(screenPosition));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    Debug.Log(hit.transform.name);
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }


    //This is the method we use to keep track of our lives.
    //We also use this method to update the UI
    public void HandlePlayerLives()
    {
        livesTracker.text = "Lives Left: " + curLives;
    }
}
