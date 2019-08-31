using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class Value_B
{
    public string quiz;     // 문제 지문
    public string[] ans = new string[4];    // 답1, 2, 3, 4
    public int pass;    // 정답 0번자리

    public Value_B(string _quiz, string _ans1, string _ans2, string _ans3, string _ans4, int _pass)
    {
        this.quiz = _quiz;
        this.ans[0] = _ans1;
        this.ans[1] = _ans2;
        this.ans[2] = _ans3;
        this.ans[3] = _ans4;
        this.pass = _pass;
    }
}

public class QuizManager_B : MonoBehaviour
{
    public static QuizManager_B instance;

    public Dictionary<int, Value_B> dictionary = new Dictionary<int, Value_B>();
    public Dictionary<int, Value_B> dictionary_temp;   // 문제를 섞기 위한 임시 변수



    void Awake()
    {
        instance = this;

        LoadMap();

        //RandomNumber();     // 문제 랜덤섞기
    }

    public void LoadMap()
    {
        TextAsset data = Resources.Load("BasicDatas", typeof(TextAsset)) as TextAsset;
        StringReader strReader = new StringReader(data.text);

        string source = strReader.ReadLine();

        int row = 0;

        while (source != null)
        {
            string[] words = source.Split(',');

            dictionary.Add(int.Parse(words[0]), new Value_B(words[1], words[2], words[3], words[4], words[5], 0));

            row++;
            source = strReader.ReadLine();
        }
        strReader.Close();

        Debug.Log("딕셔너리 개수 : " + dictionary.Count + " / " + dictionary[0].quiz);
    }


    void RandomNumber()         //문제 랜덤섞기 메소드
    {
        System.Random prng = new System.Random();

        dictionary_temp = new Dictionary<int, Value_B>();

        for (int i = 0; i < dictionary.Count - 1; i++)
        {
            int randomIndex = prng.Next(i, dictionary.Count);   // randomIndex에 랜덤 숫자가 지정
            dictionary_temp[randomIndex] = dictionary[i];
            dictionary[i] = dictionary[randomIndex];
            dictionary[randomIndex] = dictionary_temp[randomIndex];
        }
    }
}
