using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//public class UserInfo
//{
//    private int id;
//    private string name;
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
    public int nowWeapon = 0;
    private int[] availableWeapon = new int[25]; //0 : 사용불가, 1 : 사용가능
    private int[] availableHousing = new int[25]; //0 : 사용불가, 1 : 사용가능, 2 : 사용됨
    public enum ItemType
    {
        Weapon, Housing
    }

    //기본 생성자, logo씬 awake시(캐릭터자체에 달려있는 DataSave awake시) db에 있는 정보 받아오기
    public Data()
    {
        var inventoryData = DataService.Instance.GetData<Table.Data>(1);

        this.gold = inventoryData.gold;
        this.nowWeapon = inventoryData.now_weapon;
        Debug.Log("최초골드:" + this.gold);
        Debug.Log("현재 웨폰:" + this.nowWeapon);
        for (int i=0; i < inventoryData.available_weapon.Length; i++)
        {
            this.availableWeapon[i] = int.Parse(inventoryData.available_weapon[i].ToString());
            if (availableWeapon[i] == 1)
                Debug.Log("이용가능한 웨폰번호 : " + (i+1));
        }
        for(int i = 0; i < inventoryData.available_housing.Length; i++)
        {
            this.availableHousing[i] = int.Parse(inventoryData.available_housing[i].ToString());
            if (availableWeapon[i] == 1)
                Debug.Log("이용가능한 하우징번호 : " + (i+1));
        }
    }

    public Data(int gold, int nowWeapon)
    {
        this.nowWeapon = nowWeapon;
        this.gold = gold;
    }

    public void AddGold(int getGold)
    {
        gold += getGold;
        Debug.Log("gold : " + gold);
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
        availableWeapon[weaponNum] = 1;
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
        data = new Data();
    }

    private void Start()
    {
        
    }
    
    
    private void OnDestroy()
    {
        instance = null;
    }
}
