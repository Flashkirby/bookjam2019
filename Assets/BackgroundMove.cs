using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public new Transform camera;
    public Vector3 offset;

    public void Awake()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        offset = transform.position - camera.position;
    }

    Vector3 oldPos;
    // Update is called once per frame
    public void FixedUpdate()
    {
        transform.position = transform.position.SetX3((camera.position.x - offset.x) * (1f / transform.position.z));
    }
}
