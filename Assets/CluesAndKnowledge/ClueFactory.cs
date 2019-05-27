using System.Collections.Generic;
using UnityEngine;
using static Utils;

public static class ClueFactory
{
    public enum ClueTypes
    {
        Useless, //No information
        Partial, //I think they wear a hat / I think they wear something green
        Unsure, //Either or
        Complete, // They wear a green bowler hat.
    }

    private static Dictionary<ClueTypes, int> LowLevelEmployeeClueProbs = new Dictionary<ClueTypes, int>()
    {
        {ClueTypes.Useless, 20},
        {ClueTypes.Partial, 10},
        {ClueTypes.Unsure, 50},
        {ClueTypes.Complete, 20},
    };
    private static Dictionary<ClueTypes, int> ManagerEmployeeClueProbs = new Dictionary<ClueTypes, int>()
    {
        {ClueTypes.Useless, 20},
        {ClueTypes.Partial, 5},
        {ClueTypes.Unsure, 50},
        {ClueTypes.Complete, 25},
    };
    private static Dictionary<ClueTypes, int> ExecEmployeeClueProbs = new Dictionary<ClueTypes, int>()
    {
        {ClueTypes.Useless, 30},
        {ClueTypes.Partial, 20},
        {ClueTypes.Unsure, 10},
        {ClueTypes.Complete, 40},
    };
    private static Dictionary<ClueTypes, int> CEOEmployeeClueProbs = new Dictionary<ClueTypes, int>()
    {
        {ClueTypes.Useless, 40},
        {ClueTypes.Partial, 0},
        {ClueTypes.Unsure, 0},
        {ClueTypes.Complete, 60},
    };

    //Special Cases:
    //Employees in the same department
    //Manager of the employee


    public static IClue GenerateRandomClueFromEmployee(Employee employee)
    {
        int roll = rnd.Next(1, 101);

        //Generating random clue type
        ClueTypes randomClueType = ClueTypes.Useless;
        switch (employee.rank)
        {
            case Person.RankEnum.Employee:
                randomClueType = PickClueCategory(roll, LowLevelEmployeeClueProbs);
                break;

            case Person.RankEnum.Manager:
                randomClueType = PickClueCategory(roll, ManagerEmployeeClueProbs);
                break;

            case Person.RankEnum.Executive:
                randomClueType = PickClueCategory(roll, ExecEmployeeClueProbs);
                break;

            case Person.RankEnum.CEO:
                randomClueType = PickClueCategory(roll, CEOEmployeeClueProbs);
                break;

            default:
                break;
        }

        //if (Employee.currentLocation == Target.currentLocation)
        //{
        //
        //}

        IClue generatedClue = null;
        int clueCategoryRoll = rnd.Next(1, 101);
        if(clueCategoryRoll <= 70)
        {
            var features = Game.S.target.features.getTrueOrnaments();
            Feature targetFeature = features.PickRandom();
            generatedClue = new ClueFeature(randomClueType, targetFeature);
        }
        else if (clueCategoryRoll <= 90)
        {
            var floor = Game.S.target.CurrentLocation.floor;
            generatedClue = new ClueFloor(randomClueType, floor);
        }
        else
        {
            var room = Game.S.target.CurrentLocation;
            generatedClue = new ClueRoom(randomClueType, room);
        }

        return generatedClue;
    }

    private static ClueTypes PickClueCategory(int randomRoll, Dictionary<ClueTypes, int> probabilities)
    {
        ClueTypes clue = ClueTypes.Useless;
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

