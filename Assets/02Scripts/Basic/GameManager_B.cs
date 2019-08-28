using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_B : MonoBehaviour
{
    public static GameManager_B instance;
    public WaveManager_B waveManager;
    public GameObject EndImage;
    public GameObject StateUI;
    //public GameObject HitPanel;

    public Text QuizText;

    public GameObject effSuccess;
    public GameObject effFail;
    public Transform effSpawn;


    public enum Gamestate
    {
        GamePlaying, GamePause, GameOver, GameClear        
    }

    public Gamestate gamestate;

    void Awake()
    {
        instance = this;
        gamestate = Gamestate.GamePlaying;
        Time.timeScale = 1;
        waveManager.InitWave();
    }

    void Start()
    {
        StateUI.SetActive(true);
        // State UI 활성화
    }


    // 다시하기 버튼 메소드
    IEnumerator ButtonsOn()
    {
        yield return new WaitForSeconds(1f);
        EndImage.SetActive(true);
    }


    // 마우스 포인트 근처 타켓가져오는 메소드(미사용)
    public GameObject GetClickedObject()
    {   
        RaycastHit hit;
        GameObject target = null;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // 마우스 포인트 근처 좌표를 만든다.

        if (true == (Physics.Raycast(ray.origin, ray.direction * 10, out hit)))
        {   // 마우스 근처에 오브젝트가 있는지 확인
            target = hit.collider.gameObject;
        }
        return target;
    }

    // 스테이지 클리어 메소드
    public void GameClear()     
    {
        EnemyDestroy();
        QuizText.text = "C L E A R";
        gamestate = Gamestate.GameClear;
        StartCoroutine("ButtonsOn");
        StateUI.SetActive(false);
    }

    // 게임 오버 메소드
    public void GameOver()
    {
        QuizText.text = "Game Over";
        StartCoroutine("ButtonsOn");
        gamestate = Gamestate.GameOver;
        StateUI.SetActive(false);
    }

    // 게임 일시정지 메소드
    public void GamePause()
    {
        EndImage.SetActive(true);
        gamestate = Gamestate.GamePause;
        Time.timeScale = 0;
    }

    public void GamePauseFin()
    {
        EndImage.SetActive(false);
        gamestate = Gamestate.GamePlaying;
        Time.timeScale = 1;
    }

    // 다음 레벨 메소드
    public void NextLevel()     
    {
        EnemyDestroy();
        WaveManager_B.instance.DieEnemy();
        //Debug.Log("레벨업");
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

    public void SuccessEffect()
    {
        GameObject effect = Instantiate(effSuccess);
        effect.transform.position = effSpawn.transform.position;
        Destroy(effect, 3f);
    }

    public void FailEffect()
    {
        GameObject effect = Instantiate(effFail);
        effect.transform.position = effSpawn.transform.position;
        Destroy(effect, 3f);
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
