using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Neuron_AI : MonoBehaviour {

    [Header("Mode")]

    [Tooltip("Mode 0 makes the network learn from the given data. Mode 1 allow the user to test the network with any data.")]
    public int mode;

    [Header("Parameters")]

    public int framesPerBoucle;

    public float satisfayingError;

    public float adjustementValue;
    public float errorPow;
    public int maxChangesPerCoef;

    [Header("Data")]

    public float userValue;

    public float[] antecedents;
    public float[] images;

    [Header("Neurons")]

    public Basic_Neuron[] neurons;
    public Basic_Neuron inputNeuron;
    public Basic_Neuron outputNeuron;

    [Header("Miscelaneous")]

    public TextMesh errorShow;
    public TextMesh compteurShow;

    [Header("Results")]

    public float error;
    float nerror;

    float[] dift;
    int compteur;

    int neuronIndex;
    int coefIndex;
    int ma;
    float lastValue;
    float lastRan = 1;

    bool gettingerror;
    bool boucling;
    int anteindex;

    // Use this for initialization
    void Start () {
		neurons = GameObject.FindObjectsOfType<Basic_Neuron>();
        dift = new float[antecedents.Length];
        neuronIndex = neurons.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == 0)
        {
            if (!gettingerror)
            {

                if (neuronIndex < neurons.Length)
                {
                    if (coefIndex < neurons[neuronIndex].inputCoefs.Count)
                    {
                        if (ma < maxChangesPerCoef)
                        {
                            if (error <= nerror)
                            {
                                nerror = error;
                                lastValue = neurons[neuronIndex].inputCoefs[coefIndex];
                                neurons[neuronIndex].inputCoefs[coefIndex] += lastRan * adjustementValue * Mathf.Pow(error, errorPow);
                                lastRan *= 1.1f;
                                ma++;
                                gettingerror = true;
                            }
                            else
                            {
                                neurons[neuronIndex].inputCoefs[coefIndex] = lastValue;
                                lastRan = Random.Range(-1f, 1f);
                                coefIndex++;
                                ma = 0;
                            }
                        }
                        else
                        {
                            lastRan = Random.Range(-1f, 1f);
                            coefIndex++;
                            ma = 0;
                        }
                    }
                    else
                    {
                        nerror = error;
                        neuronIndex++;
                        coefIndex = 0;
                    }
                }
                else
                {
                    neuronIndex = 0;
                    nerror = error;
                }

                compteur++;

                compteurShow.text = compteur.ToString();
                anteindex = 0;
                dift = new float[antecedents.Length];
                boucling = false;
            }
            else
            {
                if (anteindex < antecedents.Length)
                {
                    if (!boucling)
                    {
                        SetAntecedent(anteindex);
                    }
                    else
                    {
                        dift[anteindex] = GetDiff(anteindex);
                        anteindex++;
                    }
                    boucling = !boucling;
                }
                else
                {
                    error = GetError(dift);
                    gettingerror = false;
                }
            }
        }
        
        if(mode == 1)
        {
            inputNeuron.outputValue = userValue;
            error = Mathf.Abs(outputNeuron.outputValue -  userValue);
        }
        errorShow.text = error.ToString();
    }

    float GetDiff(int ante)
    {
        return (Mathf.Abs(GetOutput(ante) - images[ante]) + Mathf.Abs(images[ante]) ) / Mathf.Abs(images[ante]) ;
    }

    float GetError(float[] diffs)
    {
        float diff = 0;
        int i = 0;
        while (i < images.Length)
        {
            diff += dift[i];
            i++;
        }
        return (diff / i);
    }

    void SetAntecedent(int ante)
    {
        inputNeuron.outputValue = antecedents[ante];
    }

    float GetOutput(int ante)
    {
        return outputNeuron.outputValue;
    }

}
