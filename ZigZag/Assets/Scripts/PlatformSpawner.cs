using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{

    public GameObject platform;
    public Transform lastPlatform;
    Vector3 lastPosition;
    Vector3 newPos;
    bool stop;

    void Start()
    {
        lastPosition = lastPlatform.position;
        StartCoroutine(SpawnPlatform());
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SpawnPlatform();
        }
    }

    IEnumerator SpawnPlatform()
    {
        while (!stop)
        {
            GeneratePosition();
            Instantiate(platform, newPos, Quaternion.identity);
            lastPosition = newPos;
            yield return new WaitForSeconds(0.1f);
        }
    }

    /*
    void SpawnPlatform()
    {
            GeneratePosition();
            Instantiate(platform, newPos, Quaternion.identity);
            lastPosition = newPos;
        
    } */

    void GeneratePosition()
    {
        newPos = lastPosition;

        int rand = Random.Range(0, 2);
        if (rand > 0)
        {
            newPos.x += 2f;
        }
        else
        {
            newPos.z += 2f;
        }
    }
}
