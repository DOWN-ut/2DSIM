using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnerAtom : MonoBehaviour
{
    [Header("Objectd to spawn")]

    public GameObject thing;

    public int number;
    public float size = 1;

    [Header("Coordonates limits")]

    public float maxX;
    public float minX;
    public float maxY;
    public float minY;

    [Header("Object Propeties")]

    public int[] electrons;

    public float maxMass;
    public float minMass;

    public float maxInitialSpeed;
    public float minInitialSpeed;

    [Header("Ingame Values")]

    public float X;
    public float Y;
    public float Xr;
    public float Yr;

    public List<GameObject> obj;

    // Use this for initialization
    void Awake()
    {
        obj = new List<GameObject>();

    }

    public void Spawn(int quant,int electron = 0)
    {
        int n = 0;

        while (n <= quant - 1)
        {
            X = Random.Range(minX, maxX);
            Y = Random.Range(minY, maxY);
            Xr = Random.Range(-100, 100);
            Yr = Random.Range(-100, 100);

            GameObject temp = Instantiate(thing, new Vector3(X, Y, 0), new Quaternion(Xr, Yr, 0, 0));
            temp.GetComponent<Rigidbody2D>().mass = Random.Range(minMass, maxMass);
            temp.GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)).normalized * Random.Range(minInitialSpeed, maxInitialSpeed), ForceMode2D.Impulse);

            if (electron <= 0)
            {
                temp.GetComponent<Atom>().protons = electrons[Mathf.RoundToInt(Random.Range(0, electrons.Length))];
            }
            else
            {
                temp.GetComponent<Atom>().protons = electron;
            }

            temp.GetComponent<Atom>().electrons = temp.GetComponent<Atom>().protons;

            temp.GetComponent<Atom>().visual.color = temp.GetComponent<Atom>().colors[temp.GetComponent<Atom>().electrons - 1];

            temp.transform.localScale = Vector3.one * size;

            obj.Add(temp);

            n++;
        }

    }
}
