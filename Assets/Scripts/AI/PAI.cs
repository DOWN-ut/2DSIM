using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PAI : MonoBehaviour
{
    [Header("Parameters")]

    public int[] widths;
    public int[] heights;

    [Header("Data")]

    public bool useData;
    public Texture2D[] antecedents;
    public Texture2D[] images;

    [Header("Compute")]

    public ComputeShader computer;
    public ComputeBuffer filterBuffer;
    public ComputeBuffer textureInBuffer;
    public ComputeBuffer widthBuffer;
    public int idi;

    [Header("Learning")]

    public bool learn = true;

    public float error;

    public float adjustSpeed;

    public float lastAdjust;
    public Vector2Int lastPixel;
    public int lastPixelCompo;
    public int lastFilter;

    [Header("Ingame")]

    public string round;

    public bool updatePictures;
    public bool randomisePictures;
    public bool updateFilters;
    public bool[] randomiseFilters; 

    public Color color;

    public List<Texture2D> filters;
    //public List<Color[]> filters;

    public RenderTexture outputRenderText;
    public Texture2D output;
    public Texture2D input;

    int d = 0;

    [Header("Rendering")]

    public SpriteRenderer inputRender;
    public SpriteRenderer outputRender;
    public SpriteRenderer imageRenderer;

    public TextMesh errorShow;

    public GameObject[] temps;

    public Vector3 filtersAncre;

    void Start()
    {
        round = Random.Range(0, 999999).ToString();

        int i = 0;
        filters = new List<Texture2D>();
        //filters = new List<Color[]>();

        while (i < widths.Length)
        {
            //int s = widths[i] * heights[i];
            //string nam = ("Assets/Data/PAI/" + round + i.ToString() + ".png");
            filters.Add(new Texture2D(widths[i],heights[i]));
            filters[i].Apply(false, false);
            //AssetDatabase.CreateAsset(filters[i],nam );
            //filters[i] = AssetDatabase.LoadAssetAtPath<Texture2D>(nam);
            i++;
        }

        temps = new GameObject[filters.Count];

        i = 0;
        while (i < temps.Length)
        {
            temps[i] = Instantiate(new GameObject());
            i++;
        }

        input = GeneratePicture(widths[0], heights[0], color,randomisePictures);
        input.filterMode = FilterMode.Point;
        foreach(Texture2D a in images)
        {
            a.filterMode = FilterMode.Point;
        }
        foreach (Texture2D a in antecedents)
        {
            a.filterMode = FilterMode.Point;
        }
        outputRenderText = new RenderTexture(widths[widths.Length-1], heights[heights.Length-1], 0, RenderTextureFormat.ARGB32,RenderTextureReadWrite.Default);
        outputRenderText.enableRandomWrite = true;                 // this is requred to work as compute shader side written texture
        outputRenderText.Create();                                 // yes, we need to run Create() to actually create the texture

        //computer = Resources.Load<ComputeShader>("PAIshader");
        idi = computer.FindKernel("Calc");

        UpdateFilters();
        UpdateTexture();
    }

    void Update()
    {
        if (updateFilters)
        {
            UpdateFilters();
        }
        if (updatePictures)
        {
            UpdateTexture();
        }

        int i = 0;
        while (i < filters.Count)
        {
            if (i <= 0)
            {
                InitBuffers(input, filters[i]);
                InitShader(input, filters[i]);
            }
            else
            {
                InitBuffers(output, filters[i]);
                InitShader(output, filters[i]);
            }
            computer.Dispatch(idi, input.width, input.height, 1);
            output = toTexture2D(outputRenderText); output.filterMode = FilterMode.Point;
            i++;
        }

        if (learn)
        {
            float newError = 0;
            int w = 0; int h = 0; int num = 0;
            while (w < input.width)
            {
                h = 0;
                while (h < input.height)
                {
                    Color pix = (input.GetPixel(w, h) - output.GetPixel(w, h));
                    Vector3 piv = new Vector4(pix.r, pix.g, pix.b);
                    newError += piv.magnitude;
                    h++;
                    num++;
                }
                w++;
            }
            newError /= num;
            if (newError > error)
            {
                Adjust(true); //Undo last change if it was bad
            }
            else if (newError < error)
            {
                Adjust(false,true); //Repeat last change if it was cool
            }
            if (newError != error || d > 2)
            {
                Adjust();
                d = 0;
            }
            if (newError != error)
            {
                error = newError;
            }
        }
        errorShow.text = error.ToString();

        DisplayPictures(filters, filtersAncre, 0);

        d++;
    }

    public void Adjust(bool undoLast = false,bool repeat = false)
    {
        if (!undoLast)
        {
            if (!repeat)
            {
                lastAdjust = Random.Range(-adjustSpeed, adjustSpeed);
                lastFilter = Random.Range(0, filters.Count);
                lastPixelCompo = Random.Range(0, 3);
                lastPixel = new Vector2Int(Random.Range(0, filters[lastFilter].width), Random.Range(0, filters[lastFilter].height));
            }
            else
            {
                lastAdjust *= 0.25f;
            }
        }
        else
        {
            lastAdjust = -lastAdjust;
        }
        Color c = filters[lastFilter].GetPixel(lastPixel.x, lastPixel.y);
        if (lastPixelCompo == 0)
        {
            c.r += lastAdjust;
        }
        else if (lastPixelCompo == 1)
        {
            c.g += lastAdjust;
        }
        else if (lastPixelCompo == 2)
        {
            c.b += lastAdjust;
        }
        else if (lastPixelCompo == 3)
        {
            c.r += lastAdjust;
            c.g += lastAdjust;
            c.b += lastAdjust;
        }
        filters[lastFilter].SetPixel(lastPixel.x, lastPixel.y, c);
        filters[lastFilter].Apply();
    }

    public void InitBuffers(Texture2D textureIn, Texture2D filterIn)
    {
        //Color[] pixels = new Color[textureIn.width * textureIn.height];
        //int[] ind = new int[textureIn.width * textureIn.height];
        int[] wid = new int[textureIn.width * textureIn.height];
        int w = 0; int h = 0; int i = 0;
        /*
        while (w < textureIn.width)
        {
            h = 0;
            while(h < textureIn.height)
            {
                pixels[i] = textureIn.GetPixel(w, h);
                //ind[i] = i;
                wid[i] = textureIn.width;
                h++;
                i++;
            }
            w++;
        }
        */
        //filterBuffer = new ComputeBuffer(filterIn.Length, sizeof(float)*4);
        //filterBuffer.SetData(filterIn);
        //textureInBuffer = new ComputeBuffer(pixels.Length, sizeof(float)*4);
        //textureInBuffer.SetData(pixels);
        widthBuffer = new ComputeBuffer(wid.Length, sizeof(int));
        widthBuffer.SetData(wid);
        //indexBuffer.SetData(ind);
        //indexBuffer = new ComputeBuffer(ind.Length, sizeof(int));
        //indexBuffer.SetData(ind);
    }

    private void OnDestroy()
    {
        filterBuffer.Release();
        textureInBuffer.Release();
    }

    public void InitShader(Texture2D inputed,Texture2D filtrer)
    {
        //computer.SetBuffer(idi, "textureIn", textureInBuffer);
        //computer.SetBuffer(idi, "filter", filterBuffer);
        computer.SetBuffer(idi, "width", widthBuffer);
        computer.SetTexture(idi, "textureOut", outputRenderText);
        /*
        RenderTexture inpute = new RenderTexture(inputed.width, inputed.height,0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
        inpute.enableRandomWrite = true;
        Graphics.Blit(inputed,inpute);
        inpute.Create();
        */
        Texture2D inp = new Texture2D(inputed.width, inputed.height, inputed.format,false);
        inp.LoadImage(inputed.GetRawTextureData(),false);
        computer.SetTexture(idi, "textureInput", inp);
        /*
        RenderTexture filt = new RenderTexture(filtrer.width, filtrer.height,0,RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
        filt.enableRandomWrite = true;
        Graphics.Blit(filtrer, filt);
        filt.Create();
        */
        computer.SetTexture(idi, "filtrer", filtrer);
    } 

    public Texture2D GeneratePicture(int w, int h, Color c, bool random = false)
    {
        Texture2D pic = new Texture2D(w, h);

        int iw = w;
        while (iw >= 0)
        {
            int ih = h;
            while (ih >= 0)
            {
                Color co = c;
                if (random)
                {
                    c = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
                }
                pic.SetPixel(iw, ih, c);
                ih--;
            }
            iw--;
        }

        pic.Apply();
        pic.filterMode = FilterMode.Point;

        return pic;
    }

    void DisplayPictures(List<Texture2D> pics, Vector3 ancr, int start = 0)
    {       
        int i = start; int o = pics.Count-1;
        while (i < temps.Length && o >= 0)
        {
            if (temps[i].GetComponent<SpriteRenderer>() == null)
            {
                temps[i].AddComponent<SpriteRenderer>();
            }
            temps[i].GetComponent<SpriteRenderer>().sprite = Sprite.Create(pics[o], new Rect(0, 0, pics[o].width, pics[o].height), new Vector2(0.5f, 0.5f));
            temps[i].GetComponent<SpriteRenderer>().transform.position = ancr + new Vector3(o * 2.5f, 0, 0);
            temps[i].GetComponent<SpriteRenderer>().transform.localScale = (Vector3.one * 200)/(float)Mathf.Pow(1+(pics[o].width * pics[o].height),1/2f);
            o--;
            i++;
        }
        
        inputRender.sprite = Sprite.Create(input, new Rect(0, 0, input.width, input.height), new Vector2(0.5f, 0.5f)); inputRender.transform.localScale = (Vector3.one*250) /(float)Mathf.Pow((input.width * input.height),1/2f);
        //filterRender.sprite = Sprite.Create(filter, new Rect(0, 0, filter.width, filter.height), new Vector2(0.5f, 0.5f));
        outputRender.sprite = Sprite.Create(output, new Rect(0, 0, output.width, output.height), new Vector2(0.5f, 0.5f)); outputRender.transform.localScale = (Vector3.one*250) / (float)Mathf.Pow((output.width * output.height),1/2f);
        imageRenderer.sprite = Sprite.Create(images[0], new Rect(0, 0, images[0].width, images[0].height), new Vector2(0.5f, 0.5f)); imageRenderer.transform.localScale = (Vector3.one*250) / (float)Mathf.Pow((images[0].width * images[0].height),1/2f);
    }

    public List<Texture2D> Calculate(List<Texture2D> pics, List<Texture2D> filts)
    {
        List<Texture2D> picts = pics;

        int i = 1;
        while (i < picts.Count)
        {
            Vector3 value = Vector3.zero; int num = 0;
            int iw = 0;
            while (iw < picts[i-1].width)
            {
                int ih = 0;
                while (ih < picts[i-1].height)
                {
                    value += new Vector3(picts[i - 1].GetPixel(iw, ih).r, picts[i - 1].GetPixel(iw, ih).g, picts[i - 1].GetPixel(iw, ih).b);
                    num++;
                    ih++;
                }
                iw++;
            }
            value /= num;
            iw = 0;
            while (iw < picts[i].width)
            {
                int ih = 0;
                while (ih < picts[i].height)
                {
                    picts[i].SetPixel(iw,ih,new Color(value.x*filts[i-1].GetPixel(iw,ih).r*2, value.y * filts[i-1].GetPixel(iw, ih).g*2,value.z* filts[i-1].GetPixel(iw, ih).b*2));
                    ih++;
                }
                iw++;
            }
            picts[i].Apply();
            picts[i].filterMode = FilterMode.Point;
            i++;
        }
        return picts;
    }

    public void UpdateFilters()
    {
        int i = 0;
        while (i < filters.Count)
        {
            //filters[i] = GeneratePicture(widths[i], heights[i], color, randomiseFilters[i]);
            i++;
        }
    }

    public void UpdateTexture()
    {
        if (useData)
        {
            input = antecedents[0];
            input.Apply(false, false);
        }
        else
        {
            input = GeneratePicture(input.width, input.height, color, randomisePictures);
        }
    }

    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(rTex.width, rTex.height);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
}

/*
[CustomEditor(typeof(PAI))]
public class PAI_Editor : Editor
{
    bool wantLink;
    PAI myScript;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        myScript = (PAI)target;
        if (GUILayout.Button("Update Pictures"))
        {
            *
            int i = 0;
            while (i < myScript.pictures.Count)
            {
                myScript.pictures[i] = myScript.GeneratePicture(myScript.widths[i], myScript.heights[i], myScript.color, myScript.randomisePictures);
                i++;
            }
            *
            myScript.UpdateTexture();
        }
        if (GUILayout.Button("Update Filters"))
        {           
            myScript.UpdateFilters();
        }
    }
}
    */
