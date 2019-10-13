using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    static public CanvasManager instance;

    public GameObject[] popup;      // 팝업 종류
    public string currentPopup;

    private Transform playerTr = null;   // 플레이어 위치
    public float distance;      // 플레이어와의 거리
    public float speed;         // 팝업 이동속도

    private string popupChangeSound;    // 팝업 바뀔 때 소리
    private AudioManager theAudio;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        theAudio = FindObjectOfType<AudioManager>();
        if (GameObject.FindWithTag("Player") != null)
        {
            playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }
        else if (GameObject.FindWithTag("Player") == null)
        {
            Debug.Log("플레이어를 찾을 수 없습니다. 게임을 종료합니다.");
        }
    }

    void Update()
    {

        transform.position = Vector3.Lerp(transform.position, new Vector3(playerTr.position.x, playerTr.position.y, playerTr.position.z + distance), Time.deltaTime * speed);
    }

    public void PopupChange(string _currentPopup)
    {
        Debug.Log("popup 체인지");
        for (int i = 0; i < popup.Length; i++)      // 현재 팜업 끄기
        {
            popup[i].SetActive(false);
        }

        for (int i = 0; i < popup.Length; i++)      // 현재 팜업 켜기
        {
            if (popup[i].name == _currentPopup)
            {
                currentPopup = _currentPopup;
                popup[i].SetActive(true);
                //theAudio.Play(popupChangeSound);
                return;
            }
        }
    }
}
