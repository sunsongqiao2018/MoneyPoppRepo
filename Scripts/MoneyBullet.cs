using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBullet{
    private Sprite moneyImage;
    public GameObject MoneyCube;
    private int value;
    public bool isShot;
    public MoneyBullet() {
        MoneyCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        isShot = false;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
