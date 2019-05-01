using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShootingControl : MonoBehaviour {

    public GameObject moneyBullet; //gona be GameObejct[] with rotation;
    //private DeleteChain deleteChain = new DeleteChain();
  
    private int CatchedBulletValue;
    public static bool isHoldMoney;
    private List<Vector3> positionsPool;
    public float moveSpeed = 1.0f;
    public static int gameScore;
    private Vector3[] scanner = new Vector3[72];
    //public MoneyMap moneyMap; //better hard code the position value in??
    //private int monScore = 0;

    void storeBullets() { }
    void rotateBullets() { }
    // Use this for initialization
        void Start ()
    {
        gameScore = 0;
        positionsPool = new List<Vector3>();
        
        isHoldMoney = false;
        moneyBullet = new GameObject();
        SetScanner();

    }
    IEnumerator Example()           //just a try-out function on coroutine.
    {

        Debug.Log("ha");
            yield return new WaitForSeconds(1);
        
        Debug.Log("la");
    }
    void LoadBullet(int value) {
        switch (value) {
            case 1:
               moneyBullet = Instantiate(Resources.Load("Cash1")) as GameObject;
                break;
            case 5:
                moneyBullet = Instantiate(Resources.Load("Cash5")) as GameObject;
                break;
            case 10:
                moneyBullet = Instantiate(Resources.Load("Cash10")) as GameObject;
                break;
            case 50:
                moneyBullet = Instantiate(Resources.Load("Cash50")) as GameObject;
                break;
            case 100:
                moneyBullet = Instantiate(Resources.Load("Cash100")) as GameObject;
                break;
            default:
                break;
        }
    }




    void CatchBullet() {
        
       var playerCurrentX = gameObject.transform.position.x;
        for (var y = -4.5f; y <= 4.5f; y++) {
            Vector3 tempVec = new Vector3(playerCurrentX, y);
            try
            {
                if (MoneyMap.mapDic[tempVec] != 0)
                {
                    LoadBullet(MoneyMap.mapDic[tempVec]);
                    CatchedBulletValue = MoneyMap.mapDic[tempVec];
                    //Debug.Log(string.Format("We found {0}", tempVec));
                    MoneyMap.mapDic[tempVec] = 0;
                    isHoldMoney = true;
                    break;
                }
            }
            catch (KeyNotFoundException e) {
                Debug.Log(e);
            }
            MoneyMap.IsneedAlignTiles = true;
            //Debug.Log(string.Format("No value at {0}", tempVec));
        }
    }



    //this function is not working
    //this method is to give a movement animation after player fire the money. not work yet, possible due to refresh sequecene or rate.
    void moveBullet()               
    {
        var playerCurrentX = gameObject.transform.position.x;
        Vector3 targetPosition = new Vector3();

        for (var y = -4.5f; y <= 4.5f; y++)
        {
            Vector3 tempVec = new Vector3(playerCurrentX, y);
            try
            {
                if (MoneyMap.mapDic[tempVec] != 0)
                {
                    targetPosition = tempVec;
                }
                else
                {
                    targetPosition = new Vector3(playerCurrentX, 4.5f);
                }
            }
            catch(KeyNotFoundException e) {
                Debug.Log(e.Message);
            }
        }
        var step = moveSpeed * Time.deltaTime;
        moneyBullet.transform.position = Vector3.MoveTowards(moneyBullet.transform.position, targetPosition, step);
    }






    void FireBullet()       //this method contains calculations after player fire a money bullet to map.
    {
        var playerCurrentX = gameObject.transform.position.x;
        for (var y = -4.5f; y <= 4.5f; y++)
        {
            Vector3 tempVec = new Vector3(playerCurrentX, y);
            Vector3 tempStayPosi = tempVec + Vector3.down;
            int ThisRoundPoint = 0;
            try
            {
                if (MoneyMap.mapDic[tempVec] != 0 ||(y == 4.5f && MoneyMap.mapDic[tempVec] == 0))
                {
                    MoneyMap.mapDic[tempStayPosi] = CatchedBulletValue;
                   
                        StartCoroutine(Example());
                    
                  
                    OneRoundScore(tempStayPosi, CatchedBulletValue);    //This function changes positionsPool.


                    Vector3 lastPosition = positionsPool[positionsPool.Count - 1];


                    foreach (Vector3 poolEle in positionsPool) {
                        ThisRoundPoint += MoneyMap.mapDic[poolEle];
                    }
                    if (ScoreValidation(CatchedBulletValue, ThisRoundPoint))       
                    {
                        foreach (Vector3 poolEle in positionsPool)
                        {
                            MoneyMap.mapDic[poolEle] = 0;
                        }

                        if (CatchedBulletValue == 1)
                        {
                            MoneyMap.mapDic[tempStayPosi] = 5;
                        }
                        else if (CatchedBulletValue == 5)
                        {
                            MoneyMap.mapDic[tempStayPosi] = 10;
                        }
                        else if (CatchedBulletValue == 10)
                        {
                            MoneyMap.mapDic[tempStayPosi] = 50;
                        }
                        else if (CatchedBulletValue == 50)
                        {
                            MoneyMap.mapDic[tempStayPosi] = 100;
                        }
                        else {
                            MoneyMap.mapDic[tempStayPosi] = 0;
                        }
                        gameScore += ThisRoundPoint;
                    }

                    else {
                        MoneyMap.mapDic[tempStayPosi] = CatchedBulletValue;
                    }

                    ResortMap();
                    

                    //clean up things for next round
                    Destroy(moneyBullet);
                    positionsPool.Clear();
                    break;
                }
            }
            catch (KeyNotFoundException e)
            {
                Debug.Log(e);
            }
        }
        MoneyMap.IsneedAlignTiles = true;
    }
    //UnitCheck(position, value:1,2,6,24,0); init







    void OneRoundScore(Vector3 monPoint, int monValue)          //calculate surrounding tiles that has same value with projectiles which can add together as the score.
    {
        Vector3 positionUP = monPoint + Vector3.up;
        Vector3 positionLeft = monPoint + Vector3.left;
        Vector3 positionRight = monPoint + Vector3.right;
        Vector3 positionDown = monPoint + Vector3.down;

        Vector3[] positionsThisRound = { positionUP, positionLeft, positionRight, positionDown};

        if (!positionsPool.Contains(monPoint)) {
            positionsPool.Add(monPoint);
        }
        foreach (Vector3 aroundPoint in positionsThisRound) {
            if (MoneyMap.mapDic.ContainsKey(aroundPoint) 
                && MoneyMap.mapDic[aroundPoint] == monValue 
                && !positionsPool.Contains(aroundPoint))
            {
                positionsPool.Add(aroundPoint);
                OneRoundScore(aroundPoint, monValue);
            }
        }
    }







    bool ScoreValidation(int catchedValue, int scoreThisRound) {   //this function checks if the collected score is valid to perform merge action.
        switch (catchedValue) {
            case 1:
                if (scoreThisRound >= 5) return true;
                else return false;
            case 5:
                if (scoreThisRound >= 10) return true;
                else return false;
                
            case 10:
                if (scoreThisRound >= 50) return true;
                else return false;
            case 50:
                if (scoreThisRound >= 100) return true;
                else return false;
              
            case 100:
                if (scoreThisRound >= 500) return true;
                else return false;
               
            default:
                return false;
               
        }

    }

    void SetScanner() {

        int i = 0;
        //stupid(me) using of for loop, ended each cells in scanner only contains the final tile of the map.
        //for(int i = 0; i<length; i++) ha.....

            for (float x = -3.5f; x <= 3.5f; x++) {
                for (float y = 3.5f; y >= -4.5f; y--) {
                    scanner[i] = new Vector3(x, y);
                  i++;
                }
            }
        

    }


    void ResortMap() {          //lets hard-code a position pool.
        
        List<Vector3> toMoveList = new List<Vector3>();
       // Debug.Log("function got called");
        foreach (Vector3 position in scanner) {

            if (MoneyMap.mapDic[position] != 0 && MoneyMap.mapDic.ContainsKey(position + Vector3.up) && MoneyMap.mapDic[position + Vector3.up] ==0)      //collect those need to resort tiles first.
            {  
                toMoveList.Add(position);
            } 
        }
        //to think about it, I should not use recursive calling here, because one the toMoveTile is formed, in this time window I should ONLY need to move those tiles to the designed position.

        if (toMoveList.Count != 0)
        {
            foreach (Vector3 toMoveTile in toMoveList)
            {
                //swap, rescan until toMoveList is empty;
                int tempVal = MoneyMap.mapDic[toMoveTile];
                MoneyMap.mapDic[toMoveTile] = MoneyMap.mapDic[toMoveTile + Vector3.up];
                MoneyMap.mapDic[toMoveTile + Vector3.up] = tempVal;
            }
            ResortMap();    //rescan simple but wasty;
        }
    }


    //deprecated
    void ResortMapOptimized() { //its actually not working cuz we still need to scan the entire map but first row.

        List<Vector3> toMoveList = new List<Vector3>();
        // Debug.Log("function got called");
        foreach (Vector3 position in scanner)
        {

            if (MoneyMap.mapDic[position] != 0 && MoneyMap.mapDic.ContainsKey(position + Vector3.up) && MoneyMap.mapDic[position + Vector3.up] == 0)      //collect those need to resort tiles first.
            {
                toMoveList.Add(position);
            }
        }

        while (toMoveList.Count != 0)
        {
            List<Vector3> toAddListTemp = new List<Vector3>();

            //dont remove and add elements in foreach array.
            foreach (Vector3 toMoveTile in toMoveList)
            {
              
                if (toMoveTile.y != 4.5f && MoneyMap.mapDic[toMoveTile + Vector3.up] == 0)
                {
                    //swap, rescan until toMoveList is empty;
                    int tempVal = MoneyMap.mapDic[toMoveTile];
                    MoneyMap.mapDic[toMoveTile] = MoneyMap.mapDic[toMoveTile + Vector3.up];
                    MoneyMap.mapDic[toMoveTile + Vector3.up] = tempVal;

                    toAddListTemp.Add(toMoveTile + Vector3.up);
                    
                }

            }
            Debug.Log(toAddListTemp.Count);
            toMoveList = toAddListTemp;
            //toMoveList = toAddListTemp;
           
        }
    }





    void CheckCombo() { }





	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)&&!isHoldMoney)
        {
            CatchBullet();    
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isHoldMoney) { //ready
           // moveBullet();
            FireBullet();
            isHoldMoney = false;
        }
        if (moneyBullet != null)
        {
            moneyBullet.transform.position = new Vector3(gameObject.transform.position.x,
                                                         gameObject.transform.position.y,
                                                         gameObject.transform.position.z - 0.1f);
        } //character holds bullet moving
    }
}
