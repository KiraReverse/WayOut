/******************************************************************************
filename SkillFourHealth.cs
author Clarence Phun
email k.phun@digipen.edu
due date 13 April 2018
Brief Description:
health controller for whomper's four skill object

All content © 2017 Digipen Institue of Technology, all rights reserved
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFourHealth : HealthController
{
    public float fistHP;

    SpriteRenderer sprite;
    public Sprite halfHP;

    // Use this for initialization
    void Start()
    {
        HP = fistHP;


        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (HP <= fistHP / 2)
        {
            sprite.sprite = halfHP;
        }

        if (HP <= 0)
        {
            Destroy(gameObject);
        }

        if(Input.GetKey(KeyCode.J))
        {
            HP = 0;
        }
    }



}
