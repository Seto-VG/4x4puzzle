using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChgScene : MonoBehaviour
{
    [SerializeField] private string loadScene;
    [SerializeField] private Color fadeColor = Color.black;
    [SerializeField] private float fadeSpeedMultiplier = 1.0f;

    public void OnClick()
    {
        Initiate.Fade(loadScene, fadeColor, fadeSpeedMultiplier);
    }
}
