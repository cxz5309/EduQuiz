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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "GamePotal")
        {
            currentPlace = "GamePotal";
            PopupManager.instance.PopupChange();
        }
        else if (collision.gameObject.tag == "StorePotal")
        {
            currentPlace = "StorePotal";
            PopupManager.instance.PopupChange();
        }
    }
}
