/******************************************************************************
filename ObjectSound.cs
author Clarence Phun
email k.phun@digipen.edu
due date 13 April 2018
Brief Description:
controller to store sound library and to play sounds from library for whomper
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectSound : MonoBehaviour
{
    public AudioSource sfx;

    [SerializeField]
    AudioClip[] audios;
    //Sound - arrNum
    //landing - 0
    //destroy - 1


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
