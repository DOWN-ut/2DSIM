using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron : MonoBehaviour
{
    [Header("Parameters")]

    public float maxCharge = 3f;

    public float chargeNeed = 1f;

    public float maxLenght = 2f;

    public int maxDivisions = 4;

    public bool fix;

    [Header("Ingame")]

    public float charge = 1;

    public int divisions = 1;

    public List<Neuron> inputs = new List<Neuron>();

    [Header("References")]

    public List<Transform> synapses = new List<Transform>();
    public List<Transform> synapsesRotator = new List<Transform>();
    public List<Transform> synapsesPin = new List<Transform>();

    public GameObject neuronPrefab;

    public Neuron_Manager manager;

    [Header("Display")]

    public TextMesh showCharge;

    [Header("Others")]

    public float synapseGrowFactor = 0.33f;
    public float synapseGrowSpeed = 0.02f;

    public float chargeTransfertSpeed = 0.1f;

    void Start()
    {
        manager = FindObjectOfType<Neuron_Manager>(); 
    }

    void Update()
    {
        charge = Process(charge,manager.sens);

        if(charge < -0.01f)
        {
            Destroy(gameObject);
        }

        showCharge.text = charge.ToString("F2");
    }

    void SpawnNeuron(int pin)
    {
        GameObject temp = Instantiate(neuronPrefab, synapsesPin[pin].position, transform.rotation);
        //temp.transform.position += (temp.transform.position - synapsesPin[pin].position).normalized * (neuronPrefab.transform.lossyScale.x * 0.5f);
        temp.GetComponent<Neuron>().charge = temp.GetComponent<Neuron>().chargeNeed;
        temp.GetComponent<Neuron>().inputs = new List<Neuron>();
        foreach (Transform tra in temp.GetComponent<Neuron>().synapses)
        {
            tra.localScale = new Vector3(1, 0, 1);
        }
    }

    private void FixedUpdate()
    {
        int i = 0;
        while (i < synapses.Count)
        {
            if (!synapsesPin[i].GetComponent<Neuron_Synapse>().linked)
            {
                if (synapses[i].localScale.y < (charge * synapseGrowFactor * manager.growingFactor))
                {
                    synapses[i].localScale += new Vector3(0, synapseGrowSpeed * manager.growingSpeed * (synapses.Count/(float)Mathf.Pow(i + 1,1/2f)), 0);
                }

                if (synapses[i].localScale.y > maxLenght)
                {
                    bool ok = true;
                    Collider2D[] vics = Physics2D.OverlapCircleAll(synapsesPin[i].position, synapsesPin[i].GetComponent<Neuron_Synapse>().radius*5);
                    foreach (Collider2D a in vics)
                    {
                        if (a.GetComponent<Neuron>() != null)
                        {
                            ok = false;
                        }
                    }
                    if (ok)
                    {
                        SpawnNeuron(i);
                        charge -= (maxLenght * synapseGrowFactor * manager.growingFactor) + chargeNeed;
                    }
                    synapses[i].localScale = new Vector3(1, maxLenght, 1);
                }
            }

            i++;
        }

        if (charge > maxCharge && !fix && divisions < maxDivisions)
        {
            charge -= maxCharge;
            maxCharge *= manager.divisionMaxChargeMult;

            divisions += 1;

            synapsesRotator.Add(Instantiate(synapsesRotator[0], synapsesRotator[0].parent));
            float an = 60;
            if (divisions == 3)
            {
                an = -60;
            }
            if (divisions == 4)
            {
                an = 120;
            }
            if (divisions == 5)
            {
                an = -120;
            }
            synapsesRotator[synapsesRotator.Count - 1].localRotation = Quaternion.Euler(0, 0, an);

            synapses.Add(synapsesRotator[synapsesRotator.Count - 1].GetChild(0));
            synapses[synapses.Count - 1].localScale = new Vector3(1, 0, 1);

            synapsesPin.Add(synapses[synapses.Count - 1].GetChild(0));
        }
    }

    private void LateUpdate()
    {
        //inputs = new List<Neuron>();
    }

    public float Process(float actCharge,float sens = 1)
    {
        float value = actCharge;

        foreach(Neuron a in inputs)
        {
            if (a != null)
            {
                float v = a.charge * a.chargeTransfertSpeed * sens * manager.transfertSpeed;
                int i = 0;
                while( !(a.charge - v > chargeNeed && value + v > chargeNeed) && i < 10)
                {
                    v /= (float)i;
                    i++;
                }
                if ((a.charge - v > chargeNeed && value + v > chargeNeed))
                {
                    value += v;
                    a.charge -= v;
                }
            }
        }

        return value;
    }
}
