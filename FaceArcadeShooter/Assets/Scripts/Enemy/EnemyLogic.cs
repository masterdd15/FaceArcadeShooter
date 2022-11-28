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
    [SerializeField] private Transform player;
    
    //Variables for Idle
    bool idleEnd = false;

    //Variables for Walk


    //Called when object is spawned (active
    private void Awake()
    {
        //When a enemy spawns in, they are set to idle until ready to walk
        currentState = FBOY_STATES.IDLE;
        nma = gameObject.GetComponent<NavMeshAgent>();
        //player = GameObject.FindGameObjectWithTag("Player").transform;
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
                HandleAttack();
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
        nma.SetDestination(player.position);
    }

    //This deals damage to the player at a specific interval (when the animation swipes). Only stops if player dies
    //Or the enemy dies
    private void HandleAttack()
    {

    }

    //
    private void HandleDead()
    {

    }

    private enum FBOY_STATES
    {
        IDLE,
        WALK,
        ATTACK,
        DEAD
    }
}

