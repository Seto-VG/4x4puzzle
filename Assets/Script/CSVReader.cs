using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class CSVReader : MonoBehaviour
{
    [SerializeField] private string _csvFilename;
    private TextAsset _csvFile;   //CSV�t�@�C��
    private List<string[]> _csvData = new List<string[]>();  //CSV�t�@�C���̒��g�����郊�X�g
    public int[,] temp = new int[4, 4];
    private void Awake()
    {
        _csvFile = Resources.Load(_csvFilename) as TextAsset;   //Resource�ɂ���w��̃p�X��CSV�t�@�C�����i�[
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
                temp[i, j] = (int)Convert.ToSingle(_csvData[i][j]);
                //Debug.Log(temp[i, j]);
            }
        }
    }
}