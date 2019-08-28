using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class LogoMaker : MonoBehaviour
{

    public Image LogoColor;
    // Start is called before the first frame update

    void Start()
    {
        StartCoroutine(ChangeScene());
        hitColor();
    }

    public void hitColor()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(LogoColor.DOColor(new Color(LogoColor.color.r, LogoColor.color.g, LogoColor.color.b, 0.9f), 2f));
        mySequence.Append(LogoColor.DOColor(new Color(LogoColor.color.r, LogoColor.color.g, LogoColor.color.b, 0), 2f));
    }
    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("Main");
    }
}