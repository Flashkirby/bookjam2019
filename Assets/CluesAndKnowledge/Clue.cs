using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ClueFactory;

public class Clue : Knowledge
{

    ClueTypes clueType;
    Feature feature;
    //Clue needs a ClueTypes
    //For Useless, we just need a string, we don't need to add to notebook
    //For Vague, we need a single aspect of a feature
    //For Unsure, we need an either/or
    //For perfect, we describe a feature perfectly
    public Clue(ClueTypes clueType, Feature feature)
    {
        this.clueType = clueType;


        switch (this.clueType)
        {
            case ClueTypes.Useless:

                break;
            case ClueTypes.Vague:
                break;
            case ClueTypes.Unsure:
                break;
            case ClueTypes.Perfect:
                break;
            default:
                break;
        }
    }


    public string ToFactBookString()
    {
        string baseString = "";
        if(clueType == ClueTypes.Useless) { return "USELESS"; };

        if(clueType == ClueTypes.Vague)
        {
            baseString += "They are ";
        }

        if (clueType == ClueTypes.Unsure)
        {
            baseString += "They are either ";
        }

        if (clueType == ClueTypes.Perfect)
        {
            baseString += "They are definitely ";
        }



        return "";
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public string generateDialogue()
    {
        return "";
    }



}
