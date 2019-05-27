using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ClueFactory;

public static class DialogueFactory
{
    public static void StartDialogue(List<IClue> clues, Person speaker, Person listener)
    {
        string speakerName = speaker.displayName;
        bool isSpeakerRight = speaker.transform.position.x > listener.transform.position.x;
        foreach(var clue in clues)
        {
            var d = new Dialogue(speakerName, isSpeakerRight, GenerateDialogueFrom(clue));
            Debug.Log("Dialogue: " + d.dialogueText);
            Game.S.dialogueQueue.Enqueue(d);
        }
    }

    private static string GenerateDialogueFrom(IClue clue)
    {
        if(clue is ClueFeature)
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

        string correctIdentifyString = "Oh hi there! Nice to see you again!";
        var d = new Dialogue(speakerName, isSpeakerRight, correctIdentifyString);
        Debug.Log("Dialogue: " + d.dialogueText);
        Game.S.dialogueQueue.Enqueue(d);
    }

    public static void GenerateIncorrectIdentifyDialogue(Person speaker, Person listener)
    {
        string speakerName = speaker.displayName;
        bool isSpeakerRight = speaker.transform.position.x > listener.transform.position.x;

        string correctIdentifyString = "What? What are you talking about?";
        var d = new Dialogue(speakerName, isSpeakerRight, correctIdentifyString);
        Debug.Log("Dialogue: " + d.dialogueText);
        Game.S.dialogueQueue.Enqueue(d);
    }

    public static string generateFeatureDialogue(ClueFeature clue)
    {
        string baseString = "";
        if (clue.clueType == ClueTypes.Useless) { return "Sorry, I don't know what that person looks like."; }
        if (clue.clueType == ClueTypes.Unsure) { baseString = "I think they "; }
        if (clue.clueType == ClueTypes.Partial) { baseString = "As far as I know, they "; }
        if (clue.clueType == ClueTypes.Complete) { baseString = "I'm sure they "; }

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


    public static string generateFloorDialogue(ClueFloor clue)
    {
        string baseString = "";
        if (clue.clueType == ClueTypes.Useless) { return "Sorry, I don't know where that person is."; }
        if (clue.clueType == ClueTypes.Unsure) { baseString = "They're either on the "; }
        if (clue.clueType == ClueTypes.Partial) { baseString = "As far as I know, they're on the "; }
        if (clue.clueType == ClueTypes.Complete) { baseString = "I'm certain they're on the "; }

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

    public static string generateRoomDialogue(ClueRoom clue)
    {
        string baseString = "";
        if (clue.clueType == ClueTypes.Useless) { return "Sorry, I don't where that person works."; }
        if (clue.clueType == ClueTypes.Unsure) { baseString = "The might be in the "; }
        if (clue.clueType == ClueTypes.Partial) { baseString = "I think I last saw them in the "; }
        if (clue.clueType == ClueTypes.Complete) { baseString = "Oh yeah! They were just in the "; }

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
}
