using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    public GameObject unicon;

    public GameObject GameUI;
    public GameObject GradeUI;
    public GameObject LevelUI;

    public string themeSound;
    private AudioManager theAudio;
    


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        theAudio.Play(themeSound);

        if (!GameObject.FindWithTag("Unicon"))
        {
            Instantiate(unicon);        // 유니콘 소환!
        }

        PlayerManager.instance.currentPopup = "Idle";
        UniconManager.instance.PopupChange();
    }

    public void OpenGrade()
    {
        GameUI.SetActive(false);
        GradeUI.SetActive(true);
    }
    public void OpenLevel()
    {
        GradeUI.SetActive(false);
        LevelUI.SetActive(true);
    }

    private void OnDestroy()
    {
        theAudio.Stop(themeSound);
        instance = null;
    }
}
