using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CSVReader : MonoBehaviour
{
    [SerializeField] private string _csvFilename;
    private TextAsset _csvFile;   //CSVファイル
    private List<string[]> _csvData = new List<string[]>();  //CSVファイルの中身を入れるリスト
    public int[,] temp = new int[4, 4];
    private void Awake()
    {
        _csvFile = Resources.Load(_csvFilename) as TextAsset;   //Resourceにある指定のパスのCSVファイルを格納
        StringReader reader = new StringReader(_csvFile.text);      //TextAssetをStringReaderに変換

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();   //１行ずつ読む 
            _csvData.Add(line.Split(','));    //読みこんだDataをリストにAddする
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                temp[i, j] = (int)Convert.ToSingle(_csvData[i][j]);
                //Debug.Log(temp[i, j]);
            }
        }
    }
}