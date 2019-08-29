using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl_M : MonoBehaviour
{
    public GameObject[] heart = new GameObject[5];
    // 플레이어의 몫을 표시할 오브젝트(최대 5개)

    public int HP;          // 플레이어 HP 저장

    public Transform FirePos;   // 총구
    public GameObject Bullet;   // 총알 오브젝트를 가져오기 위한 변수

    float rotSpeed = 8.0f;      // 민감도

    public Camera fpsCam;       // 카메라


    void Start()
    {
        StateUpdate();
        // 플레이어 HP 업데이트
    }


    void Update()
    {
        if (GameManager_M.instance.gamestate == GameManager_M.Gamestate.GamePlaying)
        {
            RotCtrl();
            // 마우스 방향에 따른 플레이어 시점
            Cursor.lockState = CursorLockMode.Locked;
            if (Input.GetMouseButtonDown(0))
            {
                Fire();
                // 마우스 왼쪽버튼 클릭하면 총알 발사
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    // 현재 HP를 업데이트하는 함수(다른 스크립트에서 사용 가능)
    public void StateUpdate()
    {
        if (HP == 60)
        {   // 최대 체력 50
            HP = 50;
        }
        for (int i = 0; i < HP / 10; i++)
        {   // 0부터 현재 HP까지의 하트 온
            heart[i].SetActive(true);
        }
        for (int i = HP / 10; i < heart.Length; i++)
        {   // 현재 HP부터 마지막 배열까지의 하트 오프
            if (HP == 50)
            {   // 현재 체력이 50이면 하트를 오프할 필요가 없다.
                break;
            }
            heart[i].SetActive(false);
        }
    }

    // 총알을 발사하는 메소드
    void Fire() {
        Instantiate(Bullet, FirePos.position, FirePos.rotation);
        // FirePos의 Position과 Rotation의 위치에 Bullet을 생성
    }

    // 플레이어 1인칭 시점 구현 메소드
    void RotCtrl()
    {       
        float rotX = Input.GetAxis("Mouse Y") * rotSpeed;
        float rotY = Input.GetAxis("Mouse X") * rotSpeed;

        this.transform.localRotation *= Quaternion.Euler(0, rotY, 0);
        fpsCam.transform.localRotation *= Quaternion.Euler(-rotX, 0, 0);
    }


}
