using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calc
{
    public struct Struct
    {
        public float v;
    }
    public Struct ddata;
}

public class Neuronal_FAI : MonoBehaviour
{
    [Header("Mode")]

    public bool learn = true;

    public bool useGPU;

    [Header("Parameters")]

    public float targetFPS;

    public string[] powersStr;

    public float adjustmentSpeed;
    public float movingMarge;

    public float errorPow;

    public int maxAdjustmentTries;
    public int maxAdjustmentPrecising;
    public int maxMovingTries;

    public int changingMethodDelay;
    public int stagnationMaxDuration;

    public float satisfayingError;

    public bool canUseMoving;
    public bool canUseReset;

    public ComputeShader computeShader;
    public int indexOfKernel;
    //public Calc.Struct[][][] structu;
    public float[][][] structu;
    public ComputeBuffer[][] buffers;

    public Texture2D texture;

    [Header("Data")]

    public Tableur_Reader data;
    public float[] antécédents;
    public float[] images;
    public string given; public InputField input;
    public float returned;

    [Header("Function")]

    public List<float[]> coefs = new List<float[]>();
    public List<string[]> powers = new List<string[]>();
    public float error;
    public float optiError;

    [Header("Display")]

    public TextMesh functionShow;

    public TextMesh listeShow;

    public TextMesh errorShow;

    public TextMesh timeShow;

    public TextMesh givenShow;
    public TextMesh returnedShow;
    public TextMesh returnedTranslatedShow;

    public TextMesh usingAdjustementShow;
    public TextMesh usingMovingShow;
    public TextMesh usingResetShow;

    [Header("Ingame")]

    public int time;

    public int timeSinceLastGoodAdjust;
    public int timeStagning;

    public bool adjusting; int adjustmentShowDelay; int adjustmentUses;
    public bool moving; int movingShowDelay; int movingUses;
    public bool reseting; int resetShowDelay; int resetUses;

    public bool satisfied;

    // Use this for initialization
    void Start()
    {
        optiError = 10;

        if(data!= null)
        {
            antécédents = data.ante.ToArray();
            images = data.ima.ToArray();
        }

        listeShow.text = "";
        int i = 0;
        while (i < images.Length)
        {
            listeShow.text += antécédents[i] + ">" + images[i].ToString();
            if (i < images.Length - 1)
            {
                listeShow.text += " ; ";
            }
            i++;
        }

        ReadStrPowers(',');

        i = 0;
        while (i < powers.Count)
        {
            coefs.Add(new float[powers[i].Length]);
            int o = powers[i].Length - 1;
            while (o >= 0)
            {
                coefs[i][o] = 0;
                o--;
            }
            i++;
        }

        indexOfKernel = computeShader.FindKernel("Calculate");

        if (useGPU)
        {
            //structu = new Calc.Struct[150][][];
            structu = new float[150][][];
            buffers = new ComputeBuffer[structu.Length][];
            int a = 0;
            while (a < structu.Length)
            {
                //structu[a] = new Calc.Struct[150][];
                structu[a] = new float[150][];
                buffers[a] = new ComputeBuffer[structu[a].Length];
                int b = 0;
                while (b < structu[a].Length)
                {
                    //structu[a][b] = new Calc.Struct[1];
                    structu[a][b] = new float[1];
                    int c = 0;
                    while (c < structu[a][b].Length)
                    {
                        //structu[a][b][c] = new Calc.Struct();
                        structu[a][b][c] = 1f;
                        c++;
                    }

                    buffers[a][b] = new ComputeBuffer(1, sizeof(float));
                    b++;
                }
                a++;
            }
            int s = 0;
            foreach (float[] r in coefs)
            {
                foreach(float t in r)
                {
                    s++;
                }
            }
            texture = new Texture2D(s, 1);
        }
    }

    public void Onoff()
    {
        learn = !learn;
    }

