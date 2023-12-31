using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LineManager : MonoBehaviour
{
    [Header("Rossler Atractor")]
    [SerializeField] float a = 0.2f;
    [SerializeField] float b = 0.2f;
    [SerializeField] float c = 5.7f;
    [SerializeField] float h = 0.01f;
    [SerializeField] Vector3 initialPosition;

    [Header("Particles")]
    [SerializeField] UnityEngine.LineRenderer line; 
    [SerializeField] int nParticles = 5000;
    [SerializeField] int iterations;

    float hCounter;
    List<Vector3> positions;
    Vector3 position;

    public bool drawOnUpdate = true;

    private void Awake()
    {
        if(line == null) line = gameObject.AddComponent<UnityEngine.LineRenderer>();
    }

    private void Start()
    {
        iterations = 0;
        hCounter = 0;
        positions = new();
        positions.Add(initialPosition);
        for(int i=1; i<nParticles; i++)
        {
            positions.Add(FunctionManager.RungeKutta4thOrder(Derivative, positions[^1], h, h*i));
        }
        line.positionCount = positions.Count;
        line.SetPositions(positions.ToArray());
    }

    private void Update()
    {
        if(drawOnUpdate) line.SetPositions(positions.ToArray());
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
