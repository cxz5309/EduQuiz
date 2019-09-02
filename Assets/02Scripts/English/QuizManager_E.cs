using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Val
{
    public int cnt;
    public string alpha;
    
    public Val(int _cnt, string _alpha)
    {
        cnt = _cnt;
        alpha = _alpha;
    }
}

public class Value_E
{
    public int num;         // 문제 번호
    public string quiz;     // 문제 지문
    public Sprite sprite;

    public Val[] val = new Val[10];

    public Value_E(int _num, Sprite _sprite, Val _ans1, Val _ans2, Val _ans3)
    {
        num = _num;
        sprite = _sprite;
        val[0] = _ans1;
        val[1] = _ans2;
        val[2] = _ans3;
    }

    public Value_E(int _num, Sprite _sprite, Val _ans1, Val _ans2, Val _ans3, Val _ans4)
    {
        num = _num;
        sprite = _sprite;
        val[0] = _ans1;
        val[1] = _ans2;
        val[2] = _ans3;
        val[3] = _ans4;
    }
    
    // overrode
    public Value_E(int _num, Sprite _sprite, Val _ans1, Val _ans2, Val _ans3, Val _ans4, Val _ans5)
    {
        num = _num;
        sprite = _sprite;
        val[0] = _ans1;
        val[1] = _ans2;
        val[2] = _ans3;
        val[3] = _ans4;
        val[4] = _ans5;
    }

    // overrode
    public Value_E(int _num, Sprite _sprite, Val _ans1, Val _ans2, Val _ans3, Val _ans4, Val _ans5, Val _ans6)
    {
        num = _num;
        sprite = _sprite;
        val[0] = _ans1;
        val[1] = _ans2;
        val[2] = _ans3;
        val[3] = _ans4;
        val[4] = _ans5;
        val[5] = _ans6;
    }

    // overrode
    public Value_E(int _num, Sprite _sprite, Val _ans1, Val _ans2, Val _ans3, Val _ans4, Val _ans5, Val _ans6, Val _ans7)
    {
        num = _num;
        sprite = _sprite;
        val[0] = _ans1;
        val[1] = _ans2;
        val[2] = _ans3;
        val[3] = _ans4;
        val[4] = _ans5;
        val[5] = _ans6;
        val[6] = _ans7;
    }

    // overrode
    public Value_E(int _num, Sprite _sprite, Val _ans1, Val _ans2, Val _ans3, Val _ans4, Val _ans5, Val _ans6, Val _ans7, Val _ans8)
    {
        num = _num;
        sprite = _sprite;
        val[0] = _ans1;
        val[1] = _ans2;
        val[2] = _ans3;
        val[3] = _ans4;
        val[4] = _ans5;
        val[5] = _ans6;
        val[6] = _ans7;
        val[7] = _ans8;
    }
}

public class QuizManager_E : MonoBehaviour
{
    public static QuizManager_E instance;

    public Dictionary<int, Value_E> dictionary = new Dictionary<int, Value_E>();
    public Dictionary<int, Value_E> dictionary_temp;   // 문제를 섞기 위한 임시 변수

    int num;
    

