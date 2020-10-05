/******************************************************************************
filename ChargeController.cs
author Clarence Phun
email k.phun@digipen.edu
due date 13 April 2018
Brief Description:
controller for whomper's skilltwo

All content © 2017 Digipen Institue of Technology, all rights reserved
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeController : MonoBehaviour
{
    AudioSource sound;
    private float chargeSpeed;
    private float activeTime;
    public float dmg = 15f;

    Rigidbody2D mainRgbd;
    WhomperState whomperState;
    Animator enemyAnim;
    public GameObject body;

    void OnEnable()
    {

        mainRgbd = GetComponentInParent<Rigidbody2D>();
        whomperState = GetComponentInParent<WhomperState>();
        enemyAnim = body.GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
        chargeSpeed = whomperState.chargeSpeed;
        activeTime = whomperState.chargeTime;
        StartCoroutine(Disable());

        enemyAnim.SetBool("skillTwoPreActive", false);
        Physics2D.IgnoreLayerCollision(15, 16, false);
        sound.Play();

    }

    // Update is called once per frame
    void Update()
    {

        if (whomperState.FacingRight) mainRgbd.AddRelativeForce(transform.right * chargeSpeed, ForceMode2D.Impulse);
        else mainRgbd.AddRelativeForce(transform.right * chargeSpeed * -1, ForceMode2D.Impulse);

    }

    private void OnDisable()
    {
        enemyAnim.SetBool("skillTwoActive", false);
        enemyAnim.SetBool("isIdle", true);
        Physics2D.IgnoreLayerCollision(15, 16, true);
        mainRgbd.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.gameObject.GetComponent<PlayerHealth>().IsAlive)
            {
                other.SendMessage("Damaged", dmg);
            }
        }
    }

    //timer to disable self
    IEnumerator Disable()
    {
        yield return new WaitForSeconds(activeTime);
        this.gameObject.SetActive(false);
    }
}
