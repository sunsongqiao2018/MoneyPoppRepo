using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBullet:MonoBehaviour{
    //private Sprite moneyImage;
    public int value;
    public bool isShot;
    public bool isTile;
    private Rigidbody2D rb;
    private void Awake()
    {
        
    }
    private void Start()
    {
        SpriteRenderer sprRen = gameObject.GetComponentInChildren<SpriteRenderer>();
       
        value = int.Parse(sprRen.sprite.name.Substring(4));

        BoxCollider2D col = gameObject.GetComponent<BoxCollider2D>();
        col.size = new Vector2(0.9f, 0.9f);
        if (isShot) {
           rb = gameObject.AddComponent<Rigidbody2D>();
            col.isTrigger = false;
            rb.gravityScale = 0;
        }
    }
    private void Update()
    {
        if (isShot) {
            gameObject.transform.Translate(Vector3.up * 0.3f);
            if (gameObject.transform.position.y > 5) {
                Destroy(gameObject);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MoneyBullet colBullet = collision.gameObject.GetComponent<MoneyBullet>();
        colBullet.isShot = false;
        collision.gameObject.transform.position = gameObject.transform.position;
        if (!isShot)
        {
           // Destroy(gameObject);
        }
    }
}

public class MoneyBulletShot : MoneyBullet {
    private void Start()
    {
        isShot = false;
        isTile = false;
    }
    private void Update()
    {

    }
}
public class MoneyBulletTile : MoneyBullet
{
    private void Start()
    {
        isShot = false;
        isTile = true;
    }
    private void Update()
    {

    }
}