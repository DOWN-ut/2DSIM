using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public bool isPlayer;
    [Header("Initial Life & Life Properties")]

    public int life;

    public int regenTime; //Time before health starts to regen
    public int regenSpeed; //Time between eatch health increasing
    public int regenAmount; //Quantity of health regenerated at eatch times
    int delay0; //Regen delay
    int delay01; //Regen speed delay

    public bool isTrigger = false;

    public bool destroyAtDeath;

    [Header("Ingame")]

    public int actLife;

    public int damager;
    public int specialDamager;
    public int criticalDamager;
    public int localDamager;
    int lastHitDelay;

    [Header("Showing")]

    public TextMesh show;

    public Transform jauge;public float hudSizing = 0.1f; public float hudMaxSize = 3f; public Transform hud; public bool lockJauge = true; public TextMesh lastDamages;
    public float hitedAlpha = 150f;
    float baseAlpha;

    public GameObject damageShower;
    public Color simpleDamagesColor = new Color(1,1,1,0.5f);
    public Color criticalDamagesColor = new Color(1, 0.8f, 1, 0.5f);
    public Color specialDamagesColor = new Color(0.1f, 1, 1, 0.5f);

    public Transform cam;

    // Use this for initialization
    void Start()
    {
        actLife = life;
        cam = GameObject.Find("Player").GetComponent<Controler>().cam;
        if (isPlayer)
        {
            jauge = GetComponent<PlayerClass>().HUD.GetComponent<HUDManager>().healthBar;
        }
        if (jauge != null)
        {
            if (jauge.GetComponent<RectTransform>() != null)
            {
                baseAlpha = jauge.GetChild(0).GetComponent<Image>().color.a;
            }
            else
            {
                baseAlpha = jauge.GetChild(0).GetComponent<SpriteRenderer>().color.a;
            }
        }
        StartCoroutine(Clock());
    }

    IEnumerator Clock()
    {
        if (delay0 > 0)
        {
            delay0--;
        }
        if (delay01 > 0)
        {
            delay01--;
        }
        lastHitDelay++;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Clock());
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(Clock());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
    // Update is called once per frame
    void Update()
    {

        //HUD mamaging

        if (hud != null)
        {
            if (actLife <= 0)
            {
                //hud.gameObject.SetActive(false);
            }
            else
            {
                hud.gameObject.SetActive(true);
                if (lockJauge)
                {
                    hud.LookAt(GameObject.Find("Player").GetComponent<Controler>().cam);
                    hud.localScale = Vector3.one * ((cam.position - transform.position).magnitude + 1) * hudSizing;
                    if(hud.localScale.magnitude > (Vector3.one * hudMaxSize).magnitude )
                    {
                        hud.localScale = Vector3.one * hudMaxSize;
                    }
                }
            }
        }

        //Health properties

        //Regen

        if (delay0 <= 0 && actLife > 0)
        {
            if (actLife < life && delay01 <= 0)
            {
                if (actLife + regenAmount > life)
                {
                    actLife = life;
                }
                else
                {
                    actLife = actLife + regenAmount;
                }
                delay01 = regenSpeed;
            }
        }

        //Death


        if (actLife <= 0)
        {
            if (GetComponent<ObjectInfos>() != null)
            {
                if (!GetComponent<ObjectInfos>().dead)
                {
                    GetComponent<ObjectInfos>().dead = true;
                    if (GetComponent<EntityManager>() != null)
                    {
                        GetComponent<EntityManager>().KinematismBool(false);
                    }
                }
            }
            if (destroyAtDeath)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (GetComponent<ObjectInfos>() != null)
            {
                GetComponent<ObjectInfos>().dead = false;
            }
        }

        //Damages applying

        if (GetComponent<EffectManager>() != null)
        {
            damager = Mathf.RoundToInt(damager * GetComponent<EffectManager>().resistanceBoost);
            specialDamager = Mathf.RoundToInt(specialDamager * GetComponent<EffectManager>().resistanceBoost);
            criticalDamager = Mathf.RoundToInt(criticalDamager * GetComponent<EffectManager>().resistanceBoost);
            localDamager = Mathf.RoundToInt(localDamager * GetComponent<EffectManager>().resistanceBoost);
        }

        int damage = damager + specialDamager + criticalDamager + localDamager;

        if (damageShower != null && actLife > 0 && (localDamager > 0 || specialDamager > 0) )
        {
            GameObject temp = Instantiate(damageShower, hud);
            temp.transform.position = transform.position;
            temp.transform.localRotation = Quaternion.Euler(0, 180, 0);
            if(specialDamager > 0)
            {
                temp.GetComponent<TextMesh>().color = specialDamagesColor;
                temp.GetComponent<TextMesh>().text = specialDamager.ToString();
            }
            else
            {
                temp.GetComponent<TextMesh>().color = simpleDamagesColor;
                temp.GetComponent<TextMesh>().text = localDamager.ToString();
            }
        }
        specialDamager = 0;
        criticalDamager = 0;
        localDamager = 0;
        damager = 0;

        if (GetComponent<Entity_Stats>() != null && actLife > 0)
        {
            GetComponent<Entity_Stats>().Receive(damage);
        }

        if (damage == 0)
        {
            if (lastHitDelay >=1 && jauge != null)
            {
                if (jauge.GetComponent<RectTransform>() != null)
                {
                    jauge.GetChild(0).GetComponent<Image>().color = new Color(jauge.GetChild(0).GetComponent<Image>().color.r, jauge.GetChild(0).GetComponent<Image>().color.g, jauge.GetChild(0).GetComponent<Image>().color.b, baseAlpha);
                }
                else
                {
                    jauge.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(jauge.GetChild(0).GetComponent<SpriteRenderer>().color.r, jauge.GetChild(0).GetComponent<SpriteRenderer>().color.g, jauge.GetChild(0).GetComponent<SpriteRenderer>().color.b, baseAlpha);
                }
            }
        }
        else
        {
            if (damage > 0 || (damage < 0 && actLife < life))
            {
                actLife -= damage;
            }
            delay0 = regenTime;
            lastHitDelay = 0;
            if (jauge != null)
            {
                if (jauge.GetComponent<RectTransform>() != null)
                {
                    jauge.GetChild(0).GetComponent<Image>().color = new Color(jauge.GetChild(0).GetComponent<Image>().color.r, jauge.GetChild(0).GetComponent<Image>().color.g, jauge.GetChild(0).GetComponent<Image>().color.b, hitedAlpha);
                }
                else
                {
                    jauge.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(jauge.GetChild(0).GetComponent<SpriteRenderer>().color.r, jauge.GetChild(0).GetComponent<SpriteRenderer>().color.g, jauge.GetChild(0).GetComponent<SpriteRenderer>().color.b, hitedAlpha);
                }
            }
        }

        if (actLife < 0)
        {
            actLife = 0;
        }

        //Display

        if (show != null)
        {
            show.text = actLife.ToString();
        }

        if (jauge != null)
        {
            float size = actLife / (float)life;
            Vector3 scale = Vector3.MoveTowards(jauge.localScale, new Vector3(size, 1, 1), Time.deltaTime * 3 * Mathf.Pow(Mathf.Abs(jauge.localScale.x - size) + 1, 2));
            if (jauge.GetComponent<RectTransform>() != null)
            {
                jauge.GetComponent<RectTransform>().localScale = scale;
            }
            else
            {
                jauge.localScale = scale;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (isTrigger)
        {
            if (other.GetComponent<Basic_Bullet>() != null)
            {
                int dama = other.GetComponent<Basic_Bullet>().damages;
                if (GetComponent<EffectManager>() != null)
                {
                    dama = (int)(dama * other.GetComponent<EffectManager>().damageBoost);
                }
                damager += dama;
            }
        }
    }

}
