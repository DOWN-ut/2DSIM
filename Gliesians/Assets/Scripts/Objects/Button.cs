using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    public bool activated;

    public Color yesColor = Color.green;
    public Color noColor = Color.red;
    public float intensity;

    public MeshRenderer visual;

    // Update is called once per frame
    void Update()
    {
        if (visual != null)
        {
            if (activated)
            {
                Material mat = visual.material;
                mat.color = yesColor;
                mat.SetVector("_EmissionColor", new Vector4(yesColor.r, yesColor.g, yesColor.b, 1)*intensity);
                mat.SetVector("_BaseColor", new Vector4(yesColor.r, yesColor.g, yesColor.b, 1)*intensity);
                visual.material = mat;
            }
            else
            {
                Material mat = visual.material;
                mat.color = noColor;
                mat.SetVector("_EmissionColor", new Vector4(noColor.r, noColor.g, noColor.b, 1)*intensity);
                mat.SetVector("_BaseColor", new Vector4(noColor.r, noColor.g, noColor.b, 1)*intensity);
                visual.material = mat;
            }
        }
    }
}
