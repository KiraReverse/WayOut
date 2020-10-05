/******************************************************************************
filename SkillThreeObject.cs
author Clarence Phun
email k.phun@digipen.edu
due date 13 April 2018
Brief Description:
controller for whomper's skillthree object

All content © 2017 Digipen Institue of Technology, all rights reserved
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillThreeObject : MonoBehaviour
{
    public float damage;
    public float lifetime = 2f;
    private SkillThreeHealth healthCon;
    public GameObject death;
    private AudioSource sound;

    WhomperState whomperState;

    [SerializeField]
    Collider2D[] colliders;
    // Trigger = [0]
    // Collider = [1]

    void Start()
    {
        whomperState = GameObject.FindGameObjectWithTag("Boss").GetComponent<WhomperState>();
        healthCon = GetComponent<SkillThreeHealth>();
        sound = GetComponent<AudioSource>();

        colliders[0].enabled = true;
        colliders[1].enabled = false;
        healthCon.enabled = false;

        Destroy(gameObject, 8f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerHealth>().IsAlive)
            {
                collision.SendMessage("Damaged", damage);
                gameObject.SendMessage("Damaged", damage);
            }
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Boundary Box"))
        {
            colliders[0].enabled = false;
            colliders[1].enabled = true;
            healthCon.enabled = true;
            sound.Play();
        }
    }

    //to destroy self if out of camera bounds 
    private void OnBecameInvisible()
    {
        Destroy(gameObject, 1f);
    }

    //function to create object on destruction
    void OnDestroy()
    {
        if (whomperState != null)   Instantiate(death, gameObject.transform.position, gameObject.transform.rotation);
    }

}
