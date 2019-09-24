//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class WaveManager_M : MonoBehaviour
//{
//    public static WaveManager_M instance;

//    private Dictionary<int, List<Enemy>> waveEnemyDic = new Dictionary<int, List<Enemy>>();

//    public int curWave = -1;       // 현재 스테이지 변수

//    private int curWaveEnemyCount;

//    public GameObject LightChange;       // 배경 밤으로 변경
//    //영우
//    public Text stageText;          // 스테이지 ui

//    public Transform[] SpawnPoint = new Transform[7];   // 적 생성 위치를 저장하는 배열

//    public GameObject[] Enemy = new GameObject[9];

//    int[] itemSpawnChk = new int[7];
//    // 랜덤숫자 반복 방지를 위한 체크배열 변수(스폰지역 수만큼 배열크기 지정)

//    public bool timeFlag;
//    // 적을 죽였을때 3초간 딜레이를 주기 위한 변수
//    private float limitTime = 3.0f;
//    // 3초 딜레이

//    public int hardMode;//영우

//    void Awake()
//    {
//        instance = this;
//        hardMode = 0;//영우
//    }

//    void Start()
//    {
//        StartWave();
//        // 처음 웨이브 시작
//        timeFlag = false;
//        // 처음 timeFlag를 false로 지정해준다.
//    }

//    void Update()
//    {
//        if (timeFlag)
//        {   // 적을 죽이면 timeFlag가 true가 되면서 3초간 시간이 흐르게 된다.
//            if (limitTime > 0.0f)
//            {   // 아직 제한 시간이 남았을때
//                limitTime -= Time.deltaTime;
//            }
//            else
//            {   // 제한 시간 다 지났을때
//                timeFlag = false;
//                // timeFlag 다시 false로
//                limitTime = 3.0f;
//                // limitTime 초기화
//                StartWave();
//                // 다음 웨이브 시작
//            }
//        }
//    }

//    // 웨이브 생성 메소드
//    public void InitWave()
//    {
//        Enemy enemy;

//        List<Enemy> enemyList = new List<Enemy>();

//        for (int i = 0; i < 10; i++) //QuizManager.instance.dictionary.Count 이 최대값
//        {

//            enemyList = new List<Enemy>();

//            for (int j = 0; j < 6; j++)
//            {
//                if (QuizManager.instance.dictionary[i].pass == j)
//                {
//                    enemy = new Enemy("Cube1", false, QuizManager.instance.dictionary[i].ans[j]);
//                    // 정답인 적 생성(0)
//                }
//                else
//                {
//                    enemy = new Enemy("Cube1", true, QuizManager.instance.dictionary[i].ans[j]);
//                    // 정답이 아닌 적 생성(1 ~ 3)
//                }
//                enemyList.Add(enemy);
//                // 적 생성
//            }

//            waveEnemyDic.Add(i, enemyList);
//            // 웨이브 저장
//        }
//    }
    
//    public void DieEnemy()
//    {
//        curWaveEnemyCount--;
        
//        if (curWaveEnemyCount == 0)
//        {   // 현재 남아있는 적이 없을때
//            if (curWave < QuizManager.instance.dictionary.Count - 1)
//            {   // 문제가 더 남아있을때
//            }
//            else
//            {   // 남아있는 문제가 없으면 리턴
//                return;
//            }
//        }
//    }

//    // 웨이브 시작하는 함수
//    void StartWave()
//    {
//        curWave++;
//        // 초기값 -1, 0부터 시작

//        stageText.text = "Stage " + (curWave + 1);


//        if (curWave == 5)
//        {   // 하드모드 단계를 정해주는 부분
//            hardMode = 1;
//            // 하드모드 온
//            LightChange.GetComponent<Light>().color = Color.black;
//            // 배경 변경
//        }

//        if (waveEnemyDic.ContainsKey(curWave))
//        {   // waveEnemyDic에 현재 스테이지인 curWave단계의 키가 존재 할때
//            EnemyRandomSpawn();
//            // 적 위치 랜덤 생성
//        }
//        else
//        {   // 모든 문제를 다 풀어서 더이상 문제가 남아있지 않을 때
//            GameManager.instance.GameClear();
//            // GameClear() 메소드 호출
//        }
//    }

//    // 적의 생성위치를 스폰위치 중에서 랜덤으로 결정해주는 메소드
//    public void EnemyRandomSpawn()
//    {
//        int[] EnemySpawnChk = new int[7];

//        List<Enemy> enemyList = waveEnemyDic[curWave];
//        curWaveEnemyCount = enemyList.Count;
//        // curWaveEnemyCount에 enemyList 개수만큼

//        // 랜덤숫자 반복 방지를 위한 체크배열 변수(스폰지역 수만큼 배열크기 지정)
//        for (int i = 0; i < enemyList.Count; i++)
//        {
//            GameObject enemyObj = Instantiate(Enemy[EnemyTypeRandom()], SpawnPoint[i].position, SpawnPoint[i].rotation);
//            enemyObj.name = enemyList[i].name;
//            EnemyInfo_B enemyInfo = enemyObj.GetComponent<EnemyInfo_B>();
//            enemyInfo.InitEnemyInfo(enemyList[i]);
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
