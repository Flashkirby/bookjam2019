using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Config;

public class Building : MonoBehaviour
{
    public List<Floor> allFloors;
    public List<Room> allRooms;
    public List<LiftShaft> allLifts;

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

        allLifts = new List<LiftShaft>(GetComponentsInChildren<LiftShaft>());
        allLifts.Sort((left, right) => (int)(left.transform.position.y - right.transform.position.y));
        foreach (LiftShaft shaft in allLifts)
        {
            // Set the shaft size to match floor count
            shaft.sceneExtendableShaftRenderer.size = shaft.sceneExtendableShaftRenderer.size.SetY(FLOOR_HEIGHT * allFloors.Count);

            // Set the shaft walls to match the shaft size
            shaft.sceneShaftWalls.transform.localScale = shaft.sceneShaftWalls.transform.localScale.SetY3(shaft.sceneExtendableShaftRenderer.size.y);

            shaft.building = this;
        }

        Game.S.PostBuildingAwake(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int Clamp2Floor(int floorNumber)
    {
        return Mathf.Clamp(floorNumber, 0, allFloors.Count - 1);
    }
    public Floor getFloor(int index)
    {
        return allFloors[Clamp2Floor(index)];
    }

    /// <summary>
    /// Get nearby floor or null
    /// </summary>
    public Floor getNearestFloor(float y)
    {
        foreach(var floor in allFloors)
        {
            if((int)(y / 2) == (int)(floor.transform.position.y / 2))
            {
                return floor;
            }
        }
        return null;
    }
    public Floor getNearestFloor(Transform transform)
    { return getNearestFloor(transform.position.y); }
    public int getNearestFloorNumber(float yPosition)
    { return allFloors.IndexOf(getNearestFloor(yPosition)); }

    //public List<Department> GetDepartments()
    //{
    //    return null;
    //}
}
