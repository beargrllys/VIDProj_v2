
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ARTrackedImg : MonoBehaviour
{
    public float _timer;
    public ARTrackedImageManager trackedImageManager;
    public List<GameObject> _objectList = new List<GameObject>();

    [Header("UI Reference Func")]
    public UiManage UIM;
    public DataController DB;
    public int detect_flag;
    public int ObjectSetting = 0;


    private Dictionary<string, GameObject> _prefabDic = new Dictionary<string, GameObject>();
    private ARTrackedImage _trackedImg;
    //public bool[] showObj = new bool[9];
    public List<ARTrackedImage> showObj = new List<ARTrackedImage>();
    public string nowShow = "none";
    public string beforeIdx;
    public bool isShowing = false;
    public float inductTime = 0.0f;
    private Transform save_Obj;
    public Text SensingTxt;
    public GameController GC;

    public bool TargetLock;

    void Awake()
    {
        foreach (GameObject obj in _objectList)
        {
            string tName = obj.name;
            _prefabDic.Add(tName, obj);
        }
    }

    void Update()
    {
        UIM.ChangeInduct(DB.intTostrPlanet(GC.adventureNum, "KOR"));
        Debug.Log(DB.intTostrPlanet(GC.adventureNum, "KOR"));
        isShowing = false;
        nowShow = "none";
        foreach (ARTrackedImage GO in showObj)
        {
            if (GO.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited)
            {
                ;
            }
            else if (GO.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                nowShow = GO.referenceImage.name;
                isShowing = true;
            }
        }
        if (isShowing == true)
        {
            if (_prefabDic[nowShow].activeSelf == false)
            {
                if (TargetLock)
                {
                    if (_objectList[GC.adventureNum].name == nowShow)
                    {
                        _prefabDic[nowShow].SetActive(true);
                        UIM.explainCav.SetActive(true);
                    }
                    else
                    {
                        _prefabDic[nowShow].SetActive(false);
                        UIM.explainCav.SetActive(false);
                    }
                }
                else
                {
                    _prefabDic[nowShow].SetActive(true);
                    UIM.explainCav.SetActive(true);
                }
            }
            for (int i = 0; i < _objectList.Count; i++)
            {
                if (_prefabDic[_objectList[i].name] != _prefabDic[nowShow])
                {
                    _prefabDic[_objectList[i].name].SetActive(false);
                }
            }

        }
        else
        {
            for (int i = 0; i < _objectList.Count; i++)
            {
                _prefabDic[_objectList[i].name].SetActive(false);
                UIM.explainCav.SetActive(false);
            }
        }
        if (beforeIdx == nowShow && nowShow != "none")
        {
            if (!GC.choiceFlag)
            {
                inductTime += Time.deltaTime;
                if (inductTime > 3.0f)
                {
                    GC.choiceFlag = true;
                    GC.choiceDone();
                }
            }
        }
        else
        {
            inductTime = 0;
            if (!GC.choiceFlag)
            {
                UIM.PlanetStartBtn.SetActive(false);
                //UIM.PlanetWrongBtn.SetActive(false);
            }
        }
        beforeIdx = nowShow;
    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }
    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {

        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            showObj.Add(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }
    }
    private void AddImage(ARTrackedImage trackedImage)
    {
        if (detect_flag == 1)
        {//이미 한개의 오브젝트가 detection 중 이라면
            string name = trackedImage.referenceImage.name;
            GameObject tObj = _prefabDic[name];
            tObj.SetActive(false);
        }
        if (detect_flag == 0)
        {//detection이 아닌 상황에서 새 오브젝트가 잡히면
            _trackedImg = trackedImage;
            string name = trackedImage.referenceImage.name;
            GameObject tObj = _prefabDic[name];
            detect_flag = 0;
            tObj.SetActive(false);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        GameObject tObj = _prefabDic[name];
        tObj.transform.position = trackedImage.transform.position;
        tObj.transform.rotation = trackedImage.transform.rotation;
    }

    public void init_inductTime()
    {
        inductTime = 0.0f;
        nowShow = "none";
        TargetLock = false;
    }
}



