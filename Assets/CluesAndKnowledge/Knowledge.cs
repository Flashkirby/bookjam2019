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
}

public enum Logic
{ Unknown, True, False, Maybe }
