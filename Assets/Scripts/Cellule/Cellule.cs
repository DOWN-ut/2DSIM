using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Cellule : MonoBehaviour
{
    [Header("Caracteristics")]

    public string celluleName;
    public string genetic = "A202020B202020C202020D202020E202020F202020G202020";

    public float eatDistance = 9f;

    [Header("Ingame")]

    public int resources;

    [Header("Reference")]

    public Manager manager;

    public SpriteRenderer eatCircle;

    List<char> alphabet = new List<char>(26) { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};

    [Header("Build")]

    public GameObject cellPart;

    private void Awake()
    {
        int nam = Random.Range(0, 100000);
        celluleName = nam.ToString() + genetic;
        gameObject.name = celluleName;
        GetComponent<Cellule_Part>().celluleName = celluleName;
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        ReadGene();
    }

    private void FixedUpdate()
    {
        Eat();
    }

    void Eat()
    {
        eatCircle.transform.localScale = Vector3.one * eatDistance * 0.01f;
        Collider2D[] contact = Physics2D.OverlapCircleAll(transform.position, eatDistance);
        GameObject food = null;
        foreach (Collider2D a in contact)
        {
            if (a.GetComponent<Cellule_Part>() != null)
            {
                if (a.GetComponent<Cellule_Part>().destroyed && food == null)
                {
                    food = a.gameObject;
                }
            }
        }
        if (food != null)
        {
            if ((food.GetComponent<Cellule_Part>().vitesse + GetComponent<Cellule_Part>().vitesse).magnitude < manager.projectileEatSpeed)
            {
                resources += 1;
                food.transform.position += new Vector3(0, 5000, 0);
                //Destroy(food);
            }
        }
    }

    void ReadGene()
    {
        Transform[] parents = new Transform[26]; parents[0] = transform;
        int i = 0;
        while (i < genetic.Length)
        {
            if (alphabet.Contains(genetic[i]))
            {
                int p = alphabet.IndexOf(genetic[i]); 
                while (parents[p] == null)
                {
                    p--;
                }
                GameObject temp = Instantiate(cellPart, parents[p]);
                temp.GetComponent<Cellule_Part>().noyau = this;
                temp.GetComponent<Cellule_Part>().celluleName = celluleName;
                temp.name = genetic[i].ToString() + "_" + alphabet[p].ToString();

                parents[p + 1] = temp.transform;

                List<int> vals = new List<int>();

                int a = i + 1; bool b = false;
                while (a < genetic.Length && !b)
                {
                    if (alphabet.Contains(genetic[a]))
                    {
                        b = true;
                    }
                    else
                    {
                        vals.Add(int.Parse(genetic[a].ToString()));
                        a++;
                    }
                }

                if (vals.Count > 8)
                {
                    vals.RemoveRange(8, vals.Count - 8);
                }
                while (vals.Count % 8 != 0 && vals.Count < 8)
                {
                    vals.Add(0);
                }

                int o = 0;
                while (o < vals.Count)
                {
                    int v = int.Parse(vals[o].ToString() + vals[o + 1].ToString());
                    if (o < 2)
                    {
                        temp.GetComponent<Cellule_Part>().distance = v;
                    }
                    else if (o < 4)
                    {
                        temp.GetComponent<Cellule_Part>().orientation = v;
                    }
                    else if (o < 6)
                    {
                        temp.GetComponent<Cellule_Part>().angleRotation = v;
                    }
                    else if (o < 8)
                    {
                        temp.GetComponent<Cellule_Part>().vitesseRotation = v;
                    }
                    o += 2;
                }

                i = a;
                if (a < 1)
                {
                    i++;
                }
            }
            else
            {
                i++;
            }

        }
    }

}

/*
[CustomEditor(typeof(Cellule))]
public class Cellule_Editor : Editor
{
    bool wantLink;
    Cellule myScript;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        myScript = (Cellule)target;
        if (GUILayout.tex("Update Visualisation"))
        {
            Basic_Neuron[] n = GameObject.FindObjectsOfType<Basic_Neuron>();
            foreach (Basic_Neuron a in n)
            {
                a.UpdateVisuals();
            }
        }
    }
}*/
