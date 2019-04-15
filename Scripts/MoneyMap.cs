using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMap : MonoBehaviour {

    public static Dictionary<Vector3, int> mapDic;
    public static bool IsneedAlignTiles;
    public Vector3[] moneyMapPositions;
    private GameObject[] tiles;
    private int totalSum;
   
    public int mapSize = 80;
    // Use this for initialization
    void Start () {
        IsneedAlignTiles = false;
        tiles = new GameObject[mapSize];
        LoadMap(mapSize);
        //SetInitialRows();
        //foreach (Vector3 a in moneyMapPositions) {
        //    Debug.Log(string.Format("{0} and{1}", a.x, a.y));
        //}
       
        InvokeRepeating("UpdateMapValues", 0.0f, 10.0f);
        totalSum = Random.Range(8, 100);
    }
    void SetInitialRows() {
        float tileOnePercen;
        float tileTwoPercen;
        float tileThrPercen;
        float tileTotalBase;
        float tileOneRatio;
        float tileTwoRatio;
        float tileThrRatio;
        for (int i = 0; i < 8; i++)
        {
            tileOnePercen = totalSum / 1;
            tileTwoPercen = totalSum / 5;
            tileThrPercen = totalSum / 10;
            tileTotalBase = tileOnePercen + tileTwoPercen + tileThrPercen;
            tileOneRatio = tileOnePercen / tileTotalBase;
            tileTwoRatio = tileTwoPercen / tileTotalBase;
            tileThrRatio = tileThrPercen / tileTotalBase;
            if (totalSum < 1)
            {
                totalSum = Random.Range(8, 49); //regiven value to total sum agian;
            }
            float ranDice = Random.Range(0.0f, 1.0f);
            if (ranDice < tileThrRatio)
            {
                mapDic[moneyMapPositions[i]] = 10;
                totalSum -= 10;
            }
            else if (ranDice < tileThrRatio + tileTwoRatio)
            {
                mapDic[moneyMapPositions[i]] = 5;
                totalSum -= 5;
            }
            else
            {
                mapDic[moneyMapPositions[i]] = 1;
                totalSum--;
            }
            //Debug.Log(mapDic[moneyMapPositions[i]]);
        }
    }
    void LoadMap(int size) {
        mapDic = new Dictionary<Vector3, int>();
        moneyMapPositions = new Vector3[mapSize];
        moneyMapPositions[0] = new Vector3(-3.5f, 4.5f); //set the first anchor point;
        float Yxis = 4.5f;
        for (var i = 1; i < moneyMapPositions.Length; ++i)
        {
            if (i % 8 == 0)
            {
                Yxis--;
                moneyMapPositions[i] = new Vector3(-3.5f, Yxis); //Reset to the beginning of the row 
            }
            else
            {
                moneyMapPositions[i] = new Vector3(moneyMapPositions[i - 1].x + 1, Yxis);
            }
        }
        for (var i = 0; i < moneyMapPositions.Length; i++)
        {
            mapDic.Add(moneyMapPositions[i], 0);
        }
    }
    void UpdateMapValues() {
        for (int i = 7;i >= 0;i--) {
            if (i == 0)
            {
                SetInitialRows();
            }
            else
            {
                for (int j = 0; j < 8; j++)
                {
                    mapDic[moneyMapPositions[8 * i + j]] = mapDic[moneyMapPositions[8 * (i - 1) + j]];
                }
            }
        }
        IsneedAlignTiles = true;
    }
    public void AlignTiles() {
        var i = 0;
        foreach (GameObject tileFromLastMove in tiles) {        //its alright cause we save map value info in Dictionary.
            Destroy(tileFromLastMove);
        }
        foreach (Vector3 position in moneyMapPositions) {
            switch (mapDic[position]) {
                case 0:
                    break;
                case 1:
                    tiles[i] = Instantiate( Resources.Load("Cash1") ) as GameObject;
                    tiles[i].transform.position = position;
                    break;
                case 5:
                    tiles[i] = Instantiate(Resources.Load("Cash5")) as GameObject;
                    tiles[i].transform.position = position;
                    break;
                case 10:
                    tiles[i] = Instantiate(Resources.Load("Cash10")) as GameObject;
                    tiles[i].transform.position = position;
                    break;
                case 50:
                    tiles[i] = Instantiate(Resources.Load("Cash50")) as GameObject;
                    tiles[i].transform.position = position;
                    break;
                default:
                    break;
                    }

            i++;        //dictionary's index pair with Position Matrix.
        }
        IsneedAlignTiles = false;
    }
    
    public int Gettile(Vector3 tileAt){
        if (mapDic.ContainsKey(tileAt))
        {
            return mapDic[tileAt];
        }
        else
        {
            throw new KeyNotFoundException();
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
    void Update()
    {
        if (IsneedAlignTiles) {
            AlignTiles();
        }
       
        ///----------------Folded------------------//
        //if (!ShootingControl.isHoldMoney && Input.GetKeyDown(KeyCode.Space)) //player press space and is currently not holding any money. we update map and value at the same time.
        //{

        //    UpdateMapValues();
        //    //change a trigger perhaps use a bool factor to determine the bullet is set or not.
        //}
        //we Just keep on track about the tiles in the map.
        //if(Input.GetKeyDown(KeyCode.Space) && ShootingControl.isHoldMoney) // player pressed space and is currently holding a money, we should 
        //    //From Last Row -Copy-> Second Last Row... to Second Copy-> Fisrt, Fisrt Generate New row;
        //    for (int j = 0; j < 8; j++)
        //    {   //temp solution for last row problem, works fine so far;
        //        Destroy(tiles[tiles.Length - j - 1]);
        //    }
        //    for (int i = tiles.Length - 1; i > 7; i--)
        //    {
        //        problem Last row will stack.

        //        tiles[i] = tiles[i - 8];//tiles[i - 8];
        //        if (tiles[i] != null)
        //        {
        //            tiles[i].transform.position = moneyMap[i];
        //            tiles[i].gameObject.name = "Cube" + i;
        //        }
        //    }
        //    RowMapping(8);
        //}
        //generate new row according to player shot.
        //------------fold this---------------------//
    }
}
