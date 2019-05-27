using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private static Game game;
    public static Game S { get { if (game == null) { game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>(); } return game; } }

    public List<GameObject> featureBodies;
    public List<GameObject> featurePoolHair;
    public List<GameObject> featurePoolHat;
    public List<GameObject> featurePoolGlasses;
    public List<GameObject> featurePoolBadge;

    private Dictionary<Person.RankEnum, int> rankBadgeDict;

    public GameObject pfbEmployee;
    public GameObject pfbManager;
    public GameObject pfbExecutive;
    public GameObject pfbCEO;

    public Building building;
    public List<Employee> employees;

    public Employee target;

    public Detective detective;
    public FactBook factBook;

    public SplashScreen splashScreen;

    public bool isGameOver = false;
    public bool isGameStarted = false;
    public bool isStartSplash = false;

    // Awake is called on initialisation, before the component becomes active
    void Awake()
    {
        detective = GameObject.FindGameObjectWithTag("Player").GetComponent<Detective>();
        if (detective == null) { Debug.LogError("Game cannot start without a player", game); }
    }

    internal void PostBuildingAwake(Building building)
    {
        this.building = building;
        foreach(Room room in building.allRooms)
        {
            room.GenerateAndAddPeople();
        }
    }

    // Start is called before the first frame update, after being enabled
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameStarted)
        {
            Debug.Log("Game setup!");
            string startString = "You're at this company to meet someone you met at a conference once.\n\n" +
                "All you have is a name on a minimalist business card 'John Smith'.\n\n" +
                "You might have to ask around to see if anyone has seen them.\n\n" +
                "You're feeling a little anxious today though, so you don't want to spend too long here.\n\n" +
                "(Press Space to start)\n\n";

            Debug.Log("Game start!");
            //splashScreen.ShowSplashScreen();
            //splashScreen.splashText.text = startString;
            //Game.S.detective.inMenu = true;
            //isStartSplash = true;
            isGameOver = false;


            //Get Target
            if (employees.Count == 0) Debug.LogError("No employees. This company is very flawed. ");
            target = employees.First();

            //TODO: Remove, debug for notebook
            var features = target.features.getTrueOrnaments();
            Debug.Log(features.Count);
            Feature debugFeature = features.First();

            Clue debugClue = new Clue(ClueFactory.ClueTypes.Unsure, debugFeature);
            Debug.Log(debugClue.ToFactBookString());
            factBook.addClue(debugClue.ToFactBookString());

            isGameStarted = true;
        }
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
        randomIndex = availableIndexList[UnityEngine.Random.Range(0, availableIndexList.Count)];
        rankBadgeDict.Add(Person.RankEnum.Employee, randomIndex);
        Debug.Log("Employee Badge: " + getPrefabBadgeFromRank(Person.RankEnum.Employee).GetComponent<Feature>().name);
        availableIndexList.Remove(randomIndex);

        // Manager
        randomIndex = availableIndexList[UnityEngine.Random.Range(0, availableIndexList.Count)];
        rankBadgeDict.Add(Person.RankEnum.Manager, randomIndex);
        Debug.Log("Manager Badge: " + getPrefabBadgeFromRank(Person.RankEnum.Manager).GetComponent<Feature>().name);
        availableIndexList.Remove(randomIndex);

        // Executive
        randomIndex = availableIndexList[UnityEngine.Random.Range(0, availableIndexList.Count)];
        rankBadgeDict.Add(Person.RankEnum.Executive, randomIndex);
        Debug.Log("Exec Badge: " + getPrefabBadgeFromRank(Person.RankEnum.Executive).GetComponent<Feature>().name);
        availableIndexList.Remove(randomIndex);

        // CEO
        randomIndex = availableIndexList[UnityEngine.Random.Range(0, availableIndexList.Count)];
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

    public void GameOver()
    {
        string endString = "You became too anxious and decided to excuse yourself.\n\n"
        + "You send an apologetic email and will probably try again tomorrow.\n\n"
        + "Try again? (Press Space to restart)";

        Debug.Log("Game Over!");
        splashScreen.ShowSplashScreen();
        splashScreen.splashText.text = endString;
        Game.S.detective.inMenu = true;
        isGameOver = true;
    }

    public void HandleRestart()
    {
        Debug.Log("Restart Game!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HandleStartSplash()
    {
        isStartSplash = false;
        Game.S.detective.inMenu = false;
        splashScreen.CloseSplashScreen();
    }

    /// <summary> Iterates through each prefab list, matching by name. </summary>
    /// <returns> Refernece to the pool that the feature belongs to, or null</returns>
    public List<GameObject> GetPoolSharingFeature(Feature feature)
    {
        if (featurePoolHair.Find(o => o.name == feature.name) != null) { return featurePoolHair; }
        if (featurePoolHat.Find(o => o.name == feature.name) != null) { return featurePoolHat; }
        if (featurePoolGlasses.Find(o => o.name == feature.name) != null) { return featurePoolGlasses; }
        return null;
    }

    /// <summary> Index of feature, in the given pool. </summary>
    /// <returns>The index of the feature in the pool, or -1</returns>
    public int getIndexOfFeature(List<GameObject> pool, Feature feature)
    {
        GameObject matchingPrefab = pool.Find(o => o.name == feature.name);
        return pool.IndexOf(matchingPrefab);
    }
}