using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TextMeshAdaptor : MonoBehaviour {

    public bool resizeText;
    public float textSize;
    public int textInitialLenght;
    public int baseNumOfLine = 1;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        List<string> strl = new List<string>();
        int o = 0;
        while (o < GetComponent<TextMesh>().text.Length)
        {
            string str = "";
            int p = o;
            while (p < GetComponent<TextMesh>().text.Length && GetComponent<TextMesh>().text[p] != '\n' )
            {
                str += GetComponent<TextMesh>().text[p];
                p++;
            }
            strl.Add(str);
            o = p + 2;
        }

        int textLenght = 0;
        foreach (string a in strl)
        {
            if(a.Length > textLenght)
            {
                textLenght = a.Length;
            }
        }
        if (baseNumOfLine < strl.Count)
        {
            textLenght = (int)(textLenght * Mathf.Pow((strl.Count - baseNumOfLine) + 1, 1 / 4f));
        }

        if (resizeText)
        {
            GetComponent<TextMesh>().characterSize = textSize * ((float)textInitialLenght / (float)(textLenght+1));
        }
        else
        {
            textInitialLenght = GetComponent<TextMesh>().text.Length + 1;
            textSize = GetComponent<TextMesh>().characterSize / textInitialLenght;
        }

	}
}
