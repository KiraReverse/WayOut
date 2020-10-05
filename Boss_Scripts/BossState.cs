/******************************************************************************
filename BossState.cs
author Clarence Phun
email k.phun@digipen.edu
due date 13 April 2018
Brief Description:
Abstract Class for all boss states 

All content © 2017 Digipen Institue of Technology, all rights reserved
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState : MonoBehaviour
{
    /// <notes>
    /// all ranges are in unity units 1 = 1f in x-axis 
    /// all cooldowns are realtime in seconds 1 = 1s
    /// </notes> 

    [Header("Global Variable")]
    protected Animator enemyAnim;
    protected Rigidbody2D enemyRB;

    public GameObject Target { get { return target; } set { target = value; } }
    protected GameObject target = null;
    public GameObject enemyGraphic;
    protected float distanceAway;

    public float actionCD = 3f;
    protected bool canAct;

    [Header("Boss Stats", order = 1)]
    public float moveSpeed = 1;
    public float attackRange = 6;
    public float skillOneRange = 3;
    public float skillTwoRange = 10;

    [Header("Targetting Variables", order = 2)]
    public float lockOnTimer = 3f;
    public float lockOffTimer = 3f;

    [Header("Idle Variables", order = 3)]
    public float flipTime = 5f;
    public float nextFlipChance = 0f;

    protected bool canFlip = true;
    public bool CanFlip { get { return canFlip; } set { canFlip = value; } }

    protected bool facingRight = false;
    public bool FacingRight { get { return facingRight; } set { facingRight = value; } }

    [Header("Chasing Variables", order = 4)]
    protected bool chasing;
    public bool Chasing { get { return chasing; } set { chasing = value; } }

    [Header("Attack Variables", order = 5)]
    public float cdAttack = 50;
    protected bool attackActive;

    [Header("Skill One Variables", order = 6)]
    public float cdSkillOne = 50;
    protected bool skillOneActive;

    [Header("Skill Two Variables", order = 7)]
    public float startChargeTime;
    public float cdSkillTwo = 50;
    public float chargeTime;
    protected bool skillTwoActive;
    protected bool charging;



    [Header("Skill Three Variables", order = 8)]
    public float cdSkillThree = 50;
    protected bool skillThreeActive;






    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract void Idle();
    public abstract void Chase();
    public abstract void Attack();
    public abstract void SkillOne();
    public abstract void SkillTwo();
    public abstract void SkillThree();
    public void Wait()
    {

    }

    //function to calculate distance between boss and target
    protected void DistanceAway()
    {
        distanceAway = Vector3.Distance(this.transform.position, target.transform.position);
    }

    //function to flip boss object
    public void FlipFacing()
    {
        if (!canFlip) return;



        facingRight = !facingRight;

        //transform.Rotate(new Vector3(0, 180, 0));
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
