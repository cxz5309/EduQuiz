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
                ChangeScene(e.target.gameObject.tag);
                break;
            case "BasicScene":
                if (GameManager_B.instance.gamestate == GameManager_B.Gamestate.GamePlaying)
                {
                    Fire(e.target.transform.position);
                    // 마우스 왼쪽버튼 클릭하면 총알 발사
                    if (e.target.gameObject.tag == "Pause")
                    {
                        GameManager_B.instance.GamePause();
                    }
                }
                else if(GameManager_B.instance.gamestate == GameManager_B.Gamestate.GamePause)
                {
                    if (e.target.gameObject.tag == "Pause")
                    {
                        GameManager_B.instance.GamePauseFin();
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
                break;
            case "MathScene":
                if (GameManager_M.instance.gamestate == GameManager_M.Gamestate.GamePlaying)
                {
                    Fire(e.target.transform.position);
                    // 마우스 왼쪽버튼 클릭하면 총알 발사
                    if (e.target.gameObject.tag == "Pause")
                    {
                        GameManager_M.instance.GamePause();
                    }
                }
                else if (GameManager_M.instance.gamestate == GameManager_M.Gamestate.GamePause)
                {
                    if (e.target.gameObject.tag == "Pause")
                    {
                        GameManager_M.instance.GamePauseFin();
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
                break;
            case "EnglishScene":
                if (GameManager_E.instance.gamestate == GameManager_E.Gamestate.GamePlaying)
                {
                    Fire(e.target.transform.position);
                    // 마우스 왼쪽버튼 클릭하면 총알 발사
                    if (e.target.gameObject.tag == "Pause")
                    {
                        GameManager_E.instance.GamePause();
                    }
                }
                else if (GameManager_E.instance.gamestate == GameManager_E.Gamestate.GamePause)
                {
                    if (e.target.gameObject.tag == "Pause")
                    {
                        GameManager_E.instance.GamePauseFin();
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
        Instantiate(Bullet, FirePos.position, FirePos.rotation).GetComponent<Rigidbody>().velocity = (target - this.transform.position) * 10;
        Sound.instance.shoot_sound();
    }
}