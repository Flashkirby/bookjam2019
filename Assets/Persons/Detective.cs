using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Config;

public class Detective : Person
{
    public bool inMenu;

    public float anxiety;

    public InteractHighlighter interactHighlighter;

    // Start is called before the first frame update, after being enabled
    public new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public new void Update()
    {
        base.Update();

        controlX = Input.GetAxisRaw("Horizontal");
        controlY = Input.GetAxisRaw("Vertical");

        //Handle Interaction
        Interact();
    }

    // FixedUpdate is called once per physics step
    public new void FixedUpdate()
    {
        base.Start();

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
                InteractWithEmployee();
            }
        }
    }

    private void InteractWithEmployee()
    {
        if (interactHighlighter == null) { Debug.LogError("Have you attached the highlighter to this? "); }
        else
        {
            Person p = interactHighlighter.highlightedPerson;
            if (p == null) { return; }
            if (!(p is Employee)) { return; }
            Employee employee = p as Employee;

            employee.transform.position = employee.transform.position.Sum3(0f, 2f, 0f);
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