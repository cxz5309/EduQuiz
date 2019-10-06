using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TmpLaycast : MonoBehaviour
{
    public Camera camera;
    public GameObject Bullet;
    public GameObject FirePos;
    string tag = "";

    public bool triggerSwitch;
    public string shotSound;
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

            if (Physics.Raycast(ray, out hitInfo, 100f))
            {
                int l = hitInfo.transform.gameObject.layer;
                
                switch (SceneManager.GetActiveScene().name)
                {
                    case "Main":
                        if (hitInfo.transform.gameObject.layer == 5)
                        {
                            if (hitInfo.collider.tag == "Close")
                            {
                                Debug.Log("GameClose");
                            }
                            switch (hitInfo.collider.name)
                            {
                                case "Basic":
                                case "Math":
                                case "English":
                                    tag = hitInfo.collider.tag;
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
                                    ChangeScene(tag);
                                    break;
                                case "Store":
                                    ChangeScene(hitInfo.collider.tag);
                                    break;
                            }
                            
                        }
                        else
                        {
                            if (!triggerSwitch)
                            { 
                                gameObject.transform.position = hitInfo.point + new Vector3(0, 3.7f, 0);
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
                                    gameObject.transform.position = hitInfo.point + new Vector3(0, 3.7f, 0);
                                }
                            }
                        }
                        break;
                    case "BasicScene":
                        if (hitInfo.transform.gameObject.layer == 5)
                        {
                            if (hitInfo.collider.tag == "Close")
                            {
                                Debug.Log("GameClose");
                            }
                            
                            else if (GameManager.instance.gamestate == GameManager.Gamestate.GamePause)
                            {
                                if (hitInfo.collider.tag == "Pause")
                                {
                                    GameManager.instance.GamePauseFin();
                                }
                                else
                                {
                                    ChangeScene(hitInfo.collider.tag);
                                }
                            }
                            else
                            {
                                ChangeScene(hitInfo.collider.tag);
                            }
                        }
                        if (GameManager.instance.gamestate == GameManager.Gamestate.GamePlaying)
                        {
                            Fire(hitInfo.point);
                            // 마우스 왼쪽버튼 클릭하면 총알 발사
                            if (hitInfo.collider.tag == "Pause")
                            {
                                GameManager.instance.GamePause();
                            }
                        }
                        break;
                    case "MathScene":
                        if (hitInfo.transform.gameObject.layer == 5)
                        {
                            if (hitInfo.collider.tag == "Close")
                            {
                                Debug.Log("GameClose");
                            }
                           
                            else if (GameManager.instance.gamestate == GameManager.Gamestate.GamePause)
                            {
                                if (hitInfo.collider.tag == "Pause")
                                {
                                    GameManager.instance.GamePauseFin();
                                }
                                else
                                {
                                    ChangeScene(hitInfo.collider.tag);
                                }
                            }
                            else
                            {
                                ChangeScene(hitInfo.collider.tag);
                            }
                        }
                        if (GameManager.instance.gamestate == GameManager.Gamestate.GamePlaying)
                        {
                            Fire(hitInfo.point);
                            // 마우스 왼쪽버튼 클릭하면 총알 발사
                            if (hitInfo.collider.tag == "Pause")
                            {
                                GameManager.instance.GamePause();
                            }
                        }
                        break;
                    case "EnglishScene":
                        if (hitInfo.transform.gameObject.layer == 5)
                        {
                            if (hitInfo.collider.tag == "Close")
                            {
                                Debug.Log("GameClose");
                            }
                          
                            else if (GameManager.instance.gamestate == GameManager.Gamestate.GamePause)
                            {
                                if (hitInfo.collider.tag == "Pause")
                                {
                                    GameManager.instance.GamePauseFin();
                                }
                                else
                                {
                                    ChangeScene(hitInfo.collider.tag);
                                }
                            }
                            else
                            {
                                ChangeScene(hitInfo.collider.tag);
                            }
                        }
                        if (GameManager.instance.gamestate == GameManager.Gamestate.GamePlaying)
                        {
                            Fire(hitInfo.point);
                            // 마우스 왼쪽버튼 클릭하면 총알 발사
                            if (hitInfo.collider.tag == "Pause")
                            {
                                GameManager.instance.GamePause();
                            }
                        }
                        break;


                }
            }
        }
    }

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
