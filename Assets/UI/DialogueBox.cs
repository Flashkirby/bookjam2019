using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    public Text nameLeft;
    public Text nameRight;
    public Text dialogueText;

    // Start is called before the first frame update
    void Start()
    {
        ClearDialogue();
        CloseDialogue();
        //dialogueQueue.Enqueue(new Dialogue("You", false, "Hello."));
        //dialogueQueue.Enqueue(new Dialogue("John Smithy", true, "This isn't the John Smith you're looking for."));
        //dialogueQueue.Enqueue(new Dialogue("You", false, "Oh, okay then."));
        //dialogueQueue.Enqueue(new Dialogue("John Smithy", true, "I am going to fill up this dialogue box with text now.I am going to fill up this dialogue box with text now.I am going to fill up this dialogue box with text now.I am going to fill up this dialogue box with text now.I am going to fill up this dialogue box with text now.I am going to fill up this dialogue box with text now.I am going to fill up this dialogue box with text now.I am going to fill up this dialogue box with text now."));
    }

    // Update is called once per frame
    void Update()
    {
        ProcessDialogue();
    }

    void FixedUpdate()
    {
    }



    void ProcessDialogue()
    {
        if (Game.S.dialogueQueue.Count != 0)
        {
            Dialogue topDialogue = Game.S.dialogueQueue.Peek();

            ClearDialogue();

            if (topDialogue.isSpeakerRight)
            {
                nameRight.text = topDialogue.speaker;
            }
            else
            {
                nameLeft.text = topDialogue.speaker;
            }

            dialogueText.text = topDialogue.dialogueText;
            OpenDialogue();
        }
        else
        {
            CloseDialogue();
        }
    }

    void OpenDialogue()
    {
        gameObject.SetActive(true);
    }

    void CloseDialogue()
    {
        gameObject.SetActive(false);
    }

    void ClearDialogue()
    {
        nameLeft.text = "";
        nameRight.text = "";
        dialogueText.text = "";
    }
}