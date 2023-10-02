using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FAI : MonoBehaviour
{
    [Header("Process")]
    public List<float> coefficients;
    [Tooltip("Number of point along the axis.")]
    public float resolution;
    public float range = 10;

    [Header("Data")]
    public List<float> antecedents;
    public List<float> images;

    [Header("Display")]
    public Transform errorDiagram;
    public List<Transform> points;
    public GameObject pointPrefab;
    public float lenght;
    public float height;
    public Transform pointer;

    private void Start()
    {
        int i = 0; int j = 0;
        while (i < resolution)
        {
            j = 0;
            while (j < resolution)
            {
                Vector3 pos = errorDiagram.position + (errorDiagram.forward * i * (lenght/resolution)) + (errorDiagram.right * j * (lenght/resolution));
                points.Add(Instantiate(pointPrefab, pos, Quaternion.identity,errorDiagram.GetChild(0)).transform);
                j++;
            }
            i++;
        }
    }

    void Update()
    {
        DrawError();
    }

    void DrawError()
    {
        float best = 10000;
        int i = 0; int j = 0; int o = 0;
        while (i < resolution)
        {
            j = 0;
            while (j < resolution)
            {
                coefficients[0] = i * (range/resolution);
                coefficients[1] = j * (range/resolution);
                float h = GetError(coefficients);

                if(h< best)
                {
                    pointer.localPosition = points[o].transform.localPosition;
                    best = h;
                }

                points[o].transform.localPosition = new Vector3(points[o].transform.localPosition.x, h*height, points[o].transform.localPosition.z);
                j++;
                o++;
            }
            i++;
        }
    }

    float GetError(List<float> coefs)
    {
        float r = 0;

        int i = 0;
        while(i < antecedents.Count)
        {
            r += Mathf.Abs( Calculate(antecedents[i], coefs) - images[i] );
            i++;
        }

        r /= i;

        return r;
    }

    float Calculate(float x, List<float> coefs)
    {
        float r = 0;

        int c = 0;
        while (c < coefs.Count)
        {
            if (c == 0)
            {
                r += coefs[c];
            }
            else
            {
                r += x * coefs[c];
            }
            c++;
        }

        return r;
    }
}
