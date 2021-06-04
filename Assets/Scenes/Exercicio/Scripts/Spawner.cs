using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemiesToSpwan;

    private void Start()
    {
        InvokeRepeating("SpawnNow", 8, 5);
    }


    Vector3 getRandomPos()
    {
        float numX = transform.position.x + Random.Range(-4, 4);
        float numY = transform.position.y;
        float numZ = transform.position.z;

        Vector3 newPos = new Vector3(numX, numY, numZ);
        return newPos;

    }

    void SpawnNow()
    {
        GameObject spawnEm;
        spawnEm = Instantiate(enemiesToSpwan[0], getRandomPos(), Quaternion.identity) as GameObject;
        spawnEm.transform.parent = transform;
    }
}
