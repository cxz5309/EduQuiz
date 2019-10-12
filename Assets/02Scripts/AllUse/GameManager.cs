using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject ResultUI;
    //public GameObject HitPanel;

    public GameObject CountText;
    public GameObject StageStateText;
    public GameObject GameStateText;
    
    public string themeSound;
    public string successSound = "Success";
    public string failSound = "Fail";
    private AudioManager theAudio;


    public enum Gamestate
    {
        GamePlaying, GamePause, GameOver, GameClear        
    }

    public Gamestate gamestate;

    void Awake()
    {
        instance = this;

        switch (SceneManager.GetActiveScene().name)     // 씬 바뀔때 팝업 변경해주기
        {
            case "BasicScene":
                PlayerManager.instance.currentPopup = "Basic";
                break;
            case "MathScene":
                PlayerManager.instance.currentPopup = "Math";
                break;
            case "EnglishScene":
                PlayerManager.instance.currentPopup = "English";
                break;
            case "OXScene":
                PlayerManager.instance.currentPopup = "OX";
                break;
        }
        UniconManager.instance.PopupChange();
    }

    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        theAudio.Play(themeSound);

        WaveManager.instance.InitWave();
        gamestate = Gamestate.GamePlaying;
        Time.timeScale = 1;
        // State UI 활성화
    }
    // 결과 버튼 메소드
    IEnumerator coResultButtonsOn()
    {
        yield return new WaitForSeconds(2.5f);
        ResultUI.SetActive(true);
    }

    // 다시하기 버튼 메소드
    IEnumerator coMenuButtonsOn()
    {
        yield return new WaitForSeconds(10f);
        ResultUI.SetActive(false);
        Time.timeScale = 0;
    }
    
    public void MenuButtonsOn()
    {
        StartCoroutine(coMenuButtonsOn());
    }

    // 스테이지 클리어 메소드
    public void GameClear()
    {
        ItemManager.instance.itemSpawnStop = true;
        EnemyDestroy();
        ItemDestroy();
        DataSave.instance.data.AddGold(1);
        GameStateText.SetActive(true);
        GameStateText.GetComponent<TextMeshPro>().text = "GameClear";
        GameStateText.GetComponent<Animator>().SetTrigger("GameClear");
            SliderController.instance.WaitSlider();
        gamestate = Gamestate.GameClear;
        StartCoroutine("coResultButtonsOn");
    }

    // 게임 오버 메소드
    public void GameOver()
    {
        ItemManager.instance.itemSpawnStop = true;
        EnemyDestroy();
        ItemDestroy();
        GameStateText.SetActive(true);
        GameStateText.GetComponent<TextMeshPro>().text = "GameOver";
        GameStateText.GetComponent<Animator>().SetTrigger("GameOver");
        SliderController.instance.WaitSlider();
        StartCoroutine("coResultButtonsOn");
        gamestate = Gamestate.GameOver;
    }

    // 게임 일시정지 메소드
    public void GamePause()
    {
        PlayerManager.instance.currentPopup = "Pause";
        UniconManager.instance.PopupChange();

        gamestate = Gamestate.GamePause;
        Time.timeScale = 0;
    }

    public void GamePauseFin()
    {
        switch (SceneManager.GetActiveScene().name)     // 씬마다 다르게 활성화 해주기
        {
            case "BasicScene":
                PlayerManager.instance.currentPopup = "Basic";
                break;
            case "MathScene":
                PlayerManager.instance.currentPopup = "Math";
                break;
            case "EnglishScene":
                PlayerManager.instance.currentPopup = "English";
                break;
            case "OXScene":
                PlayerManager.instance.currentPopup = "OX";
                break;
        }
        UniconManager.instance.PopupChange();

        Time.timeScale = 1;
        gamestate = Gamestate.GamePlaying;
    }

    // 다음 레벨 메소드
    public void NextLevel()     
    {
        EnemyDestroy();
        WaveManager.instance.StartWave();
        // WaveDelay를 true로 주어 3초간 딜레이를 준다.
                
    }
    
    public void GameClose()
    {
        Application.Quit();
    }

    // 모든 적을 파괴하는 메소드
    public void EnemyDestroy()
    {
        GameObject[] allCube = GameObject.FindGameObjectsWithTag("enemy");
        // allCube에 "enemy" 태그를 가지는 오브젝트 전부 넣어줌.
        foreach (GameObject i in allCube)
        {
            Destroy(i.gameObject);
            // 나머지 적들 파괴
        }
    }

    public void ItemDestroy()
    {
        GameObject[] allHP = GameObject.FindGameObjectsWithTag("HP");
        // allCube에 "enemy" 태그를 가지는 오브젝트 전부 넣어줌.
        foreach (GameObject i in allHP)
        {
            Destroy(i.gameObject);      // 나머지 적들 파괴
        }
        GameObject[] allTimer = GameObject.FindGameObjectsWithTag("Timer");
        // allCube에 "enemy" 태그를 가지는 오브젝트 전부 넣어줌.
        foreach (GameObject i in allTimer)
        {
            Destroy(i.gameObject);        // 나머지 적들 파괴
        }
    }

    public void SuccessEffect()     // 성공 이펙트
    {
        theAudio.Play(successSound);
        StageStateText.SetActive(true);
        StageStateText.GetComponent<TextMeshPro>().text = "Success";
        StageStateText.GetComponent<Animator>().SetTrigger("Success");
    }

    public void FailEffect()        // 실패 이펙트
    {
        theAudio.Play(failSound);
        StageStateText.SetActive(true);
        StageStateText.GetComponent<TextMeshPro>().text = "Fail";
        StageStateText.GetComponent<Animator>().SetTrigger("Fail");
    }

    private void OnDestroy()
    {
        theAudio.Stop(themeSound);
        instance = null;
    }
}