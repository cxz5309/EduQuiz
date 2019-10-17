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
        conversations = new string[12]
        { "안녕, 난 '유니'라고 해!\n'꿈의 농장'에 온것을 환영해!\n(화살표 버튼을 누르세요.)",
          "처음이 아니라면 아래의 close 버튼을 누르면 바로 시작할 수 있어",
          "여기는 너의 목장이야\n 우리는 이곳에서 동물을 키울 수 있어",
          "지금 나쁜 몬스터들이 너의 동물들을 노리고 있대",
          "몬스터들은 주어진 문제를 맞추게 되면 사라질거야",
          "눈앞의 포탈로 들어가면 나쁜 몬스터들을 물리칠 수 있어",
          "목장에서는 트리거를 눌러서 이동할 수 있고\n 아무데나 총쏘기도 가능해",
          "스페이스바를 눌러서 이동과 공격을 변경할 수 있어",
          "상점에서는 새로운 총과 동물들을 구매할 수 있어",
          "문제를 모두 맞춰 모든 몬스터를 물리치면 구매할 수 있는 돈을 얻을거야",
          "게임중에 쉬고싶거나 나가고싶다면 나를 다시한번 눌러줘",
          "그럼 close 버튼을 눌러 게임을 시작하자"
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
