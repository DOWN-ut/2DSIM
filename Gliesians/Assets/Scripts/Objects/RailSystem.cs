using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailSystem : MonoBehaviour
{
    public float speed;
    public float residualSpeed;

    public Transform[] checkpoints;

    public Transform outEntry;
    public Transform inEntry;

    public List<GameObject> outVictims;
    public List<int> outVictimsStates;
    public List<GameObject> inVictims;
    public List<int> inVictimsStates;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider[] outV = Physics.OverlapSphere(outEntry.position, 1);
        Collider[] inV = Physics.OverlapSphere(inEntry.position, 1);

        foreach(Collider a in outV)
        {
            if (a.GetComponent<ObjectInfos>() != null && a.GetComponent<Rigidbody>()!=null)
            {
                if (a.GetComponent<ObjectInfos>().railSystemAble && !outVictims.Contains(a.gameObject ) && !inVictims.Contains(a.gameObject))
                {
                    outVictims.Add(a.gameObject);
                    outVictimsStates.Add(checkpoints.Length - 1);
                }
            }
        }
        foreach (Collider a in inV)
        {
            if (a.GetComponent<ObjectInfos>() != null && a.GetComponent<Rigidbody>() != null)
            {
                if (a.GetComponent<ObjectInfos>().railSystemAble && !inVictims.Contains(a.gameObject) && !outVictims.Contains(a.gameObject))
                {
                    inVictims.Add(a.gameObject);
                    inVictimsStates.Add(0);
                }
            }
        }

        int i = 0;
        while (i < inVictims.Count || i < outVictims.Count)
        {
            if (i < inVictims.Count)
            {
                inVictims[i].transform.position = Vector3.MoveTowards(inVictims[i].transform.position, checkpoints[inVictimsStates[i]].position, Time.deltaTime * speed);
                inVictims[i].GetComponent<Rigidbody>().velocity = residualSpeed * (checkpoints[inVictimsStates[i]].position - inVictims[i].transform.position).normalized;

                if (inVictims[i].transform.position == checkpoints[inVictimsStates[i]].position)
                {
                    inVictimsStates[i]++;
                    if (inVictimsStates[i] == checkpoints.Length)
                    {
                        inVictims.RemoveAt(i);
                        inVictimsStates.RemoveAt(i);
                    }
                }
            }

            if (i < outVictims.Count)
            {
                outVictims[i].transform.position = Vector3.MoveTowards(outVictims[i].transform.position, checkpoints[outVictimsStates[i]].position, Time.deltaTime * speed);
                outVictims[i].GetComponent<Rigidbody>().velocity = residualSpeed * (checkpoints[outVictimsStates[i]].position - outVictims[i].transform.position).normalized;

                if (outVictims[i].transform.position == checkpoints[outVictimsStates[i]].position)
                {
                    outVictimsStates[i]--;
                    if (outVictimsStates[i] == -1)
                    {
                        outVictims.RemoveAt(i);
                        outVictimsStates.RemoveAt(i);
                    }
                }
            }

            i++;
        }
    }
}
