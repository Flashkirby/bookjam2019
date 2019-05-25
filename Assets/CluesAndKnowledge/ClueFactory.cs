using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class ClueFactory
{

    enum ClueTypes {
        True,
        False,
        Maybe,
        Useless,
    }

    public static Clue GenerateClueFromEmployeeAndTarget(Person Employee, Person Target)
    {
        // TODO: Logic here please.
        return new Clue();
    }


}

