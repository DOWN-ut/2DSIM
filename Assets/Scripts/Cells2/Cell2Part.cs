using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell2Part : MonoBehaviour
{
    public char typ;
    public int life = 10;

    public bool main;
    public float position;
    public float rotation;

    public Transform body;

    public GameObject foodSpawn;

    public void Start()
    {
        float d = 100 / 99f;
        transform.localPosition = Vector3.zero;
        position = (-50 + (position*d))*0.3f;
        rotation = (-90 + (rotation * d * 1.8f));
        transform.localRotation = Quaternion.Euler(0, 0, rotation);
        transform.localPosition += transform.parent.TransformDirection(transform.parent.up) * position;

        if (transform.parent != null)
        {
            body.position = (transform.position + transform.parent.position)/ 2f;
            body.localScale = new Vector2(body.localScale.x * 0.5f,(body.localScale.y * (transform.parent.position-transform.position).magnitude)/6f);
            body.localRotation = Quaternion.LookRotation(transform.parent.position - transform.position);
            body.localRotation = Quaternion.Euler(0,0, body.localRotation.eulerAngles.y);
        }
    }

    public void Damage(int damages)
    {
        while (damages > 0 && life > 0)
        {
            GameObject t = Instantiate(foodSpawn, transform.position,transform.rotation);
            t.GetComponent<Cell2_food>().food = typ;
            life--;
            damages--;
        }
        if(life <= 0)
        {
            int i = 2;
            while (i < transform.childCount)
            {
                transform.GetChild(i).parent = null;
                i++;
            }
            Destroy(gameObject);
        }
    }
}

