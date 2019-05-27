using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        return "BLAH! " + clue.ToFactBookString();
    }

    // FEATURE
    /* 

    public string GenerateDialogue()
    {
        string baseString = "";
        if (clueType == ClueTypes.Useless) { return "Sorry, I don't know anything about that person."; }
        if (clueType == ClueTypes.Unsure) { baseString = "Unsure: "; }
        if (clueType == ClueTypes.Partial) { baseString = "Partial Clue: "; }
        if (clueType == ClueTypes.Complete) { baseString = "Complete Clue: "; }

        string featureString = "";
        string endingString = "";
        if (clueType == ClueTypes.Unsure)
        {
            featureString += sortedFeatures[0].displayColour + (sortedFeatures[0].displayColour == "" ? "" : " ") + sortedFeatures[0].displayName;
            endingString = " or " + sortedFeatures[1].displayColour + (sortedFeatures[1].displayColour == "" ? "" : " ") + sortedFeatures[1].displayName;
        }
        else if (clueType == ClueTypes.Partial)
        {
            featureString += sortedFeatures[0].displayName;
        }
        else if (clueType == ClueTypes.Complete)
        {
            featureString += (sortedFeatures[0].displayColour == "" ? "" : " ") + sortedFeatures[0].displayName;
        }

        baseString += featureString;
        baseString += endingString;

        return baseString;
    }
    */

    // FLOOR
    /*

    public string GenerateDialogue()
    {
        string baseString = "";
        if (clueType == ClueTypes.Useless) { return "Sorry, I don't know where that person is."; }
        if (clueType == ClueTypes.Unsure) { baseString = "Unsure: "; }
        if (clueType == ClueTypes.Partial) { baseString = "Partial Clue: "; }
        if (clueType == ClueTypes.Complete) { baseString = "Complete Clue: "; }

        string featureString = "";
        string endingString = "";
        if (clueType == ClueTypes.Unsure)
        {
            featureString += sortedFloors[0].displayName;
            endingString = " or " + sortedFloors[1].displayName;
        }
        else if (clueType == ClueTypes.Partial || clueType == ClueTypes.Complete)
        {
            featureString += sortedFloors[0].displayName;
        }

        baseString += featureString;
        baseString += endingString;

        return baseString;
    }
     */

    // ROOM
    /*

    public string GenerateDialogue()
    {
        string baseString = "";
        if (clueType == ClueTypes.Useless) { return "Sorry, I don't know where that person is."; }
        if (clueType == ClueTypes.Unsure) { baseString = "Unsure: "; }
        if (clueType == ClueTypes.Partial) { baseString = "Partial Clue: "; }
        if (clueType == ClueTypes.Complete) { baseString = "Complete Clue: "; }

        string featureString = "";
        string endingString = "";
        if (clueType == ClueTypes.Unsure)
        {
            featureString += sortedRooms[0].displayName;
            endingString = " or " + sortedRooms[1].displayName;
        }
        else if (clueType == ClueTypes.Partial || clueType == ClueTypes.Complete)
        {
            featureString += sortedRooms[0].displayName;
        }

        baseString += featureString;
        baseString += endingString;

        return baseString;
    }
     */
}
