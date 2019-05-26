using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHighlighter : MonoBehaviour
{
    public SpriteRenderer sceneHighlight;
    public List<Person> people;

    public Person highlightedPerson;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FixedUpdate()
    {
    }

    public void UpdateHighlight()
    {
        if (people.Count > 0)
        {
            people.Sort((left, right) => ((int)(
                (Mathf.Abs(transform.parent.position.x - left.transform.position.x) -
                Mathf.Abs(transform.parent.position.x - right.transform.position.x))
                * 1024f
            )));

            highlightedPerson = people[0];

            sceneHighlight.gameObject.SetActive(true);
            sceneHighlight.transform.SetParent(highlightedPerson.transform, false);
        }
        else
        {
            highlightedPerson = null;

            sceneHighlight.gameObject.SetActive(false);
            sceneHighlight.transform.SetParent(transform, false);
        }
    }

    //public void OnTriggerStay2D(Collider2D other)

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Person>() != null)
        {
            people.Add(other.GetComponent<Person>());
            UpdateHighlight();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Person>() != null)
        {
            people.Remove(other.GetComponent<Person>());
            UpdateHighlight();
        }
    }
}