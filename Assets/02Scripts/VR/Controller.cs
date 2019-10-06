using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.Extras;

public class Controller : SteamVR_LaserPointer
{
    public Transform FirePos;   // 총구
    public GameObject Bullet;   // 총알 오브젝트를 가져오기 위한 변수

    public override void OnPointerClick(PointerEventArgs e)
    {
        base.OnPointerClick(e);
        switch (SceneManager.GetActiveScene().name)
        {
            case "Main":
                if (e.target.gameObject.layer == 5)
                {
                    if (e.target.gameObject.tag == "Close")
                    {
                        Application.Quit();
                    }
                    ChangeScene(e.target.gameObject.tag);
                }
                else
                {
                    //텔레포트 구현할것
                }
                break;
            case "StoreScene":
                if (e.target.gameObject.layer == 5)
                {
                    //상점 기능 참조
                }
                else
                {
                    if (e.target.gameObject.tag == "NPC")
                    {
                        StoreManager.instance.SetStoreActive();
                    }
                    else
                    {
                        //텔레포트 구현할것
                    }
                }
                break;
            case "BasicScene":
                if (e.target.gameObject.layer == 5)
                {
                    if (e.target.gameObject.tag == "Close")
                    {
                        Application.Quit();
                    }
                    else if (GameManager.instance.gamestate == GameManager.Gamestate.GamePause)
                    {
                        if (e.target.gameObject.tag == "Pause")
                        {
                            GameManager.instance.GamePauseFin();
                        }
                        else
                        {
                            ChangeScene(e.target.gameObject.tag);
                        }
                    }
                    else
                    {
                        ChangeScene(e.target.gameObject.tag);
                    }
                }
                if (GameManager.instance.gamestate == GameManager.Gamestate.GamePlaying)
                {
                    Fire(e.target.transform.position);
                    // 마우스 왼쪽버튼 클릭하면 총알 발사
                    if (e.target.gameObject.tag == "Pause")
                    {
                        GameManager.instance.GamePause();
                    }
                }
                break;
            case "MathScene":
                if (e.target.gameObject.layer == 5)
                {
                    if (e.target.gameObject.tag == "Close")
                    {
                        Application.Quit();
                    }
                    else if (GameManager.instance.gamestate == GameManager.Gamestate.GamePause)
                    {
                        if (e.target.gameObject.tag == "Pause")
                        {
                            GameManager.instance.GamePauseFin();
                        }
                        else
                        {
                            ChangeScene(e.target.gameObject.tag);
                        }
                    }
                    else
                    {
                        ChangeScene(e.target.gameObject.tag);
                    }
                }
                if (GameManager.instance.gamestate == GameManager.Gamestate.GamePlaying)
                {
                    Fire(e.target.transform.position);
                    // 마우스 왼쪽버튼 클릭하면 총알 발사
                    if (e.target.gameObject.tag == "Pause")
                    {
                        GameManager.instance.GamePause();
                    }
                }
                break;
            case "EnglishScene":
                if (e.target.gameObject.layer == 5)
                {
                    if (e.target.gameObject.tag == "Close")
                    {
                        Application.Quit();
                    }
                    else if (GameManager.instance.gamestate == GameManager.Gamestate.GamePause)
                    {
                        if (e.target.gameObject.tag == "Pause")
                        {
                            GameManager.instance.GamePauseFin();
                        }
                        else
                        {
                            ChangeScene(e.target.gameObject.tag);
                        }
                    }
                    else
                    {
                        ChangeScene(e.target.gameObject.tag);
                    }
                }
                if (GameManager.instance.gamestate == GameManager.Gamestate.GamePlaying)
                {
                    Fire(e.target.transform.position);
                    // 마우스 왼쪽버튼 클릭하면 총알 발사
                    if (e.target.gameObject.tag == "Pause")
                    {
                        GameManager.instance.GamePause();
                    }
                }
                break;
        }
    }
    public void ChangeScene(string e)
    {
        switch (e)
        {
            case "basic":
                SceneManager.LoadScene("BasicScene", LoadSceneMode.Single);
                break;
            case "math":
                SceneManager.LoadScene("MathScene", LoadSceneMode.Single);
                break;
            case "eng":
                SceneManager.LoadScene("EnglishScene", LoadSceneMode.Single);
                break;
            case "main":
                SceneManager.LoadScene("Main", LoadSceneMode.Single);
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
        Instantiate(Bullet, FirePos.position, FirePos.rotation);
            //.GetComponent<Rigidbody>().velocity = (target - this.transform.position) * 10;
        //Sound.instance.shoot_sound();
    }
}