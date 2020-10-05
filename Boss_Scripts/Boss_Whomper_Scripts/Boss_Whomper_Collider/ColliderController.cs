/******************************************************************************
filename ColliderController.cs
author Clarence Phun
email k.phun@digipen.edu
due date 13 April 2018
Brief Description:
Controller for whomper's colliders to change with animation

All content © 2017 Digipen Institue of Technology, all rights reserved
******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    [Header("Collider Animation")]
    [SerializeField]
    protected PolygonCollider2D[] colliders;
    protected int currentColliderIndex = 0;

    //function to set current collider to parameter
    public void SetColliderForSprite(int spriteNum)
    {
        colliders[currentColliderIndex].enabled = false;
        currentColliderIndex = spriteNum;
        colliders[currentColliderIndex].enabled = true;
    }
}

/// <summary>
/// Idle Frames
/// 0 = [0]
/// 1 = [1]
/// 2 = [1]
/// 3 = [2]
/// 4 = [2]
/// 5 = [3]
/// 6 = [3]
/// 7 = [3]
/// 
/// Attack Frames
/// 0 = [5]
/// 1 = [5]
/// 2 = [5]
/// 3 = [5]
/// 4 = [5]
/// 5 = [5]
/// 6 = [5]
/// 7 = [5]
/// 8 = [5]
/// 9 = [5]
/// 10 = [5]
/// 11 = [6]
/// 12 = [7]
/// 13 = [8]
/// 14 = [9]
/// 15 = [9]
/// 16 = [9]
/// 17 = [9]
/// 18 = [9]
/// 19 = [9]
/// 20 = [9]
/// 21 = [9]
/// 
/// Skill One Frames
/// 0 = [11]
/// 1 = [12]
/// 2 = [13]
/// 3 = [14]
/// 4 = [15]
/// 5 = [16]
/// 6 = [17]
/// 7 = [18]
/// 8 = [19]
/// 9 = [19]
/// 10 = [19]
/// 11 = [19]
/// 12 = [19]
/// 13 = [20]
/// 14 = [21]
/// 15 = [21]
/// 16 = [21]
/// 17 = [21]
/// 18 = [21]
/// 19 = [21]
/// 20 = [21]
/// 
/// Skill Two Frames
/// 0 = [23]
/// 1 = [24]
/// 2 = [25]
/// 3 = [25]
/// 4 = [26]
/// 5 = [26]
/// 6 = [23]
/// 7 = [27]
/// 8 = [28]
/// 9 = [29]
/// 10 = [30]
/// 11 = [30]
/// 12 = [31]
/// 13 = [32]
/// 
/// Skill Three Frames 
/// 0 = [34]
/// 1 = [35]
/// 2 = [36]
/// 3 = [37]
/// 4 = [38]
/// 5 = [38]
/// 6 = [38]
/// 7 = [38]
/// 
/// Skill Four Frames
/// 0 = [0]
/// 1 = [40]
/// 2 = [41]
/// 3 = [42]
/// 4 = [43]
/// 5 = [43]
/// 6 = [43]
/// 7 = [43]
/// 8 = [43]
/// 9 = [44]
/// 10 = [45]
/// 11 = [45]
/// 12 = [45]
/// 13 = [45]
/// 14 = [45]
/// 
/// Skill Four Recovery Frames
/// 0 = [45]
/// 1 = [45]
/// 2 = [45]
/// 3 = [45]
/// 4 = [45]
/// 5 = [45]
/// 6 = [45]
/// 7 = [45]
/// 8 = [46]
/// 9 = [0]
/// 
/// </summary>

