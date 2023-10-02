using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomImpulse : MonoBehaviour
{

    public float frequence;

    public float force;

    public float distance;

    public Vector3 startPos;

    public int d;

    bool ok;

    // Use this for initialization
    void Start()
    {
        startPos = transform.position;
    }

    void FixedUpdate()
    {
        if (d > frequence)
        {
            if (!ok)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPos + (transform.up * distance), Time.deltaTime * force);
                if (transform.position == startPos + (transform.up * distance))
                {
                    ok = true;
                }
            }
            if (ok)
            {
                if (transform.position == startPos)
                {
                    ok = false;
                    d = 0;
                }
                transform.position = Vector3.MoveTowards(transform.position, startPos, Time.deltaTime * force);
            }
        }
         
        d++;

    }
}
