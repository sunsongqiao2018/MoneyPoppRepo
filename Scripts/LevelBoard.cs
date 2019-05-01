using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBoard : MonoBehaviour
{
    [SerializeField]
    private int currentLv;
    [SerializeField] Text newText;
    // Start is called before the first frame update
    
    void ShowLevel() {
        currentLv = ShootingControl.gameScore / 1000 +1;
        newText.text = "Level " + currentLv;
    }
    // Update is called once per frame
    void Update()
    {
        ShowLevel();
    }
}
