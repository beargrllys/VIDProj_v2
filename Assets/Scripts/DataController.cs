using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct quizSet
{
    public string quiz_problem;
    public string answ;
    public string plnt1;
    public string plnt2;
    public string plnt3;

    public quizSet(string _quiz_problem, string _answ, string _plnt1, string _plnt2, string _plnt3)
    {
        this.quiz_problem = _quiz_problem;
        this.answ = _answ;
        this.plnt1 = _plnt1;
        this.plnt2 = _plnt2;
        this.plnt3 = _plnt3;
    }
}
public class DataController : MonoBehaviour
{
    public Sprite[] planetIcon = new Sprite[9];
    public string[] feature1 = new string[9] {
        "스스로 빛을 내요",
        "암석으로 이루어져 있어요",
        "암석으로 이루어져 있어요",
        "암석으로 이루어져 있어요",
        "암석으로 이루어져 있어요",
        "기체로 이루어져 있어요",
        "기체로 이루어져 있어요",
        "기체로 이루어져 있어요",
        "기체로 이루어져 있어요"
    };
    public string[] feature2 = new string[9] {
        "태양계의 중심이에요",
        "운석 구덩이가 많아요",
        "대기에 이산화탄소가 많아요",
        "물이 있고 생명체가 살아요",
        "붉은색을 띄고 있어요",
        "희미한 고리와 수많은 위성이 있어요",
        "뚜렷한 고리와 수많은 위성이 있어요",
        "옆으로 누워서 태양을 돌아요",
        "태양계의 마지막 행성이에요"
    };
    public string[] feature3 = new string[9] {
        "아주 뜨겁고 밝아요",
        "위성과 고리가 없어요",
        "위성과 고리가 없어요",
        "1개의 위성(달)을 가지고 있어요",
        "고리 없이 2개의 위성만 가지고 있어요",
        "태양계에서 가장 큰 행성이에요",
        "연노란색을 띄고 얇은 줄무늬를 가져요.",
        "많은 위성과 희미한 고리를 가져요.",
        "많은 위성과 희미한 고리를 가져요."
    };
    public string[] feature4 = new string[9] {
        "다른 행성을 합친 크기보다 커요",
        "가장 크기가 작은 행성이에요",
        "지구와 크기가 비슷해요.",
        "우리가 살아가는 행성이에요",
        "지구의 절반 크기에요",
        "가운데 커다란 폭풍이 불어요",
        "밀도가 아주 작아요",
        "청록색 대기를 가져요",
        "표면에 커다란 폭풍이 불고있어요"
    };

    public string[] Explain = new string[9] {
        "태양은 태양계의 중심으로 아주 크고 뜨거워서\n 언제나 우리 태양계를 밝게 비추고 있다.",
        "수성은 태양과 가장 가까운 행성이어서 관측하기가 쉽지 않다.\n태양을 바라보는 면은 아주 뜨겁지만 반대편은 아주 춥다.",
        "금성은 온실효과 때문에 표면온도가 아주 뜨겁다.\n지구와 가장 가까이 있어서 가장 밝게 보이는 행성이다.",
        "태양의 3번째 행성인 지구는 하나의 위성인 달을 가지고 있다.\n일 년에 태양 주변 한 바퀴를 회전한다. ",
        "화성은 지구와 같이 계절 변화가 있고 물이 존재한 흔적이 있다.\n 인류의 새로운 터전으로 많은 연구와 관심을 받고 있는 행성이다.",
        "목성은 태양계에서 가장 큰 행성으로, 태양에서 5번째로 가까운\n거리에 있다. 목성형(또는 가스형) 행성을 대표한다. ",
        "토성은 목성 다음으로 커다란 행성이다.\n밀도가 아주 낮고 고리는 돌과 얼음으로 이루어져 있다.",
        "천왕성은 목성과 토성처럼 고리를 가지고 있지만 어둡게 보인다.\n자전축이 90도 기울어져 있어 태양을 바라보며 누워서 자전한다.",
        "태양으로부터 멀리 떨어져 있어 꽁꽁 얼어붙은 상태이다.\n 중간에 커다란 폭풍이 눈에 띄며 최근에야 탐사가 이루어졌다."
    };

