using System.Collections.Generic;
using System.Linq;
using static ClueFactory;
public class ClueFloor : IClue
{

    ClueTypes clueType;
    Floor floor;
    Floor fakeFloor;
    List<Floor> sortedFloors;

    public ClueFloor(ClueTypes clueType, Floor floor)
    {
        this.clueType = clueType;
        this.floor = floor;

        if (clueType == ClueTypes.Unsure)
        {
            // Find the pool the object shares
            List<Floor> samePool = Game.S.building.allFloors;

            // Copy the pool
            List<Floor> samePoolCopy = new List<Floor>();
            samePoolCopy.AddRange(samePool);

            //Remove all duplicates from list
            samePoolCopy.RemoveAll(f => f.displayName == floor.displayName);

            // Pick from non-duplicated list
            fakeFloor = samePoolCopy.PickRandom();

            //Sort clues so we don't identify the clue by it being first
            List<Floor> fakeAndRealFloors = new List<Floor> { floor, fakeFloor };
            sortedFloors = fakeAndRealFloors.OrderBy(x => x.displayName).ToList();
        }
        else
        {
            sortedFloors = new List<Floor> { floor };
        }
    }

    public string generateDialogue()
    {
        string baseString = "";
        if (clueType == ClueTypes.Useless) { return "Sorry, I don't know where that person is."; }
        if (clueType == ClueTypes.Unsure) { baseString = "Unsure: "; }
        if (clueType == ClueTypes.Partial) { baseString = "Partial Clue: "; }
        if (clueType == ClueTypes.Complete) { baseString = "Complete Clue: "; }

        string featureString = "";
        string endingString = "";
        if (clueType == ClueTypes.Unsure)
        {
            featureString += sortedFloors[0].displayName;
            endingString = " or " + sortedFloors[1].displayName;
        }
        else if (clueType == ClueTypes.Partial || clueType == ClueTypes.Complete)
        {
            featureString += sortedFloors[0].displayName;
        }

        baseString += featureString;
        baseString += endingString;

        return baseString;
    }

    public string ToFactBookString()
    {
        string baseString = "";
        if (clueType == ClueTypes.Useless) { return "USELESS"; }
        if (clueType == ClueTypes.Unsure) { baseString = "Floor Location: "; }
        if (clueType == ClueTypes.Partial) { baseString = "Floor Location: "; }
        if (clueType == ClueTypes.Complete) { baseString = "Floor Location: "; }

        string featureString = "";
        string endingString = "";
        if (clueType == ClueTypes.Unsure)
        {
            featureString += sortedFloors[0].displayName;
            endingString = " or " + sortedFloors[1].displayName;
        }
        else if (clueType == ClueTypes.Partial || clueType == ClueTypes.Complete)
        {
            featureString += sortedFloors[0].displayName;
        }

        baseString += featureString;
        baseString += endingString;

        return baseString;
    }
}
