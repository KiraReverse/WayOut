/******************************************************************************
filename WhomperSound.cs
author Clarence Phun
email k.phun@digipen.edu
due date 13 April 2018
Brief Description:
controller to store sound library and to play sounds from library for whomper


All content © 2017 Digipen Institue of Technology, all rights reserved
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WhomperSound : MonoBehaviour
{
    public AudioSource sfx;

    [SerializeField]
    AudioClip[] audios;
    //Sound - arrNum
    //skill1 - 0
    //attack throw - 1
    //skill4 recovery - 2
    //chase - 3 
    //skill3 - 4
    //skill4- 5 
    //death - 6

    // Use this for initialization
    void Start()
    {
        sfx.volume = PlayerPrefs.GetFloat("sfxVol");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayClip(int arrNum)
    {
        sfx.clip = audios[arrNum];
        sfx.Play();
    }

}
