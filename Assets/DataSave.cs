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

public class Data
{
    private int gold = 0;
    public int nowWeapon;
    private bool[] availableWeapon;
    private int[] availableHousing; //0 : 사용불가, 1 : 사용가능, 2 : 사용됨
    public enum ItemType
    {
        Weapon, Housing
    }

    public int Gold { get => gold; set => gold = value; }

    public Data(int Gold, int nowWeapon)
    {
        this.nowWeapon = nowWeapon;
        this.Gold = Gold;
    }

    public void AddGold(int getGold)
    {
        Gold += getGold;
        Debug.Log(Gold);
    }

    public void Charge(int nowGold, int chargeGold, ItemType itemType, int itemNum)
    {
        if (nowGold < chargeGold) {
            Debug.Log("구매 불가");
        }
        else {
            switch (itemType)
            {
                case ItemType.Weapon:
                    ChargeWeapon(chargeGold, itemNum);
                    break;
                case ItemType.Housing:
                    ChargeHousing(chargeGold, itemNum);
                    break;
            }
        }
    }
    void ChargeWeapon(int chargeGold, int weaponNum)
    {
        availableWeapon[weaponNum] = false;
    }
    void ChargeHousing(int chargeGold, int housingNum)
    {
        availableHousing[housingNum] = 1;
    }
    public void ChangeWeaponData(int weaponNum)
    {
        nowWeapon = weaponNum;
    }
    public void SetHousingData(int housingNum)
    {
        availableHousing[housingNum] = 2;
    }
}


public class DataSave : MonoBehaviour
{
    public static DataSave instance;
    public Data data;

    //public UserInfo userInfo;
    //public LevelInfo basicLevelInfo;
    //public LevelInfo mahtLevelInfo;
    //public LevelInfo englishLevelInfo;


    private void Awake()
    {
        instance = this;
        data = new Data(0, 3);
    }

    private void Start()
    {
        
    }
    
    
    private void OnDestroy()
    {
        instance = null;
    }
}
