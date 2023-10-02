using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//using UnityEditor.SceneManagement;

public class Basic_Neuron : MonoBehaviour {

    [Header("Process")]

    public float outputValue;

    [Header("Links")]

    public List<Basic_Neuron> input_neurons;
    public List<float> inputCoefs;

    [Header("Miscelaneous")]

    public float visual_height;

    public Transform input_visuals;
    public Transform[] inputVisuals;
    public Transform inputBar;
    public Transform output_visual;
    public TextMesh output_txt;

    public GameObject linkVisual;

    // Use this for initialization
    void Start ()
    {
        UpdateVisuals();
    }

    // Update is called once per frame
    void Update()
    {

        if (input_neurons.Count > 0)
        {
            outputValue = Process();
        }

        UpdateShowedValues();
    }

    public float Process()
    {
        float y = 1;

        for (int i = 0; i < input_neurons.Count; i++)
        {
            y *= input_neurons[i].outputValue * inputCoefs[i];
        }
        //y /= input_neurons.Count;

        return y;
    }

    void UpdateShowedValues()
    {
        if (input_neurons.Count > 0)
        {
            for (int i = 0; i < inputVisuals.Length; i++)
            {
                inputVisuals[i].GetChild(0).GetComponent<TextMesh>().text = inputCoefs[i].ToString("F2");
            }
        }
        if (output_txt != null)
        {
            output_txt.text = outputValue.ToString("F2");
        }
    }

    public void UpdateVisuals()
    {
        //Deletes all input and output visuals

        for (int i = 0; i < input_visuals.childCount; i++)
        {
            DestroyImmediate(input_visuals.GetChild(i).gameObject);
        }

        if (input_neurons.Count > 0)
        {
            //Create new input and output visuals

            inputVisuals = new Transform[input_neurons.Count];
            for (int i = 0; i < input_neurons.Count; i++)
            {
                inputVisuals[i] = Instantiate(linkVisual, input_visuals.position - new Vector3(0, visual_height / 2f, 0), input_visuals.rotation, input_visuals).transform;

                Vector3 dir = (inputVisuals[i].position - input_neurons[i].output_visual.transform.position);

                inputVisuals[i].position += new Vector3(0, i * (visual_height / input_neurons.Count), 0);

                inputVisuals[i].GetComponent<LineRenderer>().SetPosition(0, inputVisuals[i].position);
                inputVisuals[i].GetComponent<LineRenderer>().SetPosition(1, input_neurons[i].output_visual.position);

                /*
                inputVisuals[i].GetChild(0).position = (inputVisuals[i].position + input_neurons[i].output_visual.position)/2;

                float z_rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                inputVisuals[i].rotation = Quaternion.Euler(0f, 0f, z_rot - 90);

                inputVisuals[i].GetChild(0).localScale = new Vector2(inputVisuals[i].GetChild(0).localScale.x, inputVisuals[i].GetChild(0).localScale.y * dir.magnitude);
                */

                inputVisuals[i].gameObject.SetActive(true);
            }
        }

        inputBar.localScale = new Vector2(inputBar.localScale.x, 0.2f * visual_height);

        UpdateShowedValues();

    }

}

/*
[CustomEditor(typeof(Basic_Neuron))]
public class Basic_Neuron_Editor : Editor
{
    bool wantLink;
    Basic_Neuron myScript;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        
        EditorApplication.update += Update;

        myScript = (Basic_Neuron)target;
        if (GUILayout.Button("Update Visualisation"))
        {
            Basic_Neuron[] n = GameObject.FindObjectsOfType<Basic_Neuron>();
            foreach (Basic_Neuron a in n)
            {
                a.UpdateVisuals();
            }
        }
        if (GUILayout.Button("Link (to the next selected neuron)"))
        {
            wantLink = true;
            Selection.objects = new Object[0];
        }
        if (GUILayout.Button("Unlink inputs"))
        {
            myScript.input_neurons = new List<Basic_Neuron>();
            myScript.inputCoefs = new List<float>();
        }
        if (GUI.changed && !Application.isPlaying)
        {
            //EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }
    void OnEnable() { EditorApplication.update += Update; }
    void OnDisable() { EditorApplication.update -= Update; }
    void Update()
    {
        if (wantLink && Selection.objects.Length > 0)
        {
            wantLink = false;
            myScript.input_neurons.Add(Selection.activeGameObject.GetComponent<Basic_Neuron>());
            myScript.inputCoefs.Add(1f);
            EditorUtility.SetDirty(target);
        }
    }
}*/
