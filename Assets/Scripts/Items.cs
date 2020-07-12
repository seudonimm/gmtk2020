using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
