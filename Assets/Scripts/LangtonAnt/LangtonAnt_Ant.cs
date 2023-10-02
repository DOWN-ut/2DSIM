using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LangtonAnt_Ant : MonoBehaviour
{
    public Vector2 startPosition;

    public Vector2 position;

    Vector2 decalage;

    public int step;

    public LangtonAnt_Manager manager;

    public LangtonAnt_Damier damier;

    private void Start()
    {
        manager = FindObjectOfType<LangtonAnt_Manager>();
        damier = FindObjectOfType<LangtonAnt_Damier>();

        position = startPosition;
        transform.position = position * damier.caseSize;

        decalage = new Vector2(damier.size / 2f, damier.size / 2f);
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        int move = damier.WalkOnCase((int)position.x,(int)position.y);

        transform.Rotate(0, 0, manager.rotations[move]);

        float p = 1;
        if(transform.rotation.eulerAngles.z % 90 > 1)
        {
            p = Mathf.Sqrt(2);
        }
        position += (Vector2)transform.up * manager.steps[move] * p;

        transform.position = (Vector3)((position-decalage) * damier.caseSize) + new Vector3(0,0,-10);

        step++;
    }
}
