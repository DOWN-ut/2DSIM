using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atom2 : MonoBehaviour
{
    [Header("Parameters")]

    public int electrons;

    [Header("Ingame")]

    public string surname;
    public float priority = 1;

    public List<Atom2> liaisons;

    public float charge;

    [Header("References")]

    public Atom2Manager manager;

    public SpriteRenderer visual;

    List<char> alphabet = new List<char>(26) { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
    List<char> numbers = new List<char>(10) { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    private void Start()
    {
        manager = FindObjectOfType<Atom2Manager>();

        transform.localScale = Vector3.one * Mathf.Pow(electrons, manager.sizePow) * manager.size;

        GetComponent<CircleCollider2D>().radius = manager.area;

        visual.color = manager.colors[electrons];

        int i = 10;
        while (i > 0)
        {
            char r = numbers[Random.Range(0, numbers.Count - 1)]; 
            surname += r;
            priority *= int.Parse(r.ToString());
            i--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool parenting = false;

        if (collision.GetComponent<Atom2>() != null && collision.gameObject != gameObject) //Si l'objet touché est bien un autre atome
        {
            collision.GetComponent<Atom2>().charge += Mathf.Pow(electrons,manager.electronChargePow);// * Mathf.Pow(liaisons.Count, manager.liaisonChargePow); //On ajoute de la charge à l'atome touché

            if (liaisons.Count < electrons) //Si les liaisons ne sont pas saturées
            {
                if (collision.GetComponent<Atom2>().liaisons.Count < collision.GetComponent<Atom2>().electrons) //Si les liaisons de l'autre atome ne sont pas saturées
                {
                    if (collision.GetComponent<Atom2>().electrons < electrons) //Si cet atome est plus gros que l'autre
                    {
                        parenting = true;
                    }
                    else if (collision.GetComponent<Atom2>().electrons == electrons) //Si ils sont de la meme taille ,
                    {
                        if (collision.GetComponent<Atom2>().liaisons.Count < liaisons.Count) //Si cet atome a plus de liaisons que l'autre
                        {
                            parenting = true;
                        }
                        else if( priority > collision.GetComponent<Atom2>().priority) //Si les deux atomes sont pareils, utilisation d'une priorité randomisée.
                        {
                            parenting = true;
                        }
                    }
                }
                else if(collision.GetComponent<Atom2>().charge >= manager.maxCharge * Mathf.Pow(electrons,manager.electronMaxChargePow)) //Si l'atome touché est surchargé, on appelle sa fonction de surcharge
                {
                    Saturate();
                }
            }
        }

        if (parenting)
        {
            Attach(collision.GetComponent<Atom2>());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Atom2>() != null && collision.gameObject != gameObject) //Si l'objet touché est bien un autre atome
        {
            collision.GetComponent<Atom2>().charge -= Mathf.Pow(electrons, manager.electronChargePow); //* Mathf.Pow(liaisons.Count, manager.liaisonChargePow); //On enlève de la charge à l'atome touché
        }
    }

    public void Saturate() //Quand l'atome est surchargé, il se détache de toutes ses liaisons
    {
        foreach(Atom2 a in liaisons.ToArray())
        {
            a.liaisons.Remove(this);

            liaisons.Remove(a);
        }

        if (GetComponent<Rigidbody2D>() == null)
        {
            gameObject.AddComponent<Rigidbody2D>();
        }

        transform.parent = null;
    }

    public void Attach(Atom2 target)
    {
        liaisons.Add(target);

        target.liaisons.Add(this);

        Destroy(target.GetComponent<Rigidbody2D>());

        target.transform.parent = transform;
    }
}
