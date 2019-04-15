using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    private GameObject Player;
    private Transform playerTran;
  
        // Use this for initialization
	void Start () {
        Player =gameObject;
        playerTran = Player.transform;

    }

    // Update is called once per frame
    void Update () {
        

        if (playerTran.position.x>-3.5&&Input.GetKeyDown(KeyCode.A)) {                  // insure to move inside the canvas
            playerTran.position += Vector3.left;
        }
        if (playerTran.position.x<3.5&&Input.GetKeyDown(KeyCode.D))     
        {
            playerTran.position += Vector3.right;
           

        }
    }

}
