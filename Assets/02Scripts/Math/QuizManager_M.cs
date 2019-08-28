using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

    public Dictionary<int, Value_M> dictionary;
    public Dictionary<int, Value_M> dictionary_temp;   // 문제를 섞기 위한 임시 변수

    void Awake()
    {
        instance = this;

        dictionary = new Dictionary<int, Value_M>();

        // 문제 추가
        dictionary.Add(0, new Value_M(3,"1", "+", "2", "3", "=", "3"));
        dictionary.Add(1, new Value_M(0,"2", "3", "-", "3", "=", "0"));
        dictionary.Add(2, new Value_M(2, "1", "*", "6", "3", "=", "3"));
        dictionary.Add(3, new Value_M(0,"5", "4", "-", "2", "=", "2"));
        dictionary.Add(4, new Value_M(6,"4", "+", "0", "=", "5", "4"));
        dictionary.Add(5, new Value_M(4,"1", "+", "2", "=", "6", "3"));
        dictionary.Add(6, new Value_M(0,"1", "9", "/", "3", "=", "3"));
        dictionary.Add(7, new Value_M(3,"5", "-", "1", "3", "=", "4"));
        dictionary.Add(8, new Value_M(1, "5", "-", "/", "5", "=", "1"));
        dictionary.Add(9, new Value_M(2, "4", "+", "6", "3", "=", "7"));
        dictionary.Add(10, new Value_M(2, "1", "*", "+", "5", "=", "5"));
        dictionary.Add(11, new Value_M(1, "0", "5", "*", "3", "=", "0"));
        dictionary.Add(12, new Value_M(2, "7", "-", "1", "3", "=", "4"));
        dictionary.Add(13, new Value_M(3, "8", "-", "1", "3", "=", "7"));
        dictionary.Add(14, new Value_M(0, "5", "7", "/", "1", "=", "7"));
        dictionary.Add(15, new Value_M(0, "5", "8", "/", "2", "=", "4"));
        dictionary.Add(16, new Value_M(5, "5", "*", "1", "=", "5", "4"));
        dictionary.Add(17, new Value_M(2, "9", "/", "+", "3", "=", "3"));
        dictionary.Add(18, new Value_M(1, "5", "-", "+", "3", "=", "8"));
        dictionary.Add(19, new Value_M(5, "4", "/", "2", "=", "2", "4"));
        dictionary.Add(20, new Value_M(1, "7", "/", "-", "3", "=", "4"));
        dictionary.Add(21, new Value_M(2, "6", "/", "2", "3", "=", "2"));
        dictionary.Add(22, new Value_M(5, "5", "+", "2", "=", "7", "4"));
        dictionary.Add(23, new Value_M(0, "4", "9", "+", "0", "=", "9"));
        dictionary.Add(24, new Value_M(1, "6", "-", "+", "2", "=", "8"));
        dictionary.Add(25, new Value_M(0, "4", "1", "*", "2", "=", "2"));
        dictionary.Add(26, new Value_M(4, "4", "*", "1", "=", "5", "4"));
        dictionary.Add(27, new Value_M(1, "4", "5", "*", "2", "=", "8"));
        dictionary.Add(28, new Value_M(2, "3", "*", "+", "2", "=", "6"));
        dictionary.Add(29, new Value_M(0, "5", "0", "*", "2", "=", "0"));
        dictionary.Add(30, new Value_M(4, "4", "-", "3", "=", "2", "1"));
        dictionary.Add(31, new Value_M(5, "6", "/", "2", "=", "3", "1"));
        dictionary.Add(32, new Value_M(4, "6", "-", "5", "=", "5", "1"));
        dictionary.Add(33, new Value_M(4, "6", "-", "5", "=", "5", "1"));
        dictionary.Add(34, new Value_M(0, "3", "0", "+", "0", "=", "0"));




        RandomNumber();     // 문제 랜덤섞기
    }

    // 문제 랜덤섞기 메소드
    void RandomNumber() {

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