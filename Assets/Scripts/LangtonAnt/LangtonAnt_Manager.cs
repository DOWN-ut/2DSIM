using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LangtonAnt_Manager : MonoBehaviour
{
    public float[] rotations;

    public float[] steps;

    public Color[] colors;

    public List<GameObject> ants;

    public Text stepCounter;

    private void Update()
    {
        stepCounter.text = ants[0].GetComponent<LangtonAnt_Ant>().step.ToString();
    }
}
