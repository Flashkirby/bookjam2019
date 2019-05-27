using System.Collections.Generic;
using UnityEngine;

public class Detective : Person
{
    public bool inMenu;

    public float anxiety;
    public float talkCooldown;

    public InteractHighlighter interactHighlighter;

    public bool PreventInteract { get { return inMenu || talkCooldown > 0; } }

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

        // handle cooldown after talking
        if (talkCooldown > 0) talkCooldown -= Time.deltaTime;
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
        else
        {
            StopWalk();
        }

        UpdateAnxiety();

    }

    void Interact()
    {
        if (!inMenu)
        {
            if (Input.GetButtonDown("Fire1") && !PreventInteract)
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

            // Stop and look at listener
            employee.mingleTime = -MINGLE_WAIT_MAX;
            employee.FaceDirection((int)(transform.position.x - employee.transform.position.x));

            // HMMM IS A CLUE
            List<IClue> clues = new List<IClue>();
            clues.Add(ClueFactory.GenerateRandomClueFromEmployee(employee));
            foreach (var clue in clues)
            { Game.S.factBook.addClue(clue.ToFactBookString()); }

            DialogueFactory.StartDialogue(clues, employee, this);
        }
    }

    void UpdateAnxiety()
    {
        if (!inMenu)
        {
            //anxiety += (5.083f * Time.fixedDeltaTime) / 6;
            anxiety += (0.0083f * Time.fixedDeltaTime) / 6;
        }
        if (anxiety >= 1)
        {
            Game.S.GameOver();
        }
    }
}