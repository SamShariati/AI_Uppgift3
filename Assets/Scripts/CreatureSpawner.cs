using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{

    public GameObject agentPrefab;
    private GameObject[] agentList;
    public int floorScale = 1;


    private void FixedUpdate()
    {
        agentList = GameObject.FindGameObjectsWithTag("Agent");

        //finns det inga i scenen spawna en med random pos:

        if(agentList.Length < 1)
        {
            SpawnCreature();
        }
    }


    void SpawnCreature()
    {
        int x = Random.Range(-100, 101) * floorScale;
        int z = Random.Range(-100, 101) * floorScale;
        Instantiate(agentPrefab, new Vector3((float)x, 0.75f, (float)z), Quaternion.identity);
    }
}
