/******************************************************************************
filename SkillFourFistController.cs
author Clarence Phun
email k.phun@digipen.edu
due date 13 April 2018
Brief Description:
Controller for whomper's fourth skill object one

All content © 2017 Digipen Institue of Technology, all rights reserved
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFourFistController : MonoBehaviour
{
    private GameObject mainBody;
    private WhomperState whomperState;

    public GameObject fist2;

    private bool grabbed;

    private float speed;

    // Use this for initialization
    void Start()
    {
        mainBody = GameObject.FindGameObjectWithTag("Boss");
        whomperState = mainBody.GetComponent<WhomperState>();


        grabbed = false;
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            transform.Translate(-0.5f, 0, 0);
        }
    }

    //oncollision events
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !grabbed)
        {
            if (other.gameObject.GetComponent<PlayerHealth>().IsAlive == true)
            {
                other.SendMessage("Bind", true);
                grabbed = true;
                GameObject fist = Instantiate(fist2, other.transform.position, fist2.transform.rotation) as GameObject;
                fist.SendMessage("TheStart", other.gameObject);
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        if (!grabbed)
        {
            whomperState.StartSkillFourRecovery();
        }
    }
}
