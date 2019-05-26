using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Room on a floor
/// </summary>
public class Room : MonoBehaviour
{
    public Building building;
    public Floor floor;

    public GameObject rightDoor;
    public GameObject rightWall;
    public GameObject leftDoor;
    public GameObject leftWall;

    /// <summary> Assigned room colour for badges etc. </summary>
    public Color assignedColor;

    /// <summary> Name in text used to refer to this in dialogue.  </summary>
    public string displayName;

    public void SetRightOpen(bool open)
    {
        rightDoor.SetActive(open);
        rightWall.SetActive(!open);
    }
    public void SetLeftOpen(bool open)
    {
        leftDoor.SetActive(open);
        leftWall.SetActive(!open);
    }
}