using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public GameController GC;
    public ARTrackedImg ART;
    public DataController DB;
    public Transform canvas;
    public UiManage UIM;
    public int idxNum;
    private Renderer PlanetR;
    private MeshRenderer PlanetM;
    public GameObject Planet;
    private string index;
    private Vector3 can_pos;
    private Transform PlanetT;

    private float rotVal = 0;
    // Update is called once per frame
    void Start()
    {
        index = DB.intTostrPlanet(idxNum, "ENG");
        PlanetR = Planet.GetComponent<Renderer>();
        PlanetM = Planet.GetComponent<MeshRenderer>();
        PlanetT = Planet.GetComponent<Transform>();
    }
    void Update()
    {
        if (index == ART.nowShow)
        {
            gameObject.SetActive(true);
            if (gameObject.name != "Saturn")
            {
                canvas.position = gameObject.transform.position;
                UIM.Log.text = "!!! X: " + canvas.position.x + " Y: " + canvas.position.y + " Z: " + canvas.position.z;
            }
            else
            {
                canvas.position = PlanetT.transform.position;
                UIM.Log.text = " X: " + canvas.position.x + " Y: " + canvas.position.y + " Z: " + canvas.position.z;
            }
        }


        if (GC.allFeature)
        {
            // rotVal += (Time.deltaTime * GC.turnSpeed);
            // PlanetR.material.mainTextureOffset = new Vector2(rotVal, 0);
            // PlanetM.material.mainTextureOffset = new Vector2(rotVal, 0);
            // PlanetM.material.SetTextureOffset(gameObject.name, new Vector2(rotVal, 0));
            ;
        }
    }
}
