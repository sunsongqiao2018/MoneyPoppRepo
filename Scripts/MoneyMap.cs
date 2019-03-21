using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMap : MonoBehaviour {
    public Vector3[] moneyMap;
    public GameObject[] tiles;
    private int totalSum;
    //private int currentGameStep;
    public int mapSize = 64;    //I set the map size to be n(row) * 8(Col), it would be good to give a number is n's power of 8;
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

    private void RowMapping(int col) {

        for (var i = 0; i < col; ++i) 
        {
            MapTiles(i);
        }
    }

	void Start () {
      
        CreateMap(mapSize);
        //tiles will be generated one row per shot.
        tiles = new GameObject[mapSize];
        RowMapping(8); //Only generate first row of moneys
        moneyMap[0] = new Vector3(-3.5f, 4.5f);
            Debug.Log(totalSum);
	}
    void CreateMap(int mapSize) {
        moneyMap = new Vector3[mapSize];
        moneyMap[0] = new Vector3(-3.5f, 4.5f); //set the first anchor point;
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
    //--------------------------LEVEL Section--------------------------//
    //private bool isOver() { //determine where the game is over;
    //    var result = false; // fasle : game Cont; true: Game Over;
    //    return result; 
    //}
    //private bool isPass() {        true : nextLevel; false : keep play.   }


    //private int nextLevel(); 
    //--------------------------LEVEL Section--------------------------//


    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {  //change a trigger perhaps use a bool factor to determine the bullet is set or not.

            //From Last Row -Copy-> Second Last Row... to Second Copy-> Fisrt, Fisrt Generate New row;
            for (int j = 0; j < 8; j++) {   //temp solution for last row problem, works fine so far;
                Destroy(tiles[tiles.Length-j-1]);
            }
            for (int i = tiles.Length - 1; i > 7; i--) {
                //problem Last row will stack.

                tiles[i] = tiles[i-8];//tiles[i - 8];
                if (tiles[i] != null)
                {
                    tiles[i].transform.position = moneyMap[i];
                    tiles[i].gameObject.name = "Cube" + i;
                }
            }
            RowMapping(8);
        }
		//generate new row according to player shot.
	}
}
