using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeGame_Damier : MonoBehaviour
{
    public float size;

    public float caseSize;

    public float visibleSize;

    public List<Vector2> firsts;

    public List<GameObject> cases = new List<GameObject>();

    public List<List<GameObject>> cases2D = new List<List<GameObject>>();

    public GameObject case_prefab;

    public LifeGame_Manager manager;

    private void Start()
    {
        manager = FindObjectOfType<LifeGame_Manager>();
        FindObjectOfType<Camera>().orthographicSize = 2.5f * visibleSize;
        Generate(size);
        Visibilize(visibleSize);
    }

    public void Visibilize(float vsize)
    {
        visibleSize = vsize;

        int mi = (int)((size / 2f) - (vsize / 2f)); int m = (int)((size / 2f) + (vsize / 2f));
        int x = mi;
        while (x < m)
        {
            int y = mi;
            while (y < m)
            {
                cases2D[x][y].GetComponent<LifeGame_Case>().visual.gameObject.SetActive(true);
                y++;
            }
            x++;
        }
    }

    void Generate(float taille)
    {
        int x = 0; 
        while(x < taille)
        {
            cases2D.Add(new List<GameObject>());
            int y = 0;
            while (y < taille)
            {
                Vector3 p = new Vector3 (x - (size / 2f), y - (size / 2f),transform.position.z);
                GameObject c = Instantiate(case_prefab, transform.position + (p * caseSize), transform.rotation, transform);
                //c.GetComponent<LifeGame_Case>().manager = manager;
                c.GetComponent<LifeGame_Case>().damier = this;
                c.GetComponent<LifeGame_Case>().position = new Vector2Int(x,y);
                if(firsts.Contains(new Vector2(x, y)))
                {
                    c.GetComponent<LifeGame_Case>().state = 1;
                    //c.GetComponent<LifeGame_Case>().Colorize(false);
                }
                cases.Add(c);
                cases2D[cases2D.Count - 1].Add(c);
                y++;
            }
            x++;
        }
    }
}
