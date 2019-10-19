using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonToggle : MonoBehaviour
{
    public GameObject on;
    public GameObject off;


    
    void Start()
    {
        SetSoundButton();
    }
    
    void Update()
    {
        
    }

    public void SetSoundButton()
    {
        if (gameObject.name == "Music")
        {
            if (AudioManager.instance.isBackgroundSound == true)
            {
                on.SetActive(true);
            }
            else if (AudioManager.instance.isBackgroundSound == false)
            {
                off.SetActive(true);
            }
        }
        else if(gameObject.name == "Effect")
        {
            if (AudioManager.instance.isEffectSound == true)
            {
                on.SetActive(true);
            }
            else if (AudioManager.instance.isEffectSound == false)
            {
                off.SetActive(true);
            }
        }
    }

    public void SetButtonToggle()
    {
        if (on.activeSelf == true)
        {
            on.SetActive(false);
            off.SetActive(true);
        }
        else if (on.activeSelf == false)
        {
            on.SetActive(true);
            off.SetActive(false);
        }
    }
}
