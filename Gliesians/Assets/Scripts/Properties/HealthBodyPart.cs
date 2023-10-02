using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBodyPart : MonoBehaviour
{
    public string partName;

    public float damageCoef = 1;
    public bool critical;

    public Health mainHealth;

    public bool unKinematise;

    public void Damage(int damages,Vector3 impactForce = default(Vector3),Vector3 impactPoint = default(Vector3),string effect = default(string),bool push = true,bool blow = false)
    {
        int dam = (int)(damages * damageCoef);
        if (mainHealth.actLife > 0)
        {
            mainHealth.damager += dam;
        }

        if (mainHealth.damageShower != null && mainHealth.actLife > 0 && damages > 0)
        {
            GameObject temp = Instantiate(mainHealth.damageShower, mainHealth.hud);
            temp.transform.position = impactPoint;
            temp.transform.localRotation = Quaternion.Euler(0, 180, 0);
            //temp.transform.localScale *= (mainHealth.cam.position - transform.position).magnitude * 0.1f;
            temp.GetComponent<TextMesh>().text = dam.ToString();
            if (critical)
            {
                temp.GetComponent<TextMesh>().color = mainHealth.criticalDamagesColor;
            }
            else
            {
                temp.GetComponent<TextMesh>().color = mainHealth.simpleDamagesColor;
            }
        }

        if (unKinematise)
        {
            if ( (mainHealth.actLife - dam <= 0 || blow) && mainHealth.GetComponent<EntityManager>() != null)
            {
                mainHealth.GetComponent<EntityManager>().KinematismBool(false);
                mainHealth.GetComponent<ObjectInfos>().dead = true;
                GetComponent<Rigidbody>().isKinematic = false;
            }
            else if(GetComponent<Rigidbody>() != null)
            {
                GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        if (push)
        {
            if (GetComponent<Rigidbody>() != null)
            {
                GetComponent<Rigidbody>().AddForceAtPosition((impactForce * 0.5f) + mainHealth.transform.up, impactPoint, ForceMode.Impulse);
            }
            else
            {
                mainHealth.GetComponent<Rigidbody>().AddForceAtPosition((impactForce * 0.5f) + mainHealth.transform.up, impactPoint, ForceMode.Impulse);
            }
        }
        //mainHealth.GetComponent<Rigidbody>().AddForceAtPosition(impactForce,impactPoint,ForceMode.Impulse);
    }
}
