using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour
{

    public GameObject InfoPanel;
    //ボタンを押されたとき情報画面を表示する
    public void OnClick()
    {
        
        InfoPanel.SetActive(true);
    }

}
