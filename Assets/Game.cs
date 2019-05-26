﻿using System.Collections;
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

    //public List<GameObject> featurePoolFacialHair;


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

    public Feature GetFeature()
    {
        // Get a feature object and return its feature class
        return null;
    }
}