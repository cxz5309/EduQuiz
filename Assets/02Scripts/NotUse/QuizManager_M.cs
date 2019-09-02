using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;


public class Value_M
{
    public string[] ans = new string[6];    // 답1, 2, 3, 4
    public int pass;   

    public Value_M(int _pass, string _ans1, string _ans2, string _ans3, string _ans4, string _ans5, string _ans6)
    {
        this.pass = _pass;
        this.ans[0] = _ans1;
        this.ans[1] = _ans2;
        this.ans[2] = _ans3;
        this.ans[3] = _ans4;
        this.ans[4] = _ans5;
        this.ans[5] = _ans6;
    }
}

public class QuizManager_M : MonoBehaviour
{
    public static QuizManager_M instance;

    public Dictionary<int, Value_M> dictionary = new Dictionary<int, Value_M>();
    public Dictionary<int, Value_M> dictionary_temp;   // 문제를 섞기 위한 임시 변수

    void Awake()
    {
        LoadMap();
        RandomNumber();     // 문제 랜덤섞기
    }

    public void LoadMap()
    {
        TextAsset data = Resources.Load("MathDatas", typeof(TextAsset)) as TextAsset;
        StringReader strReader = new StringReader(data.text);

        string source = strReader.ReadLine();

        while (source != null)
        {
            string[] words = source.Split(',');

            dictionary.Add(int.Parse(words[0]), new Value_M(int.Parse(words[1]), words[2], words[3], words[4], words[5], words[6], words[7]));
            
            source = strReader.ReadLine();
        }
        strReader.Close();

        Debug.Log("수학 딕셔너리 개수 : " + dictionary.Count + " / " + dictionary[0].ans[0]);
    }

    void RandomNumber()
    {           // 문제 랜덤섞기 메소드

        System.Random prng = new System.Random();

        dictionary_temp = new Dictionary<int, Value_M>();

        for (int i = 0; i < dictionary.Count - 1; i++) {
            int randomIndex = prng.Next(i, dictionary.Count);   // randomIndex에 랜덤 숫자가 지정
            dictionary_temp[randomIndex] = dictionary[i];
            dictionary[i] = dictionary[randomIndex];
            dictionary[randomIndex] = dictionary_temp[randomIndex];
        }
    }
}