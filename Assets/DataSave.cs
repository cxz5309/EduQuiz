using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo
{
    private string name;
    private int id;
    private int password;
}

public class LevelInfo
{
    public string summary; //0:국어, 1:수학, 2:영어
    public int grade;
    public int season;
    public int level;

    public LevelInfo(string summary, int grade, int season, int level)
    {
        this.summary = summary;
        this.grade = grade;
        this.season = season;
        this.level = level;
    }
}

public class DataSave : MonoBehaviour
{
    public static DataSave instance;

    public UserInfo userInfo;
    public LevelInfo basicLevelInfo;
    public LevelInfo mahtLevelInfo;
    public LevelInfo englishLevelInfo;

    public int gold;
    public int nowWeapon;
    public bool[] availableWeapon;
    public int[] availableHouseParts;//0 : 사용불가, 1 : 사용가능, 2 : 사용됨


    private void Awake()
    {
        instance = this;
    }


    private void OnDestroy()
    {
        instance = null;
    }
}
