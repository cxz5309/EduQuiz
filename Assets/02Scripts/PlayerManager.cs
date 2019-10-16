using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    //여기는 플레이어 상태 초기화하는 장소
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
            CanvasManager.instance.PopupChange("Level");
        }
        else if (other.gameObject.tag == "StorePotal")
        {
            CanvasManager.instance.PopupChange("StorePotal");
        }
    }
}
