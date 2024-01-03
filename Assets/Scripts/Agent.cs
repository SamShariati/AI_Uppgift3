using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    NN nn;
    public float viewDistance = 20;
    bool mutateMutations = false;
    float mutationAmount;
    float mutationChance;


    private void Awake()
    {
        nn = GetComponent<NN>();
    }


    public float[] CreateRaycasts(int numRaycasts, float angleBetweenRaycasts)
    {
        float[] distances = new float[numRaycasts];

        RaycastHit hit;
        for (int i=0; i<numRaycasts; i++)
        {
            float angle = ((2 * i + 1 - numRaycasts) * angleBetweenRaycasts / 2);

            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
            Vector3 rayDirection = rotation * transform.forward * -1;

            Vector3 rayStart = transform.position + Vector3.up * 0.1f;

            if(Physics.Raycast(rayStart, rayDirection, out hit, viewDistance))
            {
                Debug.DrawRay(rayStart, rayDirection * hit.distance, Color.red);
                if (hit.transform.gameObject.tag == "Food")
                {
                    distances[i] = hit.distance;
                }

                else
                {
                    distances[i] = viewDistance;
                }
            }
            else
            {
                Debug.DrawRay(rayStart, rayDirection * viewDistance, Color.red);
                distances[i] = 1;
            }
        }
        return (distances);
    }


    private void MutateCreature()
    {
        if (mutateMutations)
        {
            mutationAmount += Random.Range(-1.0f, 1.0f) / 100;
            mutationChance += Random.Range(-1.0f, 1.0f) / 100;
        }

        mutationAmount = Mathf.Max(mutationAmount, 0);
        mutationChance = Mathf.Max(mutationChance, 0);

        nn.MutateNetwork(mutationAmount, mutationChance);
    }
}
