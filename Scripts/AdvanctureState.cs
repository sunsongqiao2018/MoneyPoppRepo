using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvanctureState : MonoBehaviour {
    [SerializeField] Text TextField;
    [SerializeField] State BeginningState;

    State state;
	// Use this for initialization
	void Start () {
        state = BeginningState;
        TextField.text = state.GetStateStory();             //text will get a string into the textField;
	}
	
	// Update is called once per frame
	void Update () {
        ManageStates();
	}

    private void ManageStates()
    {
      var nextStates =  state.NextState();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            state = nextStates[0];
        }
       else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            state = nextStates[1];
        }
        TextField.text = state.GetStateStory();
    }
}
