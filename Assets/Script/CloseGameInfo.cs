using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseGameInfo : MonoBehaviour
{
    public GameObject InfoPanel;
    //閉じるボタンを押した時の処理
    public void OnClick()
    {
        
        InfoPanel.SetActive(false);
    }

}
