using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using Unity.VisualScripting;
using static UnityEditor.PlayerSettings;
using Unity.Burst.CompilerServices;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string thisScene;
    [SerializeField] private string nextScene;
    [System.NonSerialized] public bool isStart;

    public ObstaclesScript obstacles;
    public GameObject player;
    public GameObject resultObj;
    public GameObject nextStageButton;
    public TextMeshProUGUI infoTMP;
    public CSVReader csvReader;

    public int playerPosX;
    public int playerPosY;
    private int[,] board = new int[4, 4];
    Vector3 initialPlayerPos = new Vector3(3.0f, 0.0f, 0.0f);
    bool win;
    bool lose;
    int failureCount;
    void Start()
    {
        Initialize();
    }
    void Update()
    {
        if (isStart)
        {
            if (win || lose) return;
            IsGetMouseButtonUp();
        }
    }
    public void IsGetMouseButtonUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (null != hit.collider)
            {
                Vector3 pos = hit.collider.gameObject.transform.position;
                int x = (int)pos.x;
                int y = (int)pos.y;
                Debug.Log("hitCollider");
                // �אڂ��Ă��邩
                if (player.transform.position.x + 1 == pos.x && player.transform.position.y == pos.y
                 || player.transform.position.x - 1 == pos.x && player.transform.position.y == pos.y
                 || player.transform.position.x == pos.x && player.transform.position.y + 1 == pos.y
                 || player.transform.position.x == pos.x && player.transform.position.y - 1 == pos.y)
                {
                    if (-1 == board[y, x]) // �ʂ��ꏊ�̎�
                    {
                        player.transform.position = pos;
                        board[y, x] = 0;
                    }
                    else if (0 == board[y, x]) // �ʂ����ꏊ�̎�
                    {
                        infoTMP.text = "�����͂��������Ȃ�";
                    }
                    else if (1 == board[y, x]) // �Q�[���I�[�o�[�̎�
                    {
                        player.transform.position = pos;
                        GameOver();
                    }
                    else if (2 == board[y, x]) // �N���A�̎�
                    {
                        player.transform.position = pos;
                        board[y, x] = 0;
                        CompleteStage();
                    }
                }
            }
        }
    }
    public void Initialize()
    {
        isStart = false;
        win = false;
        lose = false;
        obstacles.Initialize();
        infoTMP.text = "";
        // �f�[�^��z���
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                board[i, j] = csvReader.temp[i, j];
                Debug.Log(board[i, j]);
            }
        }

        // �v���C���[�̏����ݒ�
        player.transform.position = initialPlayerPos;
        board[playerPosY, playerPosX] = 0;
        player.SetActive(false);
        // �{�^���̏����ݒ�
        nextStageButton.SetActive(false);
        resultObj.SetActive(false);
    }
    public void CompleteStage()
    {
        win = true;
        infoTMP.text = "�����؂����I";
        nextStageButton.SetActive(true);
    }
    public void GameOver()
    {
        lose = true;
        failureCount++;
        infoTMP.text = "�݂������I";
        resultObj.SetActive(true);
        if (failureCount == 3)
        {
            failureCount = 0;
            //CoroutineScript.instance.StartCoroutine("Wait3Seconds");
            SceneManager.LoadScene("GameOverScene");
        }
    }
    public void OnClickReturnTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
    public void OnClickRetry()
    {
        Initialize();
    }
    public void OnClickNextStage()
    {
        failureCount = 0;
        SceneManager.LoadScene(nextScene);
        if (nextScene == "ClearScene") return;
        Destroy(this.gameObject);
    }
    public void ActivePlayer()
    {
        player.SetActive(true);
    }
    public void OnClickDebug()//�f�o�b�O�p�i�e�z��̒l���o�͂���j
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