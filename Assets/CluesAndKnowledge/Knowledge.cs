using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class represents everything there is to know about a target.
/// </summary>
public class Knowledge
{
    private Dictionary<Feature, Logic> body;
    private Dictionary<Feature, Logic> ornaments;
    private Dictionary<Room, Logic> workplace;
    private Dictionary<Room, Logic> currentLocation;

    /// <summary> Features for the face, eg. hair, hat </summary>
    public Dictionary<Feature, Logic> Body { get; }
    public Dictionary<Feature, Logic> Ornaments { get; }
    public Dictionary<Room, Logic> Workplace { get; }
    public Dictionary<Room, Logic> CurrentLocation { get; }

    public Knowledge()
    {
        body = new Dictionary<Feature, Logic>();
        ornaments = new Dictionary<Feature, Logic>();
        workplace = new Dictionary<Room, Logic>();
        currentLocation = new Dictionary<Room, Logic>();
    }
}

public enum Logic
{ Unknown, True, False, Maybe }
