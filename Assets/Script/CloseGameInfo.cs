using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseGameInfo : MonoBehaviour
{
    public GameObject InfoPanel;
    //����{�^�������������̏���
    public void OnClick()
    {
        
        InfoPanel.SetActive(false);
    }

}
