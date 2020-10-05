/******************************************************************************
filename SkillThreeObjectShatterController.cs
author Clarence Phun
email k.phun@digipen.edu
due date 13 April 2018
Brief Description:
controller for whomper's skill's self shatter timers

All content © 2017 Digipen Institue of Technology, all rights reserved
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillThreeObjectShatterController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
