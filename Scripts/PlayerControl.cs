using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    private GameObject Player;

    private Transform playerTran;

    private MoneyBullet moneyBullet;
    private MoneyBullet moneyBulletOut;
    private bool loadCheck =false;
	// Use this for initialization
	void Start () {
        Player = GameObject.Find("MoneyFighter");
        playerTran = Player.transform;
        if (moneyBullet == null) {
            moneyBullet = new MoneyBullet();
            moneyBullet.MoneyCube.transform.position = playerTran.position;
        }
	}
    private void Fire() {
        moneyBulletOut = new MoneyBullet();
        moneyBulletOut.MoneyCube.transform.position = playerTran.position;
        loadCheck = true;
    }
	// Update is called once per frame
	void Update () {
        if (playerTran.position.x>-3.5&&Input.GetKeyDown(KeyCode.A)) {                  // insure to move inside the canvas
            playerTran.position += Vector3.left;
            moneyBullet.MoneyCube.transform.position = playerTran.position;
        }
        if (playerTran.position.x<3.5&&Input.GetKeyDown(KeyCode.D))     
        {
            playerTran.position += Vector3.right;
            moneyBullet.MoneyCube.transform.position = playerTran.position;

        }

        if (Input.GetKeyDown(KeyCode.Space))    //load bullet
        {
            Fire();
            moneyBulletOut.isShot = true;
        }
        if (loadCheck && moneyBulletOut.isShot) {
            moneyBulletOut.MoneyCube.transform.Translate(Vector3.up*0.3f);
            if (moneyBulletOut.MoneyCube.transform.position.y > 5) {
               // Destroy(moneyBulletOut);
            }
        }

    }

}
