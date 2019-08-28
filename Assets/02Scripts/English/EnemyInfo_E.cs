using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy_E
{
    public string name;             // 이름 ( 프리팹 이름 )
    public int result;             // 정답 유무
    public Val meshNum;

    public Enemy_E(string _name, int _result, Val _meshNum)
    {
        this.name = _name;
        this.result = _result;
        this.meshNum = _meshNum;
    }
}

[RequireComponent (typeof (NavMeshAgent))]
public class EnemyInfo_E : MonoBehaviour
{
    public GameObject spawnEffect;  // 적이 생성되면 발생하는 이펙트
    public GameObject HitEffect;    // 적이 죽으면 발생하는 이펙트
    public GameObject Player;

    int result;


    void Start()
    {
        GameObject spawn = Instantiate(spawnEffect, transform.position, transform.rotation);
        // 적 스폰 이펙트 메소드 호출
        Destroy(spawn, 1.0f);
        // spawn 오브젝트 제거
    }
    
    void Update()
    {
        transform.LookAt(Player.transform);
        // 적은 플레이어 방향을 바라봄.
    }


    // 충돌 처리하는 메소드
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {   // 충돌한 오브젝트의 태그가 Bullet인 경우
            Destroy(coll.gameObject);   
            // 총알 제거  


            Debug.Log("현재 EnemyKillCount : " + WaveManager_E.instance.EnemyKillCnt + " / " + "isRightResult() : " + isRightResult());
            if (WaveManager_E.instance.EnemyKillCnt == WaveManager_E.instance.EnemyCount - 1)
            {   // 해당 웨이브 몬스터 수만큼 적을 죽였다면 스테이지 클리어
                getDamage();
                // 적 제거
                if (WaveManager_E.instance.curWave < QuizManager_E.instance.dictionary.Count - 1)
                {   // 문제가 더 남아있을 때
                    WaveManager_E.instance.EnemyKillCnt = 0;
                    // KillCount 초기화
                    WaveManager_E.instance.WaveDelay = true;
                    // timeFlag를 true로 주어 3초간 딜레이를 준다.
                    WaveManager_E.instance.timerItemFlag = true;
                    // timerItemFlag true로 주어 타이머를 정지시킨다.
                }
                else
                {   // 모든 문제를 풀었을 때
                    GameManager_E.instance.GameClear();
                }
            }

            else if(WaveManager_E.instance.EnemyKillCnt != WaveManager_E.instance.EnemyCount)
            { 
                if (WaveManager_E.instance.EnemyKillCnt == isRightResult())
                {   // 정답일 때
                    Sound.instance.Correct();
                    Debug.Log("정답!");
                    WaveManager_E.instance.EnemyKillCnt++;
                    // 적 죽인 수 ++
                    getDamage();
                    // 적 제거
                }
                else if (WaveManager_E.instance.EnemyKillCnt != isRightResult())
                {   // 오답일 때
                    Sound.instance.InCorrect();
                    WaveManager_E.instance.slider.value -= 2.0f;
                    // 답이 틀릴때마다 타이머 2초씩 감소

                    //playerState.HP -= 10;   
                    //// 플레이어에게 10 데미지를 줌.
                    //playerState.StateUpdate();
                    ////영우 05.25
                    //WaveManager_E.instance.WaveDelay = true;
                    //// timeFlag를 true로 주어 3초간 딜레이를 준다.
                    //WaveManager_E.instance.timerItemFlag = true;
                    //// timerItemFlag true로 주어 타이머를 정지시킨다.

                    //if (playerState.HP > 0)
                    //{   // 플레이어 HP가 0보다 클 때
                    //    if (WaveManager_E.instance.curWave < QuizManager_E.instance.dictionary.Count - 1)
                    //    {   // 문제가 더 남아있을 때
                    //        GameManager_E.instance.EnemyKillCount = 0;
                    //        // KillCount 초기화
                    //        GameManager_E.instance.NextLevel();
                    //    }
                    //    else
                    //    {   // 모든 문제를 풀었을 때
                    //        GameManager_E.instance.GameClear();
                    //    }
                    //}
                    //else if (playerState.HP == 0)
                    //{   // 플레이어 HP가 0일 때 게임 종료
                    //    GameManager_E.instance.GameOver();
                    //}
                }
            }
        }
    }


    public void InitEnemyInfo(Enemy_E enemy)
    {
        result = enemy.result;
        //transform.position = enemy.spawnPos;
        this.transform.Find("TextMesh").GetComponent<TextMesh>().text = enemy.meshNum.alpha;
        gameObject.SetActive(true);
    }

    // 총알에 맞은 게임오브젝트 삭제하는 메소드
    public void getDamage()
    {
        gameObject.SetActive(false);
        GameObject hit = Instantiate(HitEffect, transform.position, transform.rotation);
        // hit GameObject 변수에 HitEffect를 충돌 위치에 생성한다.
        Destroy(hit, 1.0f);
        // hit 오브젝트를 제거한다.
    }

    // 답이 아닌지 맞는지 리턴해주는 메소드
    public int isRightResult()
    {
        return result;
    }

    private void OnDisable()
    {
        if (WaveManager_E.instance != null)
        {
            WaveManager_E.instance.DieEnemy();
        }
    }
}
