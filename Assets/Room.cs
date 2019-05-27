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

    public int numberOfEmployees;
    public int numberOfManagers;
    public int numberOfExecs;
    public int numberOfCEOs;

    public float getXMin { get { return leftWall.transform.position.x; } }
    public float getXMax { get { return rightWall.transform.position.x; } }

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

    public void GenerateAndAddPeople()
    {
        foreach(Person.RankEnum rank in GetRankPool())
        { InstantiatePrefabFromRank(rank); }
    }

    public Employee InstantiatePrefabFromRank(Person.RankEnum rank)
    {
        switch(rank)
        {
            case Person.RankEnum.Manager:
                numberOfManagers--;
                return InstantiatePrefabEmployee(Game.S.pfbManager); 
            case Person.RankEnum.Executive:
                numberOfExecs--;
                return InstantiatePrefabEmployee(Game.S.pfbExecutive);
            case Person.RankEnum.CEO:
                numberOfCEOs--;
                return InstantiatePrefabEmployee(Game.S.pfbCEO);
            default:
                numberOfEmployees--;
                return InstantiatePrefabEmployee(Game.S.pfbEmployee);
        }
    }

    public Employee InstantiatePrefabEmployee(GameObject prefabEmployee)
    {
        GameObject go = Instantiate(prefabEmployee);
        go.transform.position = PickRandomRoomPosition();
        Employee emp = go.GetComponent<Employee>();
        emp.features.workplace = new KeyValuePair<Room, Logic>(this, Logic.True);
        emp.features.currentLocation = new KeyValuePair<Room, Logic>(this, Logic.True);
        Game.S.employees.Add(emp);

        return emp;
    }

    public Vector3 PickRandomRoomPosition(float zMin = 0f, float zMax = 0.9f)
    {
        float xMin = getXMin;
        float xMax = getXMax;
        float randomX = Random.Range(xMin, xMax);
        float randomZ = Random.Range(zMin, zMax);
        return new Vector3(randomX, transform.position.y, randomZ);
    }

    public List<Person.RankEnum> GetRankPool()
    {
        var pool = new List<Person.RankEnum>();
        for (int i = 0; i < numberOfEmployees; i++) { pool.Add(Person.RankEnum.Employee); }
        for (int i = 0; i < numberOfManagers; i++) { pool.Add(Person.RankEnum.Manager); }
        for (int i = 0; i < numberOfExecs; i++) { pool.Add(Person.RankEnum.Executive); }
        for (int i = 0; i < numberOfCEOs; i++) { pool.Add(Person.RankEnum.CEO); }
        return pool;
    }
}