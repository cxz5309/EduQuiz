using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChange : MonoBehaviour
{
    private GameObject player;
    public static SceneChange instance;

    public GameObject storeUI;


    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Awake()
    {
        instance = this;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player.transform.position = new Vector3(0, 2.5f, -4.66f);
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
        SceneManager.LoadScene("English");
    }
    private void OnDestroy()
    {
        instance = null;
    }



    public void SetStoreActive()    // 상점 활성화 설정
    {
        if (storeUI.activeSelf == true)
        {
            Debug.Log("상점 off");
            storeUI.SetActive(false);
        }
        else if (storeUI.activeSelf == false)
        {
            Debug.Log("상점 on");
            // 여기에 텔레포트 작동 멈추게
            storeUI.SetActive(true);
        }
    }
}


