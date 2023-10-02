using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTimeManage : MonoBehaviour
{
    [Header("Cases management")]
    public Color[] color;
    public GameObject[] cases;

    [Header("Show things")]
    public string counterTitle;
    public int number = 0;
    public TextMesh counter;

    // Use this for initialization
    void Start()
    {
        cases = GameObject.FindGameObjectsWithTag("Langton_Case");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        number++;
        counter.text = counterTitle + "\n" + number;
    }

}
