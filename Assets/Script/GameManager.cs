using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string thisScene;
    [SerializeField] private string nextScene;
    [System.NonSerialized] public bool isStart;

    public ObstaclesScript obstacles; // 障害物
    public GameObject player;
    public GameObject resultObj;
    public GameObject nextStageButton;
    public TextMeshProUGUI infoTMP;
    public CSVReader LoadData;
    public float playerPosX;
    public float playerPosY;
    private int[,] board = new int[4, 4];
    bool isWin;
    bool isLose;
    int failureCount;
    StageInfo id;
    void Start()
    {
        Initialize();
    }
    void Update()
    {
        if (isStart)
        {
            if (isWin || isLose) return;

            if (Input.GetMouseButtonUp(0))
            {
                TryMove();
            }
        }
    }

    struct StageInfo // 構造体の定義
    {
        public int movablePosition;
        public int notMovablePosition;
        public int obstacles;
        public int goal;
    }

    public void Initialize()
    {
        // ステージ情報
        id.movablePosition = -1; // 移動可能
        id.notMovablePosition = 0; // 移動不可能
        id.obstacles = 1; // 障害物
        id.goal = 2; // ゴール
        
        isStart = false;
        isWin = false;
        isLose = false;
        obstacles.Initialize();
        infoTMP.text = "";

        // ステージの初期化
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                board[i, j] = LoadData.temp[i, j];
            }
        }

        // プレイヤーのポジションの初期化
        player.transform.position = new Vector3(playerPosX, playerPosY, 0.0f);

        player.SetActive(false);
        nextStageButton.SetActive(false);
        resultObj.SetActive(false);
    }

    public void TryMove()
    {
        // rayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        // コライダーに当たらなかったら処理終了
        if (hit.collider == null) { return; }

        // 選択したオブジェクトのポジションを取得
        Vector3 pos = hit.collider.gameObject.transform.position;
        // 配列に入れて判定をとるために変数に入れる
        int x = (int)pos.x;
        int y = (int)pos.y;

        // プレイヤーの隣のマスであれば
        if (player.transform.position.x + 1 == pos.x && player.transform.position.y == pos.y
         || player.transform.position.x - 1 == pos.x && player.transform.position.y == pos.y
         || player.transform.position.x == pos.x && player.transform.position.y + 1 == pos.y
         || player.transform.position.x == pos.x && player.transform.position.y - 1 == pos.y)
        {
            // 選択したものが何だったか
            if (id.movablePosition == board[y, x])
            {
                player.transform.position = pos;
                //board[y, x] = 0; // 一度通ったことを記録
            }
/*          else if (id.notMovablePosition == board[y, x]) // 一度通った場所を通れなくする場合
            {
                infoTMP.text = "そこには戻れない";
            }                                                                                   */
            else if (id.obstacles == board[y, x])
            {
                player.transform.position = pos;
                GameOver();
            }
            else if (id.goal == board[y, x])
            {
                player.transform.position = pos;
                CompleteStage();
            }
        }
    }

    public void CompleteStage()
    {
        isWin = true;
        infoTMP.text = "逃げ切った!";
        nextStageButton.SetActive(true);
    }
    public void GameOver()
    {
        isLose = true;
        failureCount++;
        infoTMP.text = "みつかった!";
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
    public void OnClickRetry() //TODO いらんから消してボタン祖設定を変える
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
    public void OnClickDebug() // デバッグ用
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