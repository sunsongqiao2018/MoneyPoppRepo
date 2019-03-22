using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingControl : MonoBehaviour {
    public GameObject moneyBullet; //gona be GameObejct[] with rotation;


    void storeBullets() { }
    void rotateBullets() { }
    // Use this for initialization
    private void LoadBullet()
    {
        moneyBullet = null;

        if (moneyBullet == null)
        {
            MapTiles();
            MoneyBullet attr = moneyBullet.GetComponent<MoneyBullet>();
            attr.isReady = true;
            moneyBullet.transform.position = new Vector3(gameObject.transform.position.x,
                                                        gameObject.transform.position.y, 0);
            
        }
        //loadCheck = true;
    }

    private void MapTiles()
    {
        int a = Random.Range(1, 5);
        switch (a)
        {
            case 1:
                moneyBullet = Instantiate(Resources.Load("Cash1")) as GameObject;

                break;
            case 2:
                moneyBullet = Instantiate(Resources.Load("Cash5")) as GameObject;

                break;
            case 3:
                moneyBullet = Instantiate(Resources.Load("Cash10")) as GameObject;

                break;
            case 4:
                moneyBullet = Instantiate(Resources.Load("Cash100")) as GameObject;

                break;
            default:
                break;

        }
    }
        void Start ()
    {


    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))    //shoot bullet &reload
        {
           
            MoneyBullet attr = moneyBullet.GetComponent<MoneyBullet>();
            attr.isShot = true;
            
            LoadBullet();

        }
    }
}
