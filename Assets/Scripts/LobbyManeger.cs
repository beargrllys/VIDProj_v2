using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManeger : MonoBehaviour
{
    public GameObject bgPic;
    public GameObject lobbywall;
    public SoundManager SM;
    void Start()
    {
        StartCoroutine("WaitAndActiveF");

    }

    public void tucStartBtn()
    {
        SM.PlaySE("ClickSE");
        SceneManager.LoadScene("MainScene");
    }

    IEnumerator WaitAndActiveF()
    {
        yield return new WaitForSeconds(3.0f);
        bgPic.SetActive(false);
        SM.Voice_Another("Start_voice");
        SM.BGM_Player();
    }
}
