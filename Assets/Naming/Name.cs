using System.Collections.Generic;
using static Utils;

public class Name
{
    public string GenerateRandomName()
    {
        int roll = rnd.Next(1, 101);

        if (roll <= 95)
        {
            return firstNames.PickRandom() + " " + lastNames.PickRandom();
        }
        else
        {
            return fullNames.PickRandom();
        }
    }


    List<string> fullNames = new List<string>
    {
        "John Smith",
        "Jane Smith",
    };

    List<string> firstNames = new List<string>
    {
        "Mirian",        "Marlyn",        "Bernardo",        "Jacquelynn",        "Geri",        "Idalia",        "Jacques",        "Anya",        "Earline",        "Sol",        "Drucilla",        "Elisabeth",        "Madalene",        "Janella",        "Hunter",        "Minh",        "Alfredo",        "Bambi",        "Zulema",        "Svetlana",        "Johnetta",        "Alyssa",        "Eufemia",        "Shawnta",        "Marita",        "Hershel",        "Craig",        "Florida",        "Lakeshia",        "Kristan",        "Mariella",        "Natosha",        "Denny",        "Maryln",        "Starla",        "Patti",        "Cherri",        "Loren",        "Jarvis",        "Anjanette",        "Ward",        "Tiera",        "Emiko",        "Leon",        "Loraine",        "Sandi",        "Clare",        "Mickie",        "Jimmie",        "Eloy",
        "John"
    };

    List<string> lastNames = new List<string>
    {
        "Mccarthy",        "Cunningham",        "Townsend",        "Donnelly",        "Bush",        "Crawford",        "Walters",        "Potter",        "Cox",        "Farmer",        "Wolf",        "Whelan",        "Morgan",        "Stevenson",        "Richardson",        "Steele",        "Spence",        "Jensen",        "Garner",        "Smith",        "Adams",        "Reese",        "Barnett",        "Thompson",        "Zhang",        "Jenkins",        "Mathis",        "Allen",        "Conner",        "Thomas",        "Jackson",        "Moore",        "Mendoza",        "Daniels",        "Ford",        "Carroll",        "Barker",        "Kay",        "Lewis",        "Torres",        "Baker",        "Hodges",        "Osborne",        "Reyes",        "Leon",        "Chapman",        "Griffin",        "Cameron",
        "Smith",
    };
}
