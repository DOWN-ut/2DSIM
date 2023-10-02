using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Tableur_Reader : MonoBehaviour
{
    [Header("Raw Data")]
    public string dataName;
    public List<string> rawData;

    [Header("Conversions")]
    public bool separateLetters; public char separateLettersCutChar;
    public bool separateSentence;
    public bool letterToNumber;

    [Header("Data")]
    public List<string> data;
    public List<float> ante;
    public List<float> ima;

    [Header("Mise en forme")]
    public string MType = "ante,ima"; //"ante,ima" ; "wording"
    public char MCutChar;
    public float power = 1;

    void Start()
    {
        ExploitData();
        Misenforme(data,MType);
    }

    void ExploitData()
    {
        rawData = ReadFile();
        if (separateSentence)
        {
            List<string> temp = new List<string>();
            int i = 0;
            while (i < rawData.Count)
            {
                temp.AddRange(StringManipulator.CutSentences(rawData[i]));
                i++;
            }
            rawData = temp;
        }
        if (separateLetters)
        {
            int i = 0;
            while (i < rawData.Count)
            {
                rawData[i] = StringManipulator.SeparateLetters(rawData[i], separateLettersCutChar);
                i++;
            }
        }
        foreach (string a in rawData)
        {
            data.Add(a);
        }
        if (letterToNumber)
        {
            int i = 0;
            while (i < data.Count)
            {
                string[] line = new string[data[i].Length];
                int o = 0;
                while (o < data[i].Length)
                {
                    if (StringManipulator.alphabet.Contains(data[i][o]))
                    {
                        string a = StringManipulator.LetterToNumber(data[i][o]).ToString();
                        line[o] = a;
                    }
                    else
                    {
                        line[o] = data[i][o].ToString();
                    }
                    o++;
                }
                string str = "";
                foreach (string a in line)
                {
                    str += a;
                }
                data[i] = str;
                i++;
            }
        }
    }

    public string Convert(string from)
    {
        string str = "";

        int i = 0;
        while (i < from.Length)
        {
            if (StringManipulator.alphabet.Contains(from[i]))
            {
                string a = (StringManipulator.LetterToNumber(from[i]) + 10).ToString();
                str += a;
            }
            i++;
        }

        return str;
    }

    public string Translate(float from)
    {
        float from2 = Mathf.Pow(from, 1 / (float)power)/100f;
        string fro = ((int)from2).ToString();
        string to = "";

        int i = 0;
        while(i < fro.Length)
        {
            string str = "";
            int o = i;
            while(o < i + 2 && o < fro.Length)
            {
                str += fro[o];
                o++;
            }
            if (str != "")
            {
                str = (int.Parse(str) - 10).ToString();
                if (int.Parse(str) > 26)
                {
                    str = "26";
                }
                if (int.Parse(str) < 0)
                {
                    str = "0";
                }
                if (letterToNumber)
                {
                    str = StringManipulator.NumberToLetter(int.Parse(str));
                }
            }
            to += str;
            i = o; 
        }

        return to;
    }

    List<string> ReadFile()
    {
        string path = "Assets/Data/FAI/" + dataName;

        StreamReader reader = new StreamReader(path);

        List<string> lines = new List<string>();
        string stl = reader.ReadLine();
        int i = 0;
        while(stl != null && i < 100)
        {
            lines.Add(stl);
            stl = reader.ReadLine();
            i++;
        }

        return lines;
    }

    void Misenforme(List<string> data,string type)
    {
        if (type == "ante,ima")
        {
            int i = 0;
            while (i < data.Count)
            {
                int o = 0;
                string st = "";
                while (data[i][o] != MCutChar)
                {
                    st += data[i][o];
                    o++;
                }
                ante.Add(float.Parse(st));

                o++;
                st = "";
                while (o < data[i].Length)
                {
                    st += data[i][o];
                    o++;
                }
                ima.Add(float.Parse(st));

                i++;
            }
        }
        else if(type == "wording")
        {
            int i = 0;
            while(i < data.Count)
            {
                List<string> newData = new List<string>();
                int m = 0;
                while (m < data[i].Length)
                {
                    string str = "";
                    int ml = m; bool stop = false;
                    while (!stop)
                    {
                        str += data[i][ml];
                        ml++;
                        if (ml >= data[i].Length)
                        {
                            stop = true;
                        }
                        else if (data[i][ml] == MCutChar)
                        {
                            stop = true;
                        }
                    }
                    newData.Add(str);
                    m = ml;
                    m++;
                }
                int o = 0;
                while (o + 1 < newData.Count)
                {
                    string an = "0";
                    string im = "0";
                    int p = 0;
                    while (p <= o)
                    {
                        int val = int.Parse(newData[p])+10;
                        an += val;
                        im += val;
                        p++;
                    }
                    im += (newData[o + 1] + 10).ToString();
                    ante.Add(Mathf.Pow(float.Parse(an),power));
                    ima.Add(Mathf.Pow(float.Parse(im),power));
                    o++;
                }
                i++;
            }
        }
    }
}
