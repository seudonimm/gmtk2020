using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] List<Button> menuOptions;
    [SerializeField] List<GameObject> currentWeapons;
    [SerializeField] List<GameObject> currentItems;

    [SerializeField] Text atkMenuHelp,beginMenuHelp, menuGuide, itemDesc;

    [SerializeField] int playerDiceVal, enemyDiceVal, playerDiceOGVal, enemyDiceOGVal;
    [SerializeField] Text playerDiceValText, enemyDiceValText, playerOGValText, enemyOGValText;

    [SerializeField] int playerHealth;
    [SerializeField] Text playerHealthText;

    [SerializeField] MenuStates menuStates, prevMenuState;
    [SerializeField] bool b0Pressed, b1Pressed, b2Pressed, b3Pressed;
    [SerializeField] GameObject selectedWeapon, selectedItem;

    [SerializeField] GameObject currEnemy;
    [SerializeField] int enemyHealth;
    [SerializeField] Text enemyHealthText;

    [SerializeField] bool bossBattle1, bossBattle2, bossBattle3;

    public AudioSource hitSound;

    public Map map;
    public Weapons weapons;
    public Items items;
    // Start is called before the first frame update
    void Start()
    {
        playerHealthText.text = playerHealth.ToString();
        enemyHealthText.text = enemyHealth.ToString();

        atkMenuHelp.enabled = false;
        menuGuide.enabled = false;
        itemDesc.enabled = false;

        playerHealth = Values.playerHealth;

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
                menuGuide.enabled = true;
                enemyHealthText.text = enemyHealth.ToString();

                menuOptions[0].GetComponentInChildren<Text>().text = "Attack";
                if (b0Pressed)
                {
                    b0Pressed = false;
                    prevMenuState = menuStates;
                    beginMenuHelp.text = "";
                    menuGuide.enabled = false;

                    menuStates = MenuStates.Atk;
                }

                menuOptions[1].GetComponentInChildren<Text>().text = "Defend";
                if (b1Pressed)
                {
                    b1Pressed = false;
                    prevMenuState = menuStates;
                    beginMenuHelp.text = "";
                    menuGuide.enabled = false;

                    menuStates = MenuStates.Def;
                }

                menuOptions[2].GetComponentInChildren<Text>().text = "Item";
                if (b2Pressed)
                {
                    b2Pressed = false;
                    prevMenuState = menuStates;
                    if (Values.playerAdderCharge != 0 || Values.playerSubtracterCharge != 0 || Values.enemyAdderCharge != 0 || Values.enemySubtracterCharge != 0)
                    {
                        menuGuide.enabled = false;

                        menuStates = MenuStates.Items;
                    }
                    else
                    {
                        beginMenuHelp.text = "NO ITEM CHARGES";
                    }
                }

                menuOptions[3].GetComponentInChildren<Text>().text = "";
                if (b3Pressed)
                {
                    b3Pressed = false;
                    beginMenuHelp.text = "";

                }


                break;

            case MenuStates.Atk:

                atkMenuHelp.enabled = true;
                menuOptions[0].GetComponentInChildren<Text>().text = currentWeapons[0].name + "   " + Values.evenerCharge;
                if (b0Pressed)
                {
                    b0Pressed = false;
                    selectedWeapon = currentWeapons[0];
                    prevMenuState = menuStates;
                    atkMenuHelp.enabled = false;

                    menuStates = MenuStates.RollDice;
                }

                menuOptions[1].GetComponentInChildren<Text>().text = currentWeapons[1].name + "   " + Values.oddenerCharge;
                if (b1Pressed)
                {
                    b1Pressed = false;
                    selectedWeapon = currentWeapons[1];
                    prevMenuState = menuStates;
                    atkMenuHelp.enabled = false;

                    menuStates = MenuStates.RollDice;
                }

                menuOptions[2].GetComponentInChildren<Text>().text = currentWeapons[2].name + "   " + "infinite";
                if (b2Pressed)
                {
                    b2Pressed = false;
                    selectedWeapon = currentWeapons[2];
                    prevMenuState = menuStates;
                    atkMenuHelp.enabled = false;

                    menuStates = MenuStates.RollDice;
                }

                menuOptions[3].GetComponentInChildren<Text>().text = currentWeapons[3].name + "   " + Values.extremesCharge;
                if (b3Pressed)
                {
                    b3Pressed = false;
                    selectedWeapon = currentWeapons[3];
                    prevMenuState = menuStates;
                    atkMenuHelp.enabled = false;

                    menuStates = MenuStates.RollDice;
                }


                break;

            case MenuStates.Def:

                RollingDice();
                if (selectedItem != null)
                {
                    if (selectedItem.name == "Player Adder")
                    {
                        items.AdderPlayer();
                        selectedItem = null;

                    }
                    if (selectedItem.name == "Player Subtracter")
                    {
                        items.SubtracterPlayer();
                        selectedItem = null;

                    }
                    if (selectedItem.name == "Enemy Adder")
                    {
                        items.AdderEnemy();
                        selectedItem = null;

                    }
                    if (selectedItem.name == "Enemy Subtracter")
                    {
                        items.SubtracterEnemy();
                        selectedItem = null;

                    }
                }

                if (Values.currDiceRollVal < Values.currEnemyDiceRollVal)
                {
                    playerHealth -= (Values.currEnemyDiceRollVal - Values.currDiceRollVal);
                    Values.playerHealth = playerHealth;
                }
                else if(Values.currDiceRollVal > Values.currEnemyDiceRollVal)
                {
                    playerHealth += (Values.currDiceRollVal - Values.currEnemyDiceRollVal);
                    Values.playerHealth = playerHealth;

                }
                else if(Values.currDiceRollVal == Values.currEnemyDiceRollVal)
                {
                    //nothing
                }

                if(playerHealth <= 0)
                {
                    //Restart Game
                }
                playerHealthText.text = playerHealth.ToString();
                Values.playerHealth = playerHealth;

                menuStates = MenuStates.Begin;

                playerDiceVal = Values.currDiceRollVal;
                enemyDiceVal = Values.currEnemyDiceRollVal;
                playerDiceValText.text = playerDiceVal.ToString();
                enemyDiceValText.text = enemyDiceVal.ToString();

                break;

            case MenuStates.Items:
                prevMenuState = menuStates;

                itemDesc.enabled = true;

                menuOptions[0].GetComponentInChildren<Text>().text = currentItems[0].name + "   " + Values.playerAdderCharge;
                if (b0Pressed)
                {
                    b0Pressed = false;
                    selectedItem = currentItems[0];
                    itemDesc.enabled = false;
                    Values.playerAdderCharge--;
                    menuStates = MenuStates.Begin;

                }
                menuOptions[1].GetComponentInChildren<Text>().text = currentItems[1].name + "   " + Values.playerSubtracterCharge;
                if (b1Pressed)
                {
                    b1Pressed = false;
                    selectedItem = currentItems[1];
                    itemDesc.enabled = false;
                    Values.playerSubtracterCharge--;
                    menuStates = MenuStates.Begin;

                }
                menuOptions[2].GetComponentInChildren<Text>().text = currentItems[2].name + "   " + Values.enemyAdderCharge;
                if (b2Pressed)
                {
                    b2Pressed = false;
                    selectedItem = currentItems[2];
                    Values.enemyAdderCharge--;
                    itemDesc.enabled = false;
                    menuStates = MenuStates.Begin;

                }
                menuOptions[3].GetComponentInChildren<Text>().text = currentItems[3].name + "   " + Values.enemySubtracterCharge;
                if (b3Pressed)
                {
                    b3Pressed = false;
                    selectedItem = currentItems[3];
                    Values.enemySubtracterCharge--;
                    itemDesc.enabled = false;
                    menuStates = MenuStates.Begin;

                }
                
                break;

            case MenuStates.RollDice:
                RollingDice();


                playerDiceValText.text = playerDiceVal.ToString();
                enemyDiceValText.text = enemyDiceVal.ToString();

                if(prevMenuState == MenuStates.Atk)
                {
                    menuStates = MenuStates.DiceModifier;
                }

                if(prevMenuState == MenuStates.Items)
                {
                    menuStates = MenuStates.ItemDiceModifier;
                }
                break;

            case MenuStates.DiceModifier:

                if (selectedWeapon.name == "Oddener")
                {
                    if (Values.oddenerCharge <= 0)
                    {
                        menuStates = MenuStates.ItemDiceModifier;

                    }
                    else
                    {
                        weapons.Oddener();
                        Values.oddenerCharge--;
                        menuStates = MenuStates.ItemDiceModifier;
                    }
                }
                if (selectedWeapon.name == "Evener")
                {
                    if (Values.EvenerCharge <= 0)
                    {
                        menuStates = MenuStates.ItemDiceModifier;

                    }
                    else
                    {
                        weapons.Evener();
                        Values.evenerCharge--;
                        menuStates = MenuStates.ItemDiceModifier;
                    }
                }
                if (selectedWeapon.name == "Basic")
                {

                    //weapons.Basic();

                    menuStates = MenuStates.ItemDiceModifier;
                }
                if (selectedWeapon.name == "Extremes")
                {
                    if (Values.extremesCharge <= 0)
                    {
                        menuStates = MenuStates.ItemDiceModifier;

                    }
                    else
                    {
                        weapons.Extremes();
                        Values.extremesCharge--;
                        menuStates = MenuStates.ItemDiceModifier;
                    }
                }

                if(selectedItem == null)
                {
                    menuStates = MenuStates.DamageEnemy;
                }
                playerDiceVal = Values.currDiceRollVal;
                enemyDiceVal = Values.currEnemyDiceRollVal;

                playerDiceValText.text = playerDiceVal.ToString();
                enemyDiceValText.text = enemyDiceVal.ToString();

                break;

            case MenuStates.ItemDiceModifier:

                if (selectedItem.name == "Player Adder")
                {
                    items.AdderPlayer();
                    //selectedItem = null;
                    menuStates = MenuStates.DamageEnemy;

                }
                if (selectedItem.name == "Player Subtracter")
                {
                    items.SubtracterPlayer();
                    //selectedItem = null;
                    menuStates = MenuStates.DamageEnemy;

                }
                if (selectedItem.name == "Enemy Adder")
                {
                    items.AdderEnemy();
                    //selectedItem = null;
                    menuStates = MenuStates.DamageEnemy;

                }
                if (selectedItem.name == "Enemy Subtracter")
                {
                    items.SubtracterEnemy();
                    //selectedItem = null;
                    menuStates = MenuStates.DamageEnemy;

                }
                else
                {
                    selectedItem = null;
                    menuStates = MenuStates.DamageEnemy;

                }

                playerDiceVal = Values.currDiceRollVal;
                enemyDiceVal = Values.currEnemyDiceRollVal;
                playerDiceValText.text = playerDiceVal.ToString();
                enemyDiceValText.text = enemyDiceVal.ToString();

                break;


            case MenuStates.DamageEnemy:
                Instantiate(hitSound, transform.position, transform.rotation);
                hitSound.Play();
                enemyHealth -= Values.currDiceRollVal;
                enemyHealthText.text = enemyHealth.ToString();

                if(enemyHealth <= 0)
                {
                    if (bossBattle1)
                    {
                        SceneManager.LoadScene("Map2");
                    }
                    if (bossBattle2)
                    {
                        SceneManager.LoadScene("Map3");
                    }
                    if (bossBattle3)
                    {
                        SceneManager.LoadScene("WinGame");
                    }
                    enemyHealth = 10;
                    enemyHealthText.text = "0";
                    map.EndBattle();
                }

                menuStates = MenuStates.DamagePlayer;

                break;

            case MenuStates.DamagePlayer:

                playerHealth -= Values.currEnemyDiceRollVal;
                Values.playerHealth = playerHealth;

                playerHealthText.text = playerHealth.ToString();

                if (playerHealth <= 0)
                {
                    //Restart Game
                    SceneManager.LoadScene("Title Scene");
                }
                else
                {

                    menuStates = MenuStates.Begin;
                }
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

        enemyDiceVal = Random.Range(1, 7);

        Values.playerOGValue = playerDiceVal;
        Values.enemyOGValue = enemyDiceVal;

        playerDiceOGVal = Values.playerOGValue;
        enemyDiceOGVal = Values.enemyOGValue;

        playerOGValText.text = playerDiceOGVal.ToString();
        enemyOGValText.text = enemyDiceOGVal.ToString();
        
       

        if (bossBattle1)
        {
            Values.currEnemyDiceRollVal += 2;
        }
        if (bossBattle2)
        {
            Values.currEnemyDiceRollVal += 3;
        }
        if (bossBattle3)
        {
            Values.currEnemyDiceRollVal += 3;

        }

        Values.currDiceRollVal = playerDiceVal;

        Values.currEnemyDiceRollVal = enemyDiceVal;
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
    ItemDiceModifier,
    ExecuteActions,
    DamageEnemy,
    DamagePlayer,
    PlayerVictory
}
