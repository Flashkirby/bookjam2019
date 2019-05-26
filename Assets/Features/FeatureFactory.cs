using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Takes prefabs of feature game objects and adds them to a person's sprite
/// </summary>
public class FeatureFactory : MonoBehaviour
{
    public Person person;

    void Start()
    {
        BodyFeature body = (BodyFeature)InstantiateFeature(Game.S.featureBodies[0]);
        person.features.SetBody(body, Logic.True);
    }

    public Feature InstantiateFeature(GameObject prefabFeature, int yOffset = 0)
    {
        GameObject go = Instantiate(prefabFeature, person.spriteOrigin);
        go.name = prefabFeature.name; // Use the gameobject name to identify the unique-ness of a feature eg. RedTrilby
        Vector3 position = go.transform.position;
        position.y += yOffset;
        go.transform.position = position;
        Feature feature = go.GetComponent<Feature>();
        feature.person = person;
        return feature;
    }
}
