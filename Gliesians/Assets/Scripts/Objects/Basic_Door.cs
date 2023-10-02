using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Door : MonoBehaviour
{
    [Header("Values")]

    public Transform[] batans;

    public float openSpeed;
    public bool synchronise;
    public bool smooth;

    public Vector3[] endPos;

    public Vector3[] endRot;

    public Button button;

    [Header("Ingame")]

    public bool opened;

    public Vector3[] startPos;
    public Vector3[] startRot;

    bool moved = true;

    // Start is called before the first frame update
    void Start()
    {
        startPos = new Vector3[batans.Length];
        startRot = new Vector3[batans.Length];
        int i = 0;
        while(i < batans.Length)
        {
            startPos[i] = batans[i].transform.localPosition;
            startRot[i] = batans[i].transform.localRotation.eulerAngles;
            i++;
        }
        //moved = opened;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (button != null)
        {
            opened = button.activated;
        }
        if (opened)
        {
            if (moved)
            {
                //bool b = true;
                int i = 0;
                while (i < batans.Length)
                {
                    float v = openSpeed; float vr = openSpeed;
                    if (smooth)
                    {
                        v *= (batans[i].transform.localPosition - endPos[i]).magnitude;
                        vr *= (batans[i].transform.localRotation.eulerAngles - endRot[i]).magnitude;
                    }
                    if (synchronise)
                    {
                        v *= (startPos[i] - endPos[i]).magnitude;
                        vr *= (startRot[i] - endRot[i]).magnitude;
                    }
                    batans[i].transform.localPosition = Vector3.MoveTowards(batans[i].transform.localPosition, endPos[i], Time.deltaTime * v);
                    batans[i].transform.localRotation = Quaternion.RotateTowards(batans[i].transform.localRotation, Quaternion.Euler(endRot[i]), Time.deltaTime * vr);
                    /*
                    if ((batans[i].transform.localPosition - endPos[i]).magnitude > 0.1f || (batans[i].transform.rotation.eulerAngles - endRot[i]).magnitude > 0.1f)
                    {
                        b = false;
                    }
                    */
                    i++;
                }
                /*
                if (b)
                {
                    moved = false;
                }
                */
            }
        }
        else 
        {
            if (moved)
            {
                //bool b = true;
                int i = 0;
                while (i < batans.Length)
                {
                    float v = openSpeed; float vr = openSpeed;
                    if (smooth)
                    {
                        v *= (batans[i].transform.localPosition - startPos[i]).magnitude;
                        vr *= (batans[i].transform.localRotation.eulerAngles - startRot[i]).magnitude;
                    }
                    if (synchronise)
                    {
                        v *= (startPos[i] - endPos[i]).magnitude;
                        vr *= (startRot[i] - endRot[i]).magnitude;
                    }
                    batans[i].transform.localPosition = Vector3.MoveTowards(batans[i].transform.localPosition, startPos[i], Time.deltaTime * v);
                    batans[i].transform.localRotation = Quaternion.RotateTowards(batans[i].transform.localRotation, Quaternion.Euler(startRot[i]), Time.deltaTime * vr);
                    /*
                    if ((batans[i].transform.localPosition - startPos[i]).magnitude > 0.1f || (batans[i].transform.rotation.eulerAngles - startRot[i]).magnitude > 0.1f)
                    {
                        b = false;
                    }
                    */
                    i++;
                }
                /*
                if (b)
                {
                    moved = true;
                }
                */
            }
        }
    }
}
