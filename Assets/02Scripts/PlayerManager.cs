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
        switch (other.gameObject.tag)
        {
            case "GamePotal":
                CanvasManager.instance.PopupChange("Level");
                break;
            case "StorePotal":
                CanvasManager.instance.PopupChange("StorePotal");
                break;
            case "AnimalPotal":
                CanvasManager.instance.PopupChange("Animal");
                break;

            case "InventoryPotal":
                MainManager.instance.InventoryOn();
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "GamePotal":
            case "StorePotal":
            case "AnimalPotal":
                CanvasManager.instance.PopupChange("Idle");
                break;

            case "InventoryPotal":
                MainManager.instance.InventoryOn();
                break;
        }
    }
}
