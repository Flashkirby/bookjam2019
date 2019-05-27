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
        Debug.Log("Generating Person", person);

        bool stillClashing = false;
        for (int i = 0; i < 6; i++)
        {
            BodyFeature body = InstantiateBodyFeature(person);
            int offset = body.headPixelYOffset;

            InstantiateHairFeature(person, null, offset);
            InstantiateHatFeature(person, null, offset);
            InstantiateFaceFeature(person, null, offset);

            person.GenerateAdditionalFeatures(offset);

            if (Game.S.target == null) break;
            if (person == Game.S.detective) break;
            if (person == Game.S.target) break;

            // CLASSSH!
            if (person.uniqueFeatureId == Game.S.target.uniqueFeatureId)
            {
                stillClashing = true;
                person.features.body = new KeyValuePair<BodyFeature, Logic>();
                person.features.ornaments.Clear();
                foreach (Transform t in person.spriteOrigin.transform)
                {
                    Destroy(t.gameObject);
                }
                Debug.Log("Person." + person.uniqueFeatureId + " matched target, retrying " + i, person);
            }
            else
            {
                stillClashing = false;
                break;
            }
        }
        if (stillClashing)
        {
            Debug.Log("Can't generate non-clashing features, deleting person. ");
            Destroy(person.gameObject);
        }

        person.PostFeatureStart();
    }

    public static Feature InstantiateFeature(Person person, GameObject prefabFeature, int yPixelOffset = 0)
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

    public BodyFeature InstantiateBodyFeature(Person person, GameObject prefabBodyFeature = null)
    {
        if(prefabBodyFeature == null)
        {
            // TODO: RANDOM BODY
            prefabBodyFeature = Game.S.featureBodies[0];
        }

        BodyFeature body = (BodyFeature)InstantiateFeature(person, prefabBodyFeature);
        person.features.SetBody(body, Logic.True);
        return body;
    }

    public static void InstantiateHairFeature(Person person, GameObject prefabFeature = null, int yPixelOffset = 0)
    {
        if (prefabFeature == null)
        {
            // TODO: RANDOM HAIR
            List<GameObject> allHairs = Game.S.featurePoolHair;

            prefabFeature = allHairs[Random.Range(0, allHairs.Count)];
            if (prefabFeature == null) return;
        }

        Feature feature = InstantiateFeature(person, prefabFeature, yPixelOffset);
        person.features.ornaments.Add(feature, Logic.True);
    }

    public static void InstantiateHatFeature(Person person, GameObject prefabFeature = null, int yPixelOffset = 0)
    {
        if (prefabFeature == null)
        {
            prefabFeature = Game.S.featurePoolHat[Random.Range(0, Game.S.featurePoolHat.Count)];
            if (prefabFeature == null) return;
        }

        Feature feature = InstantiateFeature(person, prefabFeature, yPixelOffset);
        person.features.ornaments.Add(feature, Logic.True);
    }

    public static void InstantiateFaceFeature(Person person, GameObject prefabFeature = null, int yPixelOffset = 0)
    {
        if (prefabFeature == null)
        {
            // TODO: RANDOM FACE
            List<GameObject> allFace = new List<GameObject>();
            allFace.AddRange(Game.S.featurePoolGlasses);

            prefabFeature = allFace[Random.Range(0, allFace.Count)];
            if (prefabFeature == null) return;
        }

        Feature feature = InstantiateFeature(person, prefabFeature, yPixelOffset);
        person.features.ornaments.Add(feature, Logic.True);
    }
}
