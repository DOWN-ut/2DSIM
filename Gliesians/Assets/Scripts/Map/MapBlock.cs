using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MapBlock : MonoBehaviour
{
    [Header("Paramters")]

    public string biome;

    public Biome_Bloc_script biomeScript;

    public List<GameObject> lootboxes;

    public List<Vector3> lootboxesPositions;
    public int numberPerPosition = 4;
    public float espacement = 0.1f;
    public float altitude;

    public List<GameObject> detailsCenter;
    public int detailCentreProb = 1;
    public List<GameObject> detailsEdge;
    public int numberOfDetails = 3;
    public List<Transform> detailsTransformsEdge;

    public float size;

    [Header("Others")]

    public Transform centralCheckpoint;

    public MeshRenderer mesh;
    public PostProcessVolume postProcess;
    public Light[] lights;

    public Maper_0 maper;

    public PartyManager manager;

    void Start()
    {
        mesh.GetComponent<Renderer>().material = biomeScript.mainMaterial;

        postProcess.profile = biomeScript.postProcess;

        foreach (Light a in lights)
        {
            a.color = biomeScript.mainColor;
        }

        manager.mapMainCheckpoints.Add(centralCheckpoint);

        PlaceLootBoxes();

        PlaceDetails();

    }

    void PlaceLootBoxes()
    {
        if (lootboxesPositions.Count > 0)
        {
            foreach (GameObject a in lootboxes)
            {
                if (lootboxesPositions.Count > 0)
                {
                    int ran = Random.Range(0, lootboxesPositions.Count - 1);
                    /*
                    int i = 0;
                    while (i < numberPerPosition)
                    {
                        Vector3 pos = lootboxesSpawns[ran];
                        if (pos.x == 0)
                        {
                            pos += new Vector3(i * espacement, 0, 0);
                        }
                        if (pos.y == 0)
                        {
                            pos += new Vector3(0, 0, i * espacement);
                        }
                        */
                    GameObject spa = Instantiate(a, transform.position + lootboxesPositions[ran] + new Vector3(0, altitude, 0), transform.rotation);
                    //spa.transform.rotation = Quaternion.LookRotation((transform.position + new Vector3(0, altitude, 0)) - spa.transform.position);
                    spa.transform.localScale *= size;
                    spa.transform.parent = maper.navMap.transform;
                    //i++;
                    //}
                    lootboxesPositions.Remove(lootboxesPositions[ran]);
                }
            }
        }
    }
     
    void PlaceDetails()
    {
        if (detailsTransformsEdge.Count > 0 && numberOfDetails > 0)
        {
            foreach (GameObject a in detailsEdge)
            {
                int ran = Random.Range(0, detailsTransformsEdge.Count-1);
                if (detailsTransformsEdge.Count > 0 && numberOfDetails > 0)
                {
                    GameObject spa = Instantiate(a,detailsTransformsEdge[ran].position, detailsTransformsEdge[ran].rotation);
                    spa.transform.localScale *= size;
                    spa.transform.parent = maper.navMap.transform;
                    //spa.transform.rotation = Quaternion.LookRotation((transform.position + new Vector3(0, altitude, 0)) - spa.transform.position);
                }
                detailsTransformsEdge.Remove(detailsTransformsEdge[ran]);
                detailsTransformsEdge.Remove(detailsTransformsEdge[ran]);
                numberOfDetails--;
            }
        }
        int rana = Random.Range(0, detailsCenter.Count - 1 + detailCentreProb);
        if (detailsCenter.Count > 0 && rana < detailsCenter.Count)
        {
            GameObject span = Instantiate(detailsCenter[rana], transform.position, transform.rotation);
            span.transform.localScale = Vector3.one * size;
            span.transform.parent = maper.navMap.transform;
        } 
    }

}
