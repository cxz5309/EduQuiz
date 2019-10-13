using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Item : MonoBehaviour
{
    private HPManager hpManager;   // PlayerCtrl 스크립트를 가져오는 변수
    public float stopTime = 3f;


    // 충돌 처리하는 메소드
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Bullet"))
        {   // 충돌한 오브젝트의 태그가 Bullet인 경우
            ItemManager.itemCount++;  // 생성가능한 아이템 갯수를 1개 늘려준다.
            ItemManager.instance.itemSpawnChk[int.Parse(gameObject.name)] = 0;  // 체크 0으로
            
            if (gameObject.CompareTag("HP"))
            {   // 충돌한 오브젝트의 태그가 HP인 경우
                GetHp();
            }
            else if (gameObject.CompareTag("Timer"))
            {   // 충돌한 오브젝트의 태그가 Timer 경우
                switch (SceneManager.GetActiveScene().name)
                {
                    case "BasicScene":
                    case "MathScene":
                    case "OXScene":
                        StartTimer();
                        break;
                    case "EnglishScene":
                        StartEnglishTimer();
                        break;
                }
            }

            Destroy(coll.gameObject);  // 총알 제거
            Destroy(gameObject);       // 아이템 제거
        }
    }
    void GetHp()
    {
        HPManager.instance.HP += 10;  // 플레이어의 HP 회복
        HPManager.instance.HeartCheck();   // 플레이어의 HP 업데이트
    }
    void StartEnglishTimer()
    {
        SliderController.instance.WaitSliderForSeconds(stopTime);
    }
    public void StartTimer()
    {
        GameObject[] allCube = GameObject.FindGameObjectsWithTag("enemy");
        // allCube에 "enemy" 태그를 가지는 오브젝트 전부 넣어줌.
        SliderController.instance.WaitSliderForSeconds(stopTime);

        foreach (GameObject i in allCube)
        {
            i.GetComponent<EnemyInfo_B>().stopSeconds(stopTime);
        }
    }
}
