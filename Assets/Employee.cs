using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Employee : MonoBehaviour
{
    public new BoxCollider2D collider;
    public Rigidbody2D rigidBody;
    public Transform spriteOrigin;

    /// <summary> Name in text used to refer to this in dialogue.  </summary>
    public string DisplayName { get; set; }
    public Knowledge features;

    public void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.freezeRotation = true;

        DisplayName = "Person Smith"; //Generate New Name

        //features.Body.Add(Game.Get.Feature(), Logic.True);
    }

    public void Start()
    {
        Game.S.employees.Add(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // FixedUpdate is called once per physics step
    void FixedUpdate()
    {

    }

    public void Walk(int direction)
    {
        direction = Mathf.Clamp(direction, -1, 1);
        FaceDirection(direction);

        rigidBody.velocity += new Vector2(direction, 0);
    }

    public void FaceDirection(int direction)
    {
        if (direction > 0)
        { spriteOrigin.localScale.Z(1); }
        else if (direction < 0)
        { spriteOrigin.localScale.Z(1); }
    }
}
