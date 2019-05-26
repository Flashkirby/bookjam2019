using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Config;

public class Detective : Person
{
    public float controlX;
    public float controlY;

    public bool inMenu;

    public float anxiety;

    // Start is called before the first frame update, after being enabled
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        controlX = Input.GetAxisRaw("Horizontal");
        controlY = Input.GetAxisRaw("Vertical");

        //Handle Interaction
        Interact();
    }

    // FixedUpdate is called once per physics step
    void FixedUpdate()
    {
        if (!inMenu)
        {
            if (Utils.isAxisActive(controlX))
            { Walk(Utils.Dir(controlX)); }
            else
            { StopWalk(); }
        }

        UpdateAnxiety();

    }

    void Interact()
    {
        if (!inMenu)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("Interaction!");
            }
        } else
        {
            if (Game.S.isGameOver)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Game.S.HandleRestart();
                }
            }
            if (Game.S.isStartSplash)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Game.S.HandleStartSplash();
                }
            }
        }




    }

    void UpdateAnxiety()
    {
        if (!inMenu)
        {
            //anxiety += (5.083f * Time.fixedDeltaTime) / 6;
            anxiety += (0.0083f * Time.fixedDeltaTime) / 6;
        }
        if(anxiety >= 1)
        {
            Game.S.GameOver();
        }
    }
}