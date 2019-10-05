using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;

    public GameObject GameUI;
    public GameObject GradeUI;
    public GameObject LevelUI;

    private void Awake()
    {
        instance = this;
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
        instance = null;
    }
}
