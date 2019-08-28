using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Value_B
{
    public int num;         // 문제 번호
    public string quiz;     // 문제 지문
    public string[] ans = new string[4];    // 답1, 2, 3, 4
    public int pass;    // 정답 0번자리

    public Value_B(int _num, string _quiz, string _ans1, string _ans2, string _ans3, string _ans4, int _pass)
    {
        this.num = _num;
        this.quiz = _quiz;
        this.ans[0] = _ans1;
        this.ans[1] = _ans2;
        this.ans[2] = _ans3;
        this.ans[3] = _ans4;
        this.pass = _pass;
    }
}

public class QuizManager_B : MonoBehaviour
{
    public static QuizManager_B instance;

    public Dictionary<int, Value_B> dictionary;
    public Dictionary<int, Value_B> dictionary_temp;   // 문제를 섞기 위한 임시 변수

    void Awake()
    {
        instance = this;

        dictionary = new Dictionary<int, Value_B>();


        dictionary.Add(0, new Value_B(0,
            "나는 캄캄한 (  )에 등불도 없이 걸어가다 넘어져 무릎을 다쳤다.",
            "밤", "아침", "낮", "해질녁", 0));
        dictionary.Add(1, new Value_B(1,
                    "반대쪽으로 틀다",
                    "되틀다", "배틀다", "외틀다", "안틀다", 0));
        dictionary.Add(2, new Value_B(2,
                    "그릇 위까지 수북하게 담은 밥",
                    "감투밥", "구메밥", "고두밥", "가윗밥", 0));
        dictionary.Add(3, new Value_B(3,
                    "남쪽에서 부는 바람을 이르는 말",
                    "마파람", "샛바람", "하늬바람", "된바람", 0));
        dictionary.Add(4, new Value_B(4,
                    "'옆에서'를 정확하게 소리내어 읽은 것은?",
                    "여페서", "옆해서", "여베서", "엽해서", 0));
        dictionary.Add(5, new Value_B(5,
                    "'받았어'를 정확하게 소리내어 읽은 것은?",
                    "바다써", "받아써", "바닸어", "밨아써", 0));
        dictionary.Add(6, new Value_B(6,
                    "'앞으로'를 정확하게 소리내어 읽은 것은?",
                    "아프로", "앞프로", "압으로", "압프로", 0));
        dictionary.Add(7, new Value_B(7,
            "알맞은 문장 부호를 만들어 보세요." +
            "구현아, 무슨 노래를 부르니( )",
            "?", ".", ",", "!", 0));
        dictionary.Add(8, new Value_B(8,
                    "어름치는 (  )에 사는 동물입니다.",
                    "강", "바다", "산", "호수", 0));
        dictionary.Add(9, new Value_B(9,
                    "몸이 굳게 억세다",
                    "걱세다", "까세다", "꺽세다", "검세다", 0));


        RandomNumber();     // 문제 랜덤섞기
    }

