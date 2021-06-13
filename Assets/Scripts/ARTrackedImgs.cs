
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ARTrackedImgs : MonoBehaviour
{
    public float _timer;
    public ARTrackedImageManager trackedImageManager;
    public List<GameObject> _objectList = new List<GameObject>();

    [Header("UI Reference Func")]
    public UiManage UIM;
    public DataController DB;



    private Dictionary<string, GameObject> _prefabDic = new Dictionary<string, GameObject>();
    private List<ARTrackedImage> _trackedImg = new List<ARTrackedImage>();
    public string showIndex = "none";
    public string beforeIdx;
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
            Debug.Log(tName);
        }
    }

    void Update()
    {
        UIM.ChangeInduct(DB.intTostrPlanet(GC.adventureNum, "KOR"));
        if (_trackedImg.Count > 0)
        {
            List<ARTrackedImage> tNumList = new List<ARTrackedImage>();
            for (var i = 0; i < _trackedImg.Count; i++)
            {
                GameObject value = _prefabDic[_trackedImg[i].referenceImage.name];
                if (_trackedImg[i].trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Limited && !TargetLock)
                {
                    if (showIndex != _trackedImg[i].referenceImage.name)
                    {
                        showIndex = "none";
                        value.SetActive(false);
                    }
                }
                else if (_trackedImg[i].trackingState == UnityEngine.XR.ARSubsystems.TrackingState.None && !TargetLock)
                {
                    if (showIndex != _trackedImg[i].referenceImage.name)
                    {
                        value.SetActive(false);
                    }
                    if (showIndex == _trackedImg[i].referenceImage.name)
                    {
                        showIndex = "none";
                    }
                }
                else if (_trackedImg[i].trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking && !TargetLock)
                {
                    if (showIndex == "none")
                    {
                        showIndex = _trackedImg[i].referenceImage.name;
                        value.SetActive(true);
                    }
                    else
                    {
                        if (showIndex != _trackedImg[i].referenceImage.name)
                        {
                            value.SetActive(false);
                        }
                    }
                }
            }

            if (tNumList.Count > 0 && !TargetLock)
            {
                for (var i = 0; i < tNumList.Count; i++)
                {
                    int num = _trackedImg.IndexOf(tNumList[i]);
                    _trackedImg.Remove(_trackedImg[num]);
                }
            }
        }
        if (beforeIdx == showIndex && showIndex != "none")
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
        beforeIdx = showIndex;
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
            if (!_trackedImg.Contains(trackedImage))
            {
                _trackedImg.Add(trackedImage);
            }
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            if (!_trackedImg.Contains(trackedImage))
            {
                _trackedImg.Add(trackedImage);
            }
            else
            {
                int num = _trackedImg.IndexOf(trackedImage);
            }
            UpdateImage(trackedImage);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        GameObject tObj = _prefabDic[name];
        tObj.transform.position = trackedImage.transform.position;
        tObj.transform.rotation = trackedImage.transform.rotation;
        tObj.SetActive(true);
    }

    public void init_inductTime()
    {
        inductTime = 0.0f;
        showIndex = "none";
        beforeIdx = "";
        TargetLock = false;
    }
}