    // Update is called once per frame
    void Update()
    {
        if (learn)
        {
            float newError = GetError(coefs);  
            if(newError >= optiError && !satisfied)
            {
                timeStagning++;
                /*
                if(timeStagning > changingMethodDelay)
                {
                    ByMovingOneCoef();*/
                    if (timeStagning > stagnationMaxDuration && canUseReset)
                    {
                        ResetACoef();
                        timeStagning = 0;
                    }
                //}
                /*
                else
                {
                    ByAdjustingCoefs();
                }
                */
            }
            else
            {
                //ByAdjustingCoefs();
                timeStagning = 0;
            }
            error = newError;
            if (error < optiError)
            {
                optiError = error;
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
                    if (canUseMoving)
                    {
                        ByMovingOneCoef();
                        timeSinceLastGoodAdjust = 0;
                    }
                }
                if(error >= 1000)
                {
                    ResetACoef();
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
        else
        {
            given = input.text;
            if(given == "")
            {
                given = "a";
            }
            float g = float.Parse(data.Convert(given));
            float gc = Mathf.Pow(g, data.power);
            returned = Calculate(gc, coefs);
            givenShow.text = "Given :" + '\n' + given + '\n' + g.ToString() + '\n' + gc.ToString("F3");
            returnedShow.text = "Returned :" + '\n' + returned;
            returnedTranslatedShow.text = "Returned Translated :" + '\n' + data.Translate(returned);
        }
        adjustmentShowDelay++;
        movingShowDelay++;
        resetShowDelay++;

        time++;
    }

    public void ResetACoef()
    {
        reseting = true; resetShowDelay = 0;

        float lastError = error;
        int[] a = new int[2] { 0, 0 };

        int i = coefs.Count - 1;
        while (i >= 0)
        {
            int o = coefs[i].Length - 1;
            while (o >= 0)
            {
                if (coefs[i][o] != 0)
                {
                    List<float[]> tempCoefs = new List<float[]>(); tempCoefs.AddRange(coefs.ToArray());
                    tempCoefs[i][o] = 0;

                    float newError = GetError(tempCoefs);
                    if (newError < lastError)
                    {
                        lastError = newError;
                        a = new int[2] { i, o };
                    }
                }
                o--;
            }
            i--;
        }

        /*
        i = coefs.Count - 1;
        while (i >= 0)
        {
            int o = coefs[i].Length - 1;
            while (o >= 0)
            {
                if (coefs[a[0]][a[1]] == 0)
                {
                    if (a[1] < coefs[i].Length - 1)
                    {
                        a[1]++;
                    }
                    else
                    {
                        a[1] = 0;
                    }
                }
                o--;
            }
            i--;
        }
        */

        coefs[a[0]][a[1]] = 0;    

        error = lastError;
        optiError = error;
        timeStagning = 0;

        resetUses++;
    }

    //Methods functions

    public void ByAdjustingCoefs()
    {
        adjusting = true; adjustmentShowDelay = 0;
        int i = coefs.Count - 1;
        while (i >= 0)
        {
            int o = coefs[i].Length - 1;
            while (o >= 0)
            {
                AdjustCoef(coefs[i][o], i,o);
                o--;
            }
            i--;
        }
        adjustmentUses++;
    }

    public void ByMovingOneCoef()
    {
        moving = true; movingShowDelay = 0;
        int i = coefs.Count - 1;
        while (i >= 0)
        {
            int o = coefs[i].Length - 1;
            while (o >= 0)
            {
                MoveCoef(coefs[i][o], i,o);
                o--;
            }
            i--;
        }
        movingUses++;
    }

    public void MoveCoef(float coeff, int coefindex, int coefindex2)
    {
        float marge = movingMarge;
        float newCoef = coefs[coefindex][coefindex2] - marge;
        float finalCoef = coefs[coefindex][coefindex2];

        float lastError = error;

        int i = 0;
        while (i < maxMovingTries)
        {
            newCoef += Mathf.Abs((coefs[coefindex][coefindex2] + marge) - (coefs[coefindex][coefindex2] - marge)) / (float)maxMovingTries;
            List<float[]> tempCoefs = new List<float[]>(); tempCoefs.AddRange( coefs);
            tempCoefs[coefindex][coefindex2] = newCoef;

            float newError = GetError(tempCoefs);
            if (newError < lastError)
            {
                lastError = newError;
                finalCoef = newCoef;
                if (lastError < optiError)
                {
                    timeStagning = 0;
                }
            }

            i++;
        }

        coefs[coefindex][coefindex2] = finalCoef;
    }

    public void AdjustCoef(float coeff, int coefindex,int coefindex2)
    {
        float newCoef = coeff;

        float actError = error;

        float sign = 0;

        float div = 1;

        float random = 0;

        int i = 0;
        while (div < maxAdjustmentPrecising && i < maxAdjustmentTries)
        {
            float er = Mathf.Pow(error, errorPow); if(er > 10) { er = 10; }
            float pow = 1;
            float balek;
            if (float.TryParse(powers[coefindex][coefindex2], out balek))
            {
                pow = float.Parse(powers[coefindex][coefindex2] + 1);
            }
            float range = (adjustmentSpeed * er) / (float)(div * pow);
            float rangeMin = -range; float rangeMax = range;
            if (sign > 0)
            {
                rangeMin = 0;
            }
            if (sign < 0)
            {
                rangeMax = 0;
            }

            random = Random.Range(rangeMin, rangeMax);

            newCoef += random;

            /*
            List<float[]> tempCoefs = new List<float[]>();
            int o = 0;
            while (o < coefs.Count)
            {
                tempCoefs.Add(coefs[o]);
                o++;
            }
            tempCoefs[coefindex][coefindex2] = newCoef;
            */
            List<float[]> tempCoefs = new List<float[]>(); tempCoefs.AddRange( coefs);
            tempCoefs[coefindex][coefindex2] = newCoef;

            float newActError = GetError(tempCoefs);
            if (newActError > actError)
            {
                newCoef -= random;
                if (div > 1)
                {
                    div += 0.25f;
                }
                else
                {
                    sign = -random;
                }
            }
            else
            {
                actError = newActError;
                sign = random;
                if (sign != 0)
                {
                    div += 0.25f;
                }
            }

            i++;
        }

        if (actError < error)
        {
            coefs[coefindex][coefindex2] = newCoef;
        }
    }

    public float GetError(List<float[]> coefes)
    {
        float diff = 0;
        int i = 0;
        while (i < images.Length)
        {
            float value = Calculate(antécédents[i], coefes);
            //diff += (Mathf.Abs(images[i] - value) + Mathf.Abs(images[i])) / Mathf.Abs(images[i]);
            diff += Mathf.Abs(images[i] - value) / Mathf.Abs(images[i]);
            i++;
        }
        float e = (diff / (images.Length));
        if(float.IsNaN(e))
        {
            e = 1000;
        }
        return e;
    }

    public float Calculate(float x, List<float[]> coefes)
    {
        float value = x;
        //buffers = new ComputeBuffer[coefes.Count][];
        //structu = new Calc.Struct[coefes.Count][][];
        int i = coefes.Count - 1;
        while (i >= 0)
        {
            float lineValue = 0;
            /*
            if (i < coefs.Count - 1)
            {
                lineValue = value;
            }
            */
            //int zero = -1;
            //buffers[i] = new ComputeBuffer[coefes[i].Length];
            //structu[i] = new Calc.Struct[coefes[i].Length][];
            int o = coefes[i].Length - 1;
            while (o >= 0)
            {
                if (powers[i][o] == "exp")
                {
                    lineValue += Mathf.Pow(coefes[i][o], value); //Adding exponential part (n^x)
                }
                else if (powers[i][o] == "cos")
                {
                    lineValue += coefes[i][o] * Mathf.Cos(value); //Adding cosinus part ( cos(x) )
                }
                else if (powers[i][o] == "sin")
                {
                    lineValue += coefes[i][o] * Mathf.Sin(value); //Adding sinus part ( sin(x) )
                }
                else
                {
                    if (float.Parse(powers[i][o]) != 0)
                    {
                        if (!useGPU)
                        {
                            lineValue += coefes[i][o] * Mathf.Pow(value, float.Parse(powers[i][o])); //Adding powered part ( x^n )
                        }
                        else
                        {
                            texture.SetPixel(i * o, 1, new Color(value, float.Parse(powers[i][o]), coefes[i][o]));
                            computeShader.SetTexture(indexOfKernel, "texture", texture);
                            buffers[0][0] = new ComputeBuffer(1, sizeof(float));
                            computeShader.SetBuffer(indexOfKernel, "Result", buffers[0][0]);
                            computeShader.Dispatch(indexOfKernel, 1, 1, 1);
                            /*
                            computeShader.SetFloat("value", v);
                            computeShader.SetFloat("power", float.Parse(powers[i][o]));
                            computeShader.SetFloat("coef", coefes[i][o]);
                            //buffers[i][o].SetData(structu[i][o]);
                            buffers[i][o] = new ComputeBuffer(1, sizeof(float));
                            computeShader.SetBuffer(indexOfKernel, "Result", buffers[i][o]);
                            computeShader.Dispatch(indexOfKernel, 1, 1, 1);
                            */
                        }
                    }
                    else
                    {
                        //zero = o;
                        lineValue += coefes[i][o];
                    }
                }
                o--;
            }
            value = lineValue;
            /*
            if (zero > -1)
            {
                value += coefes[i][zero]; //Adding constante
            }
            */
            i--;
        }
        if (useGPU)
        {
            /*
            computeShader.SetTexture(indexOfKernel, Shader.PropertyToID("textur"), texture);
            buffers[i][o] = new ComputeBuffer(1, sizeof(float));
            computeShader.SetBuffer(indexOfKernel, "Result", buffers[i][o]);
            computeShader.Dispatch(indexOfKernel, 1, 1, 1);
            int c1 = coefes.Count - 1;
            while (c1 >= 0)
            {
                int c2 = coefes[c1].Length - 1;
                while (c2 >= 0)
                {
                    if (buffers[c1][c2] != null && structu[c1][c2] != null)
                    {
                        float[] data = new float[1] { 0f };
                        //buffers[c1][c2].GetData(structu[c1][c2]);
                        buffers[c1][c2].GetData(data, 0, 0, 1);
                        buffers[c1][c2].Release();
                        /*
                       Calc c = new Calc();
                       c.ddata = structu[c1][c2][0];
                       * /
                        //float c = structu[c1][c2][0];
                        float c = data[0];
                        //value += c.ddata.v;
                        value += c;
                    }
                    c2--;
                }
                c1--;
            }*/
        }

        /*
        if(value > 10000)
        {
            value = 10000;
        }*/

        return value;
    }

    //Read

    public void ReadStrPowers(char cutChar)
    {
        int i = 0;
        while(i < powersStr.Length)
        {
            List<string> strl = new List<string>();
            int o = 0;
            while(o < powersStr[i].Length)
            {
                string str = "";
                int p = o;
                while (p < powersStr[i].Length && powersStr[i][p] != cutChar)
                {
                    str += powersStr[i][p];
                    p++;
                }
                strl.Add(str);
                o = p+1;
            }
            i++;
            powers.Add(strl.ToArray());
        }
    }

    //Display functions

    public string ConvertInDisplay(float value, int i,int o)
    {
        string str = "";

        if (powers[i][o] == "exp")
        {
            str += value.ToString("F2") + "^x";
        }
        else if (powers[i][o] == "cos" || powers[i][o] == "sin" || powers[i][o] == "tan")
        {
            str += value.ToString("F2") + powers[i][o] + "(x)";
        }
        else
        {
            if (powers[i][o] != "0")
            {
                str += value.ToString("F2") + "x";
                str += (powers[i][o]);
            }
        }

        if (powers[i][o] != "0")
        {
            str += "  +  ";
        }

        return str;
    }

    public void DisplayFucntion()
    {
        functionShow.text = "";
        int z = -1;
        int i = coefs.Count - 1;
        while (i >= 0)
        {
            int o = coefs[i].Length - 1;
            while (o >= 0)
            {
                functionShow.text += ConvertInDisplay(Mathf.RoundToInt(coefs[i][o] * 100) / 100f, i,o);
                if (powers[i][o] == "0")
                {
                    z = o;
                }
                o--;
            }
            if (z > -1)
            {
                functionShow.text += (Mathf.RoundToInt(coefs[i][z] * 100) / 100f).ToString("F2") + "\n";
            }
            else
            {
                functionShow.text += " 0 " + "\n";
            }
            i--;
        }

        errorShow.text = error.ToString();

        timeShow.text = time.ToString();

        usingAdjustementShow.text = "Adjusments : " + adjustmentUses.ToString();
        usingMovingShow.text = "Moves : " + movingUses.ToString();
        usingResetShow.text = "Resets : " + resetUses.ToString();
    }

}
