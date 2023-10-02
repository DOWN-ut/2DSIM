using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LangtonAnt_Damier : MonoBehaviour
{
    public float size;

    public float caseSize;

    public float visibleSize;

    public List<GameObject> cases = new List<GameObject>();

    public List<List<GameObject>> cases2D = new List<List<GameObject>>();

    public GameObject case_prefab;

    public LangtonAnt_Manager manager;

    private void Start()
    {
        manager = FindObjectOfType<LangtonAnt_Manager>();
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
                cases2D[x][y].GetComponent<LangtonAnt_Case>().visual.gameObject.SetActive(true);
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
                c.GetComponent<LangtonAnt_Case>().manager = manager;
                c.GetComponent<LangtonAnt_Case>().damier = this;
                cases.Add(c);
                cases2D[cases2D.Count - 1].Add(c);
                y++;
            }
            x++;
        }
    }

    public int WalkOnCase(int x,int y)
    {
        int r = 0;

        r = cases2D[x][y].GetComponent<LangtonAnt_Case>().state;

        if (cases2D[x][y].GetComponent<LangtonAnt_Case>().state < manager.steps.Length - 1)
        {
            cases2D[x][y].GetComponent<LangtonAnt_Case>().state += 1;
        }
        else
        {
            cases2D[x][y].GetComponent<LangtonAnt_Case>().state = 0;
        }

        cases2D[x][y].GetComponent<LangtonAnt_Case>().Colorize();

        return r;
    }
}
