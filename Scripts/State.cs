using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="State")]
public class State : ScriptableObject {

    [TextArea(10,14)][SerializeField] string storyText;
    [SerializeField] State[] nextState;
    public string GetStateStory() {
       
        return storyText;
    }
    public State[] NextState() {
        return nextState;
    }
}
 