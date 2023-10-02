using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell2_food : MonoBehaviour {

    public char food = 'B';
    public TextMesh txt;

    public void Start()
    {
        txt.text = food.ToString();
    }
}
