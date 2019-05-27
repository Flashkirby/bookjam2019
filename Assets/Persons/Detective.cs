using System.Collections.Generic;
using UnityEngine;
using static Config;

public class Detective : Person
{
    public bool inMenu;

    public float anxiety;
    public float talkCooldown;
    public float interactHoldTime;

    public ActionIcon sceneActionIcon;

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
        // hide icon if unavailble
        if (sceneActionIcon != null)
        { sceneActionIcon.hideIcon = PreventInteract; }

        if (!inMenu)
        {
            // Before max hold, after having pressed
            if (interactHoldTime < CONTROL_MIN_HOLD + CONTROL_MAX_HOLD && interactHoldTime != 0f)
            {
                // On early release
                if (Input.GetButtonUp("Fire1"))
                {
                    Debug.Log("interact release early");

                    // Release within min time, this is ok
                    if (interactHoldTime <= CONTROL_MIN_HOLD)
                    {
                        InteractWithEmployee();
                        SetInteractCooldown();
                    }
                    ResetInteract();
                }

                // Past the minimum threshold, start the slider
                if(interactHoldTime > CONTROL_MIN_HOLD)
                {
                    if (sceneActionIcon != null)
                    { sceneActionIcon.sliderShow = (interactHoldTime - CONTROL_MIN_HOLD) / CONTROL_MAX_HOLD; }
                    else { Debug.LogWarning("No action icon attached"); }
                }
            }
            else // on max hold
            {
                Debug.Log("interact hold max");

                if (interactHoldTime >= CONTROL_MAX_HOLD)
                {
                    IdentifyEmployee();
                    SetInteractCooldown();
                }
                ResetInteract();
            }



            // Press, increase timer as long as we're allowed to
            if (Input.GetButton("Fire1"))
            {
                if (!PreventInteract && interactHighlighter.highlightedPerson != null)
                {
                    interactHoldTime += Time.deltaTime;
                }
                else
                {
                    ResetInteract();
                }
            }
        }
        else
        {
            ResetInteract();
        }
    }

    public void ResetInteract()
    {
        interactHoldTime = 0;

        if (sceneActionIcon != null)
        {
            sceneActionIcon.sliderShow = 0f;
            sceneActionIcon.altIcon = false;
        }
    }
    public void SetInteractCooldown()
    {
        talkCooldown = INTERACT_COOLDOWN;
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

    private void IdentifyEmployee()
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

            employee.transform.position = employee.transform.position.Sum3(0f, 1f, 0f);

            //DialogueFactory.StartDialogue(clues, employee, this);
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