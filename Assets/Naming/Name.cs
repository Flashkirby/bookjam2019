using UnityEngine;
using System.Collections;
using static Utils;
using System.Collections.Generic;

public class Name
{

    List<string> randomNames = new List<string>
    {
        "John Smith",
        "Jane Smith",
        "Tom Thomas",
    };


    public string getRandomName()
    {
        return randomNames.PickRandom();
    }


}
