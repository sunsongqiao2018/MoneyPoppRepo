using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMap : MonoBehaviour {
    public Vector3[] moneyMap;
    private GameObject[] tiles;
	// Use this for initialization
	void Start () {
        tiles = new GameObject[40];
        moneyMap = new Vector3[40];
        moneyMap[0] = new Vector3(-3.5f, 4.5f);
        float Yxis = 4.5f;
        for (var i = 1; i < moneyMap.Length; ++i)
        {
            

            if (i % 8 == 0)
            {
                Yxis --;
                moneyMap[i] = new Vector3(-3.5f, Yxis); //Reset to row beginning
                Debug.Log(Yxis);
            }
            else
            {

                moneyMap[i] = new Vector3(moneyMap[i - 1].x + 1, Yxis);
            }
        }
                
     
        for (var i = 0; i < tiles.Length; ++i)
        {
            tiles[i] = Instantiate(Resources.Load("CashTile")) as GameObject;
            tiles[i].transform.position = moneyMap[i];
            tiles[i].name = "Cube" + i;
        }
        moneyMap[0] = new Vector3(-3.5f, 4.5f);
    

            //Debug.Log(tile.position);
	}
    void CreateMap() { }
	// Update is called once per frame
	void Update () {
		
	}
}
