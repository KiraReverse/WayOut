/******************************************************************************
filename SkillThreeController.cs
author Clarence Phun
email k.phun@digipen.edu
due date 13 April 2018
Brief Description:
controller for whomper's skillthree to manage waypoints and selection of spawn points

All content © 2017 Digipen Institue of Technology, all rights reserved
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillThreeController : MonoBehaviour
{
    public Transform waypoint1;
    public Transform waypoint2;
    public Transform waypoint3;
    public Transform waypoint4;
    public Transform waypoint5;
    public Transform waypoint6;
    public Transform waypoint7;
    public Transform waypoint8;
    public Transform waypoint9;
    public Transform wpShadow;

    public GameObject shadow;


    public GameObject skillThreeObject;

    public int numOfRocks;

    List<int> wpList;
    List<int> spawnList;

    List<GameObject> shadowObj;


    // Use this for initialization
    void Start()
    {
        wpList = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 });
        shadowObj = new List<GameObject>();
        spawnList = new List<int>();

        for (int i = 0; i < numOfRocks; ++i)
        {
            GenerateWaypoint();
        }

        StartCoroutine(RockDrop());
    }

    //function to generate unique random waypoints 
    void GenerateWaypoint()
    {
        int ranNum = wpList[Random.Range(0, wpList.Count)];
        wpList.Remove(ranNum);
        spawnList.Add(ranNum);
    }

    IEnumerator RockDrop()
    {

        foreach (int point in spawnList)
        {
            switch (point)
            {
                case 1:
                    GameObject shadowOne = Instantiate(shadow, new Vector3(waypoint1.position.x, wpShadow.position.y, shadow.transform.position.z), shadow.transform.rotation) as GameObject;
                    shadowObj.Add(shadowOne);
                    break;
                case 2:
                    GameObject shadowTwo = Instantiate(shadow, new Vector3(waypoint2.position.x, wpShadow.position.y, shadow.transform.position.z), shadow.transform.rotation) as GameObject;
                    shadowObj.Add(shadowTwo);
                    break;
                case 3:
                    GameObject shadowThree = Instantiate(shadow, new Vector3(waypoint3.position.x, wpShadow.position.y, shadow.transform.position.z), shadow.transform.rotation) as GameObject;
                    shadowObj.Add(shadowThree);
                    break;
                case 4:
                    GameObject shadowFour = Instantiate(shadow, new Vector3(waypoint4.position.x, wpShadow.position.y, shadow.transform.position.z), shadow.transform.rotation) as GameObject;
                    shadowObj.Add(shadowFour);
                    break;
                case 5:
                    GameObject shadowFive = Instantiate(shadow, new Vector3(waypoint5.position.x, wpShadow.position.y, shadow.transform.position.z), shadow.transform.rotation) as GameObject;
                    shadowObj.Add(shadowFive);
                    break;
                case 6:
                    GameObject shadowSix = Instantiate(shadow, new Vector3(waypoint6.position.x, wpShadow.position.y, shadow.transform.position.z), shadow.transform.rotation) as GameObject;
                    shadowObj.Add(shadowSix);
                    break;
                case 7:
                    GameObject shadowSeven = Instantiate(shadow, new Vector3(waypoint7.position.x, wpShadow.position.y, shadow.transform.position.z), shadow.transform.rotation) as GameObject;
                    shadowObj.Add(shadowSeven);
                    break;
                case 8:
                    GameObject shadowEight = Instantiate(shadow, new Vector3(waypoint8.position.x, wpShadow.position.y, shadow.transform.position.z), shadow.transform.rotation) as GameObject;
                    shadowObj.Add(shadowEight);
                    break;
                case 9:
                    GameObject shadowNine = Instantiate(shadow, new Vector3(waypoint9.position.x, wpShadow.position.y, shadow.transform.position.z), shadow.transform.rotation) as GameObject;
                    shadowObj.Add(shadowNine);
                    break;
            }
        }

        yield return new WaitForSeconds(2f);

        foreach (int point in spawnList)
        {
            switch (point)
            {
                case 1:
                    Instantiate(skillThreeObject, waypoint1.position, waypoint1.rotation);
                    break;
                case 2:
                    Instantiate(skillThreeObject, waypoint2.position, waypoint2.rotation);
                    break;
                case 3:
                    Instantiate(skillThreeObject, waypoint3.position, waypoint3.rotation);
                    break;
                case 4:
                    Instantiate(skillThreeObject, waypoint4.position, waypoint4.rotation);
                    break;
                case 5:
                    Instantiate(skillThreeObject, waypoint5.position, waypoint5.rotation);
                    break;
                case 6:
                    Instantiate(skillThreeObject, waypoint6.position, waypoint6.rotation);
                    break;
                case 7:
                    Instantiate(skillThreeObject, waypoint7.position, waypoint7.rotation);
                    break;
                case 8:
                    Instantiate(skillThreeObject, waypoint8.position, waypoint8.rotation);
                    break;
                case 9:
                    Instantiate(skillThreeObject, waypoint9.position, waypoint9.rotation);
                    break;
            }
        }

        foreach (GameObject s in shadowObj)
        {
            Destroy(s);
        }
        gameObject.SetActive(false);
    }


}
