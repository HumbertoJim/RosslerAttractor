using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionManager : MonoBehaviour
{
    public delegate Vector3 RungeKutta4thOrderEcuationForm(float time, Vector3 position);

    public GameObject particlePrefab;

    public int nParticles = 5;

    public float spawnTime = 2;

    public int particleCounter;
    float spawnTimeCounter;

    // Start is called before tspawnTimee first frame update
    // Check for this: https://docs.unity3d.com/2021.3/Documentation/Manual/class-LineRenderer.html

    void Start()
    {
        particleCounter = 0;
        spawnTimeCounter = 0;
    }

    private void Update()
    {
        if(particleCounter < nParticles)
        {
            spawnTimeCounter += Time.deltaTime;
            if(spawnTimeCounter > spawnTime)
            {
                spawnTimeCounter -= spawnTime;
                GameObject particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);
                particle.transform.SetParent(transform);
                particle.GetComponent<RosslerAtractorParticleController>().SetInformation(0.2f, 0.2f, 5.7f, 0.01f, 10000);
                particleCounter++;
            }
        }
    }

    public static Vector3 RungeKutta4thOrder(RungeKutta4thOrderEcuationForm ecuation, Vector3 state, float spawnTime, float time)
    {
        Vector3 k1 = spawnTime * ecuation(time, state);
        Vector3 k2 = spawnTime * ecuation(time + 0.5f * spawnTime, state + 0.5f * k1);
        Vector3 k3 = spawnTime * ecuation(time + 0.5f * spawnTime, state + 0.5f * k2);
        Vector3 k4 = spawnTime * ecuation(time + spawnTime, state + k3);
        return state + (k1 + 2f * k2 + 2f * k3 + k4) / 6f;
    }


}
