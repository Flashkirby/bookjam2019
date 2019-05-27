using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeSpacer : MonoBehaviour
{
    public Employee employee;
    public void Awake()
    {
        employee = transform.parent.GetComponent<Employee>();
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (employee == null) return;
        if (other.GetComponent<Employee>() != null)
        {
            Employee e = other.GetComponent<Employee>();
            bool bothStandingStill = employee.rigidBody.velocity.x == 0 && e.rigidBody.velocity.x == 0;
            if (bothStandingStill)
            {
                employee.mingleDir = Utils.Dir(employee.transform.position.x - e.transform.position.x);
                employee.mingleTime = Config.MINGLE_WALK_BUMP_TIME;
            }
        }
    }
}