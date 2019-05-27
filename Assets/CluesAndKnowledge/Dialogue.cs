using UnityEngine;
using System.Collections;

public class Dialogue
{
    public string speaker;
    public bool isSpeakerRight;
    public string dialogueText;

    public Dialogue(string speaker, bool isSpeakerRight, string dialogue)
    {
        this.speaker = speaker;
        this.isSpeakerRight = isSpeakerRight;
        this.dialogueText = dialogue;
    }
}
