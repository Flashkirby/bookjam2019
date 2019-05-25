using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Room on a floor
/// </summary>
public class Department : MonoBehaviour
{
    /// <summary> Name in text used to refer to this in dialogue.  </summary>
    public string DisplayName { get; set; }

    /// <summary> Starts at G (0) </summary>
    public int FloorNumber { get; set; }
}