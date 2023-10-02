using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LangtonAnt_Case : MonoBehaviour
{
    public int state;

    public LangtonAnt_Manager manager;

    public LangtonAnt_Damier damier;

    public SpriteRenderer visual;

    public void Colorize()
    {
        visual.color = manager.colors[state];
        if (!visual.gameObject.activeSelf)
        {
            damier.Visibilize(damier.visibleSize * 1.1f);
        }
    }
}