using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public static StoreManager instance;

    public GameObject AnimalstoreScreen;
    public GameObject WeaponstoreScreen;
    public GameObject buyScreen;
    public GameObject storeSelectScreen;
    public GameObject NoticeScreen;
    public string storeSelectName;
    public GameObject clickText;
    public bool getStoreActive;
    public bool getBuyActive;

    public GameObject ThisScreen;
    public SelectBuyWeapon ThisSelectBuyWeapon;

    public GameObject unicon;
    public GameObject uniconCanvas;
    public GameObject uniconObject;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        unicon = GameObject.Find("Unicon");
        uniconCanvas = unicon.transform.Find("UniconCanvas").gameObject;
        uniconObject = unicon.transform.Find("UniconObject").gameObject;
        uniconCanvas.gameObject.SetActive(false);
        uniconObject.gameObject.SetActive(false);
    }

    public void SetStoreSelect()
    {
        if(storeSelectScreen.activeSelf == false)
            storeSelectScreen.SetActive(true);
        else
            storeSelectScreen.SetActive(false);
    }

    public void SetStoreActive(string name)    // 상점 활성화 설정
    {
        storeSelectName = name;
        storeSelectScreen.SetActive(false);
        if (name == "Animal")
        {
            ThisScreen = AnimalstoreScreen;
        }
        else if (name == "Weapon")
        {
            ThisScreen = WeaponstoreScreen;
        }
        else
        {
            Debug.Log("screen 할당되지 않음");
            return;
        }
        ThisSelectBuyWeapon = ThisScreen.GetComponent<SelectBuyWeapon>();
        if (ThisScreen.activeSelf == true)
        {
            ThisScreen.SetActive(false);
            getStoreActive = false;
            clickText.SetActive(true);
        }
        else if (ThisScreen.activeSelf == false)
        {
            ThisScreen.SetActive(true);
            getStoreActive = true;
            clickText.SetActive(false);
        }
    }

    public void SetBuyActive()      // 구매 화면 활성화 설정
    {
        if (buyScreen.activeSelf == true)
        {
            buyScreen.SetActive(false);
            getBuyActive = false;
        }
        else if (buyScreen.activeSelf == false)
        {
            buyScreen.SetActive(true);
            getBuyActive = true;
        }
    }
    public void SetNotice()
    {
        if (NoticeScreen.activeSelf == false)
        {
            NoticeScreen.SetActive(true);
        }
        else
        {
            buyScreen.SetActive(false);
            getBuyActive = false;
            NoticeScreen.SetActive(false);
        }
    }

    public void ThisScreenLeft()
    {
        ThisSelectBuyWeapon.Left();
    }
    public void ThisScreenRight()
    {
        ThisSelectBuyWeapon.Right();
    }
    public int GetThisScreenIndex()
    {
        return ThisSelectBuyWeapon.chapIndex;
    }
    private void OnDestroy()
    {
        instance = null;
    }

}
