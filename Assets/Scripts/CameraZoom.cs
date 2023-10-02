using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float sensibility;

    void Update()
    {
        float f = Input.GetAxis("Mouse ScrollWheel");

        GetComponent<Camera>().orthographicSize += f * sensibility;
    }
}
