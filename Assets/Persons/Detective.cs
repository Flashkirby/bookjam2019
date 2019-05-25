using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detective : Person
{
    public float controlX;
    public float controlY;

    // Start is called before the first frame update, after being enabled
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        controlX = Input.GetAxisRaw("Horizontal");
        controlY = Input.GetAxisRaw("Vertical");
    }

    // FixedUpdate is called once per physics step
    void FixedUpdate()
    {
        if (controlX > 0.3f || controlX < -0.3f)
        { Walk(Utils.Dir(controlX)); }
        else
        { StopWalk(); }

    }
}