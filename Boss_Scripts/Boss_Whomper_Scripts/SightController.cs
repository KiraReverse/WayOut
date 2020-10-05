/******************************************************************************
filename SightController.cs
author Clarence Phun
email k.phun@digipen.edu
due date 13 April 2018
Brief Description:
Controller for Trigger based Line of sight 

All content © 2017 Digipen Institue of Technology, all rights reserved
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightController : MonoBehaviour
{

    public WhomperState bossState;
    public StateManager bossManager;
    // Use this for initialization
    void Start()
    {
        //whomperState = GetComponent<WhomperState>();
        //whomperManager = GetComponent<StateManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //if (bossState.Target == null && (bossState.lockOnTimer -= Time.deltaTime) < 0f)
            {
                bossState.Target = collision.gameObject;

                if (bossState.FacingRight && bossState.Target.transform.position.x < transform.position.x)
                {
                    bossState.FlipFacing();
                }

                else if (!bossState.FacingRight && bossState.Target.transform.position.x > transform.position.x)
                {
                    bossState.FlipFacing();
                }

                bossManager.CurrState = StateManager.Behavior.chase;
            }
        }
    }
}
