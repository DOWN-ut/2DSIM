using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionAI_0 : MonoBehaviour {

    [Header("Mode")]

    public bool learn;

    [Header("Parameters")]

    public float targetFPS;

    public float adjustmentSpeed;
    public float movingMarge;

    public float errorPow;

    public int maxAdjustmentTries;
    public int maxAdjustmentPrecising;
    public int maxMovingTries;

    public int changingMethodDelay;
    public int retryDelay;

    public float satisfayingError;

    public bool canUseMoving;
    public bool canUseReset;
    public bool allowPeriodicity;

    public string[] powers;

    [Header("Data")]

    public float[] antécédents;
    public float[] images;

    [Header("Function")]

    public float[] coefs;
    public float periodic;
    public float error;

    [Header("Display")]

    public TextMesh functionShow;
    public TextMesh hypotesisShow;

    public TextMesh listeShow;

    public TextMesh errorShow;

    public TextMesh timeShow;

    public TextMesh usingAdjustementShow;
    public TextMesh usingMovingShow;
    public TextMesh usingResetShow;

    [Header("Ingame")]

    public int time;

    public int timeSinceLastGoodAdjust;

    public bool adjusting; int adjustmentShowDelay; int adjustmentUses;
    public bool moving; int movingShowDelay; int movingUses;
    public bool reseting; int resetShowDelay; int resetUses;

    public bool satisfied;

    // Use this for initialization
    void Start ()
    {
        listeShow.text = "";
        int i = 0;
        while (i < images.Length)
        {
            listeShow.text += antécédents[i] + ">" + images[i].ToString();
            if(i < images.Length - 1)
            {
                listeShow.text += " ; ";
            }
            i++;
        }
    }

    // Update is called once per frame
    void Update() {

        if (learn)
        {

                error = GetError(coefs);


            if (allowPeriodicity)
            {
                if (periodic == 0)
                {
                    periodic = 1;
                    float nerror = GetError(coefs);
                    if (nerror > error)
                    {
                        periodic = 0;
                    }
                }
                else
                {
                    AdjustPeriod();
                }
            }
            else
            {
                periodic = 0;
            }

            DisplayFucntion();

            if (error > satisfayingError)
            {
                satisfied = false;
                if (timeSinceLastGoodAdjust < changingMethodDelay)
                {
                    ByAdjustingCoefs();
                }
                else
                {
                    if (timeSinceLastGoodAdjust < retryDelay && canUseMoving)
                    {
                        ByMovingOneCoef();
                    }
                    else
                    {
                        if (canUseReset)
                        {
                            ResetACoef();
                        }
                        timeSinceLastGoodAdjust = 0;
                    }
                }

                timeSinceLastGoodAdjust++;
            }
            else
            {
                satisfied = true;
            }

            if (adjustmentShowDelay > 5)
            {
                adjusting = false;
            }
            if (movingShowDelay > 5)
            {
                moving = false;
            }
            if (resetShowDelay > 5)
            {
                reseting = false;
            }
        }

        adjustmentShowDelay++;
        movingShowDelay++;
        resetShowDelay++;

        time++;
    }

    //Methods functions

    public void ByAdjustingCoefs()
    {
        adjusting = true; adjustmentShowDelay = 0;
        int i = coefs.Length - 1;
        while (i >= 0)
        {
            AdjustCoef(coefs[i], i);
            i--;
        }
        adjustmentUses++;
    }

    public void ByMovingOneCoef()
    {
        moving = true; movingShowDelay = 0;
        int i = coefs.Length - 1;
        while (i >= 0)
        {
            MoveCoef(coefs[i], i);
            i--;
        }
        movingUses++;
    }

    //Actions functions

    public void AdjustPeriod()
    {
        int i = 0;
        while (i < 100)
        {
            float sign = 0;
            float range = (adjustmentSpeed);
            float rangeMin = -range; float rangeMax = range;
            if (sign > 0)
            {
                rangeMin = 0;
            }
            if (sign < 0)
            {
                rangeMax = 0;
            }

            float random = Random.Range(rangeMin, rangeMax);

            periodic += random;

            float nerror = GetError(coefs);
            if (nerror > error)
            {
                periodic -= random;
            }
            else
            {
                error = nerror;
                sign = Mathf.Sign(random);
            }
            i++;
        }
    }

    public void ResetACoef()
    {
        reseting = true; resetShowDelay = 0;

        float lastError = error;
        int a = 0;

        int i = coefs.Length - 1;
        while (i >= 0)
        {
            float[] tempCoefs = new float[coefs.Length];
            int o = coefs.Length - 1;
            while (o >= 0)
            {
                tempCoefs[o] = coefs[o];
                o--;
            }
            tempCoefs[i] = 0;

            float newError = GetError(tempCoefs);
            if (newError < lastError)
            {
                lastError = newError;
                a = i;
            }

            i--;
        }
     
        int u = 0;
        while(u < coefs.Length)
        {
            if (coefs[a] == 0)
            {
                if(a < coefs.Length - 1)
                {
                    a++;
                }
                else
                {
                    a = 0;
                }
            }
            u++;
        }

        coefs[a] = 0;

        resetUses++;
    }

    public void MoveCoef(float coeff, int coefindex)
    {
        float marge = movingMarge;
        float newCoef = coefs[coefindex] - marge;
        float finalCoef = coefs[coefindex];

        float lastError = error;

        int i = 0;
        while (i < maxMovingTries)
        {
            newCoef += Mathf.Abs((coefs[coefindex] + marge) - (coefs[coefindex] - marge)) / (float)maxMovingTries;
            float[] tempCoefs = new float[coefs.Length];
            int o = coefs.Length - 1;
            while (o >= 0)
            {
                tempCoefs[o] = coefs[o];
                o--;
            }
            tempCoefs[coefindex] = newCoef;

            float newError = GetError(tempCoefs);
            if (newError < lastError)
            {
                lastError = newError;
                finalCoef = newCoef;
                timeSinceLastGoodAdjust = 0;
            }

            i++;
        }

        coefs[coefindex] = finalCoef;
    }

    public void AdjustCoef(float coeff, int coefindex)
    {
        float newCoef = coeff;

        float actError = error;

        float sign = 0;

        float div = 1;

        float random = 0;

        int i = 0;
        int tries = 0;
        while (div < maxAdjustmentPrecising && i < maxAdjustmentTries)
        {
            float er = Mathf.Pow(error, errorPow);
            float pow = 1;
            float balek;
            if (float.TryParse(powers[coefindex],out balek))
            {
                pow = float.Parse(powers[coefindex]+1);
            }
            float range = (adjustmentSpeed * er)/(div*pow);
            float rangeMin = -range; float rangeMax = range;
            if(sign > 0)
            {
                rangeMin = 0;
            }
            if (sign < 0)
            {
                rangeMax = 0;
            }

            random = Random.Range(rangeMin, rangeMax);

            newCoef += random;

            float[] tempCoefs = new float[coefs.Length];
            int o = coefs.Length - 1;
            while (o >= 0)
            {
                tempCoefs[o] = coefs[o];
                o--;
            }
            tempCoefs[coefindex] = newCoef;

            float newActError = GetError(tempCoefs);
            if (newActError > actError)
            {
                newCoef -= random;
                sign = -Mathf.Sign(random);
                if (sign != 0)
                {
                    div += 1;
                }
            }
            else
            {
                actError = newActError;
                sign = Mathf.Sign(random);
            }

            i++;
        }

        if (actError < error)
        {
            coefs[coefindex] = newCoef;
            timeSinceLastGoodAdjust = 0;
        }
    }

    public float GetError(float[] coefes)
    {
        float diff = 0;
        int i = 0;
        while (i < images.Length)
        {
            float value = Calculate(antécédents[i],coefes);
            //diff += (Mathf.Abs(images[i] - value) + Mathf.Abs(images[i])) / Mathf.Abs(images[i]);
            diff += Mathf.Abs(images[i] - value)/ Mathf.Abs(images[i]);
            i++;
        }
        return (diff/(i+1));
    }

    public float Calculate(float x, float[] coefes)
    {
        float value = 0;
        int zero = 0;
        int o = coefes.Length - 1;
        while (o >= 0)
        {
            if (powers[o] == "exp")
            {
                value += Mathf.Pow(coefes[o], x); //Adding exponential part (n^x)
            }
            else if(powers[o] == "cos")
            {
                value += Mathf.Cos(coefes[o]*x); //Adding cosinus part ( cos(x) )
            }
            else if (powers[o] == "sin")
            {
                value += Mathf.Sin(coefes[o] * x); //Adding sinus part ( sin(x) )
            }
            else
            {
                if (float.Parse(powers[o]) > 0)
                {
                    value += coefes[o] * Mathf.Pow(x, float.Parse(powers[o]) ); //Adding powered part ( x^n )
                }
                else
                {
                    zero = o ;
                }
            }
            o--;
        }

        if (periodic != 0)
        {
            value = periodic * Mathf.Cos(value); //Periodize value
        }

        value += coefes[zero]; //Adding constante

        return value;
    }

    public string ConvertInDisplay(float value, int i)
    {
        string str = "";

        if (powers[i] == "exp")
        {
            str += value + "^x";
        }
        else if (powers[i] == "cos" || powers[i] == "sin" || powers[i] == "tan")
        {
            str += powers[i] + "(" + value + "x)";
        }
        else
        {
            if (powers[i] != "0")
            {
                str += value + "x";
                str += (powers[i]).ToString();
            }
        }

        if (powers[i] != "0")
        {
            str += " + ";
        }

        return str;
    }

    //Display functions

    public void DisplayFucntion()
    {
        functionShow.text = "";
        if (periodic != 0)
        {
            functionShow.text += periodic + "cos( ";
        }
        int z = 0;
        int i = coefs.Length - 1;
        while (i >= 0)
        {
            functionShow.text += ConvertInDisplay(Mathf.RoundToInt(coefs[i]*100)/100f,i);
            if (powers[i] == "0")
            {
                z = i;
            }
            i--;
        }
        if (periodic != 0)
        {
            functionShow.text += " ) + " ;
        }
        functionShow.text += Mathf.RoundToInt(coefs[z] * 100) / 100f;

        hypotesisShow.text = "Maybe y = ";
        if (periodic !=0)
        {
            hypotesisShow.text += Mathf.RoundToInt(periodic*10)/10 + "cos( ";
        }
        i = coefs.Length - 1;
        while (i >= 0)
        {
            hypotesisShow.text += ConvertInDisplay(Mathf.RoundToInt(coefs[i]), i);
            if (powers[i] == "0")
            {
                z = i;
            }
            i--;
        }
        if (periodic!=0)
        {
            hypotesisShow.text += " ) + " ;
        }
        hypotesisShow.text += Mathf.RoundToInt(coefs[z] * 100) / 100f;

        errorShow.text = error.ToString();

        timeShow.text = time.ToString();

        usingAdjustementShow.text = "Adjusments : " + adjustmentUses.ToString();
        usingMovingShow.text = "Moves : " + movingUses.ToString();
        usingResetShow.text = "Resets : " + resetUses.ToString();
    }
}
