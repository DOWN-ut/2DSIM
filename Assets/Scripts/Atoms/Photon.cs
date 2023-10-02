using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photon : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Atom>() != null)
        {
            //other.gameObject.GetComponent<Atom>().link = new Transform[other.gameObject.GetComponent<Atom>().electrons];
        }
        Destroy(gameObject);
    }
}
