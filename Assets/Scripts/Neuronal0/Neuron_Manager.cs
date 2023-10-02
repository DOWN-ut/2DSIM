using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron_Manager : MonoBehaviour
{
    [Header("Neurons Variables")]
    public float sens = 1;
    public float transfertSpeed = 1;
    public float growingSpeed = 1;
    public float growingFactor = 1;

    public float divisionMaxChargeMult = 1.25f;

    public Camera cam;

    public float zoomSensibility = 0.05f;

    void Update()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        if (zoom > 0 || zoom < 0)
        {
            cam.orthographicSize -= Mathf.Sign(zoom) * zoomSensibility;
        }
    }
}
