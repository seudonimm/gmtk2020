using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Values
{
    public static int currDiceRollVal;
    public static int currEnemyDiceRollVal;

    public static int currDiceRollValGetSet
    {
        get
        {
            return currDiceRollVal;
        }

        set
        {
            currDiceRollVal = value;
        }
    }

    public static int currEnemyDiceRollValGetSet
    {
        get
        {
            return currEnemyDiceRollVal;
        }

        set
        {
            currEnemyDiceRollVal = value;
        }
    }

}
