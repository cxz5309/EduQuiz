using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChange : MonoBehaviour
{
    private GameObject player;
    public static SceneChange instance;


    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1;
        switch (SceneManager.GetActiveScene().name)
        {
            case "BasicLoding":
            case "EnglishLoding":
            case "MathLoding":
            case "OXLoding":
            case "ResultLoding":
                CanvasManager.instance.PopupChange("Loading");
                break;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player.transform.position = new Vector3(0, 1.5f, -4.66f);
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void SceneChangeToMain()
    {
        SceneManager.LoadScene("Main");
    }
    public void SceneChangeToBasic()
    {
        SceneManager.LoadScene("BasicScene");
    }
    public void SceneChangeToMath()
    {
        SceneManager.LoadScene("MathScene");
    }
    public void SceneChangeToEnglish()
    {
        SceneManager.LoadScene("EnglishScene");
    }
    public void SceneChangeToOX()
    {
        SceneManager.LoadScene("OXScene");
    }
    public void SceneChangeToResult()
    {
        SceneManager.LoadScene("ResultScene");
    }
    private void OnDestroy()
    {
        instance = null;
    }
}


