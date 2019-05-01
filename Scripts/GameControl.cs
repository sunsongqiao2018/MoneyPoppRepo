using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameControl: MonoBehaviour
{ 
    private void LoadScene() {
        int currentSceneNum = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneNum + 1);
    }
    private void PlayAgain() {
       
        SceneManager.LoadScene(1);
    }
    private void QuitGame() {
        Application.Quit();
    }
}
