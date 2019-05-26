using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Config;

public class FactBook : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CloseFactBook();
    }

    void CloseFactBook()
    {
        if (Input.GetButtonDown("Fire1") || Utils.isAxisActive(Input.GetAxisRaw("Horizontal")))
        {
            gameObject.SetActive(false);
            Game.S.detective.inMenu = false; 
        };
    }

}
