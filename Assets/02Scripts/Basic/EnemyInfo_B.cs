using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Enemy
{
    public string name;             // 이름 ( 프리팹 이름 )
    public bool result;             // 정답 유무
    public int order;             // 정답 유무
    public string meshNum;

    public Enemy(string name, bool result, string meshNum)
    {
        this.name = name;
        this.result = result;
        this.meshNum = meshNum;
    }
    public Enemy(string _name, int _order, string _meshNum)
    {
        this.name = _name;
        this.order = _order;
        this.meshNum = _meshNum;
    }
}

public class EnemyInfo_B : MonoBehaviour
{
    public GameObject spawnEffect;  // 적이 생성되면 발생하는 이펙트
    public GameObject HitEffect;    // 적이 죽으면 발생하는 이펙트
    public GameObject Player;

    public float MoveSpeed;       // 적 이동 속도
    private float DistanceToPlayer;      // 적과 플레이어 사이의 거리

    private Animator ani;       // 적의 Animator를 가져오는 변수

    public bool isAtacking;
    private bool result;

    public enum State { Walk, Run, Stop, Attack};
    State nowState;
    
    void Start()
    {
        GameObject spawn = Instantiate(spawnEffect, transform.position, transform.rotation);        // 적 스폰 이펙트 메소드 호출
        Destroy(spawn, 5.0f);       // spawn 오브젝트 제거

        ani = GetComponent<Animator>();      // 적의 Animator를 가져온다.
        switch (SceneManager.GetActiveScene().name)
        {
            case "BasicScene":
            case "MathScene":
                Move((State)WaveManager.instance.hardMode);
                break;
            case "EnglishScene":
                break;
        }
    }

    void Update()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "BasicScene":
            case "MathScene":
                if (HPManager.instance.HP > 0)
                {   // 플레이어 ture
                    DistanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
                    // 적과 플레이어 사이의 거리를 구해서 DistanceToPlayer 변수에 저장
                    // 플레이어 위치는 (0, 0, 0)이기 때문에 Vector3.zero를 사용

                    if (DistanceToPlayer < 5.5f&&!isAtacking)
                    {
                        isAtacking = true;
                        DistanceToPlayer = 5.5f;
                        Move(State.Attack);
                        StartCoroutine(coAttack());
                    }
                }
                break;
            case "EnglishScene":

                break;
        }
        
    }

    // 적 이동 메소드
    public void Move(State state)
    {
        switch (state)
        {
            case State.Walk:
                MoveSpeed = speedChange(WaveManager.instance.hardMode);
                ani.SetBool("walk", true);
                break;
            case State.Run:
                MoveSpeed = speedChange(WaveManager.instance.hardMode);
                ani.SetBool("run", true);
                break;
            case State.Attack:
                MoveSpeed = 0;
                ani.SetBool("run", false);
                ani.SetBool("walk", false);
                ani.SetBool("right hook", true);        // 적의 Animator의 Shot을 true로 하여 공격하게 함.
                break;
            case State.Stop:
                MoveSpeed = 0;
                break;
        }
        nowState = state;    
        transform.LookAt(Player.transform);
        // 적은 플레이어 방향을 바라봄.
        GetComponent<Rigidbody>().velocity = transform.forward * MoveSpeed;
        //transform.position += transform.forward * MoveSpeed * Time.deltaTime;
        // 초당 MoveSpeed의 거리를 이동
    }

    public void stopSeconds(float time)
    {
        StartCoroutine(stopSecondsInvoke());
    }

    IEnumerator stopSecondsInvoke()
    {
        State tmp = nowState;
        Move(State.Stop);
        yield return new WaitForSeconds(3.0f);          // 3.0초간 대기
        Move(tmp);
    }

    IEnumerator coAttack()
    {
        yield return new WaitForSeconds(3f);
        GameManager.instance.FailEffect();
        WaveManager.instance.WaveDelayStart();
        HPManager.instance.HP -= 10;          // 플레이어에게 10 데미지를 줌.
        HPManager.instance.HeartCheck();
        isAtacking = false;
    }
    

    // 충돌 처리하는 메소드
    void OnCollisionEnter(Collision coll)
    {
        Physics.IgnoreLayerCollision(9, 9);
        if (coll.gameObject.CompareTag("Bullet"))
        {   // 충돌한 오브젝트의 태그가 Bullet인 경우
            Destroy(coll.gameObject);   // 총알 제거
            DamageIffect();        // 적 제거

            switch (SceneManager.GetActiveScene().name)
            {
                case "BasicScene":
                case "MathScene":
                    // hit 오브젝트를 제거한다.
                    if (isRightResult())
                    {   // 정답일 때
                        GameManager.instance.SuccessEffect();
                        Sound.instance.Correct();
                    }
                    else
                    {   // 오답일 때
                        GameManager.instance.FailEffect();
                        Sound.instance.InCorrect();
                        HPManager.instance.HP -= 10;
                        HPManager.instance.HeartCheck();
                    }
                    WaveManager.instance.WaveDelayStart();
                    break;
            }
        }
    }

    // 노말, 하드모드에 따라 적 이동속도를 지정해주는 메소드
    public float speedChange(int hardMode)
    {   // 초기값 false
        if(hardMode == 0)
            return 2.5f;
        else
            return 4f;
    }

    public void InitEnemyInfo(Enemy enemy)
    {
        result = enemy.result;
        //transform.position = enemy.spawnPos;
        this.transform.Find("TextMesh").GetComponent<TextMesh>().text = enemy.meshNum;
        gameObject.SetActive(true);
    }
    
    // 총알에 맞은 효과(이펙트, 오브젝트 제거)
    public void DamageIffect()
    {
        gameObject.SetActive(false);
        GameObject hit = Instantiate(HitEffect, transform.position, transform.rotation);    // hit GameObject 변수에 HitEffect를 충돌 위치에 생성한다.
        hit.transform.position = gameObject.transform.position;
        hit.transform.eulerAngles = new Vector3(-90, 0, 0);
        Destroy(hit, 5.0f);      // hit 오브젝트를 제거한다.
    }

    // 답이 아닌지 맞는지 리턴해주는 메소드
    public bool isRightResult()
    {
        if (result)
        {
            return false;
        }
        else
            return true;
    }
    
}
