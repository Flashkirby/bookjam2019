using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Feature : MonoBehaviour
{
    public Person person;
    public SpriteRenderer spriteRenderer;
    /// <summary> Colour of feature </summary>
    public Color featureColor;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
