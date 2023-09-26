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
        StartCoroutine("WaitForThreeSecond");
    }
    IEnumerator WaitForThreeSecond()
    {
        yield return new WaitForSeconds(3.0f);
        Debug.Log("fadeObject");
        gameManager.isStart = true;
        obstacles.SetActive(false);
    }
}
