using System.Collections.Generic;
using System.Linq;
using static ClueFactory;

public class ClueRoom : IClue
{
    ClueTypes clueType;
    Room room;
    Room fakeRoom;
    List<Room> sortedRooms;

    public ClueRoom(ClueTypes clueType, Room room)
    {
        this.clueType = clueType;
        this.room = room;

        if (clueType == ClueTypes.Unsure)
        {
            // Find the pool the object shares
            List<Room> samePool = Game.S.building.allRooms;

            // Copy the pool
            List<Room> samePoolCopy = new List<Room>();
            samePoolCopy.AddRange(samePool);

            //Remove all duplicates from list
            samePoolCopy.RemoveAll(f => f.displayName == room.displayName);

            // Pick from non-duplicated list
            fakeRoom = samePoolCopy.PickRandom();

            //Sort clues so we don't identify the clue by it being first
            List<Room> fakeAndRealRooms = new List<Room> { room, fakeRoom };
            sortedRooms = fakeAndRealRooms.OrderBy(x => x.displayName).ToList();
        }
        else
        {
            sortedRooms = new List<Room> { room };
        }
    }

    public string ToFactBookString()
    {
        string baseString = "";
        if (clueType == ClueTypes.Useless) { return "USELESS"; }
        if (clueType == ClueTypes.Unsure) { baseString = "Room Location: "; }
        if (clueType == ClueTypes.Partial) { baseString = "Room Location: "; }
        if (clueType == ClueTypes.Complete) { baseString = "Room Location: "; }

        string featureString = "";
        string endingString = "";
        if (clueType == ClueTypes.Unsure)
        {
            featureString += sortedRooms[0].displayName;
            endingString = " or " + sortedRooms[1].displayName;
        }
        else if (clueType == ClueTypes.Partial || clueType == ClueTypes.Complete)
        {
            featureString += sortedRooms[0].displayName;
        }

        baseString += featureString;
        baseString += endingString;

        return baseString;
    }

}
