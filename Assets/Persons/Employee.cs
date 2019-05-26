using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : Person
{
    private List<Clue> Clues;

    /// <summary> Where they work. </summary>
    public Room workplace;
    /// <summary> Which room they are currently in. </summary>
    public Room currentLocation;
    /// <summary> What rank they are exec, CEO, etc.</summary>
    public RankEnum rank;

    public override void GenerateAdditionalFeatures(int yPixelOffset)
    {
        GameObject pfbBadge = Game.S.getPrefabBadgeFromRank(rank);
        FeatureFactory.InstantiateFeature(this, pfbBadge, yPixelOffset);
    }

}
