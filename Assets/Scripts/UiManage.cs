using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManage : MonoBehaviour
{
    public DataController DB;
    public GameController GC;
    [Header("PlanetChoiceBnt")]
    public GameObject PlanetStartBtn;
    public Image PlanetIcon;
    public Text PlanetBntText;

    public GameObject PlanetWrongBtn;
    public Text WrongText;

    [Header("ARSign")]
    public GameObject ARInductSign;
    public Text ARInductTxt;

    [Header("Rocket UI")]
    public Image RocketLine;
    public Image Rocket;
    public RectTransform RocketPos;
    public RectTransform DestiPos;

    [Header("Upper Button")]
    public Button BackButton;
    public Button QuitButton;

    [Header("Explain UI")]
    public GameObject explainCav;
    public GameObject[] explainBar = new GameObject[4];
    public Text[] explainTxt = new Text[4];

    [Header("Complete UI")]
    public GameObject CompleteBtn;
    public Image CompleteIcon;
    public Text CompleteBntText;

    [Header("Result UI")]
    public GameObject resultPanel;
    public GameObject NextBtn;
    public GameObject BeforeBtn;
    public GameObject TouchIcon;
    public Image PlaIcon;
    public Text PlsnetKOR;
    public Text PlanetENG;
    public Text F1;
    public Text F2;
    public Text F3;
    public Text F4;
    public Text EX;
    [Header("Log UI")]
    public Text Log;
    public SoundManager SM;
    private float Xval;
    public bool voiceTrigger = false;

    void Start()
    {
        Xval = Mathf.Abs(RocketPos.anchoredPosition.x - DestiPos.anchoredPosition.x);
        SM.BGM_Player();
    }
    public void init_UI()
    {
        PlanetStartBtn.SetActive(false);
        PlanetWrongBtn.SetActive(false);
        CompleteBtn.SetActive(false);
        ARInductSign.SetActive(true);
        for (int i = 0; i < explainBar.Length; i++)
            explainBar[i].SetActive(false);
    }

    public void ChangeInduct(string planetName)
    {
        if (!voiceTrigger)
        {
            SM.Voice_ShowRequest(DB.LanChange(planetName, "KOR"));
            voiceTrigger = true;
        }
        ARInductTxt.text = planetName + "을(를) 비춰주세요";
    }

    public void PlanetChoice(string planet)
    {
        int index;
        //PlanetWrongBtn.SetActive(false);

        PlanetBntText.text = planet;
        index = DB.strTointPlanet(planet, "KOR");
        PlanetIcon.sprite = DB.planetIcon[index];
        PlanetStartBtn.SetActive(true);
    }

    public void WrongPPanel(string wrong)
    {
        PlanetStartBtn.SetActive(false);
        WrongText.text = wrong + "입니다. 다시 시도하세요";
        PlanetWrongBtn.SetActive(true);
        StartCoroutine(WaitAndActiveF(3.0f, PlanetWrongBtn));
    }

    public void TouchQuitBtn()
    {
        SM.PlaySE("ClickSE");
#if UNITY_EDITOR//유니티 에디터에서 실행시 읽히는 코드

        UnityEditor.EditorApplication.isPlaying = false;

#else//이외 다른 플랫폼에서 실행할때 읽히는 코드

        Application.Quit();
        
#endif //플랫폼 별 사이한 코드 분기문 종료
    }

    public void TouchBackBtn()
    {
        SM.PlaySE("ClickSE");
        GC.planetBack();
    }

    public void ExplainDisplay(int displayNum)
    {
        if (displayNum == 0)
        {
            explainBar[displayNum].SetActive(true);
            explainTxt[displayNum].text = DB.feature1[GC.adventureNum];
        }
        else if (displayNum == 1)
        {
            explainBar[displayNum].SetActive(true);
            explainTxt[displayNum].text = DB.feature2[GC.adventureNum];
        }
        else if (displayNum == 2)
        {
            explainBar[displayNum].SetActive(true);
            explainTxt[displayNum].text = DB.feature3[GC.adventureNum];
        }
        else if (displayNum == 3)
        {
            explainBar[displayNum].SetActive(true);
            explainTxt[displayNum].text = DB.feature4[GC.adventureNum];
        }
    }

    public void ComBtnDisplay(string planet)
    {
        int index;
        CompleteBntText.text = planet + ", 정복했어요!";
        index = DB.strTointPlanet(planet, "KOR");
        CompleteIcon.sprite = DB.planetIcon[index];
        CompleteBtn.SetActive(true);
        SM.PlaySE("CorrectSE");
    }

    public void RocketUp(int adNum)
    {
        RocketPos.anchoredPosition = new Vector2(RocketPos.anchoredPosition.x + (Xval / 9), RocketPos.anchoredPosition.y);
        Log.text = "Xpos " + RocketPos.anchoredPosition.x + " ; " + Xval;
        Debug.Log("Xpos " + RocketPos.anchoredPosition.x + " ; " + Xval);
    }

    public void Result(int index)
    {
        PlaIcon.sprite = DB.planetIcon[index];
        PlaIcon.SetNativeSize();
        PlsnetKOR.text = DB.intTostrPlanet(index, "KOR");
        PlanetENG.text = DB.intTostrPlanet(index, "ENG");
        F1.text = DB.feature1[index];
        F2.text = DB.feature2[index];
        F3.text = DB.feature3[index];
        F4.text = DB.feature4[index];
        EX.text = DB.Explain[index];
    }

    public void goLobby()
    {
        SceneManager.LoadScene("Quiz");
    }

    IEnumerator WaitAndActiveF(float waitTime, GameObject targetObj)
    {
        yield return new WaitForSeconds(waitTime);
        targetObj.SetActive(false);
    }



}
