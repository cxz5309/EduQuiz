using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyInfo_E : MonoBehaviour
{
    public GameObject spawnEffect;  // 적이 생성되면 발생하는 이펙트
    public GameObject HitEffect;    // 적이 죽으면 발생하는 이펙트
    public GameObject Player;

    public string dieSound = "MonsterDie";
    private AudioManager theAudio;

    int order;


    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        Player = GameObject.FindGameObjectWithTag("Player");

        GameObject spawn = Instantiate(spawnEffect, transform.position, transform.rotation);        // 적 스폰 이펙트 메소드 호출
        Destroy(spawn, 5.0f);       // spawn 오브젝트 제거
    }
    
    void Update()
    {
        transform.LookAt(Player.transform);     // 적은 플레이어 방향을 바라봄.
    }
    
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {   // 충돌한 오브젝트의 태그가 Bullet인 경우
            Destroy(coll.gameObject);   // 총알 제거

            if (WaveManager.instance.EnemyKillCnt == WaveManager.instance.EnemyCount - 1)
            {   // 해당 웨이브 몬스터 수만큼 적을 죽였다면 스테이지 클리어
                DamageIffect();     // 적 제거

                GameManager.instance.SuccessEffect();       // 성공 이펙트


                if (WaveManager.instance.curWave < QuizManager.instance.dictionary.Count - 1)
                {   // 문제가 더 남아있을 때
                    WaveManager.instance.EnemyKillCnt = 0;
                    // KillCount 초기화
                    WaveManager.instance.WaveDelayStart();
                }
                else
                {   // 모든 문제를 풀었을 때
                    //GameManager.instance.GameClear();
                }
            }

            else if(WaveManager.instance.EnemyKillCnt != WaveManager.instance.EnemyCount)
            { 
                if (WaveManager.instance.EnemyKillCnt == isRightOrder())
                {   // 정답일 때
                    theAudio.Play(dieSound);
                    WaveManager.instance.EnemyKillCnt++;      // 적 죽인 수 ++
                    DamageIffect();         // 적 제거
                }
                else if (WaveManager.instance.EnemyKillCnt != isRightOrder())
                {   // 오답일 때
                    //Sound.instance.InCorrect();
                    SliderController.instance.ChangeSliderValue(-20f);// 답이 틀릴때마다 타이머 4초씩 감소
                }
            }
        }
    }


    public void InitEnemyInfo(Enemy enemy)
    {
        order = enemy.order;
        //transform.position = enemy.spawnPos;
        this.transform.Find("TextMesh").GetComponent<TextMesh>().text = enemy.meshNum;
        gameObject.SetActive(true);
    }

    
    public void DamageIffect()     // 총알에 맞은 게임오브젝트 삭제하는 메소드
    {
        gameObject.SetActive(false);
        GameObject hit = Instantiate(HitEffect);    // hit GameObject 변수에 HitEffect를 충돌 위치에 생성한다.
        hit.transform.position = gameObject.transform.position;
        hit.transform.eulerAngles = new Vector3(-90, 0, 0);
        Destroy(hit, 5.0f);      // hit 오브젝트를 제거한다.

    }


    public int isRightOrder()      // 답이 아닌지 맞는지 리턴해주는 메소드
    {
        return order;
    }
    
}
