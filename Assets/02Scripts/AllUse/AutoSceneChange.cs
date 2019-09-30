using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoSceneChange : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1;
    }
    void Start()
    {
        switch (SceneManager.GetActiveScene().name) {
            case "BasicLoding":
                StartCoroutine(BasicChangeScene());
                break;
            case "MathLoding":
                StartCoroutine(MathChangeScene());
                break;
            case "EnglishLoding":
                StartCoroutine(EnglishChangeScene());
                break;
            case "StoreLoding":
                StartCoroutine(StoreChangeScene());
                break;

        }
    }

    IEnumerator BasicChangeScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("BasicScene");
    }
    IEnumerator MathChangeScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MathScene");
    }
    IEnumerator EnglishChangeScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("EnglishScene");
    }
    IEnumerator StoreChangeScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("StoreScene");
    }
}

