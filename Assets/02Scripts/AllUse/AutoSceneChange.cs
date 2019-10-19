using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoSceneChange : MonoBehaviour
{
    private void Awake()
    {
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
            case "OXLoding":
                StartCoroutine(OXChangeScene());
                break;
            case "ResultLoding":
                StartCoroutine(ResultChangeScene());
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
    IEnumerator OXChangeScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("OXScene");
    }
    IEnumerator ResultChangeScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("ResultScene");
    }
    IEnumerator StoreChangeScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("StoreScene");
    }
}

