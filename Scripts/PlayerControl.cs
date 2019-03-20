using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    private GameObject Player;

    private Transform playerTran;
    private ShootingControl ShootControl;
    private GameObject moneyBullet;
    //private bool loadCheck =false;
    
        // Use this for initialization
 

	void Start () {
        Player = GameObject.Find("MoneyFighter");
        playerTran = Player.transform;
        ShootControl = gameObject.GetComponent<ShootingControl>();
    }
    
  

    // Update is called once per frame
    void Update () {
        

        if (playerTran.position.x>-3.5&&Input.GetKeyDown(KeyCode.A)) {                  // insure to move inside the canvas
            playerTran.position += Vector3.left;
            if (ShootControl.moneyBullet != null)
                ShootControl.moneyBullet.transform.position = new Vector3(playerTran.position.x,playerTran.position.y,0);
        }
        if (playerTran.position.x<3.5&&Input.GetKeyDown(KeyCode.D))     
        {
            playerTran.position += Vector3.right;
            if (ShootControl.moneyBullet != null)
                ShootControl.moneyBullet.transform.position = new Vector3(playerTran.position.x, playerTran.position.y, 0);

        }
    }

}
