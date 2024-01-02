using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    public float spawnRate = 1;
    public int floorScale = 1;
    public GameObject myPrefab;
    public float timeElapsed = 0;



    private void Start()
    {
        
        for(int i=0;i <100; i++)
        {
            Spawn();
        }

    }

    private void FixedUpdate()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= spawnRate)
        {
            timeElapsed = timeElapsed % spawnRate;
            Spawn();
        }

    }

    void Spawn()
    {
        int x = Random.Range(-100, 101) * floorScale;
        int z = Random.Range(-100, 101) * floorScale;
        Instantiate(myPrefab, new Vector3((float)x, 0.75f, (float)z), Quaternion.identity);
    }














}
