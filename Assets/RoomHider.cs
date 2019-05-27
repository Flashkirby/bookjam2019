using UnityEngine;
using System.Collections;

public class RoomHider : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Detective>() != null)
        {
            gameObject.SetActive(false);
        }
    }

}
