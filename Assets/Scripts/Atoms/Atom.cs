using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atom : MonoBehaviour
{
    [Header("Properties")]
    public int protons;
    public int electrons;

    public float sizePow;
    public float forcePow;

    [Header("Liaisons")]
    public List<Atom> liaisons;

    [Header("Visual")]
    public SpriteRenderer visual;
    public Color[] colors;

    [Header("Other")]
    public GameObject photon;

    public AtomManager manager;

    public ComputeShader GPU;
    int indexOfKernel;
    ComputeBuffer[] buffers;

    private void Start()
    {
        manager = FindObjectOfType<AtomManager>();

        GetComponent<Rigidbody2D>().mass *= electrons;

        transform.localScale *= Mathf.Pow(electrons, manager.sizePow);
        GetComponent<CircleCollider2D>().radius = transform.localScale.magnitude * manager.radius;

        liaisons = new List<Atom>();
    }

    private void FixedUpdate()
    {
        visual.color = colors[electrons - 1];

        //GetComponent<Rigidbody2D>().drag = manager.drag;

        if (manager.attract)
        {
            Attract();
        }

        
        if(GetComponent<Rigidbody2D>().velocity.magnitude > manager.maxVelocity)
        {
            //GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * manager.maxVelocity;
            GetComponent<Rigidbody2D>().drag = Mathf.Pow(GetComponent<Rigidbody2D>().velocity.magnitude, manager.dragPow);
        }
        else
        {
            GetComponent<Rigidbody2D>().drag = 0;
        }
        
    }

    void Attract()
    {
        foreach(Atom a in liaisons)
        {
            Vector3 forc = transform.position - a.transform.position;
            a.GetComponent<Rigidbody2D>().AddForce(forc.normalized * manager.attractForce * a.GetComponent<Rigidbody2D>().mass * Mathf.Abs(Mathf.Pow((transform.localScale.magnitude * manager.consta) - forc.magnitude,manager.attractPow)),ForceMode2D.Force);
        }
    }

    void Repulse(GameObject vic)
    {
        Vector3 forc = vic.transform.position- transform.position;
        vic.GetComponent<Rigidbody2D>().AddForce( (forc.normalized * manager.repulseForce) / (float)Mathf.Pow(forc.magnitude,manager.repulsePow) , ForceMode2D.Force);
    }

    /*
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Atom>() != null)
        {
            bool c = false;
            foreach (Atom a in liaisons)
            {
                if (a == collision.gameObject.GetComponent<Atom>())
                {
                    c = true;
                }
            }
            if (!c && liaisons.Count < electrons && collision.gameObject.GetComponent<Atom>().liaisons.Count < collision.gameObject.GetComponent<Atom>().electrons)
            {
                liaisons.Add(collision.gameObject.GetComponent<Atom>());
            }
        }
        
    }
    */

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Atom>() != null)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity /= manager.areaSlow;
            bool c = false;
            //Do not add a link with an already linked atom
            foreach (Atom a in liaisons)
            {
                if (a == collision.gameObject.GetComponent<Atom>())
                {
                    c = true;
                }
            }
            //Do not add a link if there is already all the links in this atom or in the targeted atom
            if (liaisons.Count >= electrons || collision.gameObject.GetComponent<Atom>().liaisons.Count >= collision.gameObject.GetComponent<Atom>().electrons)
            {
                c = true;
            }
            //Add a link if the targeted atom already have this one in this links 
            /*
            foreach (Atom a in collision.gameObject.GetComponent<Atom>().liaisons)
            {
                if (a == this.GetComponent<Atom>())
                {
                    c = false;
                }
            }*/
            if (!c)
            {
                liaisons.Add(collision.gameObject.GetComponent<Atom>());
                collision.gameObject.GetComponent<Atom>().liaisons.Add(this.GetComponent<Atom>());
            }
            if (manager.repulse)
            {
                bool ca = false;
                foreach (Atom a in liaisons)
                {
                    if (a == collision.gameObject.GetComponent<Atom>())
                    {
                        ca = true;
                    }
                }
                if (!ca && liaisons.Count >= electrons)
                {
                    Repulse(collision.gameObject);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Atom>() != null)
        {
            int i = 0;
            while (i < liaisons.Count)
            {
                if (liaisons[i] == collision.gameObject.GetComponent<Atom>())
                {
                    //liaisons[i].GetComponent<Rigidbody2D>().velocity /= manager.detachSlow;
                    liaisons.Remove(liaisons[i]);
                }
                i++;
            }
        }
    }


}
