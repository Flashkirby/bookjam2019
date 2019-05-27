using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Config;

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

    public Queue<Dialogue> dialogueQueue = new Queue<Dialogue>();
    public DialogueBox sceneDialogueBox;

    public Detective detective;
    public FactBook factBook;

    public SplashScreen splashScreen;

    public new Camera camera;
    private Vector3 offset;            //Private variable to store the offset distance between the player and camera

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
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = camera.transform.position - detective.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        HandleDebugInput();

        HandleGameStart();

        HandleDialogue();

        if (detective.inMenu)
        {
            if (isGameOver)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    HandleRestart();
                }
            }
            if (isStartSplash)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    HandleStartSplash();
                }
            }
        }
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        camera.transform.position = detective.transform.position + offset;

        int ppu = PIXEL_PER_UNIT;
        if (Input.GetButton("Map"))
        { ppu = PIXEL_PER_UNIT_ZOOM_OUT; }
        camera.GetComponent<UnityEngine.U2D.PixelPerfectCamera>().assetsPPU = ppu;
    }

    private void HandleGameStart()
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
            target = employees.PickRandom();

            Name nameGen = new Name();
            for (int i = 0; i < 100; i++)
            {
                Debug.Log(nameGen.GenerateRandomName());
            }

            //TODO: Remove, debug for notebook
            //for (int i = 0; i < 20; i++)
            //{
            //    var features = target.features.getTrueOrnaments();
            //    Feature debugFeature = features.PickRandom();

            //    Room debugRoom = target.CurrentLocation;

            //    Floor debugFloor = target.CurrentLocation.floor;

            //    var clueTypeArray = Enum.GetValues(typeof(ClueFactory.ClueTypes));
            //    ClueFactory.ClueTypes randomClueType = (ClueFactory.ClueTypes)clueTypeArray.GetValue(Utils.rnd.Next(clueTypeArray.Length));

            //    IClue debugClueFeature = new ClueFeature(randomClueType, debugFeature);
            //    factBook.addClue(debugClueFeature.ToFactBookString());
            //    IClue debugClueRoom = new ClueRoom(randomClueType, debugRoom);
            //    factBook.addClue(debugClueRoom.ToFactBookString());
            //    IClue debugClueFloor = new ClueFloor(randomClueType, debugFloor);
            //    factBook.addClue(debugClueFloor.ToFactBookString());
            //}

            isGameStarted = true;
        }
    }

    private void HandleDialogue()
    {
        if (sceneDialogueBox == null) { Debug.LogWarning("No dialogue box linked"); return; }

        if (dialogueQueue.Count != 0)
        {
            detective.inMenu = true;
            detective.talkCooldown = INTERACT_COOLDOWN;
            sceneDialogueBox.OpenDialogue();

            if (Input.GetButtonDown("Fire1"))
            {
                dialogueQueue.Dequeue();
                if (dialogueQueue.Count == 0)
                {
                    detective.inMenu = false;
                    sceneDialogueBox.CloseDialogue();
                }
            }
        }
    }

    // FixedUpdate is called once per physics step
    void FixedUpdate()
    {

    }

    public void HandleDebugInput()
    {
        if (Input.GetButtonDown("DEBUG_ONE"))
        {
            Game.S.detective.anxiety += 0.5f;
        }
        if (Input.GetButtonDown("DEBUG_TWO"))
        {
            Game.S.detective.anxiety -= 0.5f;
        }
        if (Input.GetButtonDown("DEBUG_THREE"))
        {
            GameWin();
        }
        if (Input.GetButtonDown("DEBUG_FOUR"))
        {
            GameOver();
        }
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
        + "You'll send an apologetic email and will probably try again tomorrow.\n\n"
        + "Try again? (Press Space to restart)";

        Debug.Log("Game Over!");
        splashScreen.ShowSplashScreen();
        splashScreen.splashText.text = endString;

        Game.S.detective.inMenu = true;
        isGameOver = true;
    }

    public void GameWin()
    {
        string winString = "You found who you were looking for!\n\n"
        + "You'll probably want to remember what they look like next time.\n\n"
        + "(Press Space to restart)";

        Debug.Log("Game Win!");
        splashScreen.ShowSplashScreen();
        splashScreen.splashText.text = winString;

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
        // TOD: featurePoolBadge
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