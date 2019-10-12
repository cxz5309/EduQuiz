using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public string currentPopup = "";


    void Awake()
    {
        instance = this;
    }



    public void BackGroundSound()
    {

    }

    public void EffectSound()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GamePotal")
        {
            currentPopup = "GamePotal";
            UniconManager.instance.PopupChange();
        }
        else if (other.gameObject.tag == "StorePotal")
        {
            currentPopup = "StorePotal";
            UniconManager.instance.PopupChange();
        }
    }
}
