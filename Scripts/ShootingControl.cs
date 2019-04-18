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
        //StartCoroutine(Example());

    }
    IEnumerator Example()           //just a try-out function on coroutine.
    {
        while (true)
        {
           
            yield return new WaitForSeconds(1);
            print(Mathf.RoundToInt( Time.time));
        }
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
    void moveBullet()               //this method is to give a movement animation after player fire the money. not work yet, possible due to refresh sequecene or rate.
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
            catch(KeyNotFoundException e) {}
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
                if (MoneyMap.mapDic[tempVec] != 0)
                {
                    MoneyMap.mapDic[tempStayPosi] = CatchedBulletValue;
                    OneRoundScore(tempStayPosi, CatchedBulletValue);
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
                    moneyBullet.transform.Translate(tempStayPosi);
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

        //if (MoneyMap.mapDic.ContainsKey(positionUP))
        //{
        //    if (MoneyMap.mapDic[positionUP] == monValue)
        //    {
        //        if (!positionsPool.Contains(monPoint))
        //        {
        //            positionsPool.Add(monPoint);
        //        }
        //        if (!positionsPool.Contains(positionUP))
        //        {
        //            positionsPool.Add(positionUP);
        //            UnitCheck(positionUP, monValue);
        //        }
        //        if (MoneyMap.mapDic[positionLeft] == monValue) {
        //            if (!positionsPool.Contains(positionLeft))
        //            {
        //                positionsPool.Add(positionLeft);
        //                UnitCheck(positionLeft, monValue);
        //            }
        //        }
        //        if (MoneyMap.mapDic[positionRight] == monValue) {
        //            if (!positionsPool.Contains(positionRight))
        //            {
        //                positionsPool.Add(positionRight);
        //                UnitCheck(positionRight, monValue);
        //            }
        //        }
               
        //    }
        //}
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

    void ResortMap() {          //lets hard-code a position pool.
        Vector3[] scanner = new Vector3[72];
        List<Vector3> toMoveArray = new List<Vector3>();
      
        moneyMap.moneyMapPositions.CopyTo(scanner, 7);
        foreach (Vector3 position in scanner) {
            if (MoneyMap.mapDic[position] != 0 && MoneyMap.mapDic.ContainsKey(position + Vector3.up) && MoneyMap.mapDic[position + Vector3.up] ==0)       //collect those need to resort tiles first.
            {
                toMoveArray.Add(position);
            } 
        }
        if (toMoveArray.Count != 0)
        {
            foreach (Vector3 toMoveTile in toMoveArray)
            {
                //swap, rescan until toMoveArray is empty;
                int tempVal = MoneyMap.mapDic[toMoveTile];
                MoneyMap.mapDic[toMoveTile] = MoneyMap.mapDic[toMoveTile + Vector3.up];
                MoneyMap.mapDic[toMoveTile + Vector3.up] = tempVal;
            }
            ResortMap();    //rescan;
        }
        MoneyMap.IsneedAlignTiles = true;
    }


    void CheckCombo() { }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)&&!isHoldMoney)
        {
            CatchBullet();    
        }
        if (Input.GetKeyDown(KeyCode.F) && isHoldMoney) { //ready
            moveBullet();
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
