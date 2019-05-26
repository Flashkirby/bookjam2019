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

    /// <summary> Assigned room colour for badges etc. </summary>
    public Color assignedColor;

    /// <summary> Name in text used to refer to this in dialogue.  </summary>
    public string displayName;
}