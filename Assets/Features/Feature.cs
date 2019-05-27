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
    private Color originalFeatureColour;
    public Color FeatureColour { get { return originalFeatureColour; } set { originalFeatureColour = value; } }

    /// <summary> They're the one wearing the "hat" over there. You can find them by their signature red "hat". </summary>
    public string displayName; 
    /// <summary> They have a "red" hat. Sometimes they wear a "blue" hat. 
    /// TYPES: Red, Yellow, Brown, Black, White, Grey, Green, Blue, Purple, Pink
    /// </summary>
    public string displayColour;

    /// <summary> They "have" a red hat. They "are" bald. </summary>
    public string linkingVerb;
    /// <summary> They are wearing "a" red hat. They are wearing "" green clothes. </summary>
    public string adjective;


    ///// <summary> They are "wearing" a hat. They're usually "wearing" their glasses. </summary>
    //public string verbProgressive;
    ///// <summary> They "wear" a stetson. They sometimes "wear" a hat. </summary>
    //public string verbPerfect;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalFeatureColour = spriteRenderer.color;
    }

    public void Update()
    {
        spriteRenderer.color = originalFeatureColour;

        if (person.interacted)
        {
            Color tmpCol = originalFeatureColour * 0.45f;
            tmpCol.a = 1f;

            spriteRenderer.color = tmpCol;
        }


    }
    public void FixedUpdate()
    {

    }

    /// <summary> Features are equal as long as they share the same Gameobject name (prefab name) </summary>
    public override bool Equals(object other)
    {
        if(other is Feature)
        {
            return gameObject.name == ((Feature)other).name;
        }
        return false;
    }
}
