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

    public void HandleDeath()
    {
        enemAnimator.SetBool("isDead", true);
    }

    public void freezeEnemy()
    {
        SkinnedMeshRenderer[] fullMesh = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach(SkinnedMeshRenderer cur in fullMesh)
        {
            for(int i = 0; i < cur.materials.Length; i++)
            {
                cur.materials[i].color = Color.grey;
            }
        }

        StartCoroutine("enemyDeath");
    }

    IEnumerator enemyDeath()
    {
        Debug.Log("Starting Death!");
        enemAnimator.SetBool("isDead", true);
        while (enemAnimator.speed > .5f)
        {
            float remove = Random.Range(.05f, .075f);
            float newSpeed = enemAnimator.speed - remove;
            if(newSpeed > .5f)
            {
                enemAnimator.speed = newSpeed;
            }
            else //We went over 0 so set to 0
            {
                enemAnimator.speed = .5f;
            }
            yield return new WaitForSeconds(.05f);
        }
        Debug.Log("He's back!");
        enemAnimator.speed = .5f;
        yield return new WaitForSeconds(10f);
        Destroy(transform.parent.gameObject);
    }
}
