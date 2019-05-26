using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Feature : MonoBehaviour
{
    public Person person;
    public SpriteRenderer spriteRenderer;
    /// <summary> Colour of feature </summary>
    private Color featureColor;

    public string displayColour;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        featureColor = spriteRenderer.color;
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
