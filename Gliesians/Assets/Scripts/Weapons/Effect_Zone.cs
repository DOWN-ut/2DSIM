using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class Effect_Zone : MonoBehaviour
{
    public float radius;
    public float height;
    public float growingSpeed;
    public float shrinkingSpeed;
    [Tooltip("The lifetime fraction at witch it starts to shrink.")]
    public float lifeTimeFraction;

    public bool keepRotation;

    public string[] effects;
    public float[] values;
    public int[] durations;

    public float upwardForce;

    public float actRadius;
    public Collider[] victims;
    int[] delays;

    Quaternion startRotation;

    public float effectAplyingFrequence = 0.1f;

    public SpriteRenderer zoneVisual;
    public Sprite zoneVisualSprite;
    public Color zoneVisualColor;
    public GameObject particles;

    private void Start()
    {
        delays = new int[durations.Length];
        startRotation = transform.rotation;
        StartCoroutine(Clock());
    }

    private void OnEnable()
    {
        if (zoneVisual != null)
        {
            zoneVisual.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        if (zoneVisual != null)
        {
            zoneVisual.gameObject.SetActive(false);
        }
    }

    //Clock
    IEnumerator Clock()
    {
        int i = 0;
        while (i < delays.Length)
        {
            delays[i]++;
            if(delays[i] > durations[i])
            {
                foreach(Collider a in victims)
                {
                    if(a != null)
                    {
                        if(a.GetComponent<EffectManager>()!= null)
                        {
                            string[] effect = new string[1] { effects[i] }; float[] value = new float[1] { values[i] }; int[] duration = new int[1] { durations[i] };
                            a.GetComponent<EffectManager>().ApplyEffects(effect, value, duration, true);
                        }
                    }
                }
            }
            i++;
        }
        yield return new WaitForSecondsRealtime(effectAplyingFrequence);
        StartCoroutine(Clock());
    }

    private void FixedUpdate()
    {
        if (keepRotation)
        {
            transform.rotation = startRotation;
        }

        Vector2 r = new Vector2(actRadius, 0);
        bool shrinking = false;
        if (GetComponent<DelayedDestroy>()!= null)
        {
            if(GetComponent<DelayedDestroy>().actLifeTime < GetComponent<DelayedDestroy>().lifeTime * (lifeTimeFraction))
            {
                r = Vector2.MoveTowards(r, new Vector2(0, 0), shrinkingSpeed);
                shrinking = true;
            }
        }
        if (actRadius < radius && !shrinking)
        {
            r = Vector2.MoveTowards(r, new Vector2(radius,0), growingSpeed);
        }
        actRadius = r.x;
        if (actRadius > radius)
        {
            actRadius = radius;
        }

        if(particles != null)
        {
            particles.transform.localScale = new Vector3(actRadius*4, height, actRadius*4);
            //particles.GetComponent<VisualEffect>().SetFloat("Spawn_circle_radius", actRadius*1);
        }
        if(zoneVisual != null)
        {
            zoneVisual.sprite = zoneVisualSprite;
            zoneVisual.color = zoneVisualColor;
            zoneVisual.transform.localScale = new Vector3(actRadius*2, actRadius*2,1);
        }

        victims = Physics.OverlapCapsule(transform.position,transform.position + (transform.up*height), actRadius);
        int i = 0;
        while(i < victims.Length)
        {
            if(victims[i].transform.position.y - transform.position.y > height+1)
            {
                victims[i] = null;
            }
            if (victims[i] != null)
            {
                if (victims[i].GetComponent<Rigidbody>() != null)
                {
                    victims[i].GetComponent<Rigidbody>().AddForce(transform.forward * upwardForce, ForceMode.Force);
                }
            }
            i++;
        }
    }

}
