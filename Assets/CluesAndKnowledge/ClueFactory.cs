using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class ClueFactory
{
    public enum ClueTypes
    {
        Useless, //No information
        Vague, //I think they wear a hat / I think they wear something green
        Unsure, //Either or
        Perfect, // They wear a green bowler hat.
        TESTING,
    }

    private static Dictionary<ClueTypes, int> LowLevelEmployeeClueProbs = new Dictionary<ClueTypes, int>()
    {
        {ClueTypes.Useless, 20},
        {ClueTypes.Vague, 10},
        {ClueTypes.Unsure, 50},
        {ClueTypes.Perfect, 20},
    };
    private static Dictionary<ClueTypes, int> ManagerEmployeeClueProbs = new Dictionary<ClueTypes, int>()
    {
        {ClueTypes.Useless, 20},
        {ClueTypes.Vague, 5},
        {ClueTypes.Unsure, 50},
        {ClueTypes.Perfect, 25},
    };
    private static Dictionary<ClueTypes, int> ExecEmployeeClueProbs = new Dictionary<ClueTypes, int>()
    {
        {ClueTypes.Useless, 30},
        {ClueTypes.Vague, 20},
        {ClueTypes.Unsure, 10},
        {ClueTypes.Perfect, 40},
    };
    private static Dictionary<ClueTypes, int> CEOEmployeeClueProbs = new Dictionary<ClueTypes, int>()
    {
        {ClueTypes.Useless, 40},
        {ClueTypes.Vague, 0},
        {ClueTypes.Unsure, 0},
        {ClueTypes.Perfect, 60},
    };

    //Special Cases:
    //Employees in the same department
    //Manager of the employee


    public static Clue GenerateRandomClueFromEmployeeAndTarget(Person Employee, Person Target)
    {
        // TODO: Logic here please.

        Random random = new System.Random();
        int roll = random.Next(1, 101);

        //Generating random clue type
        ClueTypes randomClueType = ClueTypes.TESTING;
        switch (Employee.rank)
        {
            case Person.Rank.Employee:
                randomClueType = PickClueCategory(roll, LowLevelEmployeeClueProbs);
                break;

            case Person.Rank.Manager:
                randomClueType = PickClueCategory(roll, ManagerEmployeeClueProbs);
                break;

            case Person.Rank.Executive:
                randomClueType = PickClueCategory(roll, ExecEmployeeClueProbs);
                break;

            case Person.Rank.CEO:
                randomClueType = PickClueCategory(roll, CEOEmployeeClueProbs);
                break;

            default:
                break;
        }

        //if (Employee.currentLocation == Target.currentLocation)
        //{
        //
        //}

        return new Clue(randomClueType);
    }

    private static ClueTypes PickClueCategory(int randomRoll, Dictionary<ClueTypes, int> probabilities)
    {
        ClueTypes clue = ClueTypes.TESTING;
        int totalWeight = 0;
        foreach (KeyValuePair<ClueTypes, int> entry in probabilities)
        {
            totalWeight += entry.Value;
        }


        foreach (KeyValuePair<ClueTypes, int> entry in probabilities)
        {
            //If value has 0, then it is skipped.
            if (entry.Value == 0)
            {
                continue;
            }

            //If randomRoll is under entry.Value, then it is under the threshold, and so takes entry.Key
            if (randomRoll <= entry.Value)
            {
                clue = entry.Key;
                break;
            }

            //Otherwise, we reduce the roll by the Value to allow the other weights to potentially be picked.
            randomRoll -= entry.Value;
        }
        return clue;
    }


}

