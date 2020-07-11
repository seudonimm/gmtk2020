using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    //[SerializeField] GameObject dice;
    [SerializeField] int diceVal;

    [SerializeField] Text diceText;

    // Start is called before the first frame update
    void Start()
    {
        diceVal = Random.Range(1, 7);

        diceText.text = diceVal.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
