using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell2Eater : MonoBehaviour
{
    public Cells2Genetics cent;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Cell2_food>() != null)
        {
            int nam = 0;
            while (collision.gameObject.GetComponent<Cell2_food>().food != cent.partNames[nam])
            {
                nam++;
            }
            cent.GetComponent<Cell2Reprod>().food[nam]++;
            Destroy(collision.gameObject);
        }
    }
}
