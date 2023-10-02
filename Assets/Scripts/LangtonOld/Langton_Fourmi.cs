using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Langton_Fourmi : MonoBehaviour
{

    public bool move;

    public int color;

    [Header("Else")]
    public Langton_Manager manager;

    public GameObject casePrefab;

    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<Langton_Manager>();
        color = manager.color.Length;
        print("using " + color + " colors");
    }

    void Update()
    {
        if (!this.GetComponent<Collider2D>().IsTouchingLayers())
        {
            Instantiate(casePrefab, transform.position, transform.rotation);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Langton_Case" && move)
        {
            int state = 0;
            int i = 0;
            while (i < color)
            {
                if (collider.GetComponent<Langton_Case>().state == i)
                {
                    state = i;
                }
                i++;
            }
            if (state + 1 < color)
            {
                collider.GetComponent<Langton_Case>().state = state + 1;
                collider.GetComponent<Langton_Case>().visual.color = manager.color[state + 1];
                transform.Rotate(new Vector3(0, 0, manager.fourmi_rotation[state + 1]));
                transform.position += transform.up * manager.fourmi_step[state + 1];
            }
            else
            {
                collider.GetComponent<Langton_Case>().state = 0;
                collider.GetComponent<Langton_Case>().visual.color = manager.color[0];
                transform.Rotate(new Vector3(0, 0, manager.fourmi_rotation[0]));
                transform.position += transform.up * manager.fourmi_step[0];
            }
        }
        move = false;
    }
}
