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
        if (other.GetComponent<Employee>() != null)
        {
            Employee e = other.GetComponent<Employee>();
            if (employee != null)
            {
                employee.controlX += employee.transform.position.x - e.transform.position.x;
            }
        }
    }
}
