using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    Animator enemAnimator;
    EnemyLogic enemLogic;

    private void Awake()
    {
        enemAnimator = GetComponent<Animator>();
        Debug.Log(enemAnimator);
        enemLogic = GetComponentInParent<EnemyLogic>();
    }

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleWalkingTrue()
    {
        enemAnimator.SetBool("isWalking", true);
    }

    public void HandleWalkingFalse()
    {
        enemAnimator.SetBool("isWalking", false);
    }

    public void HandleAttackTrue()
    {
        enemAnimator.SetBool("isAttacking", true);
    }

    public void HandleAttackFalse()
    {
        enemAnimator.SetBool("isAttacking", false);
    }

    public void SwipeEvent()
    {
        enemLogic.AttackOneLife();
    }

    public void freezeEnemy()
    {
        enemAnimator.speed = 0; //Makes it so all animations stop playing;
    }
}
