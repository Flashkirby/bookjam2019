using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class represents everything there is to know about a target.
/// </summary>
public class Knowledge
{
    /// <summary> Features for the face, eg. hair, hat </summary>
    public KeyValuePair<BodyFeature, Logic> body;
    public Dictionary<Feature, Logic> ornaments;
    public Dictionary<Room, Logic> workplace;
    public Dictionary<Room, Logic> currentLocation;

    public Knowledge()
    {
        body = new KeyValuePair<BodyFeature, Logic>();
        ornaments = new Dictionary<Feature, Logic>();
        workplace = new Dictionary<Room, Logic>();
        currentLocation = new Dictionary<Room, Logic>();
    }

    public void SetBody(BodyFeature feature, Logic logic)
    {
        body = new KeyValuePair<BodyFeature, Logic>(feature, logic);
    }

    public List<Feature> getTrueOrnaments()
    {
        var trueFeatures = new List<Feature>();
        foreach(var kvp in ornaments)
        {
            if(kvp.Value == Logic.True)
            {
                trueFeatures.Add(kvp.Key);
            }
        }
        return trueFeatures;
    }
}

public enum Logic
{ Unknown, True, False, Maybe }
