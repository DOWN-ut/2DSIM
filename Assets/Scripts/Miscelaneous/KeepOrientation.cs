using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOrientation : MonoBehaviour
{

    public Vector3 startRotation;

    public bool recoverAtStart = false;

    // Start is called before the first frame update
    void Start()
    {
        if (recoverAtStart)
        {
            startRotation = transform.rotation.eulerAngles ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler( startRotation);  
    }
}
