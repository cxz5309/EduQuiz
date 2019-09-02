using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Value
{
    public string quiz;     // 문제 지문
    public string[] ansB = new string[4];    // 답1, 2, 3, 4
    public string[] ansM = new string[6];    // 답1, 2, 3, 4

    public int passB;    // 정답 0번자리
    public int passM;

    public Value(string _quiz, string _ans1, string _ans2, string _ans3, string _ans4, int _pass)
    {
        this.quiz = _quiz;
        this.ansB[0] = _ans1;
        this.ansB[1] = _ans2;
        this.ansB[2] = _ans3;
        this.ansB[3] = _ans4;
        this.passB = _pass;
    }
    public Value(int _pass, string _ans1, string _ans2, string _ans3, string _ans4, string _ans5, string _ans6)
    {
        this.passM = _pass;
        this.ansM[0] = _ans1;
        this.ansM[1] = _ans2;
        this.ansM[2] = _ans3;
        this.ansM[3] = _ans4;
        this.ansM[4] = _ans5;
        this.ansM[5] = _ans6;
    }
}


public class QuizManager_B : MonoBehaviour
{
    public static QuizManager_B instance;

    public Dictionary<int, Value> dictionary;
    public Dictionary<int, Value> dictionary_temp;   // 문제를 섞기 위한 임시 변수


    void Awake()
    {
        instance = this;

        LoadMap();
        RandomNumber();     // 문제 랜덤섞기
    }

    public void LoadMap()
    {
        dictionary = new Dictionary<int, Value>();
        TextAsset data;
        StringReader strReader;
        string source;
        switch (SceneManager.GetActiveScene().name)
        {
            case "BasicScene":
                data = Resources.Load("BasicDatas", typeof(TextAsset)) as TextAsset;
                strReader = new StringReader(data.text);
                source = strReader.ReadLine();
                int row = 0;

                while (source != null)
                {
                    string[] words = source.Split(',');
                    dictionary.Add(int.Parse(words[0]), new Value(words[1], words[2], words[3], words[4], words[5], 0));

                    row++;
                    source = strReader.ReadLine();
                }
                strReader.Close();
                break;

            case "MathScene":
                data = Resources.Load("MathDatas", typeof(TextAsset)) as TextAsset;
                strReader = new StringReader(data.text);
                source = strReader.ReadLine();

                while (source != null)
                {
                    string[] words = source.Split(',');

                    dictionary.Add(int.Parse(words[0]), new Value(int.Parse(words[1]), words[2], words[3], words[4], words[5], words[6], words[7]));

                    source = strReader.ReadLine();
                }

                strReader.Close();
                break;

            //case "EnglishScene":
                //break;
        }
    }


    void RandomNumber()         //문제 랜덤섞기 메소드
    {
        System.Random prng = new System.Random();
        dictionary_temp = new Dictionary<int, Value>();

        for (int i = 0; i < dictionary.Count - 1; i++)
        {
            int randomIndex = prng.Next(i, dictionary.Count);   // randomIndex에 랜덤 숫자가 지정
            dictionary_temp[randomIndex] = dictionary[i];
            dictionary[i] = dictionary[randomIndex];
            dictionary[randomIndex] = dictionary_temp[randomIndex];
        }
    }
}
