using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMap : MonoBehaviour {
    public Vector3[] moneyMap;
    private GameObject[] tiles;
    private int totalSum;
    // Use this for initialization
    private void MapTiles(int i) {
        int a = Random.Range(1, 5);
        switch (a)
        {
            case 1:
                tiles[i] = Instantiate(Resources.Load("Cash1")) as GameObject;
                tiles[i].transform.position = moneyMap[i];
                tiles[i].name = "Cube" + i;
                totalSum++;
                break;
            case 2:
                tiles[i] = Instantiate(Resources.Load("Cash5")) as GameObject;
                tiles[i].transform.position = moneyMap[i];
                tiles[i].name = "Cube" + i;
                totalSum += 5;
                break;
            case 3:
                tiles[i] = Instantiate(Resources.Load("Cash10")) as GameObject;
                tiles[i].transform.position = moneyMap[i];
                tiles[i].name = "Cube" + i;
                totalSum += 10;
                break;
            case 4:
                tiles[i] = Instantiate(Resources.Load("Cash100")) as GameObject;
                tiles[i].transform.position = moneyMap[i];
                tiles[i].name = "Cube" + i;
                totalSum += 100;
                break;
            default:
                break;

        }
    }
	void Start () {
        CreateMap(40);
        //tiles will be generated one row per shot.
        tiles = new GameObject[40];
        for (var i = 0; i < tiles.Length; ++i)
        {
            MapTiles(i);


        }
        moneyMap[0] = new Vector3(-3.5f, 4.5f);
            Debug.Log(totalSum);
	}
    void CreateMap(int mapSize) {
        moneyMap = new Vector3[40];
        moneyMap[0] = new Vector3(-3.5f, 4.5f);
        float Yxis = 4.5f;
        for (var i = 1; i < moneyMap.Length; ++i)
        {
            if (i % 8 == 0)
            {
                Yxis--;
                moneyMap[i] = new Vector3(-3.5f, Yxis); //Reset to row beginning
            }
            else
            {
                moneyMap[i] = new Vector3(moneyMap[i - 1].x + 1, Yxis);
            }
        }
    }

	// Update is called once per frame
	void Update () {
		//generate new row according to player shot.

	}
}
