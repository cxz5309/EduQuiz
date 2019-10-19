using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    public GameObject Result;
    public Text resultText;

    public Transform[] starTr = new Transform[3];
    public GameObject StarPrefab;

    public int grade;
    public float maxScore;
    public float score;
    public string result;
    public int rewardGold;


    void Start()
    {
        CanvasManager.instance.PopupChange("Result");
        Result = GameObject.Find("Result");
        resultText = Result.transform.Find("Description").GetComponent<Text>();
        SetResult();
        doTextSet();
    }

    public void SetResult()
    {
        grade = DataSave.instance.grade;
        maxScore = DataSave.instance.maxScore;
        score = DataSave.instance.score;
        switch ((int)((score / maxScore) * 3f))
        {
            case 0:
                result = "나쁘지 않아요!";
                rewardGold = 10;
                break;
            case 1:
                result = "좋아요!";
                rewardGold = 20;
                break;
            case 2:
                result = "최고에요!";
                rewardGold = 30;
                break;
        } 
    }

    public void doTextSet()
    {
        string text = "학년 : " + grade + "\n점수 : " + score + "/" + maxScore + "\n" + rewardGold + "골드 획득!" + "\n당신의 평가는...\n" + result;
        resultText.DOText(text, 5f).OnComplete(() =>
        {
            SetStar();
        });
    }

    public void SetStar()
    {
        DataSave.instance.data.AddGold(rewardGold);

        StartCoroutine(coSetStar(rewardGold/10));
        StartCoroutine(coEnd());
    }
    IEnumerator coSetStar(float loop)
    {
        for (int i = 0; i < loop; i++)
        {
            Instantiate(StarPrefab, starTr[i]);
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator coEnd()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
