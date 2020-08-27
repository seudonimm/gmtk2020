using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//weapon modifier methods
public class Weapons : MonoBehaviour
{
    public void Basic()
    {
        //no modifier
    }

    public void Oddener()
    {
        if (Values.currDiceRollVal % 2 == 0)
        {
            Values.currDiceRollVal -= 1;
        }
        else
        {
            Values.currDiceRollVal += 1;
        }

    }

    public void Evener()
    {
        if (Values.currDiceRollVal % 2 != 0)
        {
            Values.currDiceRollVal -= 1;
        }
        else
        {
            Values.currDiceRollVal += 1;
        }

    }

    public void Extremes()
    {
        if(Values.currDiceRollVal > 3)
        {
            Values.currDiceRollVal = 30;
        }
        if(Values.currDiceRollVal < 4)
        {
            Values.currDiceRollVal = -30;
        }
    }
}
