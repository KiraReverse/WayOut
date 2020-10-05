/******************************************************************************
filename SkillFourOneController.cs
author Clarence Phun
email k.phun@digipen.edu
due date 13 April 2018
Brief Description:
controller for whomper's skillfour's furst object

All content © 2017 Digipen Institue of Technology, all rights reserved
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFourOneController : MonoBehaviour
{
    public GameObject fist;
    private GameObject mainBody;
    private WhomperState whomperState;
    private Vector3 targetPos;
    private float speed;
	// Use this for initialization
	void Start ()
    {
        mainBody = GameObject.FindGameObjectWithTag("Boss");
        whomperState = mainBody.GetComponent<WhomperState>();
        targetPos = whomperState.Target.transform.position;
        speed = whomperState.skillFourObjectSpeed;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.position.x == targetPos.x)
        {
            Destroy(gameObject);
            Instantiate(fist, new Vector3(transform.position.x, transform.position.y -3f, transform.position.z), fist.transform.rotation);
        }

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetPos.x, transform.position.y), speed * Time.deltaTime);
	}
}
