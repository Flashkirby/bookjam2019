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
    private Dictionary<Department, Logic> workplace;
    private Dictionary<Department, Logic> currentLocation;

    /// <summary> Features for the face, eg. hair, hat </summary>
    public Dictionary<Feature, Logic> Body { get; }
    public Dictionary<Feature, Logic> Ornaments { get; }
    public Dictionary<Department, Logic> Workplace { get; }
    public Dictionary<Department, Logic> CurrentLocation { get; }

    public Knowledge()
    {
        body = new Dictionary<Feature, Logic>();
        ornaments = new Dictionary<Feature, Logic>();
        workplace = new Dictionary<Department, Logic>();
        currentLocation = new Dictionary<Department, Logic>();
    }
}

public enum Logic
{ Unknown, True, False, Maybe }
