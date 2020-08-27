using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//game jam code
//controls map state of the game
public class Map : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Collider2D playerCollider;
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] Button button;
    [SerializeField] Text buttonText, getText;
    [SerializeField] bool buttonPressed, gotItemOrWeapon;
    [SerializeField] int playerDiceVal, spacesMoved;

    [SerializeField] Transform rayPoint1, rayPoint2;

    public GameObject battleScene, getScene;

    [SerializeField] float timer;

    [SerializeField] List<GameObject> allWeaponsList;
    [SerializeField] List<GameObject> allItemsList;

    [SerializeField] int rand;

    [SerializeField] bool map1, map2, map3;

    [SerializeField] Text spacesMovedText;

    RaycastHit2D hit;


    public MapStates mapStates;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = player.GetComponent<Collider2D>();
        playerRB = player.GetComponent<Rigidbody2D>();
        buttonText = button.GetComponentInChildren<Text>();

        battleScene.SetActive(false);
        getScene.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {
        hit = Physics2D.Linecast(rayPoint1.position, rayPoint2.position); //raycast becasue collider doesnt activate when teleported into

        MapStateMachine();
    }

    void MapStateMachine()
    {
        switch (mapStates)
        {

            case MapStates.Standing:

                //just standing on block
                buttonText.text = "Roll to Move";

                if (buttonPressed)
                {
                    buttonPressed = false;

                    mapStates = MapStates.Rolling;
                }

                break;

            case MapStates.Rolling:

                buttonText.text = "Rolling...";

                RollingDice();

                mapStates = MapStates.Moving;

                break;

            case MapStates.Moving:
                buttonText.text = "Moving...";

                spacesMovedText.text = spacesMoved.ToString();
                CountdownTimer();
                if (timer <= 0)
                {
                    timer = .3f;

                    Move();
                    spacesMoved++;
                }

                if(spacesMoved == Values.currDiceRollVal)
                {
                    spacesMoved = 0;
                    mapStates = MapStates.ExecuteBlockAction;
                }
                
                break;

            case MapStates.ExecuteBlockAction: //sends to correct state depending on which block of the map you stopped on

                if (hit.collider.CompareTag("EnemyBlock"))
                {
                    Debug.Log("btl");
                    mapStates = MapStates.Battle;
                }
                else if (hit.collider.CompareTag("ItemBlock"))
                {
                    Debug.Log("itm");
                    rand = Random.Range(0, allItemsList.Capacity);

                    mapStates = MapStates.ItemGet;
                }
                else if (hit.collider.CompareTag("WeaponBlock"))
                {
                    Debug.Log("wep");
                    rand = Random.Range(0, allWeaponsList.Capacity);

                    mapStates = MapStates.WeaponGet;
                }
                else if (hit.collider.CompareTag("BossBlock"))
                {
                    Debug.Log("bss");
                    mapStates = MapStates.Boss;

                }

                break;

            case MapStates.Battle: //opens battle state, see menu script

                battleScene.SetActive(true);

                mapStates = MapStates.Standing;

                break;

            case MapStates.ItemGet: //get random item
                getScene.SetActive(true);

                //Values.currItemList.Add(allItemsList[rand]);

                getText.text = "You got a " + allItemsList[rand].name + " charge";

                if(allItemsList[rand].name == "Player Adder" && !gotItemOrWeapon)
                {
                    Values.playerAdderCharge++;
                }
                if (allItemsList[rand].name == "Player Subtracter" && !gotItemOrWeapon)
                {
                    Values.playerSubtracterCharge++;
                }
                if (allItemsList[rand].name == "Enemy Adder" && !gotItemOrWeapon)
                {
                    Values.enemyAdderCharge++;
                }
                if (allItemsList[rand].name == "Enemy Subtracter" && !gotItemOrWeapon)
                {
                    Values.enemySubtracterCharge++;
                }



                gotItemOrWeapon = true;

                buttonText.text = "Click to Continue";
                Reset();

                break;

            case MapStates.WeaponGet://get random weapon
                getScene.SetActive(true);

                //Values.currWeaponList.Add(allWeaponsList[rand]);

                getText.text = "You got a " + allWeaponsList[rand].name + " charge";
                if (allWeaponsList[rand].name == "Oddener" && !gotItemOrWeapon)
                {
                    Values.oddenerCharge++;
                }
                if (allWeaponsList[rand].name == "Evener" && !gotItemOrWeapon)
                {
                    Values.evenerCharge++;
                }
                if (allWeaponsList[rand].name == "Basic" && !gotItemOrWeapon)
                {
                    Values.basicCharge++;
                }
                if (allWeaponsList[rand].name == "Extremes" && !gotItemOrWeapon)
                {
                    Values.extremesCharge++;
                }

                gotItemOrWeapon = true;

                buttonText.text = "Click to Continue";
                Reset();


                break;

            case MapStates.Boss: //sends to boss depending on level
                if (map1)
                {
                    SceneManager.LoadScene("Boss1");
                }
                if (map2)
                {
                    SceneManager.LoadScene("Boss2");
                }
                if (map3)
                {
                    SceneManager.LoadScene("Boss3");
                }
                break;
        }
    }

    private void Reset()
    {
        if (buttonPressed)
        {
            gotItemOrWeapon = false;
            buttonPressed = false;
            getText.text = "";
            getScene.SetActive(false);
            mapStates = MapStates.Standing;
        }

    }
    public void EndBattle()
    {
        battleScene.SetActive(false);
    }

    void Move()
    {
        player.transform.Translate(new Vector2(5,0));

    }

    public void ButtonClicked()
    {
        buttonPressed = true;
    }


    public void RollingDice()
    {
        playerDiceVal = Random.Range(1, 7);

        Values.currDiceRollVal = playerDiceVal;
    }

    void CountdownTimer()
    {
        timer -= Time.deltaTime;
    }

}

public enum MapStates
{
    Standing,
    Moving,
    Rolling,
    ExecuteBlockAction,
    Battle,
    ItemGet,
    WeaponGet,
    Boss
}
