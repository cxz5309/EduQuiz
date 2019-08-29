using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TmpLaycast : MonoBehaviour
{
    public Camera camera;
    public GameObject Bullet;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, 100f))
            {
                int l = hitInfo.transform.gameObject.layer;
                
                Debug.Log(" hit object : " + hitInfo.collider.name);
                switch (SceneManager.GetActiveScene().name)
                {
                    case "Main":
                        ChangeScene(hitInfo.collider.tag);
                        break;
                    case "BasicScene":
                        if (GameManager_B.instance.gamestate == GameManager_B.Gamestate.GamePlaying)
                        {
                            Fire(hitInfo.point);
                            Debug.Log(" hit point : " + hitInfo.point);
                            // 마우스 왼쪽버튼 클릭하면 총알 발사
                            if (hitInfo.collider.tag == "Pause")
                            {
                                GameManager_B.instance.GamePause();
                            }
                        }
                        else if (GameManager_B.instance.gamestate == GameManager_B.Gamestate.GamePause)
                        {
                            if (hitInfo.collider.tag == "Pause")
                            {
                                GameManager_B.instance.GamePauseFin();
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
                        break;
                    case "MathScene":
                        if (GameManager_B.instance.gamestate == GameManager_B.Gamestate.GamePlaying)
                        {
                            Fire(hitInfo.point);
                            // 마우스 왼쪽버튼 클릭하면 총알 발사
                            if (hitInfo.collider.tag == "Pause")
                            {
                                GameManager_B.instance.GamePause();
                            }
                        }
                        else if (GameManager_B.instance.gamestate == GameManager_B.Gamestate.GamePause)
                        {
                            if (hitInfo.collider.tag == "Pause")
                            {
                                GameManager_B.instance.GamePauseFin();
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
                        break;
                    case "EnglishScene":
                        if (GameManager_E.instance.gamestate == GameManager_E.Gamestate.GamePlaying)
                        {
                            Fire(hitInfo.point);
                            // 마우스 왼쪽버튼 클릭하면 총알 발사
                            if (hitInfo.collider.tag == "Pause")
                            {
                                GameManager_E.instance.GamePause();
                            }
                        }
                        else if (GameManager_E.instance.gamestate == GameManager_E.Gamestate.GamePause)
                        {
                            if (hitInfo.collider.tag == "Pause")
                            {
                                GameManager_E.instance.GamePauseFin();
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
                        break;
                }
            }
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
        Instantiate(Bullet, this.transform.position, this.transform.rotation).GetComponent<Rigidbody>().velocity=(target-this.transform.position)*10;
        Sound.instance.shoot_sound();
    }
}