    //문제 랜덤섞기 메소드
    void RandomNumber()
    {

        System.Random prng = new System.Random();

        dictionary_temp = new Dictionary<int, Value_B>();

        for (int i = 0; i < dictionary.Count - 1; i++)
        {
            int randomIndex = prng.Next(i, dictionary.Count);   // randomIndex에 랜덤 숫자가 지정
            dictionary_temp[randomIndex] = dictionary[i];
            dictionary[i] = dictionary[randomIndex];
            dictionary[randomIndex] = dictionary_temp[randomIndex];
        }
    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;


//public class Value_B
//{
//    public int num;         // 문제 번호
//    public string quiz;     // 문제 지문
//    public string[] ans = new string[4];    // 답1, 2, 3, 4
//    public int pass;    // 정답 0번자리

//    public Value_B(int _num, string _quiz, string _ans1, string _ans2, string _ans3, string _ans4, int _pass)
//    {
//        this.num = _num;
//        this.quiz = _quiz;
//        this.ans[0] = _ans1;
//        this.ans[1] = _ans2;
//        this.ans[2] = _ans3;
//        this.ans[3] = _ans4;
//        this.pass = _pass;
//    }
//}

//public class QuizManager_B : MonoBehaviour
//{
//    public static QuizManager_B instance;

//    public Dictionary<int, Value_B> dictionary;
//    public Dictionary<int, Value_B> dictionary_temp;   // 문제를 섞기 위한 임시 변수

//    void Awake()
//    {
//        instance = this;

//        dictionary = new Dictionary<int, Value_B>();


//        dictionary.Add(0, new Value_B(0,
//            "태양계에서 인간이 살고 있는 행성은?",
//            "지구", "화성", "달", "태양", 0));

//        dictionary.Add(1, new Value_B(1,
//                    "다음 중 인간의 정상 체온은?? ?",
//                    "36.8℃", "35.5℃", "37.8℃", "38.5℃", 0));

//        dictionary.Add(2, new Value_B(2,
//                    "지구에서 가장 큰 바다는?",
//                    "태평양", "대서양", "북극해", "인도양", 0));


//        dictionary.Add(3, new Value_B(3,
//                    "로마숫자 IX 에 상응하는 아랍 숫자는??",
//                    "9", "100", "7", "11", 0));


//        dictionary.Add(4, new Value_B(4,
//                    "가장 작은 소수는???",
//                    "2", "1", "0", "-1", 0));

//        dictionary.Add(5, new Value_B(5,
//                    "성인 한 명이 가진 뼈 개수는??",
//                    "206", "16", "1006", "106", 0));


//        dictionary.Add(6, new Value_B(6,
//                    "원소 H는 무엇인가요? ??",
//                    "수소", "산소", "헬륨", "나트륨", 0));


//        dictionary.Add(7, new Value_B(7,
//                    "당신은 시속 90킬로미터로 고속도로에서 등속으로 운전하고 있습니다. 45킬로미터 를 가려면 시간이 얼마나 소요되나요? ",
//                    "30분", "1시간", "45시간", "2시간", 0));


//        dictionary.Add(8, new Value_B(8,
//                    "현대 교류 (AC) 전기 공급 시스템의 설계에 대한 공헌으로 가장 잘 알려진 사람은 누구입니까?",
//                    "니콜라 테슬라", "엘론 머스크", "토마스 에디슨", "아이작 뉴턴", 0));


//        dictionary.Add(9, new Value_B(9,
//                    "아인슈타인의 출생지는?",
//                    "독일", "미국", "스위스", "오스트리아", 0));
//        dictionary.Add(10, new Value_B(10,
//                    "지구에서 가장 가까운 행성은?",
//                    "금성", "화성", "목성", "수성", 0));

//        dictionary.Add(11, new Value_B(11,
//                    "지구에서 가장 가까운 행성은?",
//                    "금성", "화성", "목성", "수성", 0));

//        dictionary.Add(11, new Value_B(11,
//                    "미국의 초대 대통령은??",
//                    "조지 워싱턴", "존 아담스", "벤자민 프랭클린", "토마스 제퍼슨", 0));

//        dictionary.Add(12, new Value_B(12,
//                    "호주의 수도?",
//                    "캔버라", "시드니", "멜버른", "모두 틀리다", 0));

//        dictionary.Add(13, new Value_B(13,
//                    "미국 대부분의 인구가 사용하는 언어는??",
//                    "영어", "불어", "스페인어", "독일어", 0));

//        dictionary.Add(14, new Value_B(14,
//                    "미국 대부분의 인구가 사용하는 언어는??",
//                    "영어", "불어", "스페인어", "독일어", 0));

//        dictionary.Add(15, new Value_B(15,
//                    "삼각형의 면적이 20cm² 이고 아래변의 길이는 5cm일 떄, 높이는 몇 cm입니까？?",
//                    "8cm", "6cm", "4cm", "2cm", 0));

//        dictionary.Add(16, new Value_B(16,
//                    "캐나다의 수도는?",
//                    "오타와", "퀘벡", "몬트리올", "토론토", 0));

//        dictionary.Add(17, new Value_B(17,
//                    "캐나다의 수도는?",
//                    "오타와", "퀘벡", "몬트리올", "토론토", 0));

//        dictionary.Add(18, new Value_B(18,
//                    "스웨덴의 수도는?",
//                    "스톡홀름", "웁살라", "비스뷔", "예테보리", 0));

//        dictionary.Add(19, new Value_B(19,
//                    "스페인의 수도는?",
//                    "마드리드", "톨레도", "세비야", "론다", 0));

//        dictionary.Add(20, new Value_B(20,
//                    "베트남의 수도는?",
//                    "하노이", "다낭", "무이네", "달랏", 0));

//        dictionary.Add(21, new Value_B(21,
//                    "윤봉길 의사가 홍커우 공원에서 던진 폭탄은 무엇이었을까요 ?", 

//                    "물통", "도시락", "신문", "시계", 0));

//        dictionary.Add(22, new Value_B(22,
//                    "지구 표면 위에 있는 두 지점사이의 가장 짧은 거리를 뜻하는 단어는 ? ", 

//                    "대권항로", "남극항로", "등각항로", "추천항로", 0));

//        dictionary.Add(23, new Value_B(23,
//                    "세계에서 가장 깊은 호수는?",
//                    "바이칼호", "카스피해", "사해", "백록담", 0));

//        dictionary.Add(24, new Value_B(24,
//                    "세계에서 섬이 가장 많은 나라는?",
//                    "인도네시아", "일본", "대한민국", "파푸아뉴기니", 0));

//        dictionary.Add(25, new Value_B(25,
//                    "세계에서 섬이 가장 많은 나라는?",
//                    "인도네시아", "일본", "대한민국", "파푸아뉴기니", 0));

//        dictionary.Add(26, new Value_B(26,
//                    "제트기가 급강하하여 음속을 돌파할 때 내는 충격파 때문에 생기는 폭발음은 ? ", 

//                    "소닉붐", "테일즈", "노바소닉", "슈퍼소닉", 0));

//        dictionary.Add(27, new Value_B(27,
//                    "고전소설이며, 우리나라 최초의 한글 소설로 전해지는 이 소설의 이름은 ? ", 

//                    "홍길동전", "금오신화", "구운몽", "춘향전", 0));


//        dictionary.Add(27, new Value_B(27,
//                    "명란젓은 어느 나라 음식인가요?",
//                    "대한민국", "일본", "중국", "러시아", 0));

//        dictionary.Add(28, new Value_B(28,
//                    "4차 산업 혁명과 관련 없는 것은?",
//                    "전기", "빅데이터", "인공지능", "로봇", 0));
//        dictionary.Add(29, new Value_B(29,
//            "4차 산업 혁명과 관련 없는 것은?",
//            "인터넷", "드론", "자율주행", "가상현실", 0));
//        dictionary.Add(30, new Value_B(30,
//            "4차 산업 혁명과 관련 없는 것은?",
//            "TR", "AR", "VR", "MR", 0));
//        dictionary.Add(31, new Value_B(31,
//            "4차 산업 혁명과 관련 없는 것은?",
//            "2D 프린터", "핀테크", "사물인터넷", "양자 컴퓨터", 0));
//        dictionary.Add(32, new Value_B(32,
//            "4차 산업 혁명과 관련 없는 것은?",
//            "가상화폐", "블록체인", "융합현실", "알파고", 0));
//        dictionary.Add(33, new Value_B(33,
//            "AR은 (   )의 줄임말이다.",
//            "Augmented Reality", "Acoustic Research", "Autonomous Region", "ARgon", 0));
//        dictionary.Add(34, new Value_B(34,
//            "VR은 (   )의 줄임말이다.",
//            "Virtual Reality", "Volume Ratio", "Velocity Rotation", "Valance Reality", 0));

//        dictionary.Add(35, new Value_B(35,
//                    "미국 1달러 지폐의 앞면에 그려진 인물은?", "워싱턴", "링컨", "제퍼슨", "루즈벨트", 0));
//        RandomNumber();     // 문제 랜덤섞기
//    }

//    // 문제 랜덤섞기 메소드
//    void RandomNumber() {

//        System.Random prng = new System.Random();

//        dictionary_temp = new Dictionary<int, Value_B>();

//        for (int i = 0; i < dictionary.Count - 1; i++) {
//            int randomIndex = prng.Next(i, dictionary.Count);   // randomIndex에 랜덤 숫자가 지정
//            dictionary_temp[randomIndex] = dictionary[i];
//            dictionary[i] = dictionary[randomIndex];
//            dictionary[randomIndex] = dictionary_temp[randomIndex];
//        }
//    }
//}