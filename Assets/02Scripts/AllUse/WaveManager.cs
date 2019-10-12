using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    private Dictionary<int, List<Enemy>> waveEnemyDic = new Dictionary<int, List<Enemy>>();
    public int curWave = -1;       // 현재 스테이지 변수
    public int lastWave = 6;    //최종 스테이지
    public int hardWave = 3;    //하드모드 진입 직전 스테이지

    public GameObject LightChange;       // 배경 밤으로 변경
    //영우
    public Text stageText;          // 스테이지 ui
    public GameObject Manual;

    public Text basicText;           // 지문 ui
    public Image englishImage;

    public Transform[] SpawnPoint = new Transform[9];   // 적 생성 위치를 저장하는 배열
    public GameObject[] Enemy = new GameObject[9];        // 적 캐릭터를 저장하는 변수

    private int enemyMaxCount;

    public int EnemyCount;
    public int EnemyKillCnt;
    
    public bool WaveDelaying;
    private float delayTime = 3.0f;
    // 3초 딜레이
    public GameObject countText;

    public int level;
    public float defaultTime;
    public int hardMode;//영우

    void Awake()
    {
        instance = this;
        hardMode = 0;//영우
    }

    void Start()
    {
        initSpawnCount();

        PopupMaunal();
        //StartWave();
        // 처음 웨이브 시작
    }
    
    public void WaveDelayStart()
    {
        if (!WaveDelaying)
        {
            StartCoroutine(coWaveDelay());
        }
    }

    IEnumerator coWaveDelay()
    {
        WaveDelaying = true;
        GameManager.instance.EnemyDestroy();
        
                SliderController.instance.WaitSlider();
        
        yield return new WaitForSeconds(delayTime);
        if (HPManager.instance.HP > 0)
        {
            if (!waveEnemyDic.ContainsKey(curWave+1))
            {  // 모든 문제를 다 풀어서 더이상 문제가 남아있지 않을 때
                GameManager.instance.GameClear();
                // GameClear() 메소드 호출
            }
            else
            {
                GameManager.instance.NextLevel();
            }
            
                    SliderController.instance.ResumeSlider();
             
            WaveDelaying = false;
        }
        else
        {
            GameManager.instance.GameOver();
        }
    }

    public void PopupMaunal()
    {
        Manual.SetActive(true);
    }
    public void CloseManual()
    {
        Manual.SetActive(false);
        FirstStart();
    }

    public void FirstStart()
    {
        countText.SetActive(true);
        Animator ani = countText.GetComponent<Animator>();
        ani.SetTrigger("StartCount");
    }

    // 웨이브 생성 메소드
    public void InitWave()
    {
        Enemy enemy;

        List<Enemy> enemyList = new List<Enemy>();
        int CubeNum=0;
        switch (SceneManager.GetActiveScene().name)
        {
            case "BasicScene":
                CubeNum = 4;
                defaultTime = 12f;
                break;
            case "MathScene":
                CubeNum = 6;
                defaultTime = 12f;
                break;
            case "EnglishScene":
                defaultTime = 20f;
                break;
            case "OXScene":
                CubeNum = 2;
                defaultTime = 12;
                break;
        }
        switch (SceneManager.GetActiveScene().name)
        {
            case "BasicScene":
            case "MathScene":
            case "OXScene":
                for (int i = 0; i < lastWave; i++)
                {
                    enemyList = new List<Enemy>();

                    for (int j = 0; j < CubeNum; j++)
                    {
                        if (QuizManager.instance.dictionary[i].pass == j)
                        {
                            enemy = new Enemy("Cube1", false, QuizManager.instance.dictionary[i].ans[j]);
                            // 정답인 적 생성(0)
                        }
                        else
                        {
                            enemy = new Enemy("Cube1", true, QuizManager.instance.dictionary[i].ans[j]);
                            // 정답이 아닌 적 생성(1 ~ 3)
                        }
                        enemyList.Add(enemy);
                        // 적 생성
                    }

                    waveEnemyDic.Add(i, enemyList);
                    // 웨이브 저장
                }
                break;
            case "EnglishScene":
                for (int i = 0; i < lastWave; i++)
                {
                    enemyList = new List<Enemy>();

                    for (int j = 0; j < 10; j++)
                    {
                        if (QuizManager.instance.dictionary[i].alphas[j] != null)
                        {
                            enemy = new Enemy("Cube1", j, QuizManager.instance.dictionary[i].alphas[j]);
                            enemyList.Add(enemy);
                        }
                    }

                    waveEnemyDic.Add(i, enemyList);
                    // 웨이브 저장
                }
                break;
        }
        
    }

    // 웨이브 시작하는 함수
    public void StartWave()
    {
        curWave++;        // 초기값 -1, 0부터 시작
        stageText.text = "Stage " + (curWave + 1);
        // 현재 스테이지 출력
        SliderController.instance.StartSlider();

        switch (SceneManager.GetActiveScene().name)
        {
            case "BasicScene":
            case "OXScene":
                basicText.text = QuizManager.instance.dictionary[curWave].quiz;
                // 인덱스 0부터 문제 출력
                break;
            case "EnglishScene":
                EnemyKillCnt = 0;
                Sprite newSprite = QuizManager.instance.dictionary[curWave].sprite;
                englishImage.overrideSprite = newSprite;
                // 이미지 출력
                break;
        }

        if (curWave == hardWave)
        {   // 하드모드 단계를 정해주는 부분
            hardMode = 1;
            // 하드모드 온
            LightChange.GetComponent<Light>().color = new Color(0,0,0,0.7f);
            // 배경 변경
        }

        if (waveEnemyDic.ContainsKey(curWave))
        {   // waveEnemyDic에 현재 스테이지인 curWave단계의 키가 존재 할때
            switch (SceneManager.GetActiveScene().name)
            {
                case "BasicScene":
                    EnemyRandomSpawn_B();
                    break;
                case "MathScene":
                    EnemyRandomSpawn_M();
                    break;
                case "EnglishScene":
                    EnemyRandomSpawn_E();
                    break;
                case "OXScene":
                    EnemySpawn_OX();
                    break;
            }
            // 적 위치 랜덤 생성
        }
    }

    public float waveTime()
    {
        return defaultTime / (1 + (hardMode * 0.5f)) / (1+(level * 0.1f));
    }


    public void initSpawnCount()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "BasicScene":
            case "MathScene":
                enemyMaxCount = 7;
                break;
            case "EnglishScene":
                enemyMaxCount = 9;
                break;
            case "OXScene":
                enemyMaxCount = 2;
                break;
        }
    }

    // 적의 생성위치를 스폰위치 중에서 랜덤으로 결정해주는 메소드
    public void EnemyRandomSpawn_B()
    {
        int[] EnemySpawnChk = new int[enemyMaxCount];

        List<Enemy> enemyList = waveEnemyDic[curWave];
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
                EnemyInfo_B enemyInfo = enemyObj.GetComponent<EnemyInfo_B>();
                enemyInfo.InitEnemyInfo(enemyList[i]);
            }
            else if (EnemySpawnChk[enemyPoint] == 1)
            {   // 이미 생성된 랜덤숫자일 때
                i--;
                // i를 한단계 되돌려준다.
            }
        }
        // 랜덤숫자 반복 방지를 위한 체크배열 변수(스폰지역 수만큼 배열크기 지정)
        for (int i = 0; i < EnemySpawnChk.Length; i++)
        {   // 랜덤숫자배열 초기화
            EnemySpawnChk[i] = 0;
        }
    }
    public void EnemyRandomSpawn_M()
    {
        int[] EnemySpawnChk = new int[enemyMaxCount];

        List<Enemy> enemyList = waveEnemyDic[curWave];
        
        for (int i = 0; i < enemyList.Count; i++)
        {
            GameObject enemyObj = Instantiate(Enemy[EnemyTypeRandom()], SpawnPoint[i].position, SpawnPoint[i].rotation);
            enemyObj.name = enemyList[i].name;
            EnemyInfo_B enemyInfo = enemyObj.GetComponent<EnemyInfo_B>();
            enemyInfo.InitEnemyInfo(enemyList[i]);
        }
                
    }
    public void EnemyRandomSpawn_E()
    {
        int[] EnemySpawnChk = new int[enemyMaxCount];

        List<Enemy> enemyList = waveEnemyDic[curWave];
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
    public void EnemySpawn_OX()
    {
        List<Enemy> enemyList = waveEnemyDic[curWave];

        for (int i = 0; i < enemyList.Count; i++)
        {   // enemyList(적) 수만큼 반복
                GameObject enemyObj = Instantiate(Enemy[i], SpawnPoint[i].position, SpawnPoint[i].rotation);
                enemyObj.name = enemyList[i].name;
                EnemyInfo_B enemyInfo = enemyObj.GetComponent<EnemyInfo_B>();
                enemyInfo.InitEnemyInfo(enemyList[i]);
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
