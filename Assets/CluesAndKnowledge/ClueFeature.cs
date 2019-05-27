using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ClueFactory;

public class ClueFeature : IClue
{
    public ClueTypes clueType;
    public Feature feature;
    public Feature fakeFeature;
    public List<Feature> sortedFeatures;

    //Clue needs a ClueTypes
    //For Useless, we just need a string, we don't need to add to notebook
    //For Vague, we need a single aspect of a feature
    //For Unsure, we need an either/or
    //For perfect, we describe a feature perfectly
    public ClueFeature(ClueTypes clueType, Feature feature)
    {
        this.clueType = clueType;
        this.feature = feature;

        if (clueType == ClueTypes.Unsure)
        {
            // Find the pool the object shares
            var samePool = Game.S.GetPoolSharingFeature(feature);

            // Copy the pool TODO: Find out if this is necessary
            List<GameObject> samePoolCopy = new List<GameObject>();
            samePoolCopy.AddRange(samePool);

            //Remove all duplicates from list
            samePoolCopy.RemoveAll(f =>
                f.GetComponent<Feature>().displayColour + " " + f.GetComponent<Feature>().displayName
                ==
                feature.displayColour + " " + feature.displayName
            );

            // Pick from non-duplicated list
            fakeFeature = samePoolCopy.PickRandom().GetComponent<Feature>();

            //Sort clues so we don't identify the clue by it being first
            List<Feature> fakeAndRealFeatures = new List<Feature> { feature, fakeFeature };
            sortedFeatures = fakeAndRealFeatures.OrderBy(x => x.displayName).ToList();
        } else
        {
            sortedFeatures = new List<Feature> { feature };
        }
    }

    public string ToFactBookString()
    {
        string baseString = "";
        if (clueType == ClueTypes.Useless) { return "USELESS"; }
        if (clueType == ClueTypes.Unsure) { baseString = "Apperance: "; }
        if (clueType == ClueTypes.Partial) { baseString = "Apperance: "; }
        if (clueType == ClueTypes.Complete) { baseString = "Apperance: "; }

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
            featureString += sortedFeatures[0].displayColour + (sortedFeatures[0].displayColour == "" ? "" : " ") + sortedFeatures[0].displayName;
        }

        baseString += featureString;
        baseString += endingString;

        return baseString;
    }

    //public override string ToString()
    //{
    //    return base.ToString();
    //}
}
