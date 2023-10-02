using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    [Header("Entities and Player")]

    public List<GameObject> ennemies;
    public List<GameObject> allies;

    public Camera playerCam;

    [Header("Places and Checkpoints")]

    public Maper_0[] mapers;

    public List<Transform> mapMainCheckpoints; // The transforms placed in the center of each mapblock

    [Header("Objectives and Spawns")]

    public GameObject payloadPrefab;

    public float minPayloadDistance = 40;

    public GameObject lastPayload;

    [Header("Sky and Time")]

    public bool sunCycle;

    public Light sun;

    public float time;

    public float lighting;

    [Header("Music and Ambience")]

    public float danger;
    public float dangerLevel1 = 3;
    public float dangerLevel2 = 6;

    public AudioSource musicDanger0;
    public AudioSource musicDanger1;
    public AudioSource musicDanger2;

    public float maxVolume;

    void Start()
    {
        GameObject[] e = GameObject.FindGameObjectsWithTag("Ennemy");
        foreach(GameObject a in e)
        {
            if (!ennemies.Contains(a))
            {
                ennemies.Add(a);
            }
        }
        e = GameObject.FindGameObjectsWithTag("Ally");
        foreach (GameObject a in e)
        {
            if (!allies.Contains(a))
            {
                allies.Add(a);
            }
        }

        playerCam = FindObjectOfType<Controler>().cam.GetComponent<Camera>();

        sun = GameObject.Find("The Sun").GetComponent<Light>();

        //playerCam.GetComponent<SEGI>().sun = sun;
    }

    void ManageEntities()
    {
        GameObject[] e = GameObject.FindGameObjectsWithTag("Ennemy");
        foreach (GameObject a in e)
        {
            if (!ennemies.Contains(a))
            {
                ennemies.Add(a);
            }
        }
        e = GameObject.FindGameObjectsWithTag("Ally");
        foreach (GameObject a in e)
        {
            if (!allies.Contains(a))
            {
                allies.Add(a);
            }
        }

        foreach (GameObject a in ennemies.ToArray())
        {
            if (a.GetComponent<ObjectInfos>().dead)
            {
                ennemies.Remove(a);
            }
        }

    }

    void ManageSkyAndTime()
    {
        time = sun.transform.localRotation.eulerAngles.x;
        lighting = 0;
        if(time <= 90)
        {
            lighting = time / 90f;
        }
        //playerCam.GetComponent<SEGI>().skyIntensity = lighting;
    }

    void ManagerMusic()
    {
        if (!musicDanger0.isPlaying)
        {
            musicDanger0.Play();
        }
        musicDanger0.volume = (danger) * 0.1f;
        if (musicDanger0.volume > maxVolume)
        {
            musicDanger0.volume = maxVolume;
        }
        if (!musicDanger1.isPlaying)
        {
            musicDanger1.Play();
        }
        musicDanger1.volume = (danger - dangerLevel1) * 0.1f; 
        if (musicDanger1.volume > maxVolume)
        {
            musicDanger1.volume = maxVolume;
        }
        if (!musicDanger2.isPlaying)
        {
            musicDanger2.Play();
        }
        musicDanger2.volume = (danger - dangerLevel2)* 0.1f;
        if (musicDanger2.volume > maxVolume)
        {
            musicDanger2.volume = maxVolume;
        }
    }

    void ManagePayloads()
    {
        if(lastPayload == null)
        {
            Maper_0 mape = mapers[Random.Range(0, mapers.Length - 1)];

            lastPayload = Instantiate(payloadPrefab, mape.mainCheckpoints[Random.Range( 0, mape.mainCheckpoints.Count - 1)].position, transform.rotation);

            lastPayload.GetComponent<Payload>().maper = mape;
        }
    }

    void FixedUpdate()
    {
        if (sunCycle)
        {
            ManageSkyAndTime();
        }

        //ManagePayloads();

        ManagerMusic();
    }
}
