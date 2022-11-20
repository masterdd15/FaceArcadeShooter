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
    [SerializeField] private RectTransform canvasRectTransform;
    
    //Vector2 represents the UI Cursor's position on the screen
    [SerializeField] Vector2 CursorPosition;

    //The sprite we are using for our gameobject
    public GameObject crosshairs;

    [SerializeField] RectTransform cursorTransform;

    private Vector3 target;


    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveObjectMouse();
    }

    public void MoveObjectMouse()
    {
        cursorTransform.anchoredPosition = new Vector2(Input.mousePosition.x - 25, Input.mousePosition.y - 25) / canvasRectTransform.localScale.x;
    }
}
