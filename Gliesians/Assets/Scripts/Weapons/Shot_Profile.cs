using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Profile : MonoBehaviour
{
    public int damages;
    public int damageTargetTeam = 1; // -1  for everyone 0 for allies 1 for ennemis

    public string[] effects;
    public float[] values;
    public int[] durations;

    public bool[] stackable = new bool[10] { true, true, true, true, true, true, true, true, true, true };

    public int[] targetTeam = new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
}

