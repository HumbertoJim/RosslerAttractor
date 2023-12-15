using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RManager : MonoBehaviour
{
    [Header("Rossler Atractor")]
    [SerializeField] float a = 0.2f;
    [SerializeField] float b = 0.2f;
    [SerializeField] float c = 5.7f;
    [SerializeField] float h = 0.01f;
    [SerializeField] Vector3 initialPosition;

    [Header("Particles")]
    [SerializeField] GameObject particlePrefab;
    [SerializeField] int nParticles = 5000;

    List<RParticle> particles;
    GameObject particle;
    float hCounter;
    [SerializeField] int iterations;

    void Start()
    {
        iterations = 0;
        hCounter = 0;
        particles = new();
        particle = Instantiate(particlePrefab, transform);
        particle.transform.position = initialPosition;
        particles.Add(particle.GetComponent<RParticle>());
    }

    private void Update()
    {
        if(particles.Count < nParticles)
        {
            iterations++;
            particle = Instantiate(particlePrefab, transform);
            particle.transform.position = FunctionManager.RungeKutta4thOrder(Derivative, particles[^1].transform.position, h, iterations*h);
            particles.Add(particle.GetComponent<RParticle>());
        }
        else
        {
            hCounter += Time.deltaTime;
            if (hCounter > h)
            {
                iterations++;
                for (int i = 0; i < particles.Count - 1; i++)
                {
                    particles[i].transform.position = particles[i + 1].transform.position;
                }
                particles[^1].transform.position = FunctionManager.RungeKutta4thOrder(Derivative, particles[^1].transform.position, h, iterations * h);
            }
        }
    }


    Vector3 Derivative(float time, Vector3 position)
    {
        float x = position.x;
        float z = position.y; // change axis y by z
        float y = position.z;
        float dx_dt = -y - z;
        float dy_dt = x + a * y;
        float dz_dt = b + z * (x - c);
        return new Vector3(dx_dt, dz_dt, dy_dt); // change axis y by z
    }
}
