using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Destroy(coll.gameObject);  // 총알 제거
            Destroy(gameObject);       // 아이템 제거

            if (gameObject.CompareTag("HP"))
            {   // 충돌한 오브젝트의 태그가 HP인 경우
                hpManager.HP += 10;  // 플레이어의 HP 회복
                hpManager.HeartCheck();   // 플레이어의 HP 업데이트
            }
            else if (gameObject.CompareTag("Timer"))
            {   // 충돌한 오브젝트의 태그가 Timer 경우
                timerFlag = true;
                StartCoroutine("EnemyStop");
            }
        }
    }

    IEnumerator EnemyStop()
    {
        yield return new WaitForSeconds(3.0f);
        timerFlag = false;
    }
}
