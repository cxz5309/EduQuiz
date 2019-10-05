using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public static StoreManager instance;

    public GameObject storeUI;
    public bool getStoreActive;

    private void Awake()
    {
        instance = this;
    }

    public void SetStoreActive()    // 상점 활성화 설정
    {
        if (storeUI.activeSelf == true)
        {
            Debug.Log("상점 off");
            storeUI.SetActive(false);
            getStoreActive = false;
        }
        else if (storeUI.activeSelf == false)
        {
            Debug.Log("상점 on");
            storeUI.SetActive(true);
            getStoreActive = true;
        }
    }
    private void OnDestroy()
    {
        instance = null;
    }

}
