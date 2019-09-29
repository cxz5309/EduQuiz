using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//public class UserInfo
//{
//    private string name;
//    private int id;
//    private int password;
//}

//public class LevelInfo
//{
//    public string summary; //0:국어, 1:수학, 2:영어
//    public int grade;
//    public int season;
//    public int level;

//    public LevelInfo(string summary, int grade, int season, int level)
//    {
//        this.summary = summary;
//        this.grade = grade;
//        this.season = season;
//        this.level = level;
//    }
//}

public class DataSave : MonoBehaviour
{
    public static DataSave instance;

    //public UserInfo userInfo;
    //public LevelInfo basicLevelInfo;
    //public LevelInfo mahtLevelInfo;
    //public LevelInfo englishLevelInfo;

    private int gold = 0;
    private int nowWeapon;
    private bool[] availableWeapon;
    private int[] availableHouseParts;//0 : 사용불가, 1 : 사용가능, 2 : 사용됨

    public int Gold { get => gold; set => gold = value; }

    private void Awake()
    {
        instance = this;
    }
    
    private void Start()
    {
    }

    public void AddGold(int getGold)
    {
        this.Gold += getGold;
        Debug.Log(Gold);
    }


    private void OnDestroy()
    {
        instance = null;
    }
}
