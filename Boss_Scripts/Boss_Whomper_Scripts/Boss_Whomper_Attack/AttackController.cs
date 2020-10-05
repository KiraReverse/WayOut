/******************************************************************************
filename AttackController.cs
author Clarence Phun
email k.phun@digipen.edu
due date 13 April 2018
Brief Description:
Controller for whomper's basic attack

All content © 2017 Digipen Institue of Technology, all rights reserved
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{

    private GameObject mainBody;
    private WhomperState whomperState;

    private Vector3 targetPos;

    private float attackDamage;
    public float moveSpeed = 15f;

    private bool attacked;
    private bool isRight;

    Rigidbody2D rgbd;

    // Use this for initialization
    void Start()
    {
        mainBody = GameObject.FindGameObjectWithTag("Boss");
        whomperState = mainBody.GetComponent<WhomperState>();

        targetPos = whomperState.Target.transform.position;

        attackDamage = 10f;

        Destroy(gameObject, 6f);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position == targetPos)
        {
            Destroy(gameObject);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

    }

    //oncollision events
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
            if (!attacked && collision.gameObject.GetComponent<PlayerHealth>().IsAlive)
            {
                collision.gameObject.SendMessage("Damaged", attackDamage);
            }

            attacked = false;
        }


    }

    //failsafe to destroy object when it leaves screen
    private void OnBecameInvisible()
    {
        Destroy(gameObject, 5f);
    }
}
