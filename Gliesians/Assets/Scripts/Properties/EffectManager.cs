using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{

    [Header("--- Effects ---")]
    [Header("-General-")]

    [Tooltip("Is this entity blowed ?")]
    public bool blowed;
    [Tooltip("Is this entity freezed ?")]
    public bool freezed;
    [Tooltip("Is this entity speed-boosted ?")]
    public bool speeded;
    [Tooltip("Is this entity burned ?")]
    public bool burned;
    [Tooltip("Is this entity resistance-boosted ?")]
    public bool shielded;
    [Tooltip("Is this entity damages-boosted ?")]
    public bool damaged;
    [Tooltip("Is this entity blind ?")]
    public bool blind;
    [Tooltip("Is this entity wallhacked ?")]
    public bool wallhacked;
    [Tooltip("Is this entity being leurred ?")]
    public bool leurred;

    [Tooltip("How is this entity translucid ?")]
    public float translucidity = 1f;

    [Tooltip("The dealed-damage multiplicator applied to this entity.")]
    public float damageBoost = 1;

    [Tooltip("The received-damage multiplicator applied to this entity.")]
    public float resistanceBoost = 1;

    [Tooltip("The moving speed multiplicator applied to this entity.")]
    public float speedBoost = 1;

    [Tooltip("The mid air speed multiplicator applied to this entity.")]
    public float midairspeedBoost = 1;

    [Tooltip("The moving speed multiplicator applied to this entity.")]
    public float gravity = 1;

    [Header("Ingame")]
    public List<string> actEffects;
    public List<float> actValues;
    public List<int> actDurations;

    public RigidbodyConstraints iniRigidbodyContraints;

    [Header("--- Things ---")]

    [Tooltip("All the visuals things of this object")]
    public GameObject[] visual;
    public int[] thisLayers;
    public GameObject[] visualMeshes;
    public Mesh mesh;
    public GameObject freezedEffect; public bool skinFrozenEffect; public GameObject actFreezedEffect;
    public GameObject boostEffect; public bool skinBoostEffect; public GameObject actBoostEffect;
    public GameObject speedEffect; public bool skinSpeedEffect; public GameObject actSpeedEffect;
    public GameObject blindIcon;
    public GameObject wallhackEffect; public GameObject actWallhackEffect;

    // Use this for initialization
    void Start()
    {
        iniRigidbodyContraints = GetComponent<Rigidbody>().constraints;
        if (visualMeshes[0] != null)
        {
            if (visualMeshes[0].GetComponent<SkinnedMeshRenderer>() != null)
            {
                mesh = visualMeshes[0].GetComponent<SkinnedMeshRenderer>().sharedMesh;
            }
            if (visualMeshes[0].GetComponent<MeshFilter>() != null)
            {
                mesh = visualMeshes[0].GetComponent<MeshFilter>().mesh;
            }
        }
        thisLayers = new int[visual.Length];
        int i = 0;
        while (i < visual.Length)
        {
            thisLayers[i] = visual[i].layer;
            i++;
        }

        StopAllCoroutines();
        StartCoroutine(Clock());
    }

    void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(Clock());
    }
    void OnDisable()
    {
        StopAllCoroutines();
    }

    //Clock
    IEnumerator Clock()
    {
        if (GetComponent<Health>() != null)
        {
            if (GetComponent<Health>().hud != null)
            {
                if (translucidity > 1)
                {
                    GetComponent<Health>().hud.gameObject.SetActive(false);
                }
                else
                {
                    GetComponent<Health>().hud.gameObject.SetActive(true);
                }
            }
        }
        foreach (GameObject a in visualMeshes)
        {
            if (a.GetComponent<MeshRenderer>() != null)
            {
                Material mat = a.GetComponent<MeshRenderer>().material;
                mat.SetColor("_BaseColor", new Color(mat.GetColor("_BaseColor").r, mat.GetColor("_BaseColor").g, mat.GetColor("_BaseColor").b, 1 / (float)translucidity));
                a.GetComponent<MeshRenderer>().material = mat;
            }
            if (a.GetComponent<SkinnedMeshRenderer>() != null)
            {
                Material mat = a.GetComponent<SkinnedMeshRenderer>().material;
                mat.SetColor("_BaseColor", new Color(mat.GetColor("_BaseColor").r, mat.GetColor("_BaseColor").g, mat.GetColor("_BaseColor").b, 1 / (float)translucidity));
                a.GetComponent<SkinnedMeshRenderer>().material = mat;
            }
        }

        int i = 0;
        while (i < actEffects.Count)
        {
            if (actEffects[i] == "fire" && GetComponent<Health>() != null)
            {
                burned = true;
                GetComponent<Health>().specialDamager += (int)actValues[i];
            }
            if (actEffects[i] == "freeze")
            {
                freezed = true;
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            if (actEffects[i] == "speed")
            {
                speeded = true;
            }
            if (actEffects[i] == "leurre")
            {
                leurred = true;
            }
            if (actEffects[i] == "blind")
            {
                blind = true;
                if (GetComponent<EntityManager>() != null)
                {
                    GetComponent<EntityManager>().victim = null;
                    GetComponent<EntityManager>().potentialVictim = null;
                }
                if (blindIcon != null)
                {
                    blindIcon.SetActive(true);
                }
            }
            if (actEffects[i] == "blow")
            {
                if (GetComponent<EntityManager>() != null)
                {
                    GetComponent<EntityManager>().KinematismBool(false);
                    blowed = true;
                }
            }
            if (actEffects[i] == "wallhack")
            {
                if (GetComponent<ObjectInfos>() != null)
                {
                    if (!GetComponent<ObjectInfos>().dead)
                    {
                        wallhacked = true;
                    }
                    else
                    {
                        wallhacked = false;
                    }
                }
                else
                {
                    wallhacked = true;
                }
            }
            actDurations[i]--;
            if (actDurations[i] <= 0)
            {
                if (actEffects[i] == "fire")
                {
                    burned = false;
                }
                if (actEffects[i] == "freeze")
                {
                    freezed = false;
                    GetComponent<Rigidbody>().constraints = iniRigidbodyContraints;
                }
                if (actEffects[i] == "speed")
                {
                    speeded = false;
                }
                if (actEffects[i] == "blind")
                {
                    blind = false;
                    if (blindIcon != null)
                    {
                        blindIcon.SetActive(false);
                    }
                }
                if (actEffects[i] == "wallhack")
                {
                    wallhacked = false;
                }
                if (actEffects[i] == "leurre")
                {
                    leurred = false;
                }
                if (actEffects[i] == "blow")
                {
                    blowed = false;
                }
                List<string> e = new List<string>(); e.Add(actEffects[i]);
                List<float> v = new List<float>(); v.Add(actValues[i]);
                List<int> d = new List<int>(1) { 0 };

                ApplyEffects(e.ToArray(), v.ToArray(), d.ToArray(), false);

                actEffects.RemoveAt(i);
                actValues.RemoveAt(i);
                actDurations.RemoveAt(i);
            }
            i++;
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Clock());
    }

    public void ApplyEffects(string[] effects, float[] values, int[] durations, bool appliorerase, bool[] stackable = null,int[] targetTeam = null,int applierTeam = 0 )//targetTeam : -1 for peu importe , 0 for allies , 1 for enemies
    {
        if(stackable == null)
        {
            stackable = new bool[10] { true, true, true, true, true, true, true, true, true, true };
        }

        if (targetTeam == null)
        {
            targetTeam = new int[10] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        }

        int o = 0;
        while (o < effects.Length)
        {
            bool apply = false;

            if (targetTeam[o] == -1)
            {
                apply = true;
            }

            if (targetTeam[o] == 0 && GetComponent<ObjectInfos>().team == applierTeam)
            {
                apply = true;
            }

            if (targetTeam[o] == 1 && GetComponent<ObjectInfos>().team != applierTeam)
            {
                apply = true;
            }

            float value = values[o];

            if (!appliorerase)
            {
                value = 1 / (float)value;
            }

            if (apply)
            {

                if (effects[o] == "speed")
                {
                    if (!stackable[o] && appliorerase)
                    {
                        value = value / (float)speedBoost;
                        /*
                        if ( (value < 1 && appliorerase) || (value > 1 && !appliorerase) )
                        {
                            value = 1;
                        }
                        */
                    }
                    if (actSpeedEffect == null)
                    {
                        if (speedEffect != null && visualMeshes[0] != null)
                        {
                            GameObject temp = Instantiate(speedEffect, visualMeshes[0].transform.position, transform.rotation, transform);
                            temp.GetComponent<DelayedDestroy>().lifeTime = durations[o] + 5;
                            temp.GetComponent<DelayedDestroy>().actLifeTime = durations[o] + 5;
                            if (skinSpeedEffect)
                            {
                                ParticleSystem ps = temp.GetComponent<ParticleSystem>(); var shape = ps.shape; shape.mesh = mesh;
                                temp.transform.position = visualMeshes[0].transform.position - new Vector3(0, 0.25f, 0);
                                temp.transform.localScale = visualMeshes[0].transform.lossyScale;
                            }
                            else
                            {
                                temp.transform.localScale = transform.lossyScale;
                            }
                            actSpeedEffect = temp;
                        }
                    }
                    else
                    {
                        actSpeedEffect.GetComponent<DelayedDestroy>().actLifeTime += durations[o];
                    }

                    speedBoost *= value;
                }
                if (effects[o] == "midairspeed")
                {
                    if (!stackable[o] && appliorerase)
                    {
                        value = value / (float)midairspeedBoost;
                        /*
                        if ( (value < 1 && appliorerase) || (value > 1 && !appliorerase) )
                        {
                            value = 1;
                        }
                        */
                    }
                    if (actSpeedEffect == null)
                    {
                        if (speedEffect != null && visualMeshes[0] != null)
                        {
                            GameObject temp = Instantiate(speedEffect, visualMeshes[0].transform.position, transform.rotation, transform);
                            temp.GetComponent<DelayedDestroy>().lifeTime = durations[o] + 5;
                            temp.GetComponent<DelayedDestroy>().actLifeTime = durations[o] + 5;
                            if (skinSpeedEffect)
                            {
                                ParticleSystem ps = temp.GetComponent<ParticleSystem>(); var shape = ps.shape; shape.mesh = mesh;
                                temp.transform.position = visualMeshes[0].transform.position - new Vector3(0, 0.25f, 0);
                                temp.transform.localScale = visualMeshes[0].transform.lossyScale;
                            }
                            else
                            {
                                temp.transform.localScale = transform.lossyScale;
                            }
                            actSpeedEffect = temp;
                        }
                    }
                    else
                    {
                        actSpeedEffect.GetComponent<DelayedDestroy>().actLifeTime += durations[o];
                    }

                    midairspeedBoost *= value;
                }
                if (effects[o] == "gravity")
                {
                    if (!stackable[o] && appliorerase)
                    {
                        value = value / (float)gravity;
                        /*
                        if ((value < 1 && appliorerase) || (value > 1 && !appliorerase))
                        {
                            value = 1;
                        }
                        */
                    }

                    gravity *= value;
                }
                if (effects[o] == "translucidity")
                {
                    if (!stackable[o] && appliorerase)
                    {
                        value = value / (float)translucidity;
                        /*
                        if ((value < 1 && appliorerase) || (value > 1 && !appliorerase))
                        {
                            value = 1;
                        }
                        */
                    }

                    translucidity *= value;
                }
                if (effects[o] == "damages")
                {
                    if (!stackable[o] && appliorerase)
                    {
                        value = value / (float)damageBoost;
                        /*
                        if ((value < 1 && appliorerase) || (value > 1 && !appliorerase))
                        {
                            value = 1;
                        }
                        */
                    }

                    damageBoost *= value;

                    if (actBoostEffect == null)
                    {
                        if (boostEffect != null && visualMeshes[0] != null)
                        {
                            GameObject temp = Instantiate(boostEffect, visualMeshes[0].transform.position, transform.rotation, transform);
                            temp.GetComponent<DelayedDestroy>().lifeTime = durations[o] + 5;
                            temp.GetComponent<DelayedDestroy>().actLifeTime = durations[o] + 5;
                            if (skinBoostEffect)
                            {
                                ParticleSystem ps = temp.GetComponent<ParticleSystem>(); var shape = ps.shape; shape.mesh = mesh;
                                temp.transform.position = visualMeshes[0].transform.position - new Vector3(0, 0.25f, 0);
                                temp.transform.localScale = visualMeshes[0].transform.lossyScale;
                            }
                            else
                            {
                                temp.transform.localScale = transform.lossyScale;
                            }
                            actBoostEffect = temp;
                        }
                    }
                    else
                    {
                        actBoostEffect.GetComponent<DelayedDestroy>().actLifeTime += durations[o];
                    }
                }
                if (effects[o] == "blow")
                {
                    if (GetComponent<EntityManager>() != null)
                    {
                        GetComponent<EntityManager>().KinematismBool(false);
                        blowed = appliorerase;
                    }
                }
                if (effects[o] == "freeze")
                {
                    if (actFreezedEffect == null)
                    {
                        if (freezedEffect != null && visualMeshes[0] != null)
                        {
                            GameObject temp = Instantiate(freezedEffect, visualMeshes[0].transform.position, transform.rotation, transform);
                            temp.GetComponent<DelayedDestroy>().lifeTime = durations[o] + 5;
                            temp.GetComponent<DelayedDestroy>().actLifeTime = durations[o] + 5;
                            if (skinFrozenEffect)
                            {
                                ParticleSystem ps = temp.GetComponent<ParticleSystem>(); var shape = ps.shape; shape.mesh = mesh;
                                temp.transform.position = visualMeshes[0].transform.position - new Vector3(0, 0.25f, 0);
                                temp.transform.localScale = visualMeshes[0].transform.lossyScale;
                            }
                            else
                            {
                                temp.transform.localScale = transform.lossyScale;
                            }
                            actFreezedEffect = temp;
                        }
                    }
                    else
                    {
                        actFreezedEffect.GetComponent<DelayedDestroy>().actLifeTime += durations[o];
                    }
                }
                if (effects[o] == "wallhack")
                {
                    if (actWallhackEffect == null)
                    {
                        if (wallhackEffect != null && visualMeshes[0] != null)
                        {
                            GameObject temp = Instantiate(wallhackEffect, visualMeshes[0].transform.position, transform.rotation, transform);
                            temp.GetComponent<DelayedDestroy>().lifeTime = durations[o] + 5;
                            temp.GetComponent<DelayedDestroy>().actLifeTime = durations[o] + 5;
                            temp.transform.localScale = transform.lossyScale;
                            actWallhackEffect = temp;
                        }
                    }
                    else
                    {
                        actWallhackEffect.GetComponent<DelayedDestroy>().actLifeTime += durations[o];
                    }
                }

                if (appliorerase)
                {
                    actEffects.Add(effects[o]);
                    actValues.Add(values[o]);
                    actDurations.Add(durations[o]);
                }
            }

            o++;
        }
    }
}
