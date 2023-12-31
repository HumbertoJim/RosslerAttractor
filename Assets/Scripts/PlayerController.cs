using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform;
    [Header("Player")]
    [SerializeField] float speed = 5;
    [SerializeField] float rotationSpeed = 300;

    [Header("Turning Settings")]
    [SerializeField] float yLimRotation = 40;

    Vector3 vector;
    float x, y, z;
    float alpha, beta;

    void Update()
    {
        // movement
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        vector = transform.position;
        vector += speed * Time.deltaTime * x * cameraTransform.right;
        vector += speed * Time.deltaTime * y * cameraTransform.forward;
        transform.position = vector;

        // rotation
        cameraTransform.eulerAngles += rotationSpeed * Time.deltaTime * new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        if (cameraTransform.eulerAngles.x > 180 && cameraTransform.eulerAngles.x < 360 - yLimRotation)
        {
            cameraTransform.eulerAngles = new Vector3(360 - yLimRotation, cameraTransform.eulerAngles.y, 0);
        }
        else if (cameraTransform.eulerAngles.x < 180 && cameraTransform.eulerAngles.x > yLimRotation)
        {
            cameraTransform.eulerAngles = new Vector3(yLimRotation, cameraTransform.eulerAngles.y, 0);
        }
    }
}
