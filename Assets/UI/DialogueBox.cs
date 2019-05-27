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

    public void OpenDialogue()
    {
        ClearDialogue();
        Dialogue topDialogue = Game.S.dialogueQueue.Peek();
        if (topDialogue.isSpeakerRight)
        {
            nameRight.text = topDialogue.speaker;
        }
        else
        {
            nameLeft.text = topDialogue.speaker;
        }

        dialogueText.text = topDialogue.dialogueText;

        gameObject.SetActive(true);
    }

    public void CloseDialogue()
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