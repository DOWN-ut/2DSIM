using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron_Synapse : MonoBehaviour
{
    public Neuron neuron;
  
    public float radius = 1;

    public bool linked;

    private void Update()
    {
        linked = false;
        Collider2D[] vics = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach(Collider2D a in vics)
        {
            if (a.GetComponent<Neuron>() != null)
            {
                if (!a.GetComponent<Neuron>().inputs.Contains(neuron) && a.GetComponent<Neuron>() != neuron)
                {
                    a.GetComponent<Neuron>().inputs.Add(neuron);
                    linked = true;
                }
            }
        }
    }

}
