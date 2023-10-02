using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionAI_drawer : MonoBehaviour
{
    [Header("-- FUNCTION DRAW --")]
    [Header("Parameters")]

    public bool autoScreenX;
    public int quadrillageDivisionsX;
    public bool autoScreenY;
    public int quadrillageDivisionsY;

    public int courbePrecision;
    public float drawSpeed;

    [Header("Ingame")]

    public Transform[] points;
    public List<Transform> courbe;

    public Vector2 lengths;

    [Header("Referances")]

    public GameObject point;
    public Transform pointsParent;

    public GameObject courbePoint;
    public Transform courbeParent;

    public Transform quadrillageStart;
    public Transform quadrillageXEnd;
    public Transform quadrillageYEnd;

    public Transform drawer;

    [Header("-- Error DRAW --")]
    [Header("Parameters")]

    public float errorPow;

    public float resolution = 20;
    public bool contractCourbe;

    public int errorQuadrillageDivisionsX;
    public float errorQuadrillageDivisionsY;

    [Header("Ingame")]

    public List<Transform> errorCourbe;
    public Vector2 errorLengths;

    float initialDrawSpeed;
    float initialResolution;

    [Header("Referances")]

    public GameObject errorPoint;
    public Transform errorParent;

    public Transform errorQuadrillageStart;
    public Transform errorQuadrillageXEnd;
    public Transform errorQuadrillageYEnd;

    public Transform errorDrawer;

    public TextMesh errorGraphMaxVal;
    public TextMesh errorGraphMidVal;
    public TextMesh errorGraphLowVal;
    public TextMesh errorGraphSatifVal;

    [Header("-- Referances --")]

    public FunctionAI_0 AI;
    public Neuronal_FAI FAI;

    void Start()
    {
        lengths = new Vector2( (quadrillageXEnd.position - quadrillageStart.position).magnitude, (quadrillageYEnd.position - quadrillageStart.position).magnitude);
        errorLengths = new Vector2((errorQuadrillageXEnd.position - errorQuadrillageStart.position).magnitude, (errorQuadrillageYEnd.position - errorQuadrillageStart.position).magnitude);

        initialDrawSpeed = drawSpeed;
        initialResolution = resolution;

        /*
        courbe = new List<Transform>(courbePrecision);
        int o = 0; 
        while (o < courbe.Count)
        {
            courbe[o] = Instantiate(courbePoint, transform.position, transform.rotation).transform;
            o++;
        }
        */
    }

    void Update()
    {
        errorGraphMaxVal.text = errorQuadrillageDivisionsY.ToString();

        errorGraphMidVal.text = Mathf.Pow( (errorQuadrillageDivisionsY/2f),1/errorPow).ToString("F2");

        errorGraphLowVal.text = Mathf.Pow( (errorQuadrillageDivisionsY/4f),1/errorPow).ToString("F2");

        float satis = 0.001f;
        float t = 10;
        if(AI != null)
        {
            t = AI.targetFPS;
            satis = AI.satisfayingError;
        }
        else if (FAI != null)
        {
            t = FAI.targetFPS;
            satis = FAI.satisfayingError;
        }

        errorGraphSatifVal.text = satis.ToString();
        errorGraphSatifVal.transform.position = new Vector3(errorGraphSatifVal.transform.position.x, errorQuadrillageStart.position.y + (Mathf.Pow(satis, errorPow) * errorLengths.y), errorGraphSatifVal.transform.position.z);

        bool satisf = false;
        if (AI != null)
        {
            satisf = AI.satisfied;
        }
        else if (FAI != null)
        {
            satisf = FAI.satisfied;
        }
        if (satisf)
        {
            drawSpeed = initialDrawSpeed;
            resolution = initialResolution;
        }
        else
        {
            if (Time.deltaTime > 1 / (float)t && drawSpeed < 21 && resolution > 0.1f)
            {
                drawSpeed *= 1.1f;
                resolution /= 1.1f;
            }
            else
            {
                drawSpeed /= 1.1f;
                resolution *= 1.1f;
            }
        }
        if(drawSpeed < initialDrawSpeed)
        {
            drawSpeed = initialDrawSpeed;
        }

        DrawFunction();
        DrawError();
    }

    void DrawFunction()
    {
        float[] antecedents = new float[1];
        float[] images = new float[1];
        if (AI != null)
        {
            antecedents = AI.antécédents;
            images = AI.images;
        }
        else if (FAI != null)
        {
            antecedents = FAI.antécédents;
            images = FAI.images;
        }

        if (autoScreenX)
        {
            quadrillageDivisionsX = 1;
            foreach (float a in antecedents)
            {
                if ((int)Mathf.Abs(a) + 1 > quadrillageDivisionsX)
                {
                    quadrillageDivisionsX = (int)Mathf.Abs(a)+1;
                }
            }
        }

        if (autoScreenY)
        {
            quadrillageDivisionsY = 1;
            foreach (float a in images)
            {
                if (Mathf.RoundToInt(Mathf.Abs(a) + 0.5f) + 1 > quadrillageDivisionsY)
                {
                    quadrillageDivisionsY = Mathf.RoundToInt(Mathf.Abs(a) + 0.5f) + 1 ;
                }
            }
        }

        foreach (Transform a in points)
        {
            Destroy(a.gameObject);
        }

        points = new Transform[antecedents.Length];

        int i = 0;
        while (i < points.Length && i < 500) //>>Anti crash I guess ?
        {
            points[i] = Instantiate(point, new Vector3(quadrillageStart.position.x + ((lengths.x / quadrillageDivisionsX) * antecedents[i]), quadrillageStart.position.y + ((lengths.y / quadrillageDivisionsY) * images[i]), transform.position.z - 0.5f), transform.rotation, pointsParent).transform;
            i++;
        }

        foreach (Transform a in courbe)
        {
            Destroy(a.gameObject);
        }

        courbe = new List<Transform>(0);
        float posX = GetXPos(GetX(i));
        float posY = GetYPos(GetY(GetX(i)));
        if (Mathf.Abs(posY) < 1000 && Mathf.Abs(drawer.position.y) < 1000)
        {
            drawer.position = new Vector3(GetXPos(GetX(0)), GetYPos(GetY(GetX(0))), drawer.position.z);
        }
        else
        {
            drawer.position = new Vector3(GetXPos(GetX(0)), 1000, drawer.position.z);
        }

        i = 0;
        while (i < courbePrecision && i < 2000)
        {
            posX = GetXPos(GetX(i));
            posY = GetYPos(GetY(GetX(i)));
            Vector3 poss = new Vector3(posX, posY, drawer.position.z);
            if (posX < lengths.x && Mathf.Abs(posY) < lengths.y)
            {
                int o = 0;
                while (drawer.position != poss && o < 70)
                {
                    if (Mathf.Abs(posY - quadrillageStart.position.y) < lengths.y + 20)
                    {
                        drawer.position = Vector2.MoveTowards(drawer.position, poss, drawSpeed);
                        if (o > 399)
                        {
                            drawer.position = poss;
                        }
                        courbe.Add(Instantiate(courbePoint, drawer.transform.position, transform.rotation, courbeParent).transform);
                    }
                    else
                    {
                        drawer.position = poss;
                        o = 400;
                    }
                    o++;
                }
            }
            else
            {
                if(Mathf.Abs(posY) < 1000)
                {
                    drawer.position = poss;
                }
                else
                {
                    drawer.position = new Vector3(poss.x,1000,poss.z);
                }
            }
            //courbe[i].transform.position = new Vector3(posX,posY , transform.position.z - 0.5f);
            i++;
        }
    }

    void DrawError()
    {
        bool satisfied = false;
        int time = 0;
        float error = 1;
        if (AI != null)
        {
            satisfied = AI.satisfied;
            time = AI.time;
            error = AI.error;
        }
        else if (FAI != null)
        {
            satisfied = FAI.satisfied;
            time = FAI.time;
            error = FAI.error;
        }

        if (!satisfied && error <= quadrillageDivisionsY)
        {
            float posX = (errorLengths.x / errorQuadrillageDivisionsX) * time;

            if (time > errorQuadrillageDivisionsX)
            {
                posX = Mathf.Abs(errorQuadrillageStart.position.x - errorQuadrillageXEnd.position.x);
                int i = 0;
                while (i < errorCourbe.Count && i < 1000)
                {
                    if (!contractCourbe)
                    {
                        errorCourbe[i].position -= new Vector3((errorLengths.x / (float)errorQuadrillageDivisionsX), 0, 0);
                    }
                    else
                    {
                        errorCourbe[i].localPosition = new Vector3((errorCourbe[i].localPosition.x / (float)((posX + (errorLengths.x / errorQuadrillageDivisionsX)) / posX)), errorCourbe[i].localPosition.y, errorCourbe[i].localPosition.z);
                    }

                    if (errorCourbe[i].position.x < errorQuadrillageStart.position.x)
                    {
                        Destroy(errorCourbe[i].gameObject);
                        errorCourbe.Remove(errorCourbe[i]);
                    }
                    i++;
                }
            }

            Vector3 pos = errorQuadrillageStart.position + new Vector3(posX, (errorLengths.y / (float)errorQuadrillageDivisionsY) * Mathf.Pow(error, errorPow), 0);
            if(pos.y > 100)
            {
                pos.y = 100;
            }

            if (time > 1)
            {
                bool good = false;
                int t = 0;
                while (!good && t < 100)
                {
                    errorDrawer.position = Vector3.MoveTowards(errorDrawer.position, pos, 1f);
                    if (time > errorQuadrillageDivisionsX)
                    {
                        int p = (resolution > 500) ? 500 : (int)resolution;
                        while (p > 0)
                        {
                            errorCourbe.Add(Instantiate(errorPoint, errorCourbe[errorCourbe.Count - 1].position + ((errorDrawer.position - errorCourbe[errorCourbe.Count - 1].position) / (float)p), transform.rotation, errorParent).transform);
                            p--;
                        }
                    }
                    else
                    {
                        errorCourbe.Add(Instantiate(errorPoint, errorDrawer.position, transform.rotation, errorParent).transform);
                    }
                    if(errorDrawer.position == pos)
                    {
                        good = true;
                    }
                    t++;
                }
            }
            else
            {
                errorDrawer.position = pos;
            }
        }

    }

    float GetX(float i)
    {
        float v = -quadrillageDivisionsX + ((quadrillageDivisionsX * (i / (float)courbePrecision))*2);
        return v;
    }
    float GetY(float x)
    {
        float v = 0;
        if(AI != null)
        {
            v = AI.Calculate(x, AI.coefs);
        }
        else if(FAI != null)
        {
            v = FAI.Calculate(x, FAI.coefs);
        }
        return v;
    }
    float GetXPos(float x)
    {
        float posX = quadrillageStart.position.x + ((lengths.x / quadrillageDivisionsX) * (x));
        return posX;
    }
    float GetYPos(float y)
    {
        float posY = quadrillageStart.position.y + ((lengths.y / quadrillageDivisionsY) * (y));
        return posY;
    }

}
