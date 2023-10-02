using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appat_object : MonoBehaviour
{
    public float portee;

    public Color[] colors;
    public float[] intensities;
    public float[] ranges;
    public float[] speeds;

    public string[] types;

    public int pos;

    private void Update()
    {
        Collider[] other = Physics.OverlapSphere(transform.position, portee);
        bool b = false;
        foreach (Collider a in other)
        {
            Vector3 tarc = new Vector3(colors[pos].r, colors[pos].g, colors[pos].b);

            if (a.GetComponent<Light_control>() != null)
            {
                a.GetComponent<Light_control>().controled = 2;

                Vector3 oldc = new Vector3(a.GetComponent<Light_control>().color.r, a.GetComponent<Light_control>().color.g, a.GetComponent<Light_control>().color.b);
                oldc = Vector3.MoveTowards(oldc, tarc, Time.deltaTime * speeds[pos]);
                a.GetComponent<Light_control>().color = new Color(oldc.x, oldc.y, oldc.z);

                a.GetComponent<Light_control>().intensity = Mathf.MoveTowards(a.GetComponent<Light_control>().intensity, intensities[pos], Time.deltaTime * speeds[pos] * (1 + Mathf.Abs(intensities[pos]-a.GetComponent<Light_control>().intensity)) );

                a.GetComponent<Light_control>().range = Mathf.MoveTowards(a.GetComponent<Light_control>().range, ranges[pos], Time.deltaTime * speeds[pos] * (1 + Mathf.Abs(ranges[pos] - a.GetComponent<Light_control>().range)));

                if (a.GetComponent<Light_control>().color == colors[pos] && a.GetComponent<Light_control>().intensity == intensities[pos] && a.GetComponent<Light_control>().range == ranges[pos])
                {
                    b = true;
                }
                else
                {
                    b = false;
                }
            }
            /*
            if(a.GetComponent<Material>()!= null)
            {
                Vector3 oldc = new Vector3(a.GetComponent<Material>().GetColor("_EmissionColor").r, a.GetComponent<Material>().GetColor("_EmissionColor").g, a.GetComponent<Material>().GetColor("_EmissionColor").b);
                oldc = Vector3.MoveTowards(oldc, tarc, Time.deltaTime * 2);
                a.GetComponent<Material>().SetColor("_EmissionColor", new Color(oldc.x, oldc.y, oldc.z));
            }
            */
        }
        if (b)
        {
            if (pos < colors.Length - 1)
            {
                pos++;
            }
            else
            {
                pos = 0;
            }
        }
    }

    /*
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Light_control>() != null)
        {
            other.GetComponent<Light_control>().controled = 2;

            Vector3 tarc = new Vector3(colors[pos].a, colors[pos].g, colors[pos].b);
            Vector3 oldc = new Vector3(other.GetComponent<Light_control>().color.a, other.GetComponent<Light_control>().color.g, other.GetComponent<Light_control>().color.b);
            oldc = Vector3.MoveTowards(oldc, tarc, Time.deltaTime * 2);
            other.GetComponent<Light_control>().color = new Color(oldc.x, oldc.y, oldc.z);

            other.GetComponent<Light_control>().intensity = Mathf.MoveTowards(other.GetComponent<Light_control>().intensity, intensities[pos], Time.deltaTime - 2);

            if (other.GetComponent<Light_control>().color == colors[pos] && other.GetComponent<Light_control>().intensity == intensities[pos])
            {
                if (pos < colors.Length)
                {
                    pos++;
                }
                else
                {
                    pos = 0;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<Light_control>().controled = 0;
        other.GetComponent<Light_control>().color = other.GetComponent<Light_control>().initialColor;
        other.GetComponent<Light_control>().intensity = other.GetComponent<Light_control>().initialIntensity;
    }
    */
}
