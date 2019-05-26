using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Person : MonoBehaviour
{
    public new BoxCollider2D collider;
    public Rigidbody2D rigidBody;
    public Transform spriteOrigin;

    /// <summary> List of their features that clues can be made from. </summary>
    public Knowledge features;
    /// <summary> Where they work. </summary>
    public Room workplace;
    /// <summary> Which room they are currently in. </summary>
    public Room currentLocation;
    /// <summary> What rank they are exec, CEO, etc.</summary>
    public RankEnum rank;

    /// <summary> Name in text used to refer to this in dialogue.  </summary>
    public string displayName;

    public bool isWalking;
    public bool interacted;

    public void Awake()
    {
        features = new Knowledge();
        Debug.Log("workplace is null");
        Debug.Log("currentLocation is null");

        collider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;

        displayName = "Person Smith"; //Generate New Name

        //features.Body.Add(Game.Get.Feature(), Logic.True);
    }

    public void Start()
    {
        Game.S.employees.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var feature in features.ornaments)
        {
            //feature.Key
        }

        AnimationDriver();
    }

    // FixedUpdate is called once per physics step
    void FixedUpdate()
    {

    }

    private void AnimationDriver()
    {

        // hack animation driver
        if (features.body.Key)
        {
            isWalking = false;
            if (Mathf.Abs(rigidBody.velocity.x) > 0.1f)
            {
                isWalking = true;
            }

            features.body.Key.isWalking = isWalking;
        }
    }

    public void Walk(int direction)
    {
        direction = Mathf.Clamp(direction, -1, 1);
        FaceDirection(direction);

        //Add new direction to current velocity
        float newX = rigidBody.velocity.x + direction;

        //Assign new velocity and limit walk speed
        if (newX < Config.MAX_WALK_SPEED && newX > -Config.MAX_WALK_SPEED)
        {
            rigidBody.velocity = rigidBody.velocity.SetX(newX);
        }
        else if (newX >= Config.MAX_WALK_SPEED)
        {
            rigidBody.velocity = rigidBody.velocity.SetX(Config.MAX_WALK_SPEED);
        }
        else if (newX <= -Config.MAX_WALK_SPEED)
        {
            rigidBody.velocity = rigidBody.velocity.SetX(-Config.MAX_WALK_SPEED);
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

    public enum RankEnum
    {
        Employee,
        Manager,
        Executive,
        CEO
    }
}
