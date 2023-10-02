using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringManipulator : MonoBehaviour
{
    public static List<char> alphabet = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',' ' };

    public static int LetterToNumber(char letter)
    {
        int n = 0;

        int i = 0;
        while(i < alphabet.Count)
        {
            if (letter == alphabet[i])
            {
                n = i;
                i = alphabet.Count;
            }
            i++;
        }

        return n;
    }

    public static string NumberToLetter(int number)
    {
        string n = "";

        n = alphabet[number].ToString();

        return n;
    }

    public static string SeparateLetters(string word,char separator)
    {
        string str = "";

        int i = 0;
        while(i < word.Length)
        {
            str += word[i];
            if(i < word.Length - 1)
            {
                str += separator;
            }
            i++;
        }

        return str;
    }

    public static List<string> CutSentences(string sentence)
    {
        List<string> str = new List<string>();

        int i = 0;
        while (i < sentence.Length)
        {
            string str2 = "";
            int o = i; bool stop = false;
            while(!stop)
            {
                str2 += sentence[o];
                o++;
                if(o >= sentence.Length)
                {
                    stop = true;
                }
                else if(sentence[o] == ' ')
                {
                    stop = true;
                }
            }
            str.Add(str2);
            i = o;
            i++;
        }

        return str;
    }

}
