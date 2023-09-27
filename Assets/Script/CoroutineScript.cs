using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineScript : MonoBehaviour
{

    public static CoroutineScript instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator WaitForOneSecond()
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Wait1Seconds");
    }
    IEnumerator WaitForThreeSecond()
    {
        yield return new WaitForSeconds(3.0f);
        Debug.Log("Wait3Seconds");
    }
    IEnumerator WaitForFiveSecond()
    {
        yield return new WaitForSeconds(5.0f);
        Debug.Log("Wait5Seconds");
    }

}

