using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Person : MonoBehaviour
{
    public new BoxCollider2D collider;
    public Rigidbody2D rigidBody;
    public Transform spriteOrigin;

    /// <summary> List of their features that clues can be made from. </summary>
    public Knowledge features;

    /// <summary> Name in text used to refer to this in dialogue.  </summary>
    public string displayName;

    public float controlX;
    public float controlY;

    public bool isFacingRight { get { return transform.localScale.x > 0; } }
    public bool isWalking;

    public bool interacted;

    public string featureNameList;
    public List<string> _debugFeaturDisplayList;

    public bool featuresInitialised { get { return features.body.Key != null; } }

    public void Awake()
    {
        features = new Knowledge();

        collider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;

        displayName = "Person Smith"; //Generate New Name
    }

    /// <summary> Override this in subclasses for more features </summary>
    public virtual void GenerateAdditionalFeatures(int yPixelOffset) { }

    /// <summary>
    /// Called during Start, after features are initliased (in FeatureFactory)
    /// </summary>
    public void PostFeatureStart()
    {
        featureNameList += features.body.Key.name;
        foreach (var feature in features.ornaments)
        {
            featureNameList += feature.Key.name;
            _debugFeaturDisplayList.Add(feature.Key.displayColour + feature.Key.displayName);
        }
        Debug.Log("generated person " + _debugFeaturDisplayList);
    }


    public void Start()
    {
    }

    // Update is called once per frame
    public void Update()
    {
        foreach (var feature in features.ornaments)
        {
            //feature.Key
        }

        AnimationDriver();
    }

    // FixedUpdate is called once per physics step
    public void FixedUpdate()
    {

    }

    // hack animation driver
    private void AnimationDriver()
    {
        isWalking = false;
        if (Mathf.Abs(rigidBody.velocity.x) > 0.01f)
        {
            isWalking = true;
        }
        features.body.Key.isWalking = isWalking;
    }

    public void Walk(int direction, float maxSpeed = Config.MAX_WALK_SPEED)
    {
        direction = Mathf.Clamp(direction, -1, 1);
        FaceDirection(direction);

        //Add new direction to current velocity
        float newX = rigidBody.velocity.x + direction;

        //Assign new velocity and limit walk speed
        if (newX < maxSpeed && newX > -maxSpeed)
        {
            rigidBody.velocity = rigidBody.velocity.SetX(newX);
        }
        else if (newX >= maxSpeed)
        {
            rigidBody.velocity = rigidBody.velocity.SetX(maxSpeed);
        }
        else if (newX <= -maxSpeed)
        {
            rigidBody.velocity = rigidBody.velocity.SetX(-maxSpeed);
        }


    }

    public void StopWalk()
    {
        rigidBody.velocity = rigidBody.velocity.SetX(0);
    }

    public void FaceDirection(int direction)
    {
        Vector3 scale = spriteOrigin.localScale;
        if (direction > 0)
        { scale.x = 1; }
        else if (direction < 0)
        { scale.x = -1; }
        spriteOrigin.localScale = scale;
    }

    // TODO: move this to employee maybe
    public enum RankEnum
    {
        Employee,
        Manager,
        Executive,
        CEO
    }
}
