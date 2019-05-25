using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Feature : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    /// <summary> Colour of feature </summary>
    public Color featureColor;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Start()
    {

    }
    void Update()
    {
        spriteRenderer.color = featureColor;
        // TODO: Highlight
        // TODO: Lowlight (desaturate)


    }
    void FixedUpdate()
    {

    }
}
