using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject resultObj;
    public TextMeshProUGUI infoTMP;
    public CSVReader csvReader;
    
    private int[,] board = new int[4, 4];
    int ActiveFloor = 0;
    bool win;
    bool lose;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                board[i, j] = csvReader.temp[i,j];
                Debug.Log(board[i, j]);
            }
        }
        Vector3 ActiveFloorPos = player.transform.position;
        int FloorX = (int)ActiveFloorPos.x;
        int FloorY = (int)ActiveFloorPos.y;
        board[FloorY, FloorX] = ActiveFloor;
    }

    // Update is called once per frame
    void Update()
    {
        if (win || lose) return;

        // infoTMP.text = "";
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (null != hit.collider)
            {
                Vector3 pos = hit.collider.gameObject.transform.position;
                int x = (int)pos.x;
                int y = (int)pos.y;
                Debug.Log("当たった");
                if (-1 == board[y, x])
                {
                    player.transform.position = pos;
                    board[y, x] = ActiveFloor; 
                }
            }
        }
    }
    public void OnClickRetry()
    {
        SceneManager.LoadScene("InGameScene");
    }
    public void OnClickDebug()//デバッグ用（各配列の値を出力する）
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Debug.Log("(i,j) = (" + i + "," + j + ") = " + board[i, j]);
            }
        }
    }
}