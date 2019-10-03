using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public static StoreManager instance;

    public GameObject storeUI;

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
        }
        else if (storeUI.activeSelf == false)
        {
            Debug.Log("상점 on");
            // 여기에 텔레포트 작동 멈추게
            storeUI.SetActive(true);
        }
    }
    private void OnDestroy()
    {
        instance = null;
    }

}
