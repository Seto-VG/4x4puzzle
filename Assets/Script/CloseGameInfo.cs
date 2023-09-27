using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseGameInfo : MonoBehaviour
{
    public GameObject InfoPanel;
    //•Â‚¶‚éƒ{ƒ^ƒ“‚ğ‰Ÿ‚µ‚½‚Ìˆ—
    public void OnClick()
    {
        
        InfoPanel.SetActive(false);
    }

}
