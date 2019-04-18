using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class ShowScore : MonoBehaviour
{
    private int scorevalue;
    private Text score;
    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scorevalue = ShootingControl.gameScore;
        score.text = "Score :" + scorevalue;
        
    }
}
