using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public bool isCounting = true;
    public int isCount = 3;
    private void Awake()
    {
        isCounting = true;
        StartCoroutine(coCountStart(isCount));
    }
    

    IEnumerator coCountStart(int count)
    {
        float time = 1;
        GetComponent<TextMeshPro>().text = count.ToString();
        GetComponent<TextMeshPro>().fontSize = 80;
        GetComponent<TextMeshPro>().color = new Color(GetComponent<TextMeshPro>().color.r, GetComponent<TextMeshPro>().color.g, GetComponent<TextMeshPro>().color.b, 1);
        while (true) { 
            if (time >= 0)
            {
                GetComponent<TextMeshPro>().fontSize -=
                (GetComponent<TextMeshPro>().fontSize / 10);
                GetComponent<TextMeshPro>().color = new Color(this.GetComponent<TextMeshPro>().color.r, this.GetComponent<TextMeshPro>().color.g, GetComponent<TextMeshPro>().color.b, this.GetComponent<TextMeshPro>().color.a - 0.1f);
            }
            else
            {
                isCount--;

                if (isCount > 0)
                {
                    time = 1;
                    StartCoroutine(coCountStart(isCount));
                    yield break;
                }
                else
                {
                    isCounting = false;
                    this.gameObject.SetActive(false);
                    yield break;
                }
            }
            time -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
