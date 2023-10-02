using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cells2Genetics : MonoBehaviour {

    public string genestring = "B9900b9900b0000B0000b9900b0000";

    public List<Transform> bodyParts;

    public List<char> partNames;
    public GameObject[] parts;

    public Transform bodyTransform;

	// Use this for initialization
	void Start () {
        GetComponent<Cell2Reprod>().food = new int[partNames.Count - 1];
        ReadGene();
	}

    /*
    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().angularDrag = GetComponent<Rigidbody2D>().angularVelocity * 10;
    }
    */

    void ReadGene()
    {
        bool paren = false;
        int i = 0;
        while (i + 2 < genestring.Length)
        {

            if (partNames.Contains(genestring[i]))
            {
                int nam = 0;
                while (nam+1 < partNames.Count && genestring[i] != partNames[nam])
                {
                    nam++;
                }
                Transform t = Instantiate(parts[nam/2]).transform;
                t.GetComponent<Cell2Part>().typ = partNames[nam];
                if(t.GetComponent<Cell2Eater>()!= null)
                {
                    t.GetComponent<Cell2Eater>().cent = this.GetComponent<Cells2Genetics>();
                }
                if (t.GetComponent<Cell2Motor>()!= null)
                {
                    t.GetComponent<Cell2Motor>().cent = this.GetComponent<Rigidbody2D>();
                }
                t.parent = bodyTransform;
                if ( (nam+2) % 2 != 0 && bodyParts.Count > 0)
                {
                    paren = false;
                    t.parent = bodyParts[bodyParts.Count - 1];                 
                }
                else
                {
                    paren = true;
                    t.GetComponent<Cell2Part>().main = true;
                }
                t.GetComponent<Cell2Part>().Start();
                bodyParts.Add(t);
                i++;
                int p = 0;
                bool done = false;
                while (i < genestring.Length && !done)
                {
                    if (!partNames.Contains(genestring[i]))
                    {
                        string st = "";
                        st += genestring[i];
                        if (i + 1 < genestring.Length)
                        {
                            if (!partNames.Contains(genestring[i + 1]))
                            {
                                i++;
                                st += genestring[i];
                            }
                        }
                        //print("st " + st);
                        if (p == 0)
                        {
                            t.GetComponent<Cell2Part>().position = float.Parse(st);
                        }
                        if (p == 1)
                        {
                            t.GetComponent<Cell2Part>().rotation = float.Parse(st);
                        }
                    }
                    else
                    {
                        done = true;
                    }
                    i++;
                    p++;
                    if (i < genestring.Length)
                    {
                        //print("pos " + genestring[i]);
                        if (partNames.Contains(genestring[i]))
                        {
                            done = true;
                        }
                    }
                    else
                    {
                        done = true;
                    }
                }
                i--;
            }
            i++;
        }
    }
}

