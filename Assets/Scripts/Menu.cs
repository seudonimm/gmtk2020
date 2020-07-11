using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] List<Button> menuOptions;
    [SerializeField] List<GameObject> currentWeapons;
    [SerializeField] List<GameObject> currentItems;

    [SerializeField] int playerDiceVal, enemyDiceVal;

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
        playerHealthText.text = playerHealth.ToString();
        enemyHealthText.text = enemyHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        MenuStateMachine();
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
                    prevMenuState = menuStates;
                    menuStates = MenuStates.Atk;
                }

                menuOptions[1].GetComponentInChildren<Text>().text = "Defend";
                if (b1Pressed)
                {
                    b1Pressed = false;
                    prevMenuState = menuStates;

                    menuStates = MenuStates.Def;
                }

                menuOptions[2].GetComponentInChildren<Text>().text = "Item";
                if (b2Pressed)
                {
                    b2Pressed = false;
                    prevMenuState = menuStates;
                    menuStates = MenuStates.Items;
                }

                menuOptions[3].GetComponentInChildren<Text>().text = "Change Equipment";
                if (b3Pressed)
                {
                    b3Pressed = false;
                    prevMenuState = menuStates;
                    menuStates = MenuStates.Equip;
                }


                break;

            case MenuStates.Atk:

                menuOptions[0].GetComponentInChildren<Text>().text = currentWeapons[0].name;
                if (b0Pressed)
                {
                    b0Pressed = false;
                    selectedWeapon = currentWeapons[0];
                    prevMenuState = menuStates;
                    menuStates = MenuStates.RollDice;
                }

                menuOptions[1].GetComponentInChildren<Text>().text = currentWeapons[1].name;
                if (b1Pressed)
                {
                    b1Pressed = false;
                    selectedWeapon = currentWeapons[1];
                    prevMenuState = menuStates;
                    menuStates = MenuStates.RollDice;
                }

                menuOptions[2].GetComponentInChildren<Text>().text = currentWeapons[2].name;
                if (b2Pressed)
                {
                    b2Pressed = false;
                    selectedWeapon = currentWeapons[2];
                    prevMenuState = menuStates;
                    menuStates = MenuStates.RollDice;
                }

                menuOptions[3].GetComponentInChildren<Text>().text = currentWeapons[3].name;
                if (b3Pressed)
                {
                    b3Pressed = false;
                    selectedWeapon = currentWeapons[3];
                    prevMenuState = menuStates;
                    menuStates = MenuStates.RollDice;
                }


                break;

            case MenuStates.Def:

                RollingDice();

                if(Values.currDiceRollVal < Values.currEnemyDiceRollVal)
                {
                    playerHealth -= (Values.currEnemyDiceRollVal - Values.currDiceRollVal);
                }
                else if(Values.currDiceRollVal > Values.currEnemyDiceRollVal)
                {
                    playerHealth += (Values.currDiceRollVal - Values.currEnemyDiceRollVal);
                }
                else if(Values.currDiceRollVal == Values.currEnemyDiceRollVal)
                {
                    //nothing
                }

                if(playerHealth <= 0)
                {
                    //Restart Game
                }

                menuStates = MenuStates.Begin;

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
                    menuStates = MenuStates.DamageEnemy;
                }
                if(selectedWeapon.name == "Evener")
                {
                    weapons.Evener();
                    menuStates = MenuStates.DamageEnemy;
                }
                if (selectedWeapon.name == "Basic")
                {
                    weapons.Basic();
                    menuStates = MenuStates.DamageEnemy;
                }
                if (selectedWeapon.name == "Extremes")
                {
                    weapons.Extremes();
                    menuStates = MenuStates.DamageEnemy;
                }

                break;

            case MenuStates.DamageEnemy:

                enemyHealth -= Values.currDiceRollVal;
                enemyHealthText.text = enemyHealth.ToString();

                if(enemyHealth <= 0)
                {
                    Destroy(currEnemy);

                }

                menuStates = MenuStates.DamagePlayer;

                break;

            case MenuStates.DamagePlayer:

                playerHealth -= Values.currEnemyDiceRollVal;
                playerHealthText.text = playerHealth.ToString();

                if(playerHealth <= 0)
                {
                    //Restart Game
                }

                menuStates = MenuStates.Begin;

                break;
        }


    }
    
    public void Button0()
    {
        b0Pressed = true;
    }
    public void Button1()
    {
        b1Pressed = true;
    }
    public void Button2()
    {
        b2Pressed = true;
    }
    public void Button3()
    {
        b3Pressed = true;
    }

    public void RollingDice()
    {
        playerDiceVal = Random.Range(1, 7);

        Values.currDiceRollVal = playerDiceVal;
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
    ExecuteActions,
    DamageEnemy,
    DamagePlayer,
    PlayerVictory
}
