using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//gets and returns values for reference and UI
public class Values
{
    public static int currDiceRollVal;
    public static int currEnemyDiceRollVal;

    public static int playerOGValue, enemyOGValue; //OG = unmodified dice roll

    public static int evenerCharge = 1, oddenerCharge = 1 , basicCharge = 1, extremesCharge = 1;
    public static int playerAdderCharge = 1, playerSubtracterCharge = 1, enemyAdderCharge = 1, enemySubtracterCharge = 1;
    public static int boss1HP, boss2HP, boss3HP;
    public static int playerHealth = 60;

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


    public static int EvenerCharge { get => evenerCharge; set => evenerCharge = value; }
}
