using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public List<Floor> allFloors;
    public List<Room> allRooms;

    void Awake()
    {
        allFloors = new List<Floor>(GetComponentsInChildren<Floor>());
        allFloors.Sort((left, right) => (int)(left.transform.position.y - right.transform.position.y));

        bool firstFloor = true;
        foreach (Floor floor in allFloors)
        {
            List<Room> floorRooms = new List<Room>(floor.GetComponentsInChildren<Room>());
            floorRooms.Sort((left, right) => (int)(left.transform.position.x - right.transform.position.x));
            floor.building = this;
            floor.rooms = floorRooms;
            allRooms.AddRange(floorRooms);

            foreach(Room room in floorRooms)
            {
                room.building = this;
                room.floor = floor;

                if(firstFloor)
                {
                    room.SetLeftOpen(true);
                    room.SetRightOpen(true);
                }
                else
                {
                    bool left = true;
                    bool right = true;
                    if(floorRooms.IndexOf(room) == 0)
                    { left = false; }
                    else if (floorRooms.IndexOf(room) == floorRooms.Count - 1)
                    { right = false; }

                    room.SetLeftOpen(left);
                    room.SetRightOpen(right);
                }
            }
            firstFloor = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public List<Department> GetDepartments()
    //{
    //    return null;
    //}
}
