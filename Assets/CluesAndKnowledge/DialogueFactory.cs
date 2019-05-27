using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ClueFactory;

public static class DialogueFactory
{

    static UselessClueGenerator uselessClueGenerator = new UselessClueGenerator();
    public static void StartDialogue(List<IClue> clues, Person speaker, Person listener)
    {
        string speakerName = speaker.displayName;
        bool isSpeakerRight = speaker.transform.position.x > listener.transform.position.x;
        foreach (var clue in clues)
        {
            var d = new Dialogue(speakerName, isSpeakerRight, GenerateDialogueFrom(clue));
            Debug.Log("Dialogue: " + d.dialogueText);
            Game.S.dialogueQueue.Enqueue(d);
        }
    }

    private static string GenerateDialogueFrom(IClue clue)
    {
        if (clue is ClueFeature)
        {
            return generateFeatureDialogue((ClueFeature)clue);
        }
        if (clue is ClueFloor)
        {
            return generateFloorDialogue((ClueFloor)clue);
        }
        if (clue is ClueRoom)
        {
            return generateRoomDialogue((ClueRoom)clue);
        }

        return "Who?";
    }

    public static void GenerateCorrectIdentifyDialogue(Person speaker, Person listener)
    {
        string speakerName = speaker.displayName;
        bool isSpeakerRight = speaker.transform.position.x > listener.transform.position.x;

        string correctIdentifyString = correctIdentifyStringList.PickRandom();
        var d = new Dialogue(speakerName, isSpeakerRight, correctIdentifyString);
        Debug.Log("Dialogue: " + d.dialogueText);
        Game.S.dialogueQueue.Enqueue(d);
    }
    static List<string> correctIdentifyStringList = new List<string> {
        "Oh hi there! Nice to see you again!",
        "Yup, that's me, hi there!",
    };


    public static void GenerateIncorrectIdentifyDialogue(Person speaker, Person listener)
    {
        string speakerName = speaker.displayName;
        bool isSpeakerRight = speaker.transform.position.x > listener.transform.position.x;

        string incorrectIdentifyString = incorrectIdentifyStringList.PickRandom();
        var d = new Dialogue(speakerName, isSpeakerRight, incorrectIdentifyString);
        Debug.Log("Dialogue: " + d.dialogueText);
        Game.S.dialogueQueue.Enqueue(d);
    }
    static List<string> incorrectIdentifyStringList = new List<string> {
        "What? What are you talking about?",
        "Who is that? I've never heard of them!",
        "I'm not them! Wait, who are you!?",
        "Do you just like getting people's names wrong?",
        "Huh? Go away."
    };


    public static void GenerateTalkingToTargetDialogue(Person speaker, Person listener)
    {
        string speakerName = speaker.displayName;
        bool isSpeakerRight = speaker.transform.position.x > listener.transform.position.x;

        string talkToTargetString = talkToTargetStringList.PickRandom();
        var d = new Dialogue(speakerName, isSpeakerRight, talkToTargetString);
        Debug.Log("Dialogue: " + d.dialogueText);
        Game.S.dialogueQueue.Enqueue(d);
    }
    static List<string> talkToTargetStringList = new List<string> {
        "Of course I know what they look like, they're me!",
        "They look like me! Because that is me!",
        "Don't you remember? That's me!"
    };

