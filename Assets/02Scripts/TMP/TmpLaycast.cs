using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TmpLaycast : MonoBehaviour
{
    public Camera camera;
    public GameObject Bullet;
    public GameObject FirePos;
    string thisTag = "";

    public bool triggerSwitch;
    public string shotSound = "Shot";
    private AudioManager theAudio;

    private void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        ChangeWeapon(DataSave.instance.data.nowWeapon);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            triggerSwitch = (triggerSwitch == true) ? false : true;
        }
        
        if (Input.GetMouseButtonUp(0))
        {

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 1000f))
            {
                int l = hitInfo.transform.gameObject.layer;
                
                switch (SceneManager.GetActiveScene().name)
                {
                    case "Main":
                        if (hitInfo.transform.gameObject.layer == 5)
                        {
                            switch (hitInfo.collider.name)
                            {
                                case "Basic":
                                case "Math":
                                case "English":
                                    thisTag = hitInfo.collider.tag;
                                    MainManager.instance.OpenGrade();
                                    break;
                                case "Grade1":
                                case "Grade2":
                                case "Grade3":
                                    MainManager.instance.OpenLevel();
                                    break;
                                case "Level1":
                                case "Level2":
                                case "Level3":
                                    ChangeScene(thisTag);
                                    break;
                                case "Store":
                                    ChangeScene(hitInfo.collider.tag);
                                    break;

                                // =============================================================== 새로 추가한것들
                                case "Confirm":
                                    if (PlayerManager.instance.currentPopup == "GamePotal")
                                        Debug.Log("게임시작");     // 겜씸으로 이동 하시죠
                                    else if(PlayerManager.instance.currentPopup == "StorePotal")
                                        ChangeScene("store");       // 상점으로 이동 하시죠
                                    break;
                                case "Setting":
                                    PlayerManager.instance.currentPopup = "Setting";
                                    UniconManager.instance.PopupChange();
                                    break;
                                case "Cencel":
                                case "Back":
                                    PlayerManager.instance.currentPopup = "Idle";
                                    UniconManager.instance.PopupChange();
                                    break;
                                case "Exit":
                                    Debug.Log("GameClose");     // 겜 종료 하시죠
                                    break;
                                case "Music":
                                    AudioManager.instance.SetBackGroundMute();
                                    Debug.Log("배경음 ONOFF");
                                    break;
                                case "Effect":
                                    AudioManager.instance.SetEffectMute();
                                    Debug.Log("효과음 ONOFF");
                                    break;
                            }
                        }
                        else
                        {
                            if (!triggerSwitch)
                            {
                                transform.position = hitInfo.point + new Vector3(0,6.19f,0);
                            }
                        }
                        if (triggerSwitch)
                        {
                            Fire(hitInfo.point);
                        }
                        break;
                    case "StoreScene":
                        if (hitInfo.transform.gameObject.layer == 5)
                        {
                            if (StoreManager.instance.getStoreActive == true)
                            {   // 상점 켜졌을 때
                                if (StoreManager.instance.getBuyActive == false)
                                {   // 구매창 안켜졌을 때
                                    switch (hitInfo.collider.tag)
                                    {
                                        case "Close":
                                            StoreManager.instance.SetStoreActive();
                                            break;
                                        case "Left":
                                            SelectBuyWeapon.instance.Left();
                                            break;
                                        case "Right":
                                            SelectBuyWeapon.instance.Right();
                                            break;
                                        case "Buy":
                                            StoreManager.instance.SetBuyActive();
                                            break;
                                    }
                                }
                                else
                                {   // 구매창 켜졌을 때
                                    switch (hitInfo.collider.tag)
                                    {
                                        case "Confirm":
                                            DataSave.instance.data.Charge(DataSave.instance.data.gold, 1, Data.ItemType.Weapon, SelectBuyWeapon.instance.chapIndex);
                                            // SelectBuyWeapon의 현재 선택된 무기 배열 번호를 참조하면 됌
                                            ChangeWeapon(DataSave.instance.data.nowWeapon);
                                            break;
                                        case "Cancel":
                                            StoreManager.instance.SetBuyActive();
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (StoreManager.instance.getStoreActive == false)
                            {
                                if (hitInfo.collider.tag == "NPC")
                                {
                                    StoreManager.instance.SetStoreActive();
                                }
                                else if(hitInfo.collider.tag == "main")
                                {
                                    ChangeScene(hitInfo.collider.tag);
                                }
                                else
                                {
                                    transform.position = hitInfo.point + new Vector3(0, 6.19f, 0);
                                }
                            }
                        }
                        break;
                    case "BasicScene":
                    case "MathScene":
                    case "EnglishScene":
                    case "OXScene":
                        if (hitInfo.transform.gameObject.layer == 5)
                        {
                            Debug.Log(GameManager.instance.gamestate);

                            switch (hitInfo.collider.tag)
                            {
                                case "Pause":       // 일시정지
                                    GameManager.instance.GamePause();
                                    break;
                                case "Back":        // 계속하기
                                    GameManager.instance.GamePauseFin();
                                    break;
                                case "Home":        // 홈으로
                                    ChangeScene("main");
                                    break;
                                case "Next":
                                    Debug.Log("다음게임 계속 진행");            // 다음 게임 계속 진행하기
                                    break;
                            }
                            
                            if(hitInfo.collider.tag == "Right")
                            {
                                WaveManager.instance.CloseManual();
                            }
                        }
                        else
                        {
                            Fire(hitInfo.point);
                        }
                        break;
                }
            }
        }
    }

    //void Teleport()
    //{
    //    //if (mPredictPath.mIsActive == true) return;
    //    Vector3 pos = mPredictPath.mGroundPos;
    //    if (pos == Vector3.zero) return;
    //    mPlayer.transform.position = pos;
    //}

    public void ChangeWeapon(int projectileNum)
    {
        Bullet = Resources.Load<GameObject>("Prefabs/Projectile/Projectile " + (++projectileNum).ToString());
    }
    
    public void ChangeScene(string tag)
    {
        switch (tag)
        {
            case "main":
                SceneManager.LoadScene("Main", LoadSceneMode.Single);
                break;
            case "basic":
                SceneManager.LoadScene("BasicScene", LoadSceneMode.Single);
                break;
            case "math":
                SceneManager.LoadScene("MathScene", LoadSceneMode.Single);
                break;
            case "eng":
                SceneManager.LoadScene("EnglishScene", LoadSceneMode.Single);
                break;
            case "store":
                SceneManager.LoadScene("StoreScene", LoadSceneMode.Single);
                break;
            case "basicRetry":
                SceneManager.LoadScene("BasicLoding", LoadSceneMode.Single);
                break;
            case "mathRetry":
                SceneManager.LoadScene("MathLoding", LoadSceneMode.Single);
                break;
            case "englishRetry":
                SceneManager.LoadScene("EnglishLoding", LoadSceneMode.Single);
                break;
        }
    }

    void Fire(Vector3 target)
    {
        Instantiate(Bullet, FirePos.transform.position, this.transform.rotation).transform.forward = target - this.transform.position;
        //.GetComponent<Rigidbody>().velocity = (target - this.transform.position) * 10;
        theAudio.Play(shotSound);
    }
}
