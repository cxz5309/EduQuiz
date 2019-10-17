using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    static public CanvasManager instance;

    public GameObject[] popup;      // 팝업 종류
    public string currentPopup;

    private Transform playerTr = null;   // 플레이어 위치
    public float distanceZ;      // 플레이어와의 Z축거리
    public float distanceY;      // 플레이어와의 Y축거리
    public float speed;         // 팝업 이동속도

    private string popupChangeSound;    // 팝업 바뀔 때 소리
    private AudioManager theAudio;

    public Text uniconText = null;
    public string[] conversations;
    public int conversationIndex = 0;

    public Text waveText = null;          // 웨이브
    public Text quizText = null;           // 지문
    public Image engImage = null;           // 그림

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

    public void SetConversation()
    {
        conversations = new string[11]
        { "안녕, 난 '유니'라고 해!\n( < > 버튼을 Click 하세요 )",
          "이곳은 '꿈의 목장'이야.\n너가 원하는 곳으로 돌아다닐 수 있고 동물을 키울 수 있어!",
          "하지만, 나쁜 몬스터들이 동물을 빼았기 위해 몰려오고 있어!!!",
          "몬스터 출몰 장소로 이동해서 몬스터를 물리쳐야 해!",
          "퀴즈를 풀어야 잡을 수 있는 아주 악독한 녀석들이지.",
          "몬스터를 물리치는데 성공하면 돈을 얻을 수 있고, 상점에서 새로운 무기와 동물을 살 수 있어",
          "퀴즈를 풀면서 나오는 아이템도 잘 활용해봐!",
          "마지막으로, 조작법을 알려줄게.",
          "트리거를 누르면 이동과 공격이 가능해.\n(SpaceBar : 이동/공격 변환)",
          "게임중에 쉬고싶거나 마을로 돌아오고 싶다면 나를 클릭해줘.",
          "( X 버튼을 Click 하세요 )"
        };
    }

    public void UniconConversation()
    {
        PopupChange("Conversation");
        PopupPosition(-5, -15);

        SetConversation();
        StartUniconText();
    }


    public void StartUniconText()
    {
        TmpLaycast.youCantDoAnything = true;

        uniconText = GameObject.Find("Description").GetComponent<Text>();
        uniconText.text = conversations[0];
    }

    public void ChangeUniconText(string tag)
    {
        switch (tag)
        {
            case "Left":
                Left();
                break;
            case "Right":
                Right();
                break;
        }
    }
    public void Left()
    {
        conversationIndex -= 1;
        if (conversationIndex < 0)
        {
            conversationIndex = 0;
            return;
        }
        uniconText.text = conversations[conversationIndex];
    }

    public void Right()
    {
        conversationIndex += 1;
        if (conversationIndex >= conversations.Length)
        {
            conversationIndex = conversations.Length - 1;
            return;
        }
        uniconText.text = conversations[conversationIndex];
    }

    public void EndUniconText()
    {
        TmpLaycast.youCantDoAnything = false;

        PopupChange("Idle");
        PopupPosition(0, -10);
    }

    public void PopupPosition(int y, int z)
    {
        distanceY = y;
        distanceZ = z;
    }

    public void SetQuizText(string text)
    {
        quizText.text = text;
    }
    public void SetStageText(string text)
    {
        waveText.text = text;
    }
    public void SetImage(Sprite sprite)
    {
        engImage.overrideSprite = sprite;
    }

    void Update()
    {

        transform.position = Vector3.Lerp(transform.position, new Vector3(playerTr.position.x, playerTr.position.y + distanceY, playerTr.position.z + distanceZ), Time.deltaTime * speed);
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
