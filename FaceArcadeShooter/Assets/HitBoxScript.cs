using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxScript : MonoBehaviour
{
    EnemyLogic rootLogic;

    private void Awake()
    {
        rootLogic = transform.parent.parent.parent.GetComponent<EnemyLogic>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDeathFromHitbox()
    {
        rootLogic.SetStateDead();
    }
}
