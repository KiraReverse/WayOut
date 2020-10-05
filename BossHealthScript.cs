/******************************************************************************
//All content © 2018 Digipen Institute of Technology Singapore, all rights reserved.
filename: BossHealthScript
author: Samuel Chen Angjie
email: angjie.chen@digipen.edu
due date 13 April 2018
Brief Description: Controlls the health bar on the UI that changes when the boss takes damage
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossHealthScript : MonoBehaviour
{
    // Set CurrentScene
    Scene currentScene;

    //Boss Health HPvalue;
    private float bossCurrentHP;
    private float bossMaxHP;
    private float HPratio;

    [SerializeField]
    private float fillAmount;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private Image content;

    //public float MaxValue { get; set; }

    // Called in the beginning
    // Finds bossCurrent and bossMax hp
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Team_wandururs" )
        {
            bossCurrentHP = GameObject.FindGameObjectWithTag("Boss").GetComponentInChildren<WhomperHealth>().HP;
            bossMaxHP = GameObject.FindGameObjectWithTag("Boss").GetComponentInChildren<WhomperHealth>().whomperHP;
        }
        if (SceneManager.GetActiveScene().name == "Knight_Stage_Final")
        {
            bossCurrentHP = GameObject.FindGameObjectWithTag("Boss").GetComponentInChildren<Knight_Health>().HP;
            bossMaxHP = GameObject.FindGameObjectWithTag("Boss").GetComponentInChildren<Knight_Health>().knightHP;
        }
            UpdateBossHealth();
    }

    // Update is called once per frame
    // Updates bar each frame
    void Update()
    {
        HealthBar();
        UpdateBossHealth();
    }

    //Functions
    //Make the health bar decrease smoothly
    private void HealthBar()
    {
        if (fillAmount != content.fillAmount)
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
    }
        
    // Called in update
    private void UpdateBossHealth()
    {
        if (SceneManager.GetActiveScene().name == "Team_wandururs")
            bossCurrentHP = GameObject.FindGameObjectWithTag("Boss").GetComponentInChildren<WhomperHealth>().HP;
        if (SceneManager.GetActiveScene().name == "Knight_Stage_Final")
            bossCurrentHP = GameObject.FindGameObjectWithTag("Boss").GetComponentInChildren<Knight_Health>().HP;

            fillAmount = Map();
    }

    // Converts the values to 0-1
    private float Map()
    {
        HPratio = bossCurrentHP / bossMaxHP;
        return HPratio;
    }
}
