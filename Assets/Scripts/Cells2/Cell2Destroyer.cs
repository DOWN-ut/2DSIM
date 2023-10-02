using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell2Destroyer : MonoBehaviour {

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Cell2Part>() != null)
        {
            collision.gameObject.GetComponent<Cell2Part>().Damage(1);
        }
    }
}
