/******************************************************************************
filename WhomperState.cs
author Clarence Phun
email k.phun@digipen.edu
due date 13 April 2018
Brief Description:
Whomper states and variables

All content © 2017 Digipen Institue of Technology, all rights reserved
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stage
{
    melee,
    range,
    both
}
public class WhomperState : MonoBehaviour
{
    [Header("Global Variable")]
    public Transform spawnBoundary;
    public GameObject enemyGraphic;
    public bool crossed;
    private Animator enemyAnim;
    private CameraFollow camShake;
    private GameObject[] players;

    private GameObject target;
    public GameObject Target { get { return target; } set { target = value; } }

    public float actionCD = 3f;
    private bool canAct;
    private float distanceAwayOne;
    private float distanceAwayTwo;
    private WhomperSound sound;

    [Space(10)]

    [Header("Boss Stats")]
    public float moveSpeed = 1;
    public float skillOneRange = 3;
    public float skillTwoRange = 10;

    private Stage currStage;
    public Stage CurrStage { get { return currStage; } set { currStage = value; } }

    public float retargetTimer = 10f;
    private bool retarget;

    [Space(10)]

    [Header("State Manager")]
    public StateManager bossManager;

    [Space(10)]

    [Header("Idle Variables")]
    private bool canFlip;

    private bool facingRight = false;
    public bool FacingRight { get { return facingRight; } set { facingRight = value; } }

    [Space(10)]

    [Header("Chasing Variables", order = 4)]
    public float chaseTimer = 2f;
    private bool isChasing = false;

    [Space(10)]

    [Header("Attack Variable")]
    public GameObject attackObject;
    public Transform attackStart;
    public float cdAttack = 50;
    private bool attackActive;

    [Space(10)]

    [Header("Skill One Variable")]
    public GameObject skillOneObject;
    public Transform skillOneStart;
    public Transform SkillOneEnd;

    public float cdSkillOne = 50;
    private bool skillOneActive;


    [Space(10)]

    [Header("Skill Two Variable")]
    public GameObject skillTwoObject;
    public float chargeSpeed = 400f;
    public float startChargeTime;
    public float cdSkillTwo = 50;
    public float chargeTime;
    private bool skillTwoActive;
    private bool charging;

    private float skillTwoPre = 0.666f;

    [Space(10)]

    [Header("Skill Three Variable")]
    public GameObject skillThreeController;
    public float cdSkillThree = 50;
    private bool skillThreeActive;

    [Space(10)]

    [Header("Skill Four Variable")]
    public GameObject skillFourObject;
    public Transform skillFourStart;
    public float cdSkillFour = 50;
    public float skillFourDmg = 10;

    public float skillFourObjectSpeed;

    private bool skillFourActive;

    // Use this for initialization
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        camShake = Camera.main.GetComponent<CameraFollow>();
        enemyAnim = GetComponentInChildren<Animator>();
        sound = GetComponent<WhomperSound>();

        /* debug test :: remember to set to false for actual gameplay */
        canAct = true;
        currStage = Stage.melee;
        retarget = true;
        crossed = false;

        attackActive = false;
        skillOneActive = false;
        skillTwoActive = false;
        skillThreeActive = false;
        skillFourActive = false;

        StartCoroutine(AttackCD());
        StartCoroutine(SkillOneCD());
        StartCoroutine(SkillTwoCD());
        StartCoroutine(SkillThreeCD());
        StartCoroutine(SkillFourCD());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (GameObject p in players)
        {
            if (p.transform.position.x > spawnBoundary.position.x)
            {
                crossed = true;

                if (currStage == Stage.melee) SetClosestTarget(players);
                else if (currStage == Stage.range) SetFurthestTarget(players);
                else if (retarget) ChooseRandomTarget(players);
            }
        }

        if (target != null && canFlip && FacingRight && target.transform.position.x < transform.position.x)
        {
            FlipFacing();
        }

        else if (target != null && canFlip && !FacingRight && target.transform.position.x > transform.position.x)
        {
            FlipFacing();
        }
    }

    //idle behaviors and transitions
    public void Idle()
    {

        enemyAnim.SetBool("isIdle", true);

        canFlip = true;

        if (target != null)
        {
            if (currStage == Stage.melee && !skillOneActive && !skillTwoActive)
            {
                bossManager.CurrState = StateManager.Behavior.chase;
            }

            if (currStage == Stage.range && !attackActive && !skillThreeActive && !skillFourActive)
            {
                bossManager.CurrState = StateManager.Behavior.chase;
            }

            if (currStage == Stage.both && !attackActive && !skillOneActive && !skillTwoActive && !skillThreeActive && !skillFourActive)
            {
                bossManager.CurrState = StateManager.Behavior.chase;
            }

        }

        if (currStage == Stage.melee || currStage == Stage.both)
        {
            if (target != null && skillTwoActive == true && canAct == true)
            {
                bossManager.CurrState = StateManager.Behavior.skillTwo;
                canAct = false;
                StartCoroutine(ActionCD());
            }

            if (target != null && skillOneActive == true && canAct == true)
            {
                bossManager.CurrState = StateManager.Behavior.skillOne;
                canAct = false;
                StartCoroutine(ActionCD());
            }
        }

        if (currStage == Stage.range || currStage == Stage.both)
        {
            if (target != null && skillThreeActive == true && canAct == true)
            {
                bossManager.CurrState = StateManager.Behavior.skillThree;
                canAct = false;
                StartCoroutine(ActionCD());
            }

            if (target != null && skillFourActive == true && canAct == true)
            {
                bossManager.CurrState = StateManager.Behavior.skillFour;
                canAct = false;
                StartCoroutine(ActionCD());
            }


            if (target != null && attackActive == true && canAct == true)
            {
                bossManager.CurrState = StateManager.Behavior.attack;
                canAct = false;
                StartCoroutine(ActionCD());
            }


        }


    }

    //chase behaviors
    public void Chase()
    {
        if (!isChasing)
        {
            sound.PlayClip(3);
            StartCoroutine(StopChasing());
            isChasing = true;
        }
        if (GetComponentInChildren<WhomperHealth>().IsAlive)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        }

        
    }

    //timer to reset behavior to idle
    IEnumerator StopChasing()
    {
       
        yield return new WaitForSeconds(chaseTimer);
        isChasing = false;
        bossManager.CurrState = StateManager.Behavior.idle;
    }

    //attack behaviors
    public void Attack()
    {

        canFlip = false;

        enemyAnim.SetBool("attackActive", true);
        enemyAnim.SetBool("isIdle", false);
        
        attackActive = false;
        StartCoroutine(AttackCD());
        StartCoroutine(Throw());

        bossManager.CurrState = StateManager.Behavior.wait;


    }

    //spawning of actual attack prefab using timers to ensure it matches with animation
    IEnumerator Throw()
    {
        yield return new WaitForSeconds(1.5f);
        sound.PlayClip(1);  
        yield return new WaitForSeconds(0.299f);
        
        Instantiate(attackObject, attackStart.position, attackObject.transform.rotation);

        yield return new WaitForSeconds(0.4006f);

        enemyAnim.SetBool("attackActive", false);
        enemyAnim.SetBool("isIdle", true);

        bossManager.CurrState = StateManager.Behavior.idle;

    }

    //timer to reset cooldown for attack
    IEnumerator AttackCD()
    {
        yield return new WaitForSeconds(cdAttack);
        attackActive = true;

    }

    //skillone behaviors
    public void SkillOne()
    {
        canFlip = false;

        StopCoroutine(AttackCD());
        attackActive = false;
        skillOneActive = false;
        
        StartCoroutine(SkillOneCD());

        enemyAnim.SetBool("skillOneActive", true);
        enemyAnim.SetBool("isIdle", false);
        sound.PlayClip(0);
        StartCoroutine(SpawnSkillOne());
        StartCoroutine(AttackCD());

        bossManager.CurrState = StateManager.Behavior.wait;

    }

    //spawning of skillone prefab using timers to ensure it matches with animation
    IEnumerator SpawnSkillOne()
    {
        yield return new WaitForSeconds(1.1f);
        GameObject fist = Instantiate(skillOneObject, skillOneStart.position, skillOneObject.transform.rotation) as GameObject;
        fist.SendMessage("TheStart", SkillOneEnd);
        yield return new WaitForSeconds(0.06f);
        StartCoroutine(camShake.ScreenShake());
        yield return new WaitForSeconds(0.54f);

        enemyAnim.SetBool("skillOneActive", false);
        enemyAnim.SetBool("isIdle", true);

        StartCoroutine(AttackCD());
        bossManager.CurrState = StateManager.Behavior.idle;
    }

    //timer to reset cooldown for skillone
    IEnumerator SkillOneCD()
    {
        yield return new WaitForSeconds(cdSkillOne);
        skillOneActive = true;

    }

    //skilltwo behaviors
    public void SkillTwo()
    {
        canFlip = false;

        StopCoroutine(AttackCD());
        attackActive = false;

        skillTwoActive = false;
        StartCoroutine(SkillTwoCD());
        StartCoroutine(Charging());
        bossManager.CurrState = StateManager.Behavior.wait;
    }

    //time to reset cooldown for skilltwo
    IEnumerator SkillTwoCD()
    {
        yield return new WaitForSeconds(cdSkillTwo);
        skillTwoActive = true;

    }

    //function to turn on animation and skilltwo object as well as reset behavior to idle
    IEnumerator Charging()
    {
        enemyAnim.SetBool("skillTwoActive", true);
        enemyAnim.SetBool("isIdle", false);
        enemyAnim.SetBool("skillTwoPreActive", true);

        yield return new WaitForSeconds(skillTwoPre + 1.5f);
        skillTwoObject.SetActive(true);
        StartCoroutine(camShake.ScreenShake());
        yield return new WaitForSeconds(chargeTime);

        StartCoroutine(AttackCD());
        bossManager.CurrState = StateManager.Behavior.idle;
    }

    //skill three behaviors
    public void SkillThree()
    {
        canFlip = false;

        StopCoroutine(AttackCD());
        attackActive = false;

        skillThreeActive = false;
        StartCoroutine(SkillThreeCD());

        StartCoroutine(SpawnSkillThree());
        enemyAnim.SetBool("skillThreeActive", true);
        enemyAnim.SetBool("isIdle", false);
        sound.PlayClip(4);
        bossManager.CurrState = StateManager.Behavior.wait;

    }


    //timer to reset cooldown of skill three
    IEnumerator SkillThreeCD()
    {
        yield return new WaitForSeconds(cdSkillThree);
        skillThreeActive = true;
    }

    //function to spawn skillthree prefab matching animation
    IEnumerator SpawnSkillThree()
    {
        yield return new WaitForSeconds(0.7f);
        Instantiate(skillThreeController, transform.position, Quaternion.identity);
        StartCoroutine(camShake.ScreenShake());
        yield return new WaitForSeconds(0.6f);

        enemyAnim.SetBool("skillThreeActive", false);
        enemyAnim.SetBool("isIdle", true);

        StartCoroutine(AttackCD());
        bossManager.CurrState = StateManager.Behavior.idle;
    }

    //skill four behavior
    public void SkillFour()
    {
        canFlip = false;

        StopCoroutine(AttackCD());
        attackActive = false;

        enemyAnim.SetBool("skillFourActive", true);
        enemyAnim.SetBool("skillFourHold", false);
        enemyAnim.SetBool("skillFourRecoveryActive", false);
        enemyAnim.SetBool("isIdle", false);
        sound.PlayClip(5);
        skillFourActive = false;
        StartCoroutine(SkillFourCD());
        StartCoroutine(SpawnSkillFour());
        bossManager.CurrState = StateManager.Behavior.wait;
    }

    //function to spawn skillfour prefab to match animation
    IEnumerator SpawnSkillFour()
    {
        yield return new WaitForSeconds(1.1f);
        StartCoroutine(camShake.ScreenShake());
        enemyAnim.SetBool("skillFourHold", true);
        Instantiate(skillFourObject, skillFourStart.position, skillFourObject.transform.rotation);
        
    }

    //function to start cooldown for skill four
    public void StartSkillFourRecovery()
    {
        StartCoroutine(SkillFourRecovery());
    }

    //timer to reset skillfour state to idle
    IEnumerator SkillFourRecovery()
    {
        sound.PlayClip(2);
        enemyAnim.SetBool("skillFourRecoveryActive", true);
        enemyAnim.SetBool("skillFourHold", false);
        yield return new WaitForSeconds(0.5f);
        enemyAnim.SetBool("skillFourHold", false);
        enemyAnim.SetBool("skillFourActive", false);
        enemyAnim.SetBool("skillFourRecoveryActive", false);
        enemyAnim.SetBool("isIdle", true);
        bossManager.CurrState = StateManager.Behavior.idle;

    }

    //timer to reset cooldown for skillfour
    IEnumerator SkillFourCD()
    {
        yield return new WaitForSeconds(cdSkillFour);
        skillFourActive = true;
    }

    //timer to reset cooldown for actions that boss can make
    IEnumerator ActionCD()
    {
        yield return new WaitForSeconds(actionCD);
        canAct = true;
    }

    //interrupt behavior
    public void Wait()
    {

    }

    //function to get the closest gameobject from an array of gameobjects
    void SetClosestTarget(GameObject[] players)
    {
        float closestDistSqr = Mathf.Infinity;
        Vector3 currPos = transform.position;

        foreach (GameObject potentialTar in players)
        {
            Vector3 directionToTar = potentialTar.transform.position - currPos;
            float dSqrToTar = directionToTar.sqrMagnitude;

            if (dSqrToTar < closestDistSqr && potentialTar.GetComponent<PlayerHealth>().IsAlive)
            {
                closestDistSqr = dSqrToTar;
                target = potentialTar;
            }
        }
    }

    //function to get the furthest gameobject from an array of gameobjects
    void SetFurthestTarget(GameObject[] players)
    {
        float closestDistSqr = Mathf.Infinity;
        Vector3 currPos = transform.position;

        foreach (GameObject potentialTar in players)
        {
            Vector3 directionToTar = potentialTar.transform.position + currPos;
            float dSqrToTar = directionToTar.sqrMagnitude;

            if (dSqrToTar < closestDistSqr && potentialTar.GetComponent<PlayerHealth>().IsAlive)
            {
                closestDistSqr = dSqrToTar;
                target = potentialTar;
            }
        }
    }

    //function to get a random gameobject from an array of gameobjects
    void ChooseRandomTarget(GameObject[] players)
    {
        target = players[Random.Range(0, players.Length)];
        retarget = false;
        StartCoroutine(AllowRetarget());
    }

    //timer to check if boss can change targets
    IEnumerator AllowRetarget()
    {
        yield return new WaitForSeconds(retargetTimer);
        retarget = true;
    }

    //function to flip direction ofboss
    public void FlipFacing()
    {
        if (!canFlip) return;



        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }


    //function to change state of boss
    public void ChangeMode()
    {
        if (currStage == Stage.melee) currStage = Stage.range;
        else currStage = Stage.both;
    }
}
