using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeAnimControl : MonoBehaviour
{
    [SerializeField]float iDif;
    [SerializeField] float dDif;
    [SerializeField] float currentSpeed; //Helps us keep track of the anim speed
    [SerializeField] Animator snakeAnim;
    [SerializeField] CursorLogic myPlayer;

    private void Awake()
    {
        snakeAnim = GetComponent<Animator>();
        currentSpeed = snakeAnim.GetFloat("SnakeSpeed");
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<CursorLogic>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(myPlayer.isShooting)
        {
            SpeedSnake();
        }
        else
        {
            SlowSnake();
        }
    }

    void SpeedSnake()
    {
        snakeAnim.SetFloat("SnakeSpeed", 4f);
    }

    void SlowSnake()
    {
        snakeAnim.SetFloat("SnakeSpeed", 1f);
    }
}
