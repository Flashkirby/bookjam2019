using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    public List<Person> occupants;

    public Detective getFirstDetective(out Detective detective)
    {
        detective = null;
        foreach (var person in occupants)
        {
            if (person is Detective) { detective = (Detective)person; break; }
        }
        return detective;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Person>() != null)
        {
            other.transform.SetParent(transform);
            occupants.Add(other.gameObject.GetComponent<Person>());
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Person>() != null)
        {
            other.transform.SetParent(null);
            occupants.Remove(other.gameObject.GetComponent<Person>());
        }
    }
}
