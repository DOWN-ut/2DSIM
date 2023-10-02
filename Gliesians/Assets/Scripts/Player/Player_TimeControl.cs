using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_TimeControl : MonoBehaviour
{
    [Header("Properties")]

    public float slowingFactor;
    public float inEffectSpeed;
    public float outEffectSpeed;

    [Header("Ingame Values")]

    public bool timing;

    public bool exter;

    //[Header("Others")]

    //public PlayerControls controls;

    void Awake()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        timing = false;
    }

    void Timing()
    {
        if ( (!timing && !exter) && Time.timeScale != 1)
        {
            Vector2 mover = Vector2.MoveTowards(new Vector2(Time.timeScale, 0), new Vector2(1, 0), outEffectSpeed);
            Time.timeScale = mover.x;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
        if( (timing || exter) && Time.timeScale != slowingFactor)
        {
            Vector2 mover = Vector2.MoveTowards(new Vector2(Time.timeScale, 0), new Vector2(slowingFactor, 0), inEffectSpeed);
            Time.timeScale = mover.x;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }

    private void Update()
    {
        timing = GetComponent<Controler>().timing;
        Timing();
    }

}
