using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIsNear : MonoBehaviour
{
    public float lightDistance;

    // Update is called once per frame
    void LateUpdate()
    {
        /*
        Collider[] oldlights = Physics.OverlapSphere(transform.position, lightDistance+0.1f);
        foreach (Collider a in oldlights)
        {
            if (a.GetComponent<Light_control>() != null)
            {
                a.GetComponent<Light_control>().enabled = false;
                a.GetComponent<Light>().enabled = false;
            }
        }
        */
        Collider[] lights = Physics.OverlapSphere(transform.position, lightDistance);
        foreach(Collider a in lights)
        {
            if (a.GetComponent<Light_control>() != null)
            {
                a.GetComponent<Light_control>().enabled = true;
                a.GetComponent<Light>().enabled = true;
            }
        }
    }
}
