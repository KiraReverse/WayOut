/******************************************************************************
filename WhomperHealth.cs
author Clarence Phun
email k.phun@digipen.edu
due date 13 April 2018
Brief Description:
Derieved class for whomper health

All content © 2017 Digipen Institue of Technology, all rights reserved
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhomperHealth : HealthController
{
    public GameObject skillTwoController;
    public float whomperHP;
    private float hpTrigger;
    public float whomperDMG;
    Animator anim;
    ColliderController cController;
    PolygonCollider2D[] colliders;
    SpriteRenderer sprite;
    WhomperSound sound;
    private bool deathclip = false;
    private WhomperState whomperState;

    void Start()
    {
        IsAlive = true;
        HP = whomperHP;
        Damage = whomperDMG;

        whomperState = GetComponentInParent<WhomperState>();
        anim = GetComponent<Animator>();
        cController = GetComponent<ColliderController>();
        colliders = GetComponents<PolygonCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        sound = GetComponentInParent<WhomperSound>();
        hpTrigger = whomperHP - (whomperHP / 10 * 3);
    }

    //disables components on death, changes behaviors on hp treshold
    private void Update()
    {


        if (HP <= 0)
        {
            IsAlive = false;
        }

        if (HP <= hpTrigger)
        {


            hpTrigger -= (whomperHP / 10 * 3);
            whomperState.ChangeMode();
        }

        if (!IsAlive)
        {
            PlayerPrefs.SetString("WhomperAlive", "false");

            if (skillTwoController.activeSelf)
            {
                skillTwoController.SetActive(false);
            }
            GetComponentInParent<WhomperState>().StopAllCoroutines();
            
            GetComponentInParent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponentInParent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;


            anim.SetBool("isAlive", IsAlive);

            if (!deathclip)
            {
                sound.PlayClip(6);
                deathclip = true;
            }
            cController.enabled = false;

            GetComponentInParent<Rigidbody2D>().gravityScale = 0;
            GetComponentInParent<WhomperState>().enabled = false;
            foreach (PolygonCollider2D p in colliders)
            {
                p.enabled = false;
            }

            GameObject[] leftovers = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject o in leftovers)
            {
                if (o.layer != LayerMask.NameToLayer("Boss"))
                {
                    Destroy(o);
                }
            }

        }
    }

    new void Damaged(float dmg)
    {
        if (!invulnerable) HP -= dmg;
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        //sprite.color = Color.blue;
        sprite.color = new Color32(135, 0, 0, 255);
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }

}
