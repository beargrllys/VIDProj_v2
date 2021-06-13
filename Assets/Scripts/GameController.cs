using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public ARTrackedImg ART;
    public DataController DB;
    public UiManage UIM;

    public int adventureNum = 0;
    public bool choiceFlag = false;
    public bool planetFlag = false;

    public bool allFeature = false;

    public float turnSpeed = 0.0005f;
    public SoundManager SM;

    private string NowPlanet;

    private int explainNum = 0;
    private int resultIndex = 0;

    public void choiceDone()
    {
        NowPlanet = ART.nowShow;
        int index = DB.strTointPlanet(NowPlanet, "ENG");
        if (adventureNum == index)
        {//정답일 경우 
            UIM.PlanetChoice(DB.intTostrPlanet(index, "KOR"));
            UIM.ARInductSign.SetActive(false);
            SM.Voice_Explain(NowPlanet);
        }
        else//오답일 경우
        {
            UIM.WrongPPanel(DB.intTostrPlanet(index, "KOR"));
            choiceFlag = false;
            ART.nowShow = "none";
            ART.inductTime = 0.0f;
            SM.Voice_Wrong(NowPlanet);
            SM.PlaySE("WrongSE");
            Handheld.Vibrate();
        }
    }

    public void TucPlanStrt()
    {
        SM.PlaySE("ClickSE");
        planetFlag = true;
        ART.TargetLock = true;
        UIM.PlanetStartBtn.SetActive(false);
        UIM.TouchIcon.SetActive(true);
    }

    public void TucPlanet()
    {
        int index = DB.strTointPlanet(NowPlanet, "ENG");
        SM.PlaySE("ClickSE");
        if (explainNum < 4)
        {
            UIM.ExplainDisplay(explainNum);
        }
        else
        {
            UIM.ExplainDisplay(explainNum);
            allFeature = true;
            UIM.TouchIcon.SetActive(false);
            UIM.ComBtnDisplay(DB.intTostrPlanet(index, "KOR"));
            planetFlag = false;
        }
        explainNum = explainNum + 1;
    }

    public void TucComp()
    {
        adventureNum++;
        SM.PlaySE("ClickSE");
        if (adventureNum < 9)
        {
            UIM.voiceTrigger = false;
            UIM.RocketUp(adventureNum);
        }
        else
        {
            SM.Voice_Another("Good_Job");
            UIM.RocketUp(adventureNum);
            UIM.resultPanel.SetActive(true);
        }
        planetBack();
    }

    public void TucResNext()
    {
        SM.PlaySE("ClickSE");
        if ((resultIndex + 1) <= 8)
        {
            resultIndex++;
            UIM.Result(resultIndex);
        }
        if (resultIndex == 8)
        {
            UIM.NextBtn.SetActive(false);
            UIM.BeforeBtn.SetActive(true);
        }
        else
        {
            UIM.NextBtn.SetActive(true);
            UIM.BeforeBtn.SetActive(true);
        }
    }

    public void TucResBefore()
    {
        SM.PlaySE("ClickSE");
        if ((resultIndex - 1) >= 0)
        {
            resultIndex--;
            UIM.Result(resultIndex);
        }
        if (resultIndex == 0)
        {
            UIM.BeforeBtn.SetActive(false);
            UIM.NextBtn.SetActive(true);
        }
        else
        {
            UIM.BeforeBtn.SetActive(true);
            UIM.NextBtn.SetActive(true);
        }
    }

    public void planetBack()
    {
        SM.PlaySE("ClickSE");
        choiceFlag = false;
        planetFlag = false;
        explainNum = 0;
        ART.init_inductTime();
        UIM.init_UI();
        allFeature = false;
    }
}
