using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cellule_Part : MonoBehaviour
{
    [Header("Properties")]

    public string celluleName = "0A202020B202020C202020D202020E202020F202020G202020";

    public float mass = 1f;

    public float distance;
    public float orientation;
    public float angleRotation;
    public float vitesseRotation;

    public Vector3 position;

    public Vector2 vitesse;

    public Vector2 vitesseRelative;

    public float stickForce = 5f;

    [Header("References")]

    public Cellule noyau;

    public Rigidbody2D physic;

    public Manager manager;

    [Header("Ingame")]

    int sensRotation = 1;

    public bool destroyed;

    public bool attracted;

    bool returned;

    [Header("Miscelaneous")]

    public Color noyauColor = Color.yellow;
    public Color partColor = Color.white;
    public Color deadColor = Color.red;

    void Start()
    {
        if (GetComponent<Cellule>() == null)
        {
            if (noyau == null)
            {
                //Récupère le centre de la cellule
                noyau = GameObject.Find(celluleName).GetComponent<Cellule>();
            }
            //Définit l'orientation de cette partie de la cellule
            orientation = (((orientation - 50) / (float)(99 - 50)) * 90);
            transform.localRotation = Quaternion.Euler(0, 0, orientation );
            //Définit la distance de cette partie par rapport à son parent
            distance = (distance * 0.0025f);
            transform.position = transform.parent.position + (transform.up * distance * noyau.transform.localScale.magnitude);
            //Définit la masse de la cellule. Chaque partie ajoute +1
            noyau.GetComponent<Cellule_Part>().mass += mass;
            //
            angleRotation = (angleRotation * 1f);
            //
            vitesseRotation = (vitesseRotation - 50) * 0.1f;
        }
        else
        {
            noyau = GetComponent<Cellule>();
        }

        position = transform.position;

        physic = noyau.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!destroyed)
        {
            if (GetComponent<Cellule>() != null)
            {
                GetComponent<SpriteRenderer>().color = noyauColor;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = partColor;
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().color = deadColor;
        }

        if (manager == null)
        {
            manager = noyau.manager;
        }

        if (GetComponent<Rigidbody2D>() != null)
        {
            GetComponent<Rigidbody2D>().drag = manager.viscosity / 10f;
            GetComponent<Rigidbody2D>().gravityScale = manager.gravity;
        }

        Information();

        if (!destroyed && noyau != null)
        {
            Rotation();

            Fluide();

            Contact();
        }

        if (destroyed && noyau != null)
        {
            Destruction(false);
            //DestroyImmediate(gameObject);
        }
    }

    void Information() //Récupère des informations sur cette partie de la cellule
    {
        //Calcul de la vitesse de cette partie de la cellule
        vitesse = (transform.position - position) / (float)transform.lossyScale.magnitude;

        if (noyau != null)
        {
            //Calcul de la vitesse relative au noyau de la cellule
            vitesseRelative = vitesse - noyau.GetComponent<Cellule_Part>().vitesse;
        }

        //Obtention de la position actuelle de cette partie de la cellule
        position = transform.position;
    }

    void Rotation() //Fait tourner cette partie de la cellule avec une certaine vitesse
    {
        //Test de l'angle actuel de cette partie de cellue
        if (Mathf.Abs(Quaternion.Angle(transform.localRotation, Quaternion.Euler(0, 0, orientation))) >= angleRotation && ! returned)
        {
            sensRotation = -sensRotation; //On inverse le sens de rotation si l'angle dépasse l'angle maximal
            returned = true;
        }
        else
        {
            returned = false;
        }
        transform.Rotate(new Vector3(0, 0, sensRotation), vitesseRotation); //On fait tourner cette partie dans un certain sens avec une certain vitesse
    }

    void Fluide() //Application d'une force à la cellule en fonction de la vitesse relative au centre de cette partie de la celllule, comme si elle se déplaçait dans un fluide 
    {
        Vector2 distanceCentre = noyau.transform.position - transform.position; //Récupération du vecteur de cettre partie jusqu'au centre de la cellule 
        float force = (Vector2.Dot(distanceCentre.normalized, vitesseRelative.normalized) + 0.5f) * vitesseRelative.magnitude * manager.generalCelluleSpeed; //Définit la force qui poussera la cellule en fonction de la vitesse relative de cette partie à son centre
        noyau.GetComponent<Rigidbody2D>().AddForce(distanceCentre.normalized * force * manager.viscosity, ForceMode2D.Force); //Application de la force
    }

    void Contact()
    {
        Collider2D[] contact = Physics2D.OverlapCircleAll(transform.position, 7f);
        GameObject victim = null;
        GameObject proj = null;
        foreach(Collider2D a in contact)
        {
            if(a.GetComponent<Cellule_Part>() != null)
            {
                if (a.GetComponent<Cellule_Part>().celluleName != celluleName)
                {
                    if (!a.GetComponent<Cellule_Part>().destroyed)
                    {
                        if (victim == null)
                        {
                            victim = a.gameObject;
                        }
                    }
                    else if (proj == null)
                    {
                        proj = a.gameObject;
                    }
                }
            }
        }
        if (victim != null)
        {
            if ((victim.GetComponent<Cellule_Part>().vitesse + vitesse).magnitude > manager.breakingVelocity)
            {
                if (victim.GetComponent<Cellule_Part>().vitesseRelative.magnitude > vitesseRelative.magnitude)
                {
                    Destruction(true);
                }
            }
        }
        if (proj != null)
        {
            if ((proj.GetComponent<Cellule_Part>().vitesse + vitesse).magnitude > manager.projectileDestruction)
            {
                Destruction(true);
            }
        }
    }

    void Destruction(bool detachChilds)
    {
        if (detachChilds)
        {
            if (GetComponent<Rigidbody2D>() == null)
            {
                gameObject.AddComponent<Rigidbody2D>();
                GetComponent<Rigidbody2D>().mass = mass;
                GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            }
            physic = GetComponent<Rigidbody2D>();
            transform.parent = null;
        }

        int i = 0;
        while (i < transform.childCount)
        {
            if (detachChilds)
            {
                transform.GetChild(i).gameObject.AddComponent<Rigidbody2D>();
                transform.GetChild(i).gameObject.GetComponent<Rigidbody2D>().mass = 1f + transform.GetChild(i).childCount;
                GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            }
            if (transform.GetChild(i).gameObject.GetComponent<Cellule_Part>() != null)
            {
                transform.GetChild(i).gameObject.GetComponent<Cellule_Part>().destroyed = true;
                if (!detachChilds)
                {
                    transform.GetChild(i).gameObject.GetComponent<Cellule_Part>().physic = transform.gameObject.GetComponent<Cellule_Part>().physic;
                }
            }
            i++;
        }

        if (detachChilds)
        {
            transform.DetachChildren();
        }

        noyau = null;
        destroyed = true;
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Cellule_Part>() != null)
        {
            if (!collision.gameObject.GetComponent<Cellule_Part>().destroyed)
            {
                if (collision.gameObject.GetComponent<Cellule_Part>().vitesse.magnitude > vitesse.magnitude)
                {
                    if ((collision.gameObject.GetComponent<Cellule_Part>().vitesse + vitesse).magnitude > manager.breakingVelocity)
                    {
                        print(collision.gameObject.name);
                        Destruction(true);
                    }
                }
            }
            else
            {
                //collision.gameObject.GetComponent<Cellule_Part>().attracted = false;
            }
        }
    }
    */
    
    /*
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Cellule_Part>() != null)
        {
            if (collision.gameObject.GetComponent<Cellule_Part>().destroyed && collision.gameObject.GetComponent<Cellule_Part>().attracted)
            {
                collision.gameObject.GetComponent<Cellule_Part>().attracted = false;
            }
        }
    }
    */
}
