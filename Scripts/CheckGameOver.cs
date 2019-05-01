using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CheckGameOver : MonoBehaviour
{
    private Vector3[] lastRow = new Vector3[8];
    private bool hasValue = false;
    // Start is called before the first frame update
    void Start()
    {
        float lastX = -3.5f;
        for (int i = 0; i < lastRow.Length; i++)
        {
            lastRow[i] = new Vector3(lastX, -4.5f);
            lastX++;

        }
        InvokeRepeating("GameOver", 0.0f, 1.0f);
        
        StartCoroutine(IsGameOver(8));
    }
    private IEnumerator IsGameOver(int waitTime)
    {
        Debug.Log(hasValue);
        while (hasValue)
        {
            yield return new WaitForSeconds(waitTime);
            SceneManager.LoadScene("OVERScene");
            Debug.Log("game over!");
            
        }
        while (!hasValue) {
            yield return new WaitForSeconds(waitTime);
            Debug.Log("keep going!");
            StartCoroutine(IsGameOver(5));
            break;
        }
    }
    private void GameOver()
    {
        //LastRow has Value and map is due to update;
        foreach (Vector3 pos in lastRow)
        {

            if (MoneyMap.mapDic[pos] != 0)
            {
                hasValue = true;
                break;
            }
            hasValue = false;
        }
    }
}
