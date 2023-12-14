using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class RosslerAtractorParticleController : MonoBehaviour
{
    float a, b, c;
    float h, lifeTime;
    bool infoFixed;

    float lifeTimeCounter;
    float hCounter;

    private void Update()
    {
        if(infoFixed)
        {
            lifeTimeCounter += Time.deltaTime;
            if(lifeTimeCounter > lifeTime)
            {
                Destroy(gameObject);
            }
            else
            {
                hCounter += Time.deltaTime;
                if(hCounter >= h)
                {
                    hCounter -= h;
                    transform.position = FunctionManager.RungeKutta4thOrder(Derivative, transform.position, h, lifeTime-hCounter); // -hCounter for resuduals
                }
            }
        }
    }

    public void SetInformation(float a, float b, float c, float h, float lifeTime)
    {
        if(!infoFixed)
        {
            infoFixed = true;
            this.a = a;
            this.b = b;
            this.c = c;
            this.h = h;
            this.lifeTime = lifeTime;
            
            hCounter = 0;
            lifeTimeCounter = 0;
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
