using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    public GameObject GameUI;
    public GameObject GradeUI;
    public GameObject LevelUI;

    public string themeSound;
    private AudioManager theAudio;

    public Text text;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        theAudio.Play(themeSound);
    }

    private void Update()
    {
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
