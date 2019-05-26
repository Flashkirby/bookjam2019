using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Features are pieces of distinct visual information about a person. See factory class to see how they are added.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class Feature : MonoBehaviour
{
    public Person person;
    public SpriteRenderer spriteRenderer;
    /// <summary> Colour of feature </summary>
    private Color featureColour;
    public Color FeatureColour { get { return featureColour; } }

    public string displayColour;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        featureColour = spriteRenderer.color;
    }

    void Update()
    {
        spriteRenderer.color = featureColour;
        // TODO: Highlight
        // TODO: Lowlight (desaturate)


    }
    void FixedUpdate()
    {

    }
}
