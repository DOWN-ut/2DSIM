using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTimeCase : MonoBehaviour {

    [Tooltip("Main Properties")]
    public int state;
    public int maxLoss;
    public int maxGain;
    public int max;

    [Header("Ingame")]
    public int next;
    public int less;

    [Header("Visual")]
    public SpriteRenderer visual;

    [Header("Else")]
    public SpaceTimeManage manager;
    bool b;

    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<SpaceTimeManage>();
    }

    private void FixedUpdate()
    {
        next = state;
        less = 0 ;

        visual.color = new Color(visual.color.r, visual.color.g, visual.color.b, (float)state / (float)max);

        b = true;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (state > 0)
        {

            if (collision.gameObject.GetComponent<SpaceTimeCase>() != null && collision.gameObject != this.gameObject)
            {
                if (collision.gameObject.GetComponent<SpaceTimeCase>().state < state)
                {
                    collision.gameObject.GetComponent<SpaceTimeCase>().next += 1;
                    less += 1;
                    if(less > maxLoss)
                    {
                        less = maxLoss;
                    }
                }
            }
        }

    }

    private void LateUpdate()
    {
        if (b)
        {
            if (next > max)
            {
                next = max;
            }
            if (next - state > maxGain)
            {
                next = state + maxGain;
            }
            state = next;
            state -= less;
            b = false;
        }
    }

}
