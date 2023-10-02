using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuronal_Network : MonoBehaviour
{
    public List<Neurone_Layer> layers;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
    }
}

[System.Serializable]
public class Neurone_Layer
{
    public List<Neurone> neurones;
}

[System.Serializable]
public class Neurone
{
    public List<float> inputs;

    public float bias;
}
