using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TmpLaycast : MonoBehaviour
{
    public Camera camera;
    public GameObject Bullet;
    public GameObject FirePos;

    public bool triggerSwitch;
    public static bool youCantDoAnything;

    public string shotSound = "Shot";
    private AudioManager theAudio;

    public int[] stage;
    public int stageOrder = 0;
    private void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        ChangeWeapon(DataSave.instance.data.nowWeapon);
        stage = new int[10];
        GetRandomInt();
    }

    void Update()
    {
        
            if (Input.GetKeyDown(KeyCode.Space))
            {
                triggerSwitch = (triggerSwitch == true) ? false : true;
                Debug.Log("트리거 변경");
            }

            if (Input.GetMouseButtonUp(0))
            {
                int layerMask = (-1) - (1 << LayerMask.NameToLayer("Ignore Raycast"));
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo, 1000f, layerMask))
                {
                    Debug.Log(hitInfo.transform.name);
                    switch (SceneManager.GetActiveScene().name)
                    {
                        case "Main":
                        if (youCantDoAnything)
                        {
                            switch (hitInfo.collider.tag)
                            {
                                case "Left":
                                    CanvasManager.instance.ChangeUniconText(hitInfo.collider.tag);
                                    break;
                                case "Right":
                                    CanvasManager.instance.ChangeUniconText(hitInfo.collider.tag);
                                    break;
                                case "Close":
                                    CanvasManager.instance.EndUniconText();
                                    break;
                            }
                            if (hitInfo.transform.gameObject.layer == 11)
                            {
                                UniconController.instance.ClickAniEvent(Random.Range(1, 7));
                            }
                        }
                        else
                        {
                            if (hitInfo.transform.gameObject.layer == 5)
                            {
                                switch (hitInfo.collider.tag)
                                {
                                    case "Store":
                                        ChangeScene(hitInfo.collider.tag);
                                        break;
                                    // =============================================================== 새로 추가한것들
                                    case "Level":
                                        PopupGamePotalWithLevel(int.Parse(hitInfo.collider.name));
                                        break;
                                    case "Confirm":
                                        if (CanvasManager.instance.currentPopup == "GamePotal")
                                        {
                                            ChangeScene(GameSceneRandomToString());// 겜씸으로 이동 하시죠
                                        }
                                        else if (CanvasManager.instance.currentPopup == "StorePotal")
                                            ChangeScene("store");       // 상점으로 이동 하시죠
                                        break;
                                    case "Setting":
                                        CanvasManager.instance.PopupChange("Setting");
                                        break;
                                    case "Back":
                                        CanvasManager.instance.PopupChange("Idle");
                                        break;
                                    case "Exit":
                                        Debug.Log("GameClose");     // 겜 종료 하시죠
                                        break;
                                    case "Music":
                                        AudioManager.instance.SetBackGroundMute();
                                        hitInfo.collider.GetComponent<ButtonToggle>().SetButtonToggle();
                                        break;
                                    case "Effect":
                                        AudioManager.instance.SetEffectMute();
                                        hitInfo.collider.GetComponent<ButtonToggle>().SetButtonToggle();
                                        break;
                                }
                            }
                            else if (hitInfo.transform.gameObject.layer == 11)
                            {
                                UniconController.instance.ClickAniEvent(Random.Range(1, 7));
                            }
                            else
                            {
                                if (!triggerSwitch)
                                {
                                    transform.position = hitInfo.point + new Vector3(0, 2.69f, 0);
                                }
                            }
                            if (triggerSwitch)
                            {
                                Fire(hitInfo.point);
                            }
                        }
                        break;
                        case "StoreScene":
                        if (hitInfo.transform.gameObject.layer == 5)
                        {
                            if (StoreManager.instance.getStoreActive == true)
                            {   // 상점창이 등장한 후
                                if (StoreManager.instance.getBuyActive == false)
                                {   // 구매창 안켜졌을 때
                                    switch (hitInfo.collider.tag)
                                    {
                                        case "Close":
                                            StoreManager.instance.SetStoreActive(hitInfo.collider.name);
                                            break;
                                        case "Left":
                                            StoreManager.instance.ThisScreenLeft();
                                            break;
                                        case "Right":
                                            StoreManager.instance.ThisScreenRight();
                                            break;
                                        case "Buy":
                                            StoreManager.instance.SetBuyActive();
                                            break;
                                    }
                                }
                                else
                                {
                                    // 구매창 켜졌을 때
                                    if (StoreManager.instance.NoticeScreen.activeSelf == false)
                                    {
                                        if (StoreManager.instance.storeSelectName == "Weapon")
                                        {
                                            switch (hitInfo.collider.tag)
                                            {
                                                case "Confirm":
                                                    DataSave.instance.data.Charge(DataSave.instance.data.gold, 50, Data.ItemType.Weapon, StoreManager.instance.GetThisScreenIndex());
                                                    // SelectBuyWeapon의 현재 선택된 무기 배열 번호를 참조하면 됌
                                                    StoreManager.instance.SetGold();
                                                    ChangeWeapon(DataSave.instance.data.nowWeapon);
                                                    break;
                                                case "Cancel":
                                                    StoreManager.instance.SetBuyActive();
                                                    break;
                                            }
                                        }
                                        else if (StoreManager.instance.storeSelectName == "Animal")
                                        {
                                            // 구매창 켜졌을 때
                                            switch (hitInfo.collider.tag)
                                            {
                                                case "Confirm":
                                                    DataSave.instance.data.Charge(DataSave.instance.data.gold, 50, Data.ItemType.Animal, StoreManager.instance.GetThisScreenIndex());
                                                    StoreManager.instance.SetGold();
                                                    break;
                                                case "Cancel":
                                                    StoreManager.instance.SetBuyActive();
                                                    break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (hitInfo.collider.tag == "Confirm")
                                            StoreManager.instance.SetNotice();
                                    }
                                }
                            }
                            else
                            {   //상점이 아직 안켜졌을때
                                //동물과 무기 선택창, 픽된 이름으로 상점을 선택
                                StoreManager.instance.SetStoreActive(hitInfo.collider.name);
                            }
                        }
                        else
                        {
                            if (hitInfo.collider.tag == "NPC")
                            {
                                StoreManager.instance.SetStoreSelect();
                            }
                            else if (hitInfo.collider.tag == "main")
                            {
                                ChangeScene(hitInfo.collider.tag);
                            }
                            else if(hitInfo.collider.tag == "Close")
                            {
                                StoreManager.instance.SetStoreSelect();
                            }
                            else
                            {
                                transform.position = hitInfo.point + new Vector3(0, 2.69f, 0);
                            }
                        }
                        break;
                        case "BasicScene":
                        case "MathScene":
                        case "EnglishScene":
                        case "OXScene":
                        if (hitInfo.transform.gameObject.layer == 5)
                        {
                            switch (hitInfo.collider.tag)
                            {
                                case "Pause":       // 일시정지
                                    GameManager.instance.GamePause();
                                    break;
                                case "Music":
                                    AudioManager.instance.SetBackGroundMute();
                                    hitInfo.collider.GetComponent<ButtonToggle>().SetButtonToggle();
                                    break;
                                case "Effect":
                                    AudioManager.instance.SetEffectMute();
                                    hitInfo.collider.GetComponent<ButtonToggle>().SetButtonToggle();
                                    break;

                                case "Back":        // 계속하기
                                    GameManager.instance.GamePauseFin();
                                    break;
                                case "Retry":
                                    ChangeScene(GetSceneNameToTag(SceneManager.GetActiveScene().name));
                                    break;
                                case "Home":        // 홈으로
                                    GameObject.Find("Unicon").transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                                    ChangeScene("main");
                                    Debug.Log("재시작시 같은 과목임");            // 다음 게임 계속 진행하기
                                    break;
                                case "Next":
                                    stageOrder++;
                                    GameObject.Find("Unicon").transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                                    ChangeScene(GameSceneRandomToString());
                                    Debug.Log("다음게임 계속 진행");            // 다음 게임 계속 진행하기
                                    break;
                            }

                            if (hitInfo.collider.tag == "Right")
                            {
                                WaveManager.instance.CloseManual();
                            }
                        }
                        else if(hitInfo.transform.gameObject.layer == 11)
                        {
                            if (GameManager.instance.gamestate != GameManager.Gamestate.GamePause)
                            {
                                GameManager.instance.GamePause();
                            }
                            else
                            {
                                GameManager.instance.GamePauseFin();
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

    public void PopupGamePotalWithLevel(int grade)
    {
        DataSave.instance.grade = grade;
        CanvasManager.instance.PopupChange("GamePotal");
    }

    public void ChangeWeapon(int projectileNum)
    {
        Bullet = Resources.Load<GameObject>("Prefabs/Projectile/Projectile " + (++projectileNum).ToString());
    }
    
    public string GetSceneNameToTag(string sceneName)
    {
        switch (sceneName)
        {
            default:
                return "Main";
            case "BasicScene":
                return "basicRetry";
            case "MathScene":
                return "mathRetry";
            case "EnglishScene":
                return "enghishRetry";
            case "OXScene":
                return "OXRetry";
        }
    }

    public string GameSceneRandomToString()
    {
        switch(stage[stageOrder])
        {
            case 0:
                return "basicRetry";
            case 1:
                return "mathRetry";
            case 2:
                return "englishRetry";
            case 3:
                return "OXRetry";
            case 4:
                return "resultRetry";
        }
        return "main";
    }

    public void ChangeScene(string tag)
    {
        switch (tag)
        {
            case "main":
                SceneManager.LoadScene("Main", LoadSceneMode.Single);
                break;
            case "Basic":
                SceneManager.LoadScene("BasicScene", LoadSceneMode.Single);
                break;
            case "Math":
                SceneManager.LoadScene("MathScene", LoadSceneMode.Single);
                break;
            case "English":
                SceneManager.LoadScene("EnglishScene", LoadSceneMode.Single);
                break;
            case "OX":
                SceneManager.LoadScene("OXScene", LoadSceneMode.Single);
                break;
            case "OXRetry":
                SceneManager.LoadScene("OXLoding", LoadSceneMode.Single);
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
            case "store":
                SceneManager.LoadScene("StoreScene", LoadSceneMode.Single);
                break;
            case "resultRetry":
                SceneManager.LoadScene("ResultLoding", LoadSceneMode.Single);
                break;
            case "result":
                SceneManager.LoadScene("ResultScene", LoadSceneMode.Single);
                break;
        }
    }

    void Fire(Vector3 target)
    {
        Instantiate(Bullet, FirePos.transform.position, this.transform.rotation).transform.forward = target - this.transform.position;
        theAudio.Play(shotSound);
    }
    public void GetRandomInt()
    {
        int[] stageChk = new int[4];

        // 랜덤숫자 반복 방지를 위한 체크배열 변수(스폰지역 수만큼 배열크기 지정)
        for (int i = 0; i < stageChk.Length; i++)
        {
            int tmpStage = Random.Range(0, stageChk.Length);
            // EnemySpawnChk 배열의 크기만큼 랜덤시드
            if (stageChk[tmpStage] == 0)
            {   // 체크된 배열이 아닌경우
                stageChk[tmpStage] = 1;
                // 해당 랜덤숫자배열 체크
                stage[i] = tmpStage;
            }
            else if (stageChk[tmpStage] == 1)
            {   // 이미 생성된 랜덤숫자일 때
                i--;
                // i를 한단계 되돌려준다.
            }
        }
        stage[4] = 4;
    }
}
