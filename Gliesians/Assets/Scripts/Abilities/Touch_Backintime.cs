using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch_Backintime : MonoBehaviour
{
    [Header("Values")]
    public int returnTime = 5;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EntityManager>() != null)
        {
            other.transform.position = other.gameObject.GetComponent<EntityManager>().positions[other.gameObject.GetComponent<EntityManager>().positions.Count - returnTime];
            print(other.name);  
        }
    }
}
