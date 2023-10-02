using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class Basic_Grenade : MonoBehaviour
{
    [Header("Main Properties")]

    [Tooltip("The shot profile used by the explosion")]
    public Shot_Profile shot_profile;

    [Tooltip("Damages done by the explosion. A negative value will heal the target. If using a shot_profile, this will be a coeficient applied to the damages.")]
    public float damages = 1;
    [Tooltip("The minimum damages done by the explosion, after the max distance. If using a shot_profile, this will be a coeficient applied to the damages.")]
    public float minDamages = 1;

    [Tooltip("The power the distance is raised to for the falloff damages calculation.")]
    public float falloffPow = 2;

    [Tooltip("The blow force of the explosion.")]
    public float blow;

    [Tooltip("The radius of the explosion.")]
    public float rayon;
    [Tooltip("The radius in which the explosion will touch its target even if they are hiden by an obstacle.")]
    public float blastRayon;

    [Tooltip("The throw speed of the grenade.")]
    public float speed;

    [Tooltip("The time before the explosion, in 1/10 of seconds.")]
    public int timing;

    [Tooltip("Will this grenade explode when someone shoots it ?")]
    public bool killable;
    [Tooltip("Will this grenade explode when it hits a shield ?")]
    public bool shieldboomable = true;
    [Tooltip("Will this grenade explode when it hits something ?")]
    public bool explodeOnImpact;
    [Tooltip("The time before the explosion, after the impact (if explodeOnImpact is true).")]
    public int timingAfterImpact;
    [Tooltip("Is this grenade sticky ?")]
    public bool sticky;
    [Tooltip("Is this grenade very not bouncy ?")]
    public bool nobounces;
    [Tooltip("Is this grenade detonable manualy ?")]
    public bool detonable;
    [Tooltip("Does this grenade makes something comes, like a zone with effects ?")]
    public GameObject toSpawn;

    [Tooltip("How this weapon is affected by slowmotion ? 1 for real time, and lower values to increase the rate of fire over time.")]
    public float timePow = 0.5f;

    [Tooltip("The image used to show the area of effect.")]
    public Sprite aimZoneSprite;
    public Color aimZoneColor = new Color(1, 1, 1, 0.1f);
    public Gradient aimColor = new Gradient();

    [Header("Ingame")]

    public bool aiming;
    bool willThrow;

    public bool throwed;
    public bool exploded;

    public int timer;

    [Header("Others")]

    public AbilityManager main;

    [Tooltip("The transform from where the grenade is shooted.")]
    public Transform crosshair;

    public AudioSource audio; public float audioPitchVariation = 0.25f;
    public List<ParticleSystem> explosionParticles;
    public ParticleSystem lifetimeParticles;
    public GameObject visual;

    public Collider[] undesirableColliders;

    public LayerMask explosionLayer;

    public int lineAimResolution = 20;

    void Start()
    {
        if (explosionParticles != null)
        {
            foreach (ParticleSystem a in explosionParticles)
            {
                a.Stop();
            }
        }
        if (lifetimeParticles != null)
        {
            lifetimeParticles.Stop();
        }
        StopAllCoroutines();
        StartCoroutine(Clock());
    }

    private void OnEnable()
    {
        if (explosionParticles != null)
        {
            foreach (ParticleSystem a in explosionParticles)
            {
                a.Stop();
            }
        }
        if (lifetimeParticles != null)
        {
            lifetimeParticles.gameObject.SetActive(true);
            lifetimeParticles.Stop();
        }
        StopAllCoroutines();
        StartCoroutine(Clock());
    }

    private void OnDisable()
    {
        if (explosionParticles != null)
        {
            foreach (ParticleSystem a in explosionParticles)
            {
                a.Stop();
            }
        }
        if (lifetimeParticles != null)
        {
            lifetimeParticles.gameObject.SetActive(false);
        }
        StopAllCoroutines();
    }


    //Clock
    IEnumerator Clock()
    {
        if (throwed)
        {
            timer++;
        }
        yield return new WaitForSecondsRealtime(0.1f * Mathf.Pow(1/(float)Time.timeScale, timePow));
        StartCoroutine(Clock());
    }

    void Update()
    {
        if (GetComponent<AbilityManager>() != null)
        { 
            crosshair = GetComponent<AbilityManager>().crosshair; 
        }

        if (!exploded)
        {
            if (GetComponent<AbilityManager>() != null)
            {
                if (GetComponent<AbilityManager>().main != null && crosshair != null)
                {
                    if (GetComponent<AbilityManager>().main.GetComponent<Controler>().aim)
                    {
                        aiming = true;
                    }
                    else
                    {
                        aiming = false;
                    }
                    if (GetComponent<AbilityManager>().main.GetComponent<Controler>().shooting)
                    {
                        aiming = true;
                        willThrow = true;
                        if (throwed && GetComponent<AbilityManager>().main.actWeapon == GetComponent<AbilityManager>().main.weapons.Count && !GetComponent<AbilityManager>().main.handleAction)
                        {
                            timer = timing;
                        }
                    }
                    else if (willThrow && !throwed)
                    {
                        Throw();
                        GetComponent<AbilityManager>().main.handleAction = false;
                        throwed = true;
                        willThrow = false;
                    }
                    if (aiming && !throwed)
                    {
                        Aim();
                    }
                    else
                    {
                        GetComponent<AbilityManager>().main.animator.SetBool("Throwing", false);
                        GetComponent<AbilityManager>().main.aimLine.gameObject.SetActive(false);
                        GetComponent<AbilityManager>().main.aimLineEnd.gameObject.SetActive(false);
                    }
                }
            }

            if (main != null)
            {
                if (main.pressed && main.pressDuration < 2 && timer > 2)
                {
                    Explode();
                }
            }

            if (timer >= timing)
            {
                Explode();
            }

            if (GetComponent<Health>() != null)
            {
                if (GetComponent<Health>().actLife <= 0 && killable)
                {
                    Explode();
                }
            }

            if (lifetimeParticles != null)
            {
                lifetimeParticles.Play();
            }
            if (explosionParticles != null)
            {
                foreach (ParticleSystem a in explosionParticles)
                {
                    a.Stop();
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Throw()
    {
        GetComponent<AbilityManager>().main.animator.SetBool("Throwed", true);
        GetComponent<Rigidbody>().AddForce(crosshair.forward * speed, ForceMode.VelocityChange);
        GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        GetComponent<AbilityManager>().Throw();
        foreach(Collider a in undesirableColliders)
        {
            a.enabled = false;
        }
    }

    void Aim()
    {
        GetComponent<AbilityManager>().main.animator.SetBool("Throwed", false);
        GetComponent<AbilityManager>().main.animator.speed = 1 / (float)Time.timeScale;
        GetComponent<AbilityManager>().main.animator.SetBool("Throwing", true);

        GetComponent<AbilityManager>().main.aimLine.colorGradient = aimColor;
        GetComponent<AbilityManager>().main.aimLine.gameObject.SetActive(true);
        GetComponent<AbilityManager>().main.aimLine.positionCount = lineAimResolution;
        GetComponent<AbilityManager>().main.aimLine.SetPosition(0, transform.position);
        Vector3[] pos = new Vector3[GetComponent<AbilityManager>().main.aimLine.positionCount]; pos[0] = transform.position;
        int p = 0;
        int i = 1;
        while (i < GetComponent<AbilityManager>().main.aimLine.positionCount)
        {
            float o = i / (float)lineAimResolution;
            float x = speed * o * Mathf.Cos(Vector3.SignedAngle(GetComponent<AbilityManager>().main.transform.forward, crosshair.forward, -transform.right) * Mathf.Deg2Rad);
            float y = (speed * o * Mathf.Sin(Vector3.SignedAngle(GetComponent<AbilityManager>().main.transform.forward, crosshair.forward, -transform.right)) * Mathf.Deg2Rad) - (0.5f * GetComponent<Physicbody>().gravityMagnitude * (o * o));
            pos[i] = transform.position + (crosshair.transform.forward * x) + new Vector3(0, y, 0);
            GetComponent<AbilityManager>().main.aimLine.SetPosition(i, pos[i]);
            Collider[] touch = Physics.OverlapSphere(pos[i], transform.lossyScale.magnitude);
            foreach (Collider b in touch)
            {
                if ((b.transform.position - transform.position).magnitude > 1)
                {
                    p = i - 1;
                }
            }
            i++;
        }
        if (p > 0)
        {
            RaycastHit hit;
            Ray Rayon = new Ray(crosshair.transform.position, pos[p] - crosshair.transform.position);
            if (Physics.Raycast(Rayon, out hit))
            {
                GetComponent<AbilityManager>().main.aimLineEnd.position = hit.point + (hit.normal * 0.01f);
                GetComponent<AbilityManager>().main.aimLineEnd.localScale = Vector3.one * rayon;
                if (hit.collider.gameObject.layer != 25 && hit.collider.gameObject.layer != 12 && hit.collider.gameObject.layer != 11 && hit.collider.gameObject.layer != 14 && hit.collider.gameObject.layer != 25 && hit.collider.gameObject.layer != 5 && hit.collider.gameObject.layer != 13)
                {
                    GetComponent<AbilityManager>().main.aimLineEnd.rotation = Quaternion.LookRotation(hit.normal);
                    GetComponent<AbilityManager>().main.aimLineEnd.GetComponent<SpriteRenderer>().sprite = aimZoneSprite;
                    GetComponent<AbilityManager>().main.aimLineEnd.GetComponent<SpriteRenderer>().color = aimZoneColor;
                    GetComponent<AbilityManager>().main.aimLineEnd.gameObject.SetActive(true);
                }
                else
                {
                    GetComponent<AbilityManager>().main.aimLineEnd.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            GetComponent<AbilityManager>().main.aimLineEnd.gameObject.SetActive(false);
        }
    }

    int Falloff(float distance,float damages,float minDamages)
    {
        int d = (int)damages;

        float r = Mathf.Pow(1+distance, falloffPow);

        d = (int)(d / (float)r);

        if (d < minDamages)
        {
            d = (int)minDamages;
        }
        
        return d;
    }

    public void Explode()
    {
        List<Collider> victims = new List<Collider>();
        victims.AddRange(Physics.OverlapSphere(transform.position, rayon));

        Collider[] sureVictims = Physics.OverlapSphere(transform.position, blastRayon);

        foreach (Collider a in victims.ToArray())
        {
            Vector3 distance = (a.transform.position - transform.position);
            RaycastHit hit;
            Ray Rayon = new Ray(transform.position, distance * 10000);
            if (Physics.Raycast(Rayon, out hit, 10000,explosionLayer))
            {
                if (hit.collider == a)
                {
                    bool b = true;

                    bool damag = false;

                    if (a.GetComponent<ObjectInfos>() != null)
                    {
                        if (a.GetComponent<ObjectInfos>() .team == GetComponent<ObjectInfos>().team)
                        {
                            b = false;
                        }
                        if (shot_profile.damageTargetTeam == 0 && GetComponent<ObjectInfos>().team == a.GetComponent<ObjectInfos>().team)
                        {
                            damag = true;
                        }

                        if (shot_profile.damageTargetTeam == 1 && GetComponent<ObjectInfos>().team != a.GetComponent<ObjectInfos>().team)
                        {
                            damag = true;
                        }
                    }
                    if (b)
                    {
                        if (a.GetComponent<EffectManager>() != null)
                        {
                            a.GetComponent<EffectManager>().ApplyEffects(shot_profile.effects, shot_profile.values, shot_profile.durations, true, shot_profile.stackable, shot_profile.targetTeam, GetComponent<ObjectInfos>().team);
                        }
                        if (a.GetComponent<Rigidbody>() != null)
                        {
                            a.GetComponent<Rigidbody>().AddExplosionForce(blow, transform.position, rayon);
                        }

                        if (shot_profile.damageTargetTeam == -1)
                        {
                            damag = true;
                        }

                        if (a.GetComponent<Health>() != null && damag)
                        {
                            a.GetComponent<Health>().damager += Falloff((a.transform.position-transform.position).magnitude, damages * shot_profile.damages, minDamages * shot_profile.damages);
                        }
                    }
                }
                else
                {
                    victims.Remove(a);
                }
            }
        }
        foreach (Collider a in sureVictims)
        {
            if (!victims.Contains(a))
            {
                bool b = true;

                bool damag = false;
                if (a.GetComponent<ObjectInfos>() != null)
                {
                    if (a.GetComponent<ObjectInfos>().team == GetComponent<ObjectInfos>().team)
                    {
                        b = false;
                    }
                    if (shot_profile.damageTargetTeam == 0 && GetComponent<ObjectInfos>().team == a.GetComponent<ObjectInfos>().team)
                    {
                        damag = true;
                    }

                    if (shot_profile.damageTargetTeam == 1 && GetComponent<ObjectInfos>().team != a.GetComponent<ObjectInfos>().team)
                    {
                        damag = true;
                    }
                }
                if (b)
                {
                    if (a.GetComponent<EffectManager>() != null)
                    {
                        a.GetComponent<EffectManager>().ApplyEffects(shot_profile.effects, shot_profile.values, shot_profile.durations, true);
                    }
                    if (a.GetComponent<Rigidbody>() != null)
                    {
                        a.GetComponent<Rigidbody>().AddExplosionForce(blow, transform.position, rayon);
                    }

                    if (shot_profile.damageTargetTeam == -1)
                    {
                        damag = true;
                    }

                    if (a.GetComponent<Health>() != null && damag)
                    {
                        a.GetComponent<Health>().damager += (int)(damages * shot_profile.damages);
                    }
                }
            }
        }
        audio.pitch *= Random.Range(1-audioPitchVariation, 1+audioPitchVariation);
        audio.Play();

        if (explosionParticles != null)
        {
            foreach (ParticleSystem a in explosionParticles.ToArray())
            {
                a.transform.parent = null;
                if (a.GetComponent<DelayedDestroy>() != null)
                {
                    a.GetComponent<DelayedDestroy>().enabled = true;
                }
                a.gameObject.SetActive(true);
                a.Stop();
                a.Play();
                explosionParticles.Remove(a);
            }
        }
        if (lifetimeParticles != null)
        {
            lifetimeParticles.Stop();
            lifetimeParticles.transform.parent = null;
            if (lifetimeParticles.GetComponent<DelayedDestroy>() != null)
            {
                lifetimeParticles.GetComponent<DelayedDestroy>().enabled = true;
            }
        }

        visual.SetActive(false);
        if (GetComponent<Collider>() != null)
        {
            GetComponent<Collider>().enabled = false;
        }
        GetComponent<Rigidbody>().isKinematic = true;
        if(toSpawn != null)
        {
            GameObject temp = Instantiate(toSpawn, transform.position, Quaternion.Euler(0,0,0), null);
        }
        timing = 0;
        exploded = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (throwed)
        {
            if (explodeOnImpact)
            {
                timing = timingAfterImpact;
                timer = 0;
            } 
            if (sticky)
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                Vector3 pos = transform.position;
                GameObject temp = new GameObject();
                transform.parent = temp.transform;
                temp.transform.parent = collision.transform;
                transform.position = pos;
            }
            if (nobounces)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }
}
