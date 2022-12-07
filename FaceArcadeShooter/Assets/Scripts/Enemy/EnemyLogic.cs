using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/**
 * This script will be used on individual enemies
 * We will control their actions using the FBOY States
 * In the most basic cases, we want our zombies to spawn, walk towards the player, and attack
 * When the player reaches them, they will set to the death state, and despawn aftwerwards
 * */
public class EnemyLogic : MonoBehaviour
{

    [SerializeField] FBOY_STATES currentState;

    //Navmesh Logic
    [SerializeField] private NavMeshAgent nma;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject player;

    [SerializeField] private EnemyAnimation enemAnimController;
    
    //Variables for Idle
    bool idleEnd = false;

    //Variables for Walk


    //Variables for attacking
    public bool isAttacking = false;

    //Called when object is spawned (active
    private void Awake()
    {
        //When a enemy spawns in, they are set to idle until ready to walk
        currentState = FBOY_STATES.IDLE;
        nma = gameObject.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemAnimController = GetComponentInChildren<EnemyAnimation>();
    }
    

    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case FBOY_STATES.IDLE:
                HandleIdle();
                break;
            case FBOY_STATES.WALK:
                HandleWalk();
                break;
            case FBOY_STATES.ATTACK:
                if (!isAttacking)
                {
                    HandleAttack();
                }
                //StartCoroutine(TempAttackTimer());
                break;
            case FBOY_STATES.DEAD:
                HandleDead();
                break;
        }
    }

    //I created this state as a waiting period before the zombie moves towards the player
    private void HandleIdle()
    {
        currentState = FBOY_STATES.WALK;

    }

    //The zombie should stay in this state until they reach the player (or die trying muhahahaha)
    private void HandleWalk()
    {
        //transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        nma.SetDestination(target.position);
        enemAnimController.HandleWalkingTrue();
        //float distance = Vector3.Distance(player.transform.position, transform.position);
        //Debug.Log(distance);
        if(nma.remainingDistance < nma.stoppingDistance && !nma.pathPending)
        {
            enemAnimController.HandleWalkingFalse();
            currentState = FBOY_STATES.ATTACK;
        }
    }

    //This deals damage to the player at a specific interval (when the animation swipes). Only stops if player dies
    //Or the enemy dies
    private void HandleAttack()
    {
        enemAnimController.HandleAttackTrue();
        bool checkDead = player.GetComponent<CursorLogic>().isDead;
        if (!isAttacking && !checkDead) //If player is not dead and zombie isn't already attacking
        {
            isAttacking = true;
            //StartCoroutine(TempAttackTimer());
        }
    }

    //This is a temp coroutine to test out the lives
    IEnumerator TempAttackTimer()
    {
        yield return new WaitForSeconds(1f); //We can have this wait for a specific moment in the animation

        //AttackOneLife();
        //If the players health is 0, we should stop attacking
        isAttacking = false;
    }

    //This method takes away one health from the player
    //Returns true if player is still alive, and false is player is dead
    public bool AttackOneLife()
    {
        player.GetComponent<CursorLogic>().curLives--; //Subtracts one life
        if(player.GetComponent<CursorLogic>().curLives > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetStateDead()
    {
        currentState = FBOY_STATES.DEAD;
    }

    //
    private void HandleDead()
    {
        Debug.Log("ENEMY DIED!");
        nma.isStopped = true;
        enemAnimController.freezeEnemy();
    }

    private enum FBOY_STATES
    {
        IDLE,
        WALK,
        ATTACK,
        DEAD
    }
}

