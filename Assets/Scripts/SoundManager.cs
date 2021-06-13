using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource SE;
    public AudioSource BGM;
    public AudioSource Voice;

    public DataController DB;

    public void PlaySE(string AC)
    {
        switch (AC)
        {
            case "WrongSE":
                SE.PlayOneShot(DB.WrongSE);
                break;
            case "CorrectSE":
                SE.PlayOneShot(DB.CorrectSE);
                break;
            case "ClickSE":
                SE.PlayOneShot(DB.ClickSE);
                break;
        }
    }

    public void Quiz_Voice(int quiz)
    {
        switch (quiz)
        {
            case 0:
                Voice.clip = DB.Quiz_voice[0];
                break;
            case 1:
                Voice.clip = DB.Quiz_voice[1];
                break;
            case 2:
                Voice.clip = DB.Quiz_voice[2];
                break;
            case 3:
                Voice.clip = DB.Quiz_voice[3];
                break;
            case 4:
                Voice.clip = DB.Quiz_voice[4];
                break;
        }
        Voice.Play();
    }

    public void Voice_Explain(string planet)
    {//정확한 행성을 비추면 나오는 목소리
        Voice.Stop();
        switch (planet)
        {
            case "Sun":
                Voice.clip = DB.Expain_voice[0];
                break;
            case "Mercury":
                Voice.clip = DB.Expain_voice[1];
                break;
            case "Venus":
                Voice.clip = DB.Expain_voice[2];
                break;
            case "Earth":
                Voice.clip = DB.Expain_voice[3];
                break;
            case "Mars":
                Voice.clip = DB.Expain_voice[4];
                break;
            case "Jupiter":
                Voice.clip = DB.Expain_voice[5];
                break;
            case "Saturn":
                Voice.clip = DB.Expain_voice[6];
                break;
            case "Uranus":
                Voice.clip = DB.Expain_voice[7];
                break;
            case "Neptune":
                Voice.clip = DB.Expain_voice[8];
                break;
            default:
                Debug.Log("Error Ocurr");
                break;
        }
        Voice.Play();
    }

    public void Voice_ShowRequest(string planet)
    {//정확한 행성을 비추면 나오는 목소리
        Voice.Stop();
        switch (planet)
        {
            case "Sun":
                Voice.clip = DB.Show_voice[0];
                break;
            case "Mercury":
                Voice.clip = DB.Show_voice[1];
                break;
            case "Venus":
                Voice.clip = DB.Show_voice[2];
                break;
            case "Earth":
                Voice.clip = DB.Show_voice[3];
                break;
            case "Mars":
                Voice.clip = DB.Show_voice[4];
                break;
            case "Jupiter":
                Voice.clip = DB.Show_voice[5];
                break;
            case "Saturn":
                Voice.clip = DB.Show_voice[6];
                break;
            case "Uranus":
                Voice.clip = DB.Show_voice[7];
                break;
            case "Neptune":
                Voice.clip = DB.Show_voice[8];
                break;
            default:
                Debug.Log("Error Ocurr");
                break;
        }
        Voice.Play();
    }

    public void Voice_Wrong(string planet)
    {//정확한 행성을 비추면 나오는 목소리
        Voice.Stop();
        switch (planet)
        {
            case "Sun":
                Voice.clip = DB.Wrong_voice[0];
                break;
            case "Mercury":
                Voice.clip = DB.Wrong_voice[1];
                break;
            case "Venus":
                Voice.clip = DB.Wrong_voice[2];
                break;
            case "Earth":
                Voice.clip = DB.Wrong_voice[3];
                break;
            case "Mars":
                Voice.clip = DB.Wrong_voice[4];
                break;
            case "Jupiter":
                Voice.clip = DB.Wrong_voice[5];
                break;
            case "Saturn":
                Voice.clip = DB.Wrong_voice[6];
                break;
            case "Uranus":
                Voice.clip = DB.Wrong_voice[7];
                break;
            case "Neptune":
                Voice.clip = DB.Wrong_voice[8];
                break;
            default:
                Debug.Log("Error Ocurr");
                break;
        }
        Voice.Play();
    }

    public void Voice_Another(string Request)
    {//정확한 행성을 비추면 나오는 목소리
        Voice.Stop();
        switch (Request)
        {
            case "Start_voice":
                Voice.clip = DB.Start_voice;
                break;
            case "Good_Job":
                Voice.clip = DB.GoodJob_voice;
                break;
            case "Quiz_Correct":
                Voice.clip = DB.Quiz_Correct;
                break;
            case "Quiz_Wrong":
                Voice.clip = DB.Quiz_Wrong;
                break;
            case "Quiz_Induce_floor":
                Voice.clip = DB.Quiz_Induce_floor;
                break;
            default:
                Debug.Log("Error Ocurr");
                break;
        }
        Voice.Play();
    }

    public void BGM_Player()
    {//정확한 행성을 비추면 나오는 목소리
        BGM.Stop();
        BGM.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
