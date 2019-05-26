using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    //public List<GameObject> featurePoolFacialHair;


    public List<Room> rooms;
    public List<Person> employees;

    public Detective detective;

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
            splashScreen.ShowSplashScreen();
            splashScreen.splashText.text = startString;
            Game.S.detective.inMenu = true;
            isGameOver = false;
            isStartSplash = true;

            isGameStarted = true;
        }
    }

    // FixedUpdate is called once per physics step
    void FixedUpdate()
    {

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
}