using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{

    public GameObject agentPrefab;
    float elapsedTime;
    public float energy = 10;
    public float reproductionThreshhold = 20;
    float currentEnergy = 10;
    float reproductionEnergy=0;
    public int numberOfChildren = 2;


    public void ManageEnergy()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= 1f)
        {
            elapsedTime = elapsedTime % 1f;

            energy -= 1f;
        }

        //ifall agenten får slut på energy så dör den.

        float agentY = this.transform.position.y;
        if(energy <= 0 || agentY < -10)
        {
            this.transform.Rotate(0, 0, 180);
            Destroy(this.gameObject, 3);
            GetComponent<Movement>().enabled = false;
        }


        //ifall agenten har tillräckligt med energy så förökar den sig.

        if(energy >= reproductionThreshhold)
        {
            reproductionEnergy = 0;
            Reproduce();
        }
    }

    public void Reproduce()
    {
        for(int i=0; i < numberOfChildren; i++)
        {
            GameObject child = Instantiate(agentPrefab, new Vector3((float)this.transform.position.x + Random.Range(-10, 11), 3f, (float)this.transform.position.z + Random.Range(-10, 11)), Quaternion.identity);

            child.GetComponent<NN>().layers = GetComponent<NN>().CopyLayers();
        }
        reproductionEnergy = 0;
    }
}
