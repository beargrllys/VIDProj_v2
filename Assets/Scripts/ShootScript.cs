using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class ShootScript : MonoBehaviour
{
    public GameObject gameObjectToInstatiate;
    public GameObject arCamera;
    public ARSessionOrigin ARSO;
    public MeshRenderer MeshRend;
    public Material MeshFade;
    public QuizManager QM;
    public DataController DB;
    public GameObject spawnedObject;
    public GameObject[] spawnedPlanet;
    public GameObject[] showingPlanet;
    public GameObject DoneButton;
    private ARRaycastManager _arRaycastManger;
    private Vector2 touchPosition;
    public int probIdx;
    private string probAnw;
    public Transform[] spwanPoint;

    public Image fadeImg;
    public SoundManager SM;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        _arRaycastManger = ARSO.GetComponent<ARRaycastManager>();
        QM.QuizeText.text = "위치를 잡아주세요.";
        DB.quizeSetting();
        MeshRend = ARSO.GetComponent<ARPlaneManager>().planePrefab.GetComponent<MeshRenderer>();
        QM.CirclePos.localPosition = QM.posVec[0];
    }

    void Start()
    {
        Debug.Log("Check!!");
        SM.BGM_Player();
        SM.Voice_Another("Quiz_Induce_floor");
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                touchPosition = Input.GetTouch(0).position;
                if (!QM.posConfirm)
                {
                    init();
                    Shoot();
                }
                else
                {
                    Debug.Log("Touch");
                    Shoot();
                }
            }
        }
    }

    // bool TryGetTouchPosition(out Vector2 touchPosition){
    //     if(Input.touchCount > 0){
    //         touchPosition = Input.GetTouch(0).position;
    //         return true;
    //     }

    //     touchPosition = default;
    //     return false;
    // }

    public void Setting()
    {
        int idx1 = DB.strTointPlanet(DB.quizList[probIdx].plnt1, "KOR");
        int idx2 = DB.strTointPlanet(DB.quizList[probIdx].plnt2, "KOR");
        int idx3 = DB.strTointPlanet(DB.quizList[probIdx].plnt3, "KOR");
        QM.QuizeText.text = DB.quizList[probIdx].quiz_problem;
        QM.CirclePos.localPosition = QM.posVec[probIdx];
        probAnw = DB.quizList[probIdx].answ;
        showingPlanet[0] = Instantiate(spawnedPlanet[idx1], spwanPoint[0].position, spwanPoint[0].rotation);
        showingPlanet[1] = Instantiate(spawnedPlanet[idx2], spwanPoint[1].position, spwanPoint[1].rotation);
        showingPlanet[2] = Instantiate(spawnedPlanet[idx3], spwanPoint[2].position, spwanPoint[2].rotation);
        SM.Quiz_Voice(probIdx);
    }

    public void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            if (hit.transform.gameObject.tag == "Planet")
            {
                if (hit.transform.gameObject.name == probAnw + "(Clone)")
                {
                    Destroy(showingPlanet[0]);
                    showingPlanet[0] = null;
                    Destroy(showingPlanet[1]);
                    showingPlanet[1] = null;
                    Destroy(showingPlanet[2]);
                    showingPlanet[2] = null;
                    probIdx++;
                    SM.Voice_Another("Quiz_Correct");
                    SM.PlaySE("CorrectSE");
                    StartCoroutine("Correct");
                }
                else
                {
                    SM.Voice_Another("Quiz_Wrong");
                    SM.PlaySE("WrongSE");
                    StartCoroutine("Wrong");
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }

    public void init()
    {

        if (_arRaycastManger.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        //if (true)
        {
            var hitPose = hits[0].pose;

            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(gameObjectToInstatiate, hitPose.position, hitPose.rotation);
                //spawnedObject = Instantiate(gameObjectToInstatiate, new Vector3(0, 0, 0), Quaternion.identity);
                spwanPoint[2] = spawnedObject.transform.GetChild(2).transform;
                spwanPoint[1] = spawnedObject.transform.GetChild(1).transform;
                spwanPoint[0] = spawnedObject.transform.GetChild(0).transform;
                QM.posConfirm = true;
                MeshRend.material = MeshFade;
                QM.QuizeText.text = "fddfdfdfd";
                Setting();
            }
        }
    }

    public void TouchQuitBtn()
    {
#if UNITY_EDITOR//유니티 에디터에서 실행시 읽히는 코드

        UnityEditor.EditorApplication.isPlaying = false;

#else//이외 다른 플랫폼에서 실행할때 읽히는 코드

        Application.Quit();
        
#endif //플랫폼 별 사이한 코드 분기문 종료
    }

    public void doneButton()
    {
        StartCoroutine("Fadein");
    }
    IEnumerator Wrong()
    {
        QM.QuizeText.text = "다시 생각해보세요.";
        yield return new WaitForSeconds(2.0f);
        QM.QuizeText.text = DB.quizList[probIdx].quiz_problem;
    }

    IEnumerator Correct()
    {
        if (probIdx == 5)
        {
            QM.QuizeText.text = "모두 정복했어요!";
            yield return new WaitForSeconds(2.0f);
            DoneButton.SetActive(true);
        }
        else
        {
            QM.QuizeText.text = "정답입니다!";
            yield return new WaitForSeconds(2.0f);
            Setting();
        }
    }

    IEnumerator Fadein()
    {
        Color fadeColor = fadeImg.color;
        fadeImg.enabled = true;
        float start = 0f, end = 1f;
        float fadeTime = 1.0f;
        float delTime = 0f;

        fadeColor.a = Mathf.Lerp(start, end, delTime);

        while (fadeColor.a < 1.0f)
        {
            delTime += Time.deltaTime / fadeTime;
            Debug.Log(fadeColor.a);
            fadeColor.a = Mathf.Lerp(start, end, delTime);

            fadeImg.color = fadeColor;

            yield return new WaitForSeconds(0.001f);
        }
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Lobby");
    }
}
