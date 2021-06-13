using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManage : MonoBehaviour
{
    public GameController GC;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && GC.planetFlag)
        {
            Vector2 Pos = Input.GetTouch(0).position;
            Vector3 theTouch = new Vector3(Pos.x, Pos.y, 0.0f);

            Ray ray = Camera.main.ScreenPointToRay(theTouch);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    if (hit.collider.CompareTag("Planet"))
                    {
                        GC.TucPlanet();
                    }
                }
            }
        }
    }
}
