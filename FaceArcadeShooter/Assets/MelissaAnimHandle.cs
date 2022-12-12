using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelissaAnimHandle : MonoBehaviour
{
    [SerializeField] Animator melissaAnim;
    [SerializeField] CursorLogic myPlayer;

    // Start is called before the first frame update
    void Start()
    {
        melissaAnim = GetComponent<Animator>();
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<CursorLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myPlayer.isShooting)
        {
            SetSmiling(true);
        }
        else
        {
            SetSmiling(false);
        }
    }

    private void SetSmiling(bool state)
    {
        melissaAnim.SetBool("isSmiling", state);
    }


}