    public static string generateFeatureDialogue(ClueFeature clue)
    {
        string baseString = "";
        if (clue.clueType == ClueTypes.Useless) { return uselessClueGenerator.getUselessClueString(); }
        if (clue.clueType == ClueTypes.Unsure) { baseString = featureDialogueUnsureStarters.PickRandom() + " "; }
        if (clue.clueType == ClueTypes.Partial) { baseString = featureDialoguePartialStarters.PickRandom() + " "; }
        if (clue.clueType == ClueTypes.Complete) { baseString = featureDialogueCompleteStarters.PickRandom() + " "; }

        string connectingString = "";
        connectingString += clue.sortedFeatures[0].linkingVerb != "" ? clue.sortedFeatures[0].linkingVerb + " " : "";
        connectingString += clue.sortedFeatures[0].adjective != "" ? clue.sortedFeatures[0].adjective + " " : "";
        baseString += connectingString;

        string featureString = "";

        string endingString = ".";
        if (clue.clueType == ClueTypes.Unsure)
        {
            featureString =
                clue.sortedFeatures[0].displayColour +
                (clue.sortedFeatures[0].displayColour == "" ? "" : " ") +
                clue.sortedFeatures[0].displayName;

            string endingConnectingString = "";
            endingConnectingString += clue.sortedFeatures[1].linkingVerb != "" ? clue.sortedFeatures[1].linkingVerb + " " : "";
            endingConnectingString += clue.sortedFeatures[1].adjective != "" ? clue.sortedFeatures[1].adjective + " " : ""; ;

            endingString = " or " +
                endingConnectingString +
                clue.sortedFeatures[1].displayColour +
                (clue.sortedFeatures[1].displayColour == "" ? "" : " ") +
                clue.sortedFeatures[1].displayName +
                ".";
        }
        else if (clue.clueType == ClueTypes.Partial)
        {
            featureString += clue.sortedFeatures[0].displayName;
        }
        else if (clue.clueType == ClueTypes.Complete)
        {
            featureString += clue.sortedFeatures[0].displayColour + (clue.sortedFeatures[0].displayColour == "" ? "" : " ") + clue.sortedFeatures[0].displayName;
        }

        baseString += featureString;
        baseString += endingString;

        return baseString;
    }
    static List<string> featureDialogueUnsureStarters = new List<string>{
        "I think they",
        "Uhh, I think they",
        "I'm not sure, either they",
    };

    static List<string> featureDialoguePartialStarters = new List<string>{
        "As far as I know, they",
        "Last I saw them, they",
    };

    static List<string> featureDialogueCompleteStarters = new List<string>{
        "I'm sure they",
        "They definitely"
    };

    public static string generateFloorDialogue(ClueFloor clue)
    {
        string baseString = "";
        if (clue.clueType == ClueTypes.Useless) { return uselessClueGenerator.getUselessClueString(); }
        if (clue.clueType == ClueTypes.Unsure) { baseString = floorDialogueUnsureStarters.PickRandom() + " "; }
        if (clue.clueType == ClueTypes.Partial) { baseString = floorDialoguePartialStarters.PickRandom() + " "; }
        if (clue.clueType == ClueTypes.Complete) { baseString = floorDialogueCompleteStarters.PickRandom() + " "; }

        string featureString = "";
        string endingString = ".";
        if (clue.clueType == ClueTypes.Unsure)
        {
            featureString += clue.sortedFloors[0].displayName;
            endingString = " or " + clue.sortedFloors[1].displayName + ".";
        }
        else if (clue.clueType == ClueTypes.Partial || clue.clueType == ClueTypes.Complete)
        {
            featureString += clue.sortedFloors[0].displayName;
        }

        baseString += featureString;
        baseString += endingString;

        return baseString;
    }
    static List<string> floorDialogueUnsureStarters = new List<string>{
        "They're either on the",
        "Floor? They may be on the"
    };

    static List<string> floorDialoguePartialStarters = new List<string>{
        "As far as I know, they're on the",
        "Unsure, maybe they're on the"
    };

    static List<string> floorDialogueCompleteStarters = new List<string>{
        "I'm certain they're on the",
        "I just saw them on the"
    };


    public static string generateRoomDialogue(ClueRoom clue)
    {
        string baseString = "";
        if (clue.clueType == ClueTypes.Useless) { return uselessClueGenerator.getUselessClueString(); }
        if (clue.clueType == ClueTypes.Unsure) { baseString = roomDialogueUnsureStarters.PickRandom() + " "; }
        if (clue.clueType == ClueTypes.Partial) { baseString = roomDialoguePartialStarters.PickRandom() + " "; }
        if (clue.clueType == ClueTypes.Complete) { baseString = roomDialogueCompleteStarters.PickRandom() + " "; }

        string featureString = "";
        string endingString = ".";
        if (clue.clueType == ClueTypes.Unsure)
        {
            featureString += clue.sortedRooms[0].displayName;
            endingString = " or the " + clue.sortedRooms[1].displayName + ".";
        }
        else if (clue.clueType == ClueTypes.Partial || clue.clueType == ClueTypes.Complete)
        {
            featureString += clue.sortedRooms[0].displayName;
        }

        baseString += featureString;
        baseString += endingString;

        return baseString;
    }

    static List<string> roomDialogueUnsureStarters = new List<string>{
        "They might be in the",
        "I'm not sure, I think they're either in the"
    };

    static List<string> roomDialoguePartialStarters = new List<string>{
        "I think I last saw them in the",
        "They could be in the"
    };

    static List<string> roomDialogueCompleteStarters = new List<string>{
        "Oh yeah! They were just in the",
        "They're in the"
    };
}
