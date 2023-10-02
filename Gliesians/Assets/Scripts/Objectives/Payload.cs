using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Payload : MonoBehaviour
{
    public float speed;

    public Maper_0 maper;

    public Transform objective;

    public int team;

    public bool allyNear;
    public bool ennemyNear;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (allyNear)
        {
            GetComponent<NavMeshAgent>().speed = speed;
        }
        if (ennemyNear)
        {
            GetComponent<NavMeshAgent>().speed = -speed;
        }
        if((ennemyNear && allyNear)|| (!ennemyNear && !allyNear))
        {
            GetComponent<NavMeshAgent>().speed = 0;
        }

        if(objective != null)
        {
            GetComponent<NavMeshAgent>().SetDestination(objective.position);
        }
        else
        {
            Transform temp = maper.mainCheckpoints[Random.Range(0, maper.mainCheckpoints.Count - 1)];

            if ((temp.position - transform.position).magnitude >= maper.manager.minPayloadDistance)
            {

                NavMeshPath navP = new NavMeshPath();
                GetComponent<NavMeshAgent>().CalculatePath(temp.position, navP);

                if (navP.status == NavMeshPathStatus.PathComplete)
                { 
                    objective = temp;
                }

            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<ObjectInfos>() != null)
        {
            if(other.GetComponent<EntityManager>()!=null  && other.GetComponent<ObjectInfos>().team != team)
            {
                ennemyNear = true;
            }
            else if(other.GetComponent<PlayerClass>() != null && other.GetComponent<ObjectInfos>().team == team)
            {
                allyNear = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ObjectInfos>() != null)
        {
            if (other.GetComponent<EntityManager>() != null && other.GetComponent<ObjectInfos>().team != team)
            {
                ennemyNear = false;
            }
            else if (other.GetComponent<PlayerClass>() != null && other.GetComponent<ObjectInfos>().team == team)
            {
                allyNear = false;
            }
        }
    }
}
