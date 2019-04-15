using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoneyBullet : MonoBehaviour
{
    //private Sprite moneyImage;
    public int value;
    public bool isShot;
    public bool isReady;
    private Rigidbody2D rbBullet;
    private BoxCollider2D col;
    public float moveSpeed = 4.0f;
    private static int bulletCount;

    private MoneyMap mMap;
    //UnityEvent bulletEvent = new UnityEvent();

    private void Start()
    {
        mMap = GameObject.Find("Map").GetComponent<MoneyMap>();


        // bulletEvent.AddListener(BulletTrigger);
        SpriteRenderer sprRen = gameObject.GetComponentInChildren<SpriteRenderer>();

        value = int.Parse(sprRen.sprite.name.Substring(4)); //get money values;
        isShot = false;


        col = gameObject.GetComponent<BoxCollider2D>();
        col.size = new Vector2(0.9f, 0.9f);

    }
    private void Update()
    {
        if (isShot)
        {
            if (rbBullet == null)
            {
                rbBullet = gameObject.AddComponent<Rigidbody2D>();
                rbBullet.isKinematic = true;
                //col.enabled = false;
            }
            rbBullet.velocity = Vector2.up * moveSpeed;

            if (gameObject.transform.position.y > 5)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D bullet)
    {
        //need to do when Trigger : compare value -> equal ? merge : stack;
        // check surrounding.
        if (!isShot &&!isReady)
        {
            var tileCol = gameObject.GetComponent<MoneyBullet>();
            var bulletCol = bullet.gameObject.GetComponent<MoneyBullet>();
            int collisionNumber = int.Parse(gameObject.name.Substring(4));  //get the number of moneytile that bullet collide with 
            var mapGeter = GameObject.Find("Map").GetComponent<MoneyMap>();
            if (tileCol.value == bulletCol.value)
            {

                tileCol.value += bulletCol.value;

                Destroy(bulletCol.gameObject);     //not right, but let it be first.
                //add first then check surrrounding.
               
            }
            else
            {   if (collisionNumber + 8 < mapGeter.mapSize)
                {
                    mapGeter.SetTile(bullet.gameObject, collisionNumber + 8);
                    bullet.transform.position = mapGeter.GetMap(collisionNumber + 8);
                    bulletCol.name = ("Cube" + collisionNumber + 8);
                    bulletCol.isReady = false;
                    bulletCol.isShot = false;
                    bulletCol.moveSpeed = 0f;   //stop bullet 
                }

            }
        }
    }


    private void OnTriggerExit(Collider bullet)
    {
        //merge bullet with save value;
    }
}

//public class MoneyBulletTile : MoneyBullet
//{
//    private void Start()
//    {
//        isShot = false;
//        isTile = true;
//    }
//    private void Update()
//    {

//    }
//}