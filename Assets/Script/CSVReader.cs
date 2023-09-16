using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CSVReader : MonoBehaviour
{
    private TextAsset _csvFile;   //CSV�t�@�C��

    public List<string[]> _csvData = new List<string[]>();  //CSV�t�@�C���̒��g�����郊�X�g

    public static int[,] board = new int[4, 4];
    void Start()
    {
        _csvFile = Resources.Load("FirstStgData") as TextAsset;   //Resource�ɂ���w��̃p�X��CSV�t�@�C�����i�[
        StringReader reader = new StringReader(_csvFile.text);      //TextAsset��StringReader�ɕϊ�

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();   //�P�s���ǂ� 
            _csvData.Add(line.Split(','));    //�ǂ݂���Data�����X�g��Add����
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                board[i, j] = (int)Convert.ToSingle(_csvData[i][j]);
                Debug.Log(board[i, j]);
            }
        }
    }
}