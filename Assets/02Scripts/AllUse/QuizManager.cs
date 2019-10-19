using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Value
{
    public string quiz;     // 문제 지문
    public string[] ans;   // 답

    public Sprite sprite;//영어 그림
    public string[] alphas = new string[10];//영어 알파벳

    public int pass;    // 정답 0번자리
    public Value(string _quiz, string _pass)
    {
        ans = new string[2];
        this.quiz = _quiz;
        this.ans[0] = "O";
        this.ans[1] = "X";
        if(_pass == "0")
            this.pass = 0;
        else
            this.pass = 1;
    }
    public Value(string _quiz, string _ans1, string _ans2, string _ans3, string _ans4, int _pass)
    {
        ans = new string[4];
        this.quiz = _quiz;
        this.ans[0] = _ans1;
        this.ans[1] = _ans2;
        this.ans[2] = _ans3;
        this.ans[3] = _ans4;
        this.pass = _pass;
    }
    public Value(int _pass, string _ans1, string _ans2, string _ans3, string _ans4, string _ans5, string _ans6)
    {
        ans = new string[6];

        this.pass = _pass;
        this.ans[0] = _ans1;
        this.ans[1] = _ans2;
        this.ans[2] = _ans3;
        this.ans[3] = _ans4;
        this.ans[4] = _ans5;
        this.ans[5] = _ans6;
    }
    public Value(Sprite _sprite, string _ans)
    {

        sprite = _sprite;
        for (int i = 0; i < _ans.Length; i++)
        {
            alphas[i] = _ans[i].ToString();
        }
    }
}
public class QuizManager : MonoBehaviour
{
    public static QuizManager instance;

    public Dictionary<int, Value> dictionary;
    public Dictionary<int, Value> dictionary_temp;   // 문제를 섞기 위한 임시 변수

    public int grade;

    void Awake()
    {
        instance = this;
        grade = DataSave.instance.grade;
        LoadMap();
        RandomNumber();     // 문제 랜덤섞기
    }

    public void LoadMap()
    {
        Debug.Log("이거시 언제쯤 시작될까");
        dictionary = new Dictionary<int, Value>();
        TextAsset data;
        StringReader strReader;
        string source;

        switch (SceneManager.GetActiveScene().name)
        {
            case "BasicScene":
                data = Resources.Load("BasicDatas" + grade.ToString(), typeof(TextAsset)) as TextAsset;
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
                data = Resources.Load("MathDatas" + grade.ToString(), typeof(TextAsset)) as TextAsset;
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

            case "EnglishScene":
                data = Resources.Load("EnglishDatas" + grade.ToString(), typeof(TextAsset)) as TextAsset;
                strReader = new StringReader(data.text);
                source = strReader.ReadLine();

                while (source != null)
                {
                    string[] words = source.Split(',');

                    dictionary.Add(int.Parse(words[0]), new Value(Resources.Load<Sprite>("EnglishIMG/" + words[1]), words[1].ToUpper()));
                    source = strReader.ReadLine();
                }
                strReader.Close();
                break;

            case "OXScene":
                data = Resources.Load("OXDatas" + grade.ToString(), typeof(TextAsset)) as TextAsset;
                strReader = new StringReader(data.text);
                source = strReader.ReadLine();
                Debug.Log(source);

                while (source != null)
                {
                    string[] words = source.Split(',');

                    dictionary.Add(int.Parse(words[0]), new Value(words[1], words[2]));
                    source = strReader.ReadLine();
                }
                strReader.Close();
                break;
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
    private void OnDestroy()
    {
        instance = null;
    }
}
