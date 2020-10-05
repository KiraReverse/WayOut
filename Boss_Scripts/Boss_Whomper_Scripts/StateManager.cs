/******************************************************************************
filename StateManager.cs
author Clarence Phun
email k.phun@digipen.edu
due date 13 April 2018
Brief Description:
State machine for boss behaviors

All content © 2017 Digipen Institue of Technology, all rights reserved
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    private Behavior currState;
    public Behavior CurrState { get { return currState; } set { currState = value; } }

    public WhomperState bossState;

    public enum Behavior
    {
        wait,
        idle,
        chase,
        attack,
        skillOne,
        skillTwo,
        skillThree,
        skillFour
    }

    // Use this for initialization
    void Start()
    {
        currState = Behavior.idle;
    }

    // Update is called once per frame
    void Update()
    {

        switch (currState)
        {
            case Behavior.wait:
                bossState.Wait();
                break;

            case Behavior.idle:
                bossState.Idle();
                break;

            case Behavior.chase:
                bossState.Chase();
                break;

            case Behavior.attack:
                bossState.Attack();
                break;

            case Behavior.skillOne:
                bossState.SkillOne();
                break;

            case Behavior.skillTwo:
                bossState.SkillTwo();
                break;

            case Behavior.skillThree:
                bossState.SkillThree();
                break;

            case Behavior.skillFour:
                bossState.SkillFour();
                break;

        }
    }
}
