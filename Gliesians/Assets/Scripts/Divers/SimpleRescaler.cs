using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRescaler : MonoBehaviour {

    public float speed;
    
    public Vector3 startSize;
    public Vector3 endSize;

    // Use this for initialization
    void Start()
    {
        transform.localScale = startSize;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.localScale = Vector3.MoveTowards(transform.localScale, endSize, Time.deltaTime * speed);

    }
}
