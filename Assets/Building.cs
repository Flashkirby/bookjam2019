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
            }
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
