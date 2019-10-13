using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;


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
            CanvasManager.instance.PopupChange("GamePotal");
        }
        else if (other.gameObject.tag == "StorePotal")
        {
            CanvasManager.instance.PopupChange("StorePotal");
        }
    }
}
