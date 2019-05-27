using UnityEngine;
using System.Collections;

public class RoomHider : MonoBehaviour
{
    public void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Detective>() != null)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

}
