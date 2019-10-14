using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public static StoreManager instance;

    public GameObject storeScreen;
    public GameObject buyScreen;
    public GameObject clickText;
    public bool getStoreActive;
    public bool getBuyActive;


    private void Awake()
    {
        instance = this;
    }

    public void SetStoreActive()    // 상점 활성화 설정
    {
        if (storeScreen.activeSelf == true)
        {
            storeScreen.SetActive(false);
            getStoreActive = false;
            clickText.SetActive(true);
        }
        else if (storeScreen.activeSelf == false)
        {
            storeScreen.SetActive(true);
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

    private void OnDestroy()
    {
        instance = null;
    }

}
