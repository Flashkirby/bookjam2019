using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureFactory : MonoBehaviour
{
    public Person person;

    public void Start()
    {
        //BodyFeature body = (BodyFeature)InstantiateFeature(Game.S.featureBodies[0]);
        //person.features.SetBody(body, Logic.True);
    }

    public Feature InstantiateFeature(GameObject featureObject)
    {
        GameObject go = Instantiate(featureObject);
        Feature feature = go.GetComponent<Feature>();
        return feature;
    }
}
