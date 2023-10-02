using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terre : MonoBehaviour {

    void OnTriggerStay2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Controller>().gravity = Vector3.MoveTowards(collision.gameObject.GetComponent<Controller>().gravity, -transform.up, Time.deltaTime * 10);       
    }
}
