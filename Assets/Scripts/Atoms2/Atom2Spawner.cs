using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atom2Spawner : MonoBehaviour
{
    public int[] electrons;

    public int[] quantities;

    public float maxSpeed = 10;

    public Vector2 spawnLimits;

    public GameObject atom;

    void Awake()
    {
        int i = 0;
        while(i < electrons.Length)
        {
            Spawn(electrons[i], quantities[i]);
            i++;
        }
    }

    public void Spawn(int electrons,int quantity)
    {
        int i = quantity;

        while (i > 0)
        {
            Vector3 position = new Vector3(Random.Range(-spawnLimits.x, spawnLimits.x), Random.Range(-spawnLimits.y, spawnLimits.y), 0);

            Vector2 speed = new Vector2(Random.Range(-1,1), Random.Range(-1, 1)) * Random.Range(0, maxSpeed);

            GameObject t = Instantiate(atom, position, transform.rotation);

            t.GetComponent<Rigidbody2D>().velocity = speed;

            t.GetComponent<Atom2>().electrons = electrons;

            i--;
        }
    }

}
