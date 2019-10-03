using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TmpLaycast : MonoBehaviour
{
    public Camera camera;
    public GameObject Bullet;
    
    void Update()
    {
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
                            ChangeScene(hitInfo.collider.tag);
                        }
                        else
                        {
                            gameObject.transform.position = hitInfo.point + new Vector3(0, 3.7f, 0);
                        }
                        break;
                    case "StoreScene":
                        if (hitInfo.collider.tag == "NPC")
                        {
                            SceneChange.instance.SetStoreActive();
                        }
                        else
                        {
                            gameObject.transform.position = hitInfo.point + new Vector3(0, 3.7f, 0);
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

    public void ChangeWeapon()
    {
        //!!!!!!!!!!!!!!!!!!!!
    }


    public void ChangeScene(string e)
    {
        switch (e)
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
        Instantiate(Bullet, this.transform.position, this.transform.rotation).transform.forward = target - this.transform.position;
        //.GetComponent<Rigidbody>().velocity = (target - this.transform.position) * 10;
        Sound.instance.shoot_sound();
    }
}
