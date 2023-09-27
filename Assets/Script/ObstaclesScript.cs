using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesScript : MonoBehaviour
{
    public GameObject obstacles;
    public GameManager gameManager;
    void Start()
    {
        obstacles.SetActive(true);
        StartCoroutine("WaitForFiveSecond");
    }
    IEnumerator WaitForFiveSecond()
    {
        yield return new WaitForSeconds(5.0f);
        //Debug.Log("fadeObject");
        gameManager.isStart = true;
        gameManager.ActivePlayer();
        obstacles.SetActive(false);
    }
}
