using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject prefabCircle;
    public GameObject prefabDiamond;
    public GameObject resultObj;
    public TextMeshProUGUI infoTMP;
    int nowPlayer;

    int[,] board = new int[3, 3];//3*3��int�^�Q�����z����`
    bool win;
    bool draw;
    // Start is called before the first frame update
    void Start()
    {
        //2�����z��board�̑S�Ă̒l���������i-1�Ɂj
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = -1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (win || draw) return;

        infoTMP.text = (nowPlayer + 1) + "P's Turn";
        bool next = false;
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (null != hit.collider)
            {
                Vector3 pos = hit.collider.gameObject.transform.position;
                int x = (int)pos.x + 1;
                int y = (int)pos.y + 1;
                if (-1 == board[y, x])
                {
                    GameObject prefab = prefabCircle;
                    if (1 == nowPlayer) prefab = prefabDiamond;
                    Instantiate(prefab, pos, Quaternion.identity);//�����܂ł�1�����z��ver.�Ɠ����L��
                    board[y, x] = nowPlayer; //2�����z��Ȃ̂�[x,y]�Ƀv���C���[�ԍ�������
                    next = true;
                }
            }
        }
        if (next)//���s�`�F�b�N�J�n
        {
            win = false;
            int prefabNum;//�v���t�@�u�����Ԑ����J�E���g����ϐ�

            //�����т̒���
            for (int i = 0; i < 3; i++)
            {
                prefabNum = 0;//������
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == -1 || board[i, j] != nowPlayer)//�󂫃}�X�܂�����̃R�}������ꍇ
                    {
                        prefabNum = 0;//���т����Z�b�g
                    }
                    else
                    {
                        prefabNum++;//�����̃R�}�Ȃ�J�E���g
                    }

                    if (prefabNum == 3)//�R�������珟��
                    {
                        resultObj.SetActive(true);
                        infoTMP.text = (nowPlayer + 1) + "P Win!";
                        win = true;
                    }
                }
            }

            //�c���т̒���
            for (int i = 0; i < 3; i++)
            {
                prefabNum = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (board[j, i] == -1 || board[j, i] != nowPlayer)
                    {
                        prefabNum = 0;
                    }
                    else
                    {
                        prefabNum++;
                    }

                    if (prefabNum == 3)
                    {
                        resultObj.SetActive(true);
                        infoTMP.text = (nowPlayer + 1) + "P Win!";
                        win = true;
                    }
                }
            }

            //�΂ߕ��сi�E�オ��j�̒���
            for (int i = 0; i < 3; i++)
            {
                prefabNum = 0;
                int up = 0;//�ジ��p

                for (int j = i; j < 3; j++)
                {
                    if (board[j, up] == -1 || board[j, up] != nowPlayer)
                    {
                        prefabNum = 0;
                    }
                    else
                    {
                        prefabNum++;
                    }

                    if (prefabNum == 3)
                    {
                        resultObj.SetActive(true);
                        infoTMP.text = (nowPlayer + 1) + "P Win!";
                        win = true;
                    }

                    up++;
                }
            }

            //�΂ߕ��сi�E������j�̒���
            for (int i = 0; i < 3; i++)
            {
                int down = 2;//������p
                prefabNum = 0;

                for (int j = i; j < 3; j++)
                {
                    if (board[j, down] == -1 || board[j, down] != nowPlayer)
                    {
                        prefabNum = 0;
                    }
                    else
                    {
                        prefabNum++;
                    }

                    if (prefabNum == 3)
                    {
                        resultObj.SetActive(true);
                        infoTMP.text = (nowPlayer + 1) + "P Win!";
                        win = true;
                    }

                    down--;
                }
            }
            if (!win)
            {
                //��������
                int cnt = 0; //�󂢂Ă�ꏊ�𐔂���p
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == -1) //�󂫃}�X����������
                        {
                            cnt++;//�J�E���g����
                        }
                    }
                }

                if (0 == cnt) //�󂫃}�X���Ȃ��ꍇ
                {
                    resultObj.SetActive(true);
                    infoTMP.text = "Draw!";//������������
                    draw = true;
                }

                nowPlayer++;
                if (2 <= nowPlayer)
                {
                    nowPlayer = 0;
                }
            }

        }
    }

    public void OnClickRetry()
    {
        SceneManager.LoadScene("InGameScene");
    }
    public void OnClickDebug()//�f�o�b�O�p�i�e�z��̒l���o�͂���j
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Debug.Log("(i,j) = (" + i + "," + j + ") = " + board[i, j]);
            }
        }
    }
}