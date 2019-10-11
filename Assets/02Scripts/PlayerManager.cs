using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public string currentPlace = "";


    void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GamePotal")
        {
            currentPlace = "GamePotal";
            PopupManager.instance.PopupChange();

            Debug.Log("플레이어 GamePotal");
        }
        else if (other.gameObject.tag == "StorePotal")
        {
            currentPlace = "StorePotal";
            PopupManager.instance.PopupChange();
            Debug.Log("플레이어 StorePotal");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
