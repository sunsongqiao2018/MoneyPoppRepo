using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/***********----------------Code Coupling too much!!!!---------------******/
public class ShootingControl : MonoBehaviour {

    public GameObject moneyBullet; //gona be GameObejct[] with rotation;
    //private DeleteChain deleteChain = new DeleteChain();
  
    private int CatchedBulletValue;
    public static bool isHoldMoney;
    private List<Vector3> positionsPool;
    public float moveSpeed = 1.0f;
    //private int monScore = 0;
    void storeBullets() { }
    void rotateBullets() { }
    // Use this for initialization
        void Start ()
    {
        positionsPool = new List<Vector3>();
        
        isHoldMoney = false;
        moneyBullet = new GameObject();
        StartCoroutine(Example());

    }
    IEnumerator Example()
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
                    Debug.Log(string.Format("We found {0}", tempVec));
                    MoneyMap.mapDic[tempVec] = 0;
                    isHoldMoney = true;
                    break;
                }
            }
            catch (KeyNotFoundException e) {
                Debug.Log(e);
            }
            MoneyMap.IsneedAlignTiles = true;
            Debug.Log(string.Format("No value at {0}", tempVec));
        }
    }
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
            catch(KeyNotFoundException e) {}
        }
        var step = moveSpeed * Time.deltaTime;
        moneyBullet.transform.position = Vector3.MoveTowards(moneyBullet.transform.position, targetPosition, step);
    }
    void FireBullet()
    {
        var playerCurrentX = gameObject.transform.position.x;
        for (var y = -4.5f; y <= 4.5f; y++)
        {
            Vector3 tempVec = new Vector3(playerCurrentX, y);
            int ThisRoundPoint = 0;
            try
            {
                if (MoneyMap.mapDic[tempVec] != 0)
                {
                    MoneyMap.mapDic[tempVec + Vector3.down] = CatchedBulletValue;
                    UnitCheck(tempVec + Vector3.down, CatchedBulletValue);
                    Vector3 lastPosition = positionsPool[positionsPool.Count - 1];

                    foreach (Vector3 poolEle in positionsPool) {
                        ThisRoundPoint += MoneyMap.mapDic[poolEle];
                        MoneyMap.mapDic[poolEle] = 0;
                    }
                    if (ThisRoundPoint >= 5 && ThisRoundPoint < 10)
                    {
                        MoneyMap.mapDic[lastPosition] = 5;
                        Debug.Log("Give a five dollar here!");
                    }
                    else if (ThisRoundPoint >= 10 && ThisRoundPoint < 50)
                    {
                        MoneyMap.mapDic[lastPosition] = 10;
                        Debug.Log("give a ten dollar here!");
                    }
                    else if (ThisRoundPoint >= 50)
                    {
                        MoneyMap.mapDic[lastPosition] = 50;
                        Debug.Log("give a fifty dollar here! sweet!");

                    }
                    else {
                        MoneyMap.mapDic[tempVec + Vector3.down] = CatchedBulletValue;
                        Debug.Log("ohh.. you didnt earn any points this round");
                    }
                    moneyBullet.transform.Translate(tempVec+Vector3.down);
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

    void UnitCheck(Vector3 monPoint, int monValue) {
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
                        UnitCheck(aroundPoint, monValue);
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

    }   //this function checks how many money at sound has same value;
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
