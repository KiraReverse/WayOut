/******************************************************************************
filename SkillFourImmobilizeController.cs
author Clarence Phun
email k.phun@digipen.edu
due date 13 April 2018
Brief Description:
controller for whomper's skillfour's objects and behaviors

All content © 2017 Digipen Institue of Technology, all rights reserved
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFourImmobilizeController : MonoBehaviour
{
    private GameObject mainBody;
    private WhomperState whomperState;
    PlayerHealth pH;
    private GameObject target;
    public GameObject Target { get { return target; } }
    public GameObject death;

    public float tickTime = 1;
    public float dmg = 5;

    void TheStart(GameObject v)
    {

        target = v;
        pH = v.GetComponent<PlayerHealth>();
        StartCoroutine(DamageTick());

        target.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);

        Physics2D.IgnoreLayerCollision(15, 24, true);
    }

    // Use this for initialization
    void Start()
    {
        mainBody = GameObject.FindGameObjectWithTag("Boss");
        whomperState = mainBody.GetComponent<WhomperState>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Destroy(gameObject);
        }
        if (!pH.IsAlive)
        {
            Destroy(gameObject);
        }

        target.GetComponent<PlayerMovement>().LockMovement();
        target.GetComponent<PlayerMovement>().LockAttacks();
        transform.position = target.transform.position;
    }

    private void OnDestroy()
    {
        if (whomperState != null) { whomperState.StartSkillFourRecovery(); }
        Physics2D.IgnoreLayerCollision(15, 24, false);
        Instantiate(death, gameObject.transform.position, death.transform.rotation);
        if (target != null && pH.IsAlive)
        {
            target.SendMessage("Bind", false);
        }
    }

    //timer to deal damage over time
    IEnumerator DamageTick()
    {
        yield return new WaitForSeconds(tickTime);
        target.SendMessage("Damaged", dmg);
        StartCoroutine(DamageTick());

    }
}
