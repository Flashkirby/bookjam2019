﻿using System.Collections.Generic;
using UnityEngine;
using static Config;

public class Employee : Person
{
    /// <summary> Where they work. </summary>
    public Room Workplace { get { return features.workplace.Key; } }
    /// <summary> Which room they are currently in. </summary>
    public Room CurrentLocation { get { return features.currentLocation.Key; } }
    /// <summary> What rank they are exec, CEO, etc.</summary>
    public RankEnum rank;

    bool isMingling;

    /// <summary> Negative when waiting, positive when moving </summary>
    public float mingleTime;
    public int mingleDir;

    public new void Start()
    {
        base.Start();
        isMingling = true;

        mingleTime = -Random.Range(0, MINGLE_WAIT_MAX);
        mingleDir = Utils.RandomDir();

        FaceDirection(mingleDir);
    }

    public new void FixedUpdate()
    {
        base.FixedUpdate();

        if(isMingling)
        {
            if (mingleTime < 0)
            {
                mingleTime += Time.fixedDeltaTime;
                if (mingleTime >= 0)
                {   // Start walking
                    mingleTime = Random.Range(MINGLE_WALK_MIN, MINGLE_WALK_MAX);
                    mingleDir = Utils.RandomDir();
                }
            }
            else
            {
                mingleTime -= Time.fixedDeltaTime;
                if (mingleTime < 0)
                {   //start waiting
                    mingleTime = -Random.Range(MINGLE_WAIT_MIN, MINGLE_WAIT_MAX);
                }

                // Too far right, or too far left, turn around
                if ((mingleDir > 0 && transform.position.x > features.currentLocation.Key.getXMax) ||
                    (mingleDir < 0 && transform.position.x < features.currentLocation.Key.getXMin))
                {
                    mingleDir = -mingleDir;
                    mingleTime += 1f;
                }

                controlX += mingleDir;
            }
        }

        // Read controls
        if (Utils.isAxisActive(controlX))
        { Walk(Utils.Dir(controlX), MAX_EMPLOYEE_WALK_SPEED); }
        else
        { StopWalk(); }

        // Prevent from leaving their current room
        if (transform.position.x > features.currentLocation.Key.getXMax)
        { transform.position = transform.position.SetX3(features.currentLocation.Key.getXMax); }
        if (transform.position.x < features.currentLocation.Key.getXMin)
        { transform.position = transform.position.SetX3(features.currentLocation.Key.getXMin); }

        // Reset control
        controlX = 0;
        controlY = 0;
    }

    public override void GenerateAdditionalFeatures(int yPixelOffset)
    {
        GameObject pfbBadge = Game.S.getPrefabBadgeFromRank(rank);
        Feature badge = FeatureFactory.InstantiateFeature(this, pfbBadge, yPixelOffset);
        badge.FeatureColour = Workplace.assignedColor;
    }

}
