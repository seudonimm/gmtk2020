using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBlock : MonoBehaviour
{
    [SerializeField] GameObject pathBlock;
    [SerializeField] int rand;

    // Start is called before the first frame update
    void Start()
    {
        pathBlock = this.gameObject;

        rand = Random.Range(1, 4);

        if(rand <= 1)
        {
            pathBlock.tag = "EnemyBlock";
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if(rand == 2)
        {
            pathBlock.tag = "ItemBlock";
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;

        }
        else if(rand == 3)
        {
            pathBlock.tag = "WeaponBlock";
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
