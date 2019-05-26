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

    /// <summary> They're the one wearing the "hat" over there. You can find them by their signature red "hat". </summary>
    public string displayName; 
    /// <summary> They have a "red" hat. Sometimes they wear a "blue" hat. 
    /// TYPES: Red, Yellow, Brown, Black, White, Grey, Green, Blue, Purple, Pink
    /// </summary>
    public string displayColour;
    /// <summary> They are "wearing" a hat. They're usually "wearing" their glasses. </summary>
    public string verbProgressive;
    /// <summary> They "wear" a stetson. They sometimes "wear" a hat. </summary>
    public string verbPerfect;
    
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
