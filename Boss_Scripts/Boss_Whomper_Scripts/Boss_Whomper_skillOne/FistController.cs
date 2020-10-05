/******************************************************************************
filename FistController.cs
author Clarence Phun
email k.phun@digipen.edu
due date 13 April 2018
Brief Description:
controller for whomper's skillone's collider

All content © 2017 Digipen Institue of Technology, all rights reserved
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistController : MonoBehaviour
{


    private Transform fistEnd;
    public float fistSpeed = 0.5f;
    private float fistDmg = 100f;


    Rigidbody2D rgbd;



    //function to recieve the position of where the object should end and to destroy self
    void TheStart(Transform v)
    {
        fistEnd = v;

        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, fistEnd.transform.position, fistSpeed);


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerHealth>().IsAlive)
            {
                other.SendMessage("Damaged", fistDmg);
            }
        }
    }


}
