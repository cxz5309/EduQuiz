using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Item_B : MonoBehaviour
{
    private HPManager_B hpManager;   // PlayerCtrl 스크립트를 가져오는 변수
    public static bool timerFlag;     // 타이머가 작동하는지 안하는지

    void Start()
    {
        hpManager = GameObject.Find("HPManager").GetComponent<HPManager_B>();
        // Player 오브젝트를 찾아서 PlayerCtrl 스크립트를 가져온다.
    }

    // 충돌 처리하는 메소드
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {   // 충돌한 오브젝트의 태그가 Bullet인 경우
            ItemManager_B.itemCount++;  // 생성가능한 아이템 갯수를 1개 늘려준다.
            ItemManager_B.instance.itemSpawnChk[int.Parse(gameObject.name)] = 0;  // 체크 0으로
            
            if (gameObject.CompareTag("HP"))
            {   // 충돌한 오브젝트의 태그가 HP인 경우
                hpManager.HP += 10;  // 플레이어의 HP 회복
                hpManager.HeartCheck();   // 플레이어의 HP 업데이트
            }
            else if (gameObject.CompareTag("Timer"))
            {   // 충돌한 오브젝트의 태그가 Timer 경우
                StartCoroutine("EnemyStop");
            }

            Destroy(coll.gameObject);  // 총알 제거
            Destroy(gameObject);       // 아이템 제거
        }
    }
    
    public void StartTimer()
    {
        GameObject[] allCube = GameObject.FindGameObjectsWithTag("enemy");
        // allCube에 "enemy" 태그를 가지는 오브젝트 전부 넣어줌.
        foreach (GameObject i in allCube)
        {
            i.GetComponent<EnemyInfo_B>().Move(EnemyInfo_B.State.Stop);
        }
    }

    public void EndTimer()
    {
        GameObject[] allCube = GameObject.FindGameObjectsWithTag("enemy");
        // allCube에 "enemy" 태그를 가지는 오브젝트 전부 넣어줌.
        foreach (GameObject i in allCube)
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "BasicScene":
                    i.GetComponent<EnemyInfo_B>().Move((EnemyInfo_B.State)WaveManager_B.instance.hardMode);

                    break;
                case "MathScene":
                    i.GetComponent<EnemyInfo_B>().Move((EnemyInfo_B.State)WaveManager_M.instance.hardMode);
                    break;
            }
        }
    }

    IEnumerator EnemyStop()
    {
        for (int i = 0; i < 2; i++)
        {
            if(i==0) StartTimer();
            if(i==1) EndTimer();
            yield return new WaitForSeconds(3f);
        }
    }
}
