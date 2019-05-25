using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public Building building;
    public List<Room> rooms;

    public string displayName;
    public string shortName;

    public void Awake()
    {

    }
}
