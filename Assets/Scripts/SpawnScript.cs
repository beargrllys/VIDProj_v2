using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public Transform[] spwanPoint;
    public GameObject[] balloons;
    void Start()
    {
        StartCoroutine(StartSpawning());
    }
    IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(4.0f);

        for (int i = 0; i < 3; i++)
        {
            Instantiate(balloons[i], spwanPoint[i].position, Quaternion.identity);

        }

        StartCoroutine(StartSpawning());
    }
}
