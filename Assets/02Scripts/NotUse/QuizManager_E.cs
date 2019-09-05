//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.IO;
//using UnityEngine.UI;


//public class Val
//{
//    public int cnt;
//    public string alpha;
    
//    public Val(int _cnt, string _alpha)
//    {
//        cnt = _cnt;
//        alpha = _alpha;
//    }
//}

//public class Value_E
//{
//    public string quiz;     // 문제 지문
//    public Sprite sprite;

//    public Val[] val = new Val[10];
//    public Value_E(Sprite _sprite, string _ans)
//    {
//        sprite = _sprite;
//        for(int i = 0; i < _ans.Length; i++)
//        {
//            val[i] = new Val(i, _ans[i].ToString());
//        }
//    }
//}

//public class QuizManager_E : MonoBehaviour
//{
//    public static QuizManager_E instance;

//    public Dictionary<int, Value_E> dictionary = new Dictionary<int, Value_E>();
//    public Dictionary<int, Value_E> dictionary_temp;   // 문제를 섞기 위한 임시 변수
    
    

//    // 문제 랜덤섞기 메소드
//    void RandomNumber() {

//        System.Random prng = new System.Random();

//        dictionary_temp = new Dictionary<int, Value_E>();

//        for (int i = 0; i < dictionary.Count - 1; i++) {
//            int randomIndex = prng.Next(i, dictionary.Count);   // randomIndex에 랜덤 숫자가 지정
//            dictionary_temp[randomIndex] = dictionary[i];
//            dictionary[i] = dictionary[randomIndex];
//            dictionary[randomIndex] = dictionary_temp[randomIndex];
//        }
//    }
//}