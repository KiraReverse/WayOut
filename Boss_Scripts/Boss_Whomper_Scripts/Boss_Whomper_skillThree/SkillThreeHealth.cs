/******************************************************************************
filename SkillThreeHealth.cs
author Clarence Phun
email k.phun@digipen.edu
due date 13 April 2018
Brief Description:
controller for whomper's skillthree health

All content © 2017 Digipen Institue of Technology, all rights reserved
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillThreeHealth : HealthController
{
    public float rockHP;

    
    SpriteRenderer sprite;
    public Sprite halfHP;

    // Use this for initialization
    void Start()
    {
        HP = rockHP;
        
        sprite = GetComponent<SpriteRenderer>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= rockHP / 2)
        {
            sprite.sprite = halfHP;
        }


        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    
}