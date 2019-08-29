using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager_E : MonoBehaviour
{
    public static WaveManager_E instance;

    private Dictionary<int, List<Enemy_E>> waveEnemyDic = new Dictionary<int, List<Enemy_E>>();

    public int curWave = -1;       // 현재 스테이지 변수

    public int curWaveEnemyCount;
    public int EnemyCount;

    public GameObject[] Enemy = new GameObject[9];      // 적 캐릭터를 저장하는 변수
    public GameObject LightChange;       // 배경 밤으로 변경

    public Text stageText;          // 스테이지 ui
    public Image stageImage;

    static int EnemyMaxCount = 9;
    public Transform[] SpawnPoint = new Transform[EnemyMaxCount];   // 적 생성 위치를 저장하는 배열
    int[] itemSpawnChk = new int[EnemyMaxCount];         // 랜덤숫자 반복 방지를 위한 체크배열 변수(스폰지역 수만큼 배열크기 지정)

    public bool WaveDelay;      // 적을 죽였을때 3초간 딜레이를 주기 위한 변수
    private float limitTime = 3.0f;     // 3초 딜레이


    public bool hardMode;//영우

    public HPManager_E hpManager;   // 
    public Slider slider;
    public bool timeOver;
    float timeRemaining = 20.0f;     // 제한시간

    public bool timerItemFlag;     // 타이머가 작동하는지 안하는지

    public int EnemyKillCnt;


    void Awake()
    {
        instance = this;
        hardMode = false;//영우
    }

    void Start()
    {
        hpManager = GameObject.Find("HPManager").GetComponent<HPManager_E>();
        // Player 오브젝트를 찾아서 PlayerCtrl 스크립트를 가져온다.
        StartWave();
        // 처음 웨이브 시작
        WaveDelay = false;
        timeOver = false;
        timerItemFlag = false;
        slider.value = timeRemaining;
    }

    void FixedUpdate()
    {
        if (hpManager.HP == 0)
        {
            GameManager.instance.GameOver();
        }

        if (WaveDelay)
        {   // 적을 죽이면 timeFlag가 true가 되면서 3초간 시간이 흐르게 된다.
            if (limitTime > 0.0f)
            {   // 아직 제한 시간이 남았을때
                if (hardMode)
                {
                    limitTime -= (Time.deltaTime * 2.0f);
                }
                else if (!hardMode)
                {
                    limitTime -= Time.deltaTime;
                }
            }
            else
            {   // 제한 시간 다 지났을때
                WaveDelay = false;
                // timeFlag 다시 false로
                limitTime = 1.5f;
                // limitTime 초기화
                if (QuizManager_E.instance.dictionary[curWave] != null)
                {   
                    StartWave();
                    // 다음 웨이브 시작
                }
                else if (QuizManager_E.instance.dictionary[curWave] == null)
                {
                    GameManager.instance.GameClear();
                }
            }
        }

        if(!timeOver)
        {   // timeOver가 false일 때
            if (slider.value > 0)
            {
                if (timerItemFlag != true)
                {
                    slider.value -= Time.deltaTime;
                }
                else
                {
                }
            }
            else
            {   // 남은시간 0이하일 때
                timeOver = true;     // 타임오버
                WaveDelay = true;   // 3초딜레이
                hpManager.HP -= 10;
                hpManager.HeartCheck();  // 플레이어 체력 감소 후 업데이트
                GameManager.instance.FailEffect();
                GameManager.instance.EnemyDestroy();    // 모든 적 제거
            }
        }
    }

    // 웨이브 생성 메소드
    public void InitWave()
    {
        Enemy_E enemy;

        List<Enemy_E> enemyList = new List<Enemy_E>();

        for (int i = 0; i < 10; i++)
        {
            enemyList = new List<Enemy_E>();

            for (int j = 0; j < 10; j++)
            {
                if (QuizManager_E.instance.dictionary[i].val[j] != null)
                {
                    enemy = new Enemy_E("Cube1", j, QuizManager_E.instance.dictionary[i].val[j]);
                    enemyList.Add(enemy);
                }
            }

            waveEnemyDic.Add(i, enemyList);
            // 웨이브 저장
        }
    }


    // 이부분 다시 정의 하시오 킹영우씨.
    public void DieEnemy()
    {
        curWaveEnemyCount--;
        
        if (curWaveEnemyCount == 0)
        {   // 현재 남아있는 적이 없을때
            if (curWave < QuizManager_E.instance.dictionary.Count - 1)
            {   // 문제가 더 남아있을때

            }
            else
            {   // 남아있는 문제가 없으면 리턴
                return;
            }
        }
    }

    // 웨이브 시작하는 함수
    void StartWave()
    {
        slider.value = timeRemaining;
        timeOver = false;
        timerItemFlag = false;
        EnemyKillCnt = 0;

        curWave++;
        // 초기값 -1, 0부터 시작

        stageText.text = "Stage " + (curWave + 1);
        // 현재 스테이지 출력
        Sprite newSprite = QuizManager_E.instance.dictionary[curWave].sprite;
        stageImage.overrideSprite = newSprite;
        // 이미지 출력

        if (curWave == 5)
        {   // 하드모드 단계를 정해주는 부분
            hardMode = true;
            // 하드모드 온
            LightChange.GetComponent<Light>().color = Color.black;
            // 배경 변경
        }

        if (waveEnemyDic.ContainsKey(curWave))
        {   // waveEnemyDic에 현재 스테이지인 curWave단계의 키가 존재 할때
            EnemyRandomSpawn();
            // 적 위치 랜덤 생성
        }
        else
        {   // 모든 문제를 다 풀어서 더이상 문제가 남아있지 않을 때
            GameManager.instance.GameClear();
            // GameClear() 메소드 호출
        }
    }

    // 적의 생성위치를 스폰위치 중에서 랜덤으로 결정해주는 메소드
    public void EnemyRandomSpawn()
    {
        int[] EnemySpawnChk = new int[EnemyMaxCount];

        List<Enemy_E> enemyList = waveEnemyDic[curWave];
        curWaveEnemyCount = enemyList.Count;
        // curWaveEnemyCount에 enemyList 개수만큼
        EnemyCount = enemyList.Count;

        // 랜덤숫자 반복 방지를 위한 체크배열 변수(스폰지역 수만큼 배열크기 지정)
        for (int i = 0; i < enemyList.Count; i++)
        {   // enemyList(적) 수만큼 반복
            int enemyPoint = Random.Range(0, EnemySpawnChk.Length);
            // EnemySpawnChk 배열의 크기만큼 랜덤시드
            if (EnemySpawnChk[enemyPoint] == 0)
            {   // 체크된 배열이 아닌경우
                EnemySpawnChk[enemyPoint] = 1;
                // 해당 랜덤숫자배열 체크
                GameObject enemyObj = Instantiate(Enemy[EnemyTypeRandom()], SpawnPoint[enemyPoint].position, SpawnPoint[enemyPoint].rotation);
                enemyObj.name = enemyList[i].name;
                EnemyInfo_E enemyInfo = enemyObj.GetComponent<EnemyInfo_E>();
                enemyInfo.InitEnemyInfo(enemyList[i]);
            }
            else if (EnemySpawnChk[enemyPoint] == 1)
            {   // 이미 생성된 랜덤숫자일 때
                i--;
                // i를 한단계 되돌려준다.
            }
        }

        for (int i = 0; i < EnemySpawnChk.Length; i++)
        {   // 랜덤숫자배열 초기화
            EnemySpawnChk[i] = 0;
        }
    }

    int EnemyTypeRandom()
    {
        int enemyType = Random.Range(0, Enemy.Length);

        return enemyType;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
