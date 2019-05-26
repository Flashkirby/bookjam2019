using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Config;

/// <summary>
/// Takes prefabs of feature game objects and adds them to a person's sprite
/// </summary>
public class FeatureFactory : MonoBehaviour
{
    public Person person;

    void Start()
    {
        BodyFeature body = InstantiateBodyFeature();
        int offset = body.headPixelYOffset;

        InstantiateHairFeature(null, offset);
        InstantiateHatFeature(null, offset);
        InstantiateFaceFeature(null, offset);

        person.PostFeatureStart();
    }

    public Feature InstantiateFeature(GameObject prefabFeature, int yPixelOffset = 0)
    {
        if (prefabFeature == null) return null;
        GameObject go = Instantiate(prefabFeature, person.spriteOrigin);
        go.name = prefabFeature.name; // Use the gameobject name to identify the unique-ness of a feature eg. RedTrilby
        Vector3 position = go.transform.position;
        position.y += yPixelOffset * PIXEL;
        go.transform.position = position;
        Feature feature = go.GetComponent<Feature>();
        feature.person = person;
        return feature;
    }

    public BodyFeature InstantiateBodyFeature(GameObject prefabBodyFeature = null)
    {
        if(prefabBodyFeature == null)
        {
            // TODO: RANDOM BODY
            prefabBodyFeature = Game.S.featureBodies[0];
        }

        BodyFeature body = (BodyFeature)InstantiateFeature(prefabBodyFeature);
        person.features.SetBody(body, Logic.True);
        return body;
    }

    public void InstantiateHairFeature(GameObject prefabFeature = null, int yPixelOffset = 0)
    {
        if (prefabFeature == null)
        {
            // TODO: RANDOM HAIR
            List<GameObject> allHairs = new List<GameObject>();
            allHairs.AddRange(Game.S.featurePoolShortHair);
            allHairs.AddRange(Game.S.featurePoolLongHair);

            prefabFeature = allHairs[Random.Range(0, allHairs.Count)];
            if (prefabFeature == null) return;
        }

        Feature feature = InstantiateFeature(prefabFeature, yPixelOffset);
        person.features.ornaments.Add(feature, Logic.True);
    }

    public void InstantiateHatFeature(GameObject prefabFeature = null, int yPixelOffset = 0)
    {
        if (prefabFeature == null)
        {
            prefabFeature = Game.S.featurePoolHat[Random.Range(0, Game.S.featurePoolHat.Count)];
            if (prefabFeature == null) return;
        }

        Feature feature = InstantiateFeature(prefabFeature, yPixelOffset);
        person.features.ornaments.Add(feature, Logic.True);
    }

    public void InstantiateFaceFeature(GameObject prefabFeature = null, int yPixelOffset = 0)
    {
        if (prefabFeature == null)
        {
            // TODO: RANDOM FACE
            List<GameObject> allFace = new List<GameObject>();
            allFace.AddRange(Game.S.featurePoolGlasses);

            prefabFeature = allFace[Random.Range(0, allFace.Count)];
            if (prefabFeature == null) return;
        }

        Feature feature = InstantiateFeature(prefabFeature, yPixelOffset);
        person.features.ornaments.Add(feature, Logic.True);
    }
}
