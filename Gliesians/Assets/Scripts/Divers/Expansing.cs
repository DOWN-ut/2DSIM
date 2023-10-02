using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expansing : MonoBehaviour
{
    public Vector3 startPosition;
    public float distanceFactor;

    public Vector3 startSize = Vector3.one;

    private void Start()
    {
        startSize = transform.localScale;
        startPosition = transform.position;
    }

    void Update()
    {
        transform.localScale = startSize * (1+Mathf.Pow((startPosition - transform.position).magnitude, distanceFactor));
    }
}