    public quizSet[] quizList = new quizSet[5];

    public string[] quizQ = new string[5] {
        "스스로 빛을 내는 별은?",
        "붉은 색을 띄고 지구의 절반 크기를 가진 행성은?",
        "태양계에서 가장 커다란 행성은?",
        "뚜렷한 고리를 가지고 많은 위성을 가지는 행성은?",
        "태양계의 가장 바깥쪽 행성으로 푸른 빛을 띄는 행성은?"
    };

    public string[] answ = new string[5] {
        "Sun",
        "Mars",
        "Jupiter",
        "Saturn",
        "Neptune"
    };

    public string[] plnt1 = new string[5] {
        "태양",
        "태양",
        "목성",
        "목성",
        "지구"
    };
    public string[] plnt2 = new string[5] {
        "천왕성",
        "지구",
        "수성",
        "토성",
        "해왕성"
    };
    public string[] plnt3 = new string[5] {
        "화성",
        "화성",
        "금성",
        "금성",
        "천왕성"
    };

    public void quizeSetting()
    {
        for (int i = 0; i < 5; i++)
        {
            quizList[i].quiz_problem = quizQ[i];
            quizList[i].answ = answ[i];
            quizList[i].plnt1 = plnt1[i];
            quizList[i].plnt2 = plnt2[i];
            quizList[i].plnt3 = plnt3[i];
        }
    }

    public bool[] check = new bool[9] { false, false, false, false, false, false, false, false, false };

    public string[] planetNameEng = new string[9] {
        "Sun",
        "Mercury",
        "Venus",
        "Earth",
        "Mars",
        "Jupiter",
        "Saturn",
        "Uranus",
        "Neptune"
    };
    public string[] planetNameKor = new string[9] {
        "태양",
        "수성",
        "금성",
        "지구",
        "화성",
        "목성",
        "토성",
        "천왕성",
        "해왕성"
    };

    public AudioClip Start_voice;
    public AudioClip GoodJob_voice;
    public AudioClip Quiz_Correct;
    public AudioClip Quiz_Wrong;
    public AudioClip Quiz_Induce_floor;
    public AudioClip[] Expain_voice;
    public AudioClip[] Quiz_voice;
    public AudioClip[] Show_voice;
    public AudioClip[] Wrong_voice;

    public AudioClip WrongSE;
    public AudioClip CorrectSE;
    public AudioClip ClickSE;
    public AudioClip BGM;

    public string LanChange(string txt, string originalLan)
    {
        int index;
        if (originalLan == "ENG")
        {
            index = strTointPlanet(txt, originalLan);
            return intTostrPlanet(index, "KOR");
        }
        else if (originalLan == "KOR")
        {
            index = strTointPlanet(txt, originalLan);
            return intTostrPlanet(index, "ENG");
        }
        Debug.LogError("No search Data");
        return "Error!";
    }
    public string intTostrPlanet(int val, string lan)
    {
        if (lan == "ENG")
        {
            return planetNameEng[val];
        }
        else if (lan == "KOR")
        {
            return planetNameKor[val];
        }
        Debug.LogError("No search Data");
        return "Error!";
    }

    public int strTointPlanet(string val, string lan)
    {
        if (lan == "ENG")
        {
            for (int i = 0; i < check.Length; i++)
            {
                if (planetNameEng[i] == val)
                {
                    return i;
                }
            }
        }
        else if (lan == "KOR")
        {
            for (int i = 0; i < check.Length; i++)
            {
                if (planetNameKor[i] == val)
                {
                    return i;
                }
            }
        }
        Debug.LogError("No search Data");
        return -1;
    }
}
