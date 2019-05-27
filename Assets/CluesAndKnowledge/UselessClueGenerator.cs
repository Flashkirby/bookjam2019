using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UselessClueGenerator 
{

    public string getUselessClueString() {
        return uselessClues.PickRandom();
}

    List<string> uselessClues = new List<string>
    {
        "Sorry, I don't where that person works.",
        "Sorry, I don't know where that person is.",
        "Sorry, I don't know what that person looks like.",
        "Does that person even work here?",
        "I'm sad, I don't have any pick & mix places near me.",
        "The coffee here is terrible! Why do I even work here!",
        "I don't know nothing about anything about nothing.",
        "I wish I had more time to stand around doing nothing.",
        "What does this company do? I'm not even sure myself.",
        "Does this place have toilets?",
        "I don't want to talk to you.",
        "I miss when games were good. Wanna play Tetris?",
        "Why is no one doing any work?",
        "I heard there's a game jam coming up. It sounds really cool!",
        "Do you ever wonder if you're trapped inside a computer?",

    };
}
