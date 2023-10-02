using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGame_Manager : MonoBehaviour
{
    public bool simulate;
    public bool pre;

    public int over;
    public int under;
    public int born;

    public Color[] colors;

    public int step;
    public Text stepCounter;

    private void FixedUpdate()
    {
        step++;
        stepCounter.text = step.ToString();
    }
}
