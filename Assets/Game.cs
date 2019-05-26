using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static Game game;
    public static Game S { get { if (game == null) { game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>(); } return game; } }

    public List<GameObject> featureBodies;
    public List<GameObject> featurePoolShortHair;
    public List<GameObject> featurePoolLongHair;
    public List<GameObject> featurePoolHat;
    public List<GameObject> featurePoolGlasses;
    public List<GameObject> featurePoolBadge;

    private Dictionary<Person.RankEnum, int> rankBadgeDict;

    public List<Room> rooms;
    public List<Person> employees;

    public Detective detective;

    // Awake is called on initialisation, before the component becomes active
    void Awake()
    {
        detective = GameObject.FindGameObjectWithTag("Player").GetComponent<Detective>();
        if (detective == null) { Debug.LogError("Game cannot start without a player", game); }
    }

    // Start is called before the first frame update, after being enabled
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // FixedUpdate is called once per physics step
    void FixedUpdate()
    {

    }

    public void GenerateNewBadgeAssignment()
    {
        rankBadgeDict = new Dictionary<Person.RankEnum, int>();

        var availableIndexList = new List<int>();
        int randomIndex = -1;
        for(int i = 0; i < featurePoolBadge.Count; i++)
        { availableIndexList.Add(i); }

        // Employee
        randomIndex = availableIndexList[Random.Range(0, availableIndexList.Count)];
        rankBadgeDict.Add(Person.RankEnum.Employee, randomIndex);
        Debug.Log("Employee Badge: " + getPrefabBadgeFromRank(Person.RankEnum.Employee).GetComponent<Feature>().name);
        availableIndexList.Remove(randomIndex);

        // Manager
        randomIndex = availableIndexList[Random.Range(0, availableIndexList.Count)];
        rankBadgeDict.Add(Person.RankEnum.Manager, randomIndex);
        Debug.Log("Manager Badge: " + getPrefabBadgeFromRank(Person.RankEnum.Manager).GetComponent<Feature>().name);
        availableIndexList.Remove(randomIndex);

        // Executive
        randomIndex = availableIndexList[Random.Range(0, availableIndexList.Count)];
        rankBadgeDict.Add(Person.RankEnum.Executive, randomIndex);
        Debug.Log("Exec Badge: " + getPrefabBadgeFromRank(Person.RankEnum.Executive).GetComponent<Feature>().name);
        availableIndexList.Remove(randomIndex);

        // CEO
        randomIndex = availableIndexList[Random.Range(0, availableIndexList.Count)];
        rankBadgeDict.Add(Person.RankEnum.CEO, randomIndex);
        Debug.Log("CEO Badge: " + getPrefabBadgeFromRank(Person.RankEnum.CEO).GetComponent<Feature>().name);
        availableIndexList.Remove(randomIndex);
    }
    public GameObject getPrefabBadgeFromRank(Person.RankEnum rank)
    {
        if (rankBadgeDict == null) GenerateNewBadgeAssignment();

        if (rankBadgeDict.TryGetValue(rank, out int index))
        {
            return featurePoolBadge[index];
        }
        Debug.LogError("Uh oh couldn't find a badge for the rank", this);
        return null;
    }

    public Feature GetFeature()
    {
        // Get a feature object and return its feature class
        return null;
    }
}