    void Awake()
    {
        instance = this;


        Debug.Log("aaaaa");

        dictionary.Add(0, new Value_E(0, Resources.Load <Sprite>("ant"), new Val(0, "A"), new Val(1, "N"), new Val(2, "T")));

        Debug.Log("bbbbb");

        num = 1;
        dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("bag"),
            new Val(0, "B"), new Val(1, "A"), new Val(2, "G")));
        num = 2;
        dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("bean"),
            new Val(0, "B"), new Val(1, "E"), new Val(2, "A"), new Val(3, "N")));
        num = 3;
        dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("bear"),
            new Val(0, "B"), new Val(1, "E"), new Val(2, "A"), new Val(3, "R")));
        num = 4;
        dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("bird"),
            new Val(0, "B"), new Val(1, "I"), new Val(2, "R"), new Val(3, "D")));
        num = 5;
        dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("box"),
            new Val(0, "B"), new Val(1, "O"), new Val(2, "X")));
        num = 6;
        dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("car"),
            new Val(0, "C"), new Val(1, "A"), new Val(2, "R")));
        num = 7;
        dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("chair"),
            new Val(0, "C"), new Val(1, "H"), new Val(2, "A"), new Val(3, "I"), new Val(4, "R")));
        num = 8;
        dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("clothes"),
            new Val(0, "C"), new Val(1, "L"), new Val(2, "O"), new Val(3, "T"), new Val(4, "H"), new Val(5, "E"), new Val(6, "S")));
        num = 9;
        dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("cloud"),
            new Val(0, "C"), new Val(1, "L"), new Val(2, "O"), new Val(3, "U"), new Val(4, "D")));
        num = 10;
        dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("computer"),
            new Val(0, "C"), new Val(1, "O"), new Val(2, "M"), new Val(3, "P"), new Val(4, "U"), new Val(5, "T"), new Val(6, "E"), new Val(7, "R")));
        //num = 11;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("desk"),
        //    new Val(0, "D"), new Val(1, "E"), new Val(2, "S"), new Val(3, "K")));
        //num = 12;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("dog"),
        //    new Val(0, "D"), new Val(1, "O"), new Val(2, "G")));
        //num = 13;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("ear"),
        //    new Val(0, "E"), new Val(1, "A"), new Val(2, "R")));
        //num = 14;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("cat"),
        //    new Val(0, "C"), new Val(1, "A"), new Val(2, "T")));
        //num = 15;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("flower"),
        //    new Val(0, "F"), new Val(1, "L"), new Val(2, "O"), new Val(3, "W"), new Val(4, "E"), new Val(5, "R")));
        //num = 16;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("frog"),
        //    new Val(0, "F"), new Val(1, "R"), new Val(2, "O"), new Val(3, "G")));
        //num = 17;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("fruit"),
        //    new Val(0, "F"), new Val(1, "R"), new Val(2, "U"), new Val(3, "I"), new Val(4, "T")));
        //num = 18;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("glove"),
        //    new Val(0, "G"), new Val(1, "L"), new Val(2, "O"), new Val(3, "V"), new Val(4, "E")));
        //num = 19;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("hand"),
        //    new Val(0, "H"), new Val(1, "A"), new Val(2, "N"), new Val(3, "D")));
        //num = 20;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("house"),
        //    new Val(0, "H"), new Val(1, "O"), new Val(2, "U"), new Val(3, "S"), new Val(4, "E")));
        //num = 21;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("juice"),
        //    new Val(0, "J"), new Val(1, "U"), new Val(2, "I"), new Val(3, "C"), new Val(4, "E")));
        //num = 22;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("meat"),
        //    new Val(0, "M"), new Val(1, "E"), new Val(2, "A"), new Val(3, "T")));
        //num = 23;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("milk"),
        //    new Val(0, "M"), new Val(1, "I"), new Val(2, "L"), new Val(3, "K")));
        //num = 24;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("monkey"),
        //    new Val(0, "M"), new Val(1, "O"), new Val(2, "N"), new Val(3, "K"), new Val(4, "E"), new Val(5, "Y")));
        //num = 25;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("mouth"),
        //    new Val(0, "M"), new Val(1, "O"), new Val(2, "U"), new Val(3, "T"), new Val(4, "H")));
        //num = 26;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("nose"),
        //    new Val(0, "N"), new Val(1, "O"), new Val(2, "S"), new Val(3, "E")));
        //num = 27;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("orange"),
        //    new Val(0, "O"), new Val(1, "R"), new Val(2, "A"), new Val(3, "N"), new Val(4, "G"), new Val(5, "E")));
        //num = 28;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("pencil"),
        //    new Val(0, "P"), new Val(1, "E"), new Val(2, "N"), new Val(3, "C"), new Val(4, "I"), new Val(5, "L")));
        //num = 29;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("phone"),
        //    new Val(0, "P"), new Val(1, "H"), new Val(2, "O"), new Val(3, "N"), new Val(4, "E")));
        //num = 30;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("pig"),
        //    new Val(0, "P"), new Val(1, "I"), new Val(2, "G")));
        //num = 31;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("rain"),
        //    new Val(0, "R"), new Val(1, "A"), new Val(2, "I"), new Val(3, "N")));
        //num = 32;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("rice"),
        //    new Val(0, "R"), new Val(1, "I"), new Val(2, "C"), new Val(3, "E")));
        //num = 33;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("rose"),
        //    new Val(0, "R"), new Val(1, "O"), new Val(2, "S"), new Val(3, "E")));
        //num = 34;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("sea"),
        //    new Val(0, "S"), new Val(1, "E"), new Val(2, "A")));
        //num = 35;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("sky"),
        //    new Val(0, "S"), new Val(1, "K"), new Val(2, "Y")));
        //num = 36;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("snow"),
        //    new Val(0, "S"), new Val(1, "N"), new Val(2, "O"), new Val(3, "W")));
        //num = 37;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("sun"),
        //    new Val(0, "S"), new Val(1, "U"), new Val(2, "N")));
        //num = 38;
        //dictionary.Add(num, new Value_E(num, Resources.Load<Sprite>("water"),
        //    new Val(0, "W"), new Val(1, "A"), new Val(2, "T"), new Val(3, "E"), new Val(4, "R")));

        //Debug.Log("영어 딕셔너리 개수 : " + dictionary.Count + " / " + dictionary[0].sprite);


        //RandomNumber();     // 문제 랜덤섞기
    }

    // 문제 랜덤섞기 메소드
    void RandomNumber() {

        System.Random prng = new System.Random();

        dictionary_temp = new Dictionary<int, Value_E>();

        for (int i = 0; i < dictionary.Count - 1; i++) {
            int randomIndex = prng.Next(i, dictionary.Count);   // randomIndex에 랜덤 숫자가 지정
            dictionary_temp[randomIndex] = dictionary[i];
            dictionary[i] = dictionary[randomIndex];
            dictionary[randomIndex] = dictionary_temp[randomIndex];
        }
    }
}