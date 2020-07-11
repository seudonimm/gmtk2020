using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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
        if(Values.currDiceRollVal == 6)
        {
            Values.currDiceRollVal = 99;
        }
        if(Values.currDiceRollVal == 1)
        {
            Values.currDiceRollVal = 0;
        }
    }
}
