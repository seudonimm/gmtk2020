using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//methods for items that alter dice rolls
public class Items : MonoBehaviour
{
    public void AdderPlayer()
    {
        Values.currDiceRollVal += 2;
    }
    public void SubtracterPlayer()
    {
        Values.currDiceRollVal -= 2;

    }
    public void AdderEnemy()
    {
        Values.currEnemyDiceRollVal += 2;
    }
    public void SubtracterEnemy()
    {
        Values.currEnemyDiceRollVal -= 2;

    }
}
