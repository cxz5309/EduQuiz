using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager instance;

    public GameObject[] popup;

    private string popupChangeSound;
    private AudioManager theAudio;
    

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
    }
    


    public void PopupChange()
    {
        Debug.Log("popup 체인지");
        for (int i = 0; i < popup.Length; i++)      // 현재 팜업 끄기
        {
            popup[i].SetActive(false);
        }

        for (int i = 0; i < popup.Length; i++)      // 현재 팜업 켜기
        {
            if (popup[i].name == PlayerManager.instance.currentPlace)
            {
                popup[i].SetActive(true);
                theAudio.Play(popupChangeSound);
                return;
            }
        }
    }
}
