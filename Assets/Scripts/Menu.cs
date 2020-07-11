using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] List<Button> menuOptions;
    [SerializeField] List<GameObject> currentWeapons;
    [SerializeField] List<GameObject> currentItems;

    [SerializeField] int diceVal;

    [SerializeField] int playerHealth;
    [SerializeField] Text playerHealthText;

    [SerializeField] MenuStates menuStates, prevMenuState;
    [SerializeField] bool b0Pressed, b1Pressed, b2Pressed, b3Pressed;
    [SerializeField] GameObject selectedWeapon;

    [SerializeField] GameObject currEnemy;
    [SerializeField] int enemyHealth;
    [SerializeField] Text enemyHealthText;

    public Weapons weapons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MenuStateMachine()
    {
        switch (menuStates)
        {
            case MenuStates.Begin:

                menuOptions[0].GetComponentInChildren<Text>().text = "Attack";
                if (b0Pressed)
                {
                    b0Pressed = false;
                    menuStates = MenuStates.Atk;
                }

                menuOptions[1].GetComponentInChildren<Text>().text = "Defend";
                if (b1Pressed)
                {
                    b1Pressed = false;
                    menuStates = MenuStates.Def;
                }

                menuOptions[2].GetComponentInChildren<Text>().text = "Item";
                if (b2Pressed)
                {
                    b2Pressed = false;
                    menuStates = MenuStates.Items;
                }

                menuOptions[3].GetComponentInChildren<Text>().text = "Change Equipment";
                if (b3Pressed)
                {
                    b3Pressed = false;
                    menuStates = MenuStates.Equip;
                }


                break;

            case MenuStates.Atk:

                menuOptions[0].GetComponentInChildren<Text>().text = currentWeapons[0].name;
                if (b0Pressed)
                {
                    b0Pressed = false;
                    selectedWeapon = currentWeapons[0];
                    menuStates = MenuStates.RollDice;
                }

                menuOptions[1].GetComponentInChildren<Text>().text = currentWeapons[1].name;
                if (b1Pressed)
                {
                    b1Pressed = false;
                    selectedWeapon = currentWeapons[1];
                    menuStates = MenuStates.RollDice;
                }

                menuOptions[2].GetComponentInChildren<Text>().text = currentWeapons[2].name;
                if (b2Pressed)
                {
                    b2Pressed = false;
                    selectedWeapon = currentWeapons[2];
                    menuStates = MenuStates.RollDice;
                }

                menuOptions[3].GetComponentInChildren<Text>().text = currentWeapons[3].name;
                if (b3Pressed)
                {
                    b3Pressed = false;
                    selectedWeapon = currentWeapons[3];
                    menuStates = MenuStates.RollDice;
                }


                break;

            case MenuStates.Def:



                break;

            case MenuStates.Items:

                menuOptions[0].GetComponentInChildren<Text>().text = currentItems[0].name;
                menuOptions[1].GetComponentInChildren<Text>().text = currentItems[1].name;
                menuOptions[2].GetComponentInChildren<Text>().text = currentItems[2].name;
                menuOptions[3].GetComponentInChildren<Text>().text = currentItems[3].name;

                break;

            case MenuStates.RollDice:
                RollingDice();

                if(prevMenuState == MenuStates.Atk)
                {
                    menuStates = MenuStates.DiceModifier;
                }


                break;

            case MenuStates.DiceModifier:

                if (selectedWeapon.name == "Oddener")
                {
                    weapons.Oddener();
                    menuStates = MenuStates.ExecuteActions;
                }
                if(selectedWeapon.name == "Evener")
                {
                    weapons.Evener();
                    menuStates = MenuStates.ExecuteActions;
                }

                break;

            case MenuStates.ExecuteActions:



                break;
        }


    }
    
    public void Button0()
    {
        switch (menuStates)
        {
            case MenuStates.Begin:

                menuStates = MenuStates.Atk;

                break;

            case MenuStates.Atk:

                menuStates = MenuStates.RollDice;

                break;
        }
    }
    public void Button1()
    {
        menuStates = MenuStates.Atk;
    }
    public void Button2()
    {
        menuStates = MenuStates.Atk;
    }
    public void Button3()
    {
        menuStates = MenuStates.Atk;
    }

    public void RollingDice()
    {
        diceVal = Random.Range(1, 7);

        Values.currDiceRollVal = diceVal;
    }
}

public enum MenuStates
{
    Begin,
    Atk,
    Def,
    Equip,
    Items,
    Item1,
    Item2,
    Item3,
    Item4,
    Equip1,
    Equip2,
    Equip3,
    Equip4,
    RollDice,
    DiceModifier,
    ExecuteActions
}
