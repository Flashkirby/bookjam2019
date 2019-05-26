using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureFactory : MonoBehaviour
{
    public Person person;

    void Start()
    {
        BodyFeature body = (BodyFeature)InstantiateFeature(Game.S.featureBodies[0]);
        person.features.SetBody(body, Logic.True);
    }

    public Feature InstantiateFeature(GameObject featureObject, int yOffset = 0)
    {
        GameObject go = Instantiate(featureObject, person.spriteOrigin);
        go.name = featureObject.name; // Use the gameobject name to identify the unique-ness of a feature eg. RedTrilby
        Vector3 position = go.transform.position;
        position.y += yOffset;
        go.transform.position = position;
        Feature feature = go.GetComponent<Feature>();
        feature.person = person;
        return feature;
    }
}
