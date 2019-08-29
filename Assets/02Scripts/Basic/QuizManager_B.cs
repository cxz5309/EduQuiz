using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Value_B
{
    public int num;         // 문제 번호
    public string quiz;     // 문제 지문
    public string[] ans = new string[4];    // 답1, 2, 3, 4
    public int pass;    // 정답 0번자리

    public Value_B(int _num, string _quiz, string _ans1, string _ans2, string _ans3, string _ans4, int _pass)
    {
        this.num = _num;
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

    public Dictionary<int, Value_B> dictionary;
    public Dictionary<int, Value_B> dictionary_temp;   // 문제를 섞기 위한 임시 변수

    void Awake()
    {
        instance = this;

        dictionary = new Dictionary<int, Value_B>();


        dictionary.Add(0, new Value_B(0,
            "나는 캄캄한 (  )에 등불도 없이 걸어가다 넘어져 무릎을 다쳤다.",
            "밤", "아침", "낮", "해질녁", 0));
        dictionary.Add(1, new Value_B(1,
                    "반대쪽으로 틀다",
                    "되틀다", "배틀다", "외틀다", "안틀다", 0));
        dictionary.Add(2, new Value_B(2,
                    "그릇 위까지 수북하게 담은 밥",
                    "감투밥", "구메밥", "고두밥", "가윗밥", 0));
        dictionary.Add(3, new Value_B(3,
                    "남쪽에서 부는 바람을 이르는 말",
                    "마파람", "샛바람", "하늬바람", "된바람", 0));
        dictionary.Add(4, new Value_B(4,
                    "'옆에서'를 정확하게 소리내어 읽은 것은?",
                    "여페서", "옆해서", "여베서", "엽해서", 0));
        dictionary.Add(5, new Value_B(5,
                    "'받았어'를 정확하게 소리내어 읽은 것은?",
                    "바다써", "받아써", "바닸어", "밨아써", 0));
        dictionary.Add(6, new Value_B(6,
                    "'앞으로'를 정확하게 소리내어 읽은 것은?",
                    "아프로", "앞프로", "압으로", "압프로", 0));
        dictionary.Add(7, new Value_B(7,
            "알맞은 문장 부호를 만들어 보세요." +
            "구현아, 무슨 노래를 부르니( )",
            "?", ".", ",", "!", 0));
        dictionary.Add(8, new Value_B(8,
                    "어름치는 (  )에 사는 동물입니다.",
                    "강", "바다", "산", "호수", 0));
        dictionary.Add(9, new Value_B(9,
                    "몸이 굳게 억세다",
                    "걱세다", "까세다", "꺽세다", "검세다", 0));


        RandomNumber();     // 문제 랜덤섞기
    }

    //문제 랜덤섞기 메소드
    void RandomNumber()
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
