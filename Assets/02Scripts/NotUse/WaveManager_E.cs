//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class WaveManager_E : MonoBehaviour
//{
//    public static WaveManager_E instance;

//    private Dictionary<int, List<Enemy>> waveEnemyDic = new Dictionary<int, List<Enemy>>();

//    public int curWave = -1;       // 현재 스테이지 변수

//    public int EnemyCount;

//    public GameObject[] Enemy = new GameObject[9];      // 적 캐릭터를 저장하는 변수
//    public GameObject LightChange;       // 배경 밤으로 변경

//    public Text stageText;          // 스테이지 ui
//    public Image stageImage;

//    static int EnemyMaxCount = 9;
//    public Transform[] SpawnPoint = new Transform[EnemyMaxCount];   // 적 생성 위치를 저장하는 배열
    
//    public bool WaveDelaying;      // 적을 죽였을때 3초간 딜레이를 주기 위한 변수
//    private float delayTime = 3.0f;     // 3초 딜레이

//    public GameObject textMesh;
//    public int hardMode;//영우
//    public int EnemyKillCnt;


//    void Awake()
//    {
//        instance = this;
//        hardMode = 0;//영우
//    }

//    void Start()
//    {
//        FirstStart();
//        // 처음 웨이브 시작
//        SliderController.instance.WaitSlider();
//    }
    
//    void Update()
//    {
//    }

//    public void WaveDelayStart()
//    {
//        if (!WaveDelaying)
//        {
//            StartCoroutine(coWaveDelay());
//        }
//    }

//    IEnumerator coWaveDelay()
//    {
//        WaveDelaying = true;
//        GameManager.instance.EnemyDestroy();
//        SliderController.instance.WaitSlider();
//        yield return new WaitForSeconds(delayTime);
//        if (HPManager.instance.HP > 0)
//        {
//            if (!waveEnemyDic.ContainsKey(curWave+1))
//            {  // 모든 문제를 다 풀어서 더이상 문제가 남아있지 않을 때
//                GameManager.instance.GameClear();
//                // GameClear() 메소드 호출
//            }
//            else
//            {
//                GameManager.instance.NextLevel();
//            }
//        }
//        else
//        {
//            GameManager.instance.GameOver();
//        }
//        SliderController.instance.ResumeSlider();
//        WaveDelaying = false;
//    }

//    public void FirstStart()
//    {
//        textMesh.SetActive(true);
//        Animator ani = textMesh.GetComponent<Animator>();
//        ani.SetTrigger("StartCount");
//    }

//    // 웨이브 생성 메소드
//    public void InitWave()
//    {
//        Enemy enemy;

//        List<Enemy> enemyList = new List<Enemy>();

//        for (int i = 0; i < 10; i++)
//        {
//            enemyList = new List<Enemy>();

//            for (int j = 0; j < 10; j++)
//            {
//                if (QuizManager.instance.dictionary[i].alphas[j] != null)
//                {
//                    enemy = new Enemy("Cube1", j, QuizManager.instance.dictionary[i].alphas[j]);
//                    enemyList.Add(enemy);
//                }
//            }

//            waveEnemyDic.Add(i, enemyList);
//            // 웨이브 저장
//        }
//    }

//    // 웨이브 시작하는 함수
//    public void StartWave()
//    {
//        SliderController.instance.StartSlider();
//        EnemyKillCnt = 0;

//        curWave++;
//        // 초기값 -1, 0부터 시작

//        stageText.text = "Stage " + (curWave + 1);
//        // 현재 스테이지 출력
//        Sprite newSprite = QuizManager.instance.dictionary[curWave].sprite;
//        stageImage.overrideSprite = newSprite;
//        // 이미지 출력

//        if (curWave == 5)
//        {   // 하드모드 단계를 정해주는 부분
//            hardMode = 1;
//            // 하드모드 온
//            LightChange.GetComponent<Light>().color = Color.black;
//            // 배경 변경
//        }
//        if (waveEnemyDic.ContainsKey(curWave))
//        {  // 모든 문제를 다 풀어서 더이상 문제가 남아있지 않을 때
//            EnemyRandomSpawn();
//        }

//    }

//    // 적의 생성위치를 스폰위치 중에서 랜덤으로 결정해주는 메소드
//    public void EnemyRandomSpawn()
//    {
//        int[] EnemySpawnChk = new int[EnemyMaxCount];

//        List<Enemy> enemyList = waveEnemyDic[curWave];
//        EnemyCount = enemyList.Count;

//        // 랜덤숫자 반복 방지를 위한 체크배열 변수(스폰지역 수만큼 배열크기 지정)
//        for (int i = 0; i < enemyList.Count; i++)
//        {   // enemyList(적) 수만큼 반복

//            int enemyPoint = Random.Range(0, EnemySpawnChk.Length);
//            // EnemySpawnChk 배열의 크기만큼 랜덤시드
//            if (EnemySpawnChk[enemyPoint] == 0)
//            {   // 체크된 배열이 아닌경우
//                EnemySpawnChk[enemyPoint] = 1;
//                // 해당 랜덤숫자배열 체크
//                GameObject enemyObj = Instantiate(Enemy[EnemyTypeRandom()], SpawnPoint[enemyPoint].position, SpawnPoint[enemyPoint].rotation);
//                enemyObj.name = enemyList[i].name;
//                EnemyInfo_E enemyInfo = enemyObj.GetComponent<EnemyInfo_E>();
//                enemyInfo.InitEnemyInfo(enemyList[i]);
//            }
//            else if (EnemySpawnChk[enemyPoint] == 1)
//            {   // 이미 생성된 랜덤숫자일 때
//                i--;
//                // i를 한단계 되돌려준다.
//            }
//        }

//        for (int i = 0; i < EnemySpawnChk.Length; i++)
//        {   // 랜덤숫자배열 초기화
//            EnemySpawnChk[i] = 0;
//        }
//    }

//    int EnemyTypeRandom()
//    {
//        int enemyType = Random.Range(0, Enemy.Length);

//        return enemyType;
//    }

//    private void OnDestroy()
//    {
//        instance = null;
//    }
//}
