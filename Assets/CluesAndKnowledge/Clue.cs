using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ClueFactory;

public class Clue : Knowledge
{

    ClueTypes clueType;
    Feature feature;
    Feature fakeFeature;
    List<Feature> sortedFeatures;

    //Clue needs a ClueTypes
    //For Useless, we just need a string, we don't need to add to notebook
    //For Vague, we need a single aspect of a feature
    //For Unsure, we need an either/or
    //For perfect, we describe a feature perfectly
    public Clue(ClueTypes clueType, Feature feature)
    {
        this.clueType = clueType;
        this.feature = feature;

        if(clueType == ClueTypes.Unsure)
        {
            // Find the pool the object shares
            var samePool = Game.S.GetPoolSharingFeature(feature);

            // Copy the pool TODO: Find out if this is necessary
            List<GameObject> samePoolCopy = new List<GameObject>();
            samePoolCopy.AddRange(samePool);

            //Remove all duplicates from list
            samePoolCopy.RemoveAll(f => f.GetComponent<Feature>().displayName == feature.displayName);

            // Pick from non-duplicated list
            fakeFeature = samePoolCopy.PickRandom().GetComponent<Feature>();
            
            //Sort clues so we don't identify the clue by it being first
            List<Feature> fakeAndRealFeatures = new List<Feature> { feature, fakeFeature };
            sortedFeatures = fakeAndRealFeatures.OrderBy(x => x.displayName).ToList();
        }
    }


    public string ToFactBookString()
    {
        string baseString = "";

        //Build prefix
        if(clueType == ClueTypes.Useless) { return "USELESS"; };

        if (clueType == ClueTypes.Unsure)
        {
            baseString = "Either: ";
        }

        if (clueType == ClueTypes.Perfect || clueType == ClueTypes.Vague)
        {
            baseString = "Definitely: ";
        }

        //Build feature and ending
        string featureSubject = "";
        string endingString = "";
        if (clueType == ClueTypes.Unsure)
        {
            featureSubject = sortedFeatures[0].displayName;
            endingString = " or " + sortedFeatures[1].displayName;

        } else
        {
            featureSubject = feature.displayName;
            endingString = "";
        }

        //Combine the strings
        baseString += featureSubject;
        baseString += endingString;

        return baseString;
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public string generateDialogue()
    {

        string baseString = "";
        if (clueType == ClueTypes.Useless)
        {
            return "Sorry, I don't know anything about that person.";
        }

        if (clueType == ClueTypes.Vague)
        {
            baseString = "Vague Clue: ";
        }

        if (clueType == ClueTypes.Unsure)
        {
            baseString = "Unsure: ";
        }

        if (clueType == ClueTypes.Perfect)
        {
            baseString = "Perfect: ";
        }

        string featureVerb; //TODO:
        string featureSubject; //TODO:

        string endingString = ".";
        if (clueType == ClueTypes.Unsure)
        {
            Game.S.GetPoolSharingFeature(feature);


            Feature fakeFeature; //TODO:
            endingString = " or PUTSOMETHING HERE.";
        }

        baseString += endingString;

        return baseString;
    }
}
