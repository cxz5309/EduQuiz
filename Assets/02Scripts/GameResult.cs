using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GameResult : MonoBehaviour
{
    public GameObject Result0;
    public GameObject Result1;
    public GameObject StarPrefab;

    public int grade;
    public int level;
    public float allQuiz;
    public float successQuiz ;
    public float score;
    public string scoreChk;

    public Transform[] Tr = new Transform[3];


    private void Start()
    {
        Result0.GetComponent<Text>().DOText(DoUserInfo(grade, level), 1f, false).SetEase(Ease.Linear).OnComplete(() => {
            Result1.GetComponent<Text>().DOText(DoScore(score, scoreChk), 3f, false).OnComplete(() =>
            {
                SetStar();
                GameManager.instance.MenuButtonsOn();
            });
        });
    }

    public string DoUserInfo(int grade, int level)
    {
        string userInfoText;
        userInfoText = "\t\t학년 : " + grade + "\n\t\t난이도: " + level;
        return userInfoText;
    }

    public string DoScore(float score, string scoreChk) {
        string scoreText;
        scoreText = "\t\t모든 문제:" + allQuiz + "\n\t\t내가 푼 문제:" + successQuiz + "\n\t\t총점:" + score.ToString() + "\n\t\t" + scoreChk + "!!";
        return scoreText;
    }
    public void SetStar()
    {
        StartCoroutine(coSetStar(score));
    }
    IEnumerator coSetStar(float score_)
    {
        for(int i = 0; i < (int)(score_ / 33.3f); i++)
        {
            Instantiate(StarPrefab, Tr[i]);
            yield return new WaitForSeconds(1f);
        }
    }
}
