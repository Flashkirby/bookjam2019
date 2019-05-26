using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Config;

[RequireComponent(typeof(SpriteRenderer))]
public class LiftShaft : MonoBehaviour
{
    public Building building;

    public SpriteRenderer sceneExtendableShaftRenderer;
    public GameObject sceneShaftWalls;
    public Lift lift;
    public int currentFloor = 0;
    public int targetFloor = 0;
    public float goalY;

    // Update is called once per frame
    public void Update()
    {
        Detective detective;
        if (lift.getFirstDetective(out detective) != null)
        {
            if (Utils.isAxisActive(detective.controlY))
            {
                if(detective.controlY > 0)
                {
                    targetFloor = currentFloor + 1;
                }
                else
                {
                    targetFloor = currentFloor - 1;
                }
            }
        }
    }

    public void FixedUpdate()
    {
        if (currentFloor != targetFloor)
        {
            Floor next = building.getFloor(targetFloor);
            goalY = next.transform.position.y;

            // Already on the top/bottom floor, no move
            if (goalY == lift.transform.position.y)
            {
                targetFloor = building.Clamp2Floor(targetFloor);
            }

            sceneShaftWalls.SetActive(true);
        }
        else
        {
            sceneShaftWalls.SetActive(false);
        }

        if (lift.transform.position.y != goalY)
        {
            lift.transform.position = lift.transform.position.SetY3(
                Mathf.Clamp(
                    goalY, 
                    lift.transform.position.y - LIFT_SPEED * Time.fixedDeltaTime, 
                    lift.transform.position.y + LIFT_SPEED * Time.fixedDeltaTime)
            );
            if(lift.transform.position.y == goalY)
            {
                currentFloor = building.Clamp2Floor(targetFloor);
                ArrivedAtFloor(building.getFloor(currentFloor));
            }
        }
    }

    public void ArrivedAtFloor(Floor floor)
    {
        Debug.Log(floor.displayName);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if( other.gameObject.tag == "Player")
        {
            targetFloor = building.getNearestFloorNumber(other.transform.position.y);
        }
    }
}
