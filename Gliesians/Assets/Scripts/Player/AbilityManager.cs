using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [Header("Values")]

    [Tooltip("The name of this ability,weapon, or anything. For the inventory stacking.")]
    public string prenom;
    [Tooltip("For weapons. The speed of unsheathing.")]
    public float unsheatheSpeed = 2;
    [Tooltip("For weapons. Is this weapon used for the two main weapons ?")]
    public bool doubleWeapon;
    [Tooltip("For weapons, the moving speed coefficient while aiming.")]
    public float aimSpeed = 1;
    [Tooltip("For weapons, zoom coefficient while aiming. For abilities, zoom effect during casting.")]
    public float aimZoom = 1;
    [Tooltip("For abilities. The type of use, 0 for on press, 1 for on release or after a press duration, 2 for use while pressing. Set -1 if this is not an ability")]
    public int abilityType = 0;
    [Tooltip("For abilities, 1 for first, 2 for second,5 for third (yeah that's weird), 3 for ultimate, 4 for passive. Set it to 0 if this is a weapon, -1 if it's a usable object, life a grenade.")]
    public int abilityNum = 1;
    [Tooltip("For passive abilities, the key to use. Set -1 if it is not usable. 0 for jump key.")]
    public int passiveKey = -1;
    [Tooltip("The duration between the pressing of the button and the actual start of the ability.")]
    public int castDelay;
    [Tooltip("For abilities, min time between each uses.")]
    public int betweenUsesTime = 5;
    [Tooltip("For abilities, its cooldown.")]
    public int cooldown;
    [Tooltip("For abilities, use it to make the cooldown depend on the way the ability is used.")]
    public int minCooldown;
    [Tooltip("For abilities, its munitions.")]
    public int munitions;
    public bool dontusemunitions;
    [Tooltip("For type 1 abilities, the duration after it casts even if the button is not released. Keep to 0 to disable")]
    public int maxPressDuration;
    [Tooltip("For abilities. Do this ability behave like an ultimate ; do all the munitions need to be used before the ability ends ?")]
    public bool ultimate;
    [Tooltip("For abilities, mostly ultimates, the abilities that become unvaiable during this ability. 0, 1 and 2 for the weapons, 3 and 4 for the abilities, 5 for the passive.")]
    public bool[] blockedAbilities = new bool[6];
    [Tooltip("For ultimate abilities, if needed, the ability it will steal the key to be used. 0 for weapon shot , 1 and 2 for abilities.")]
    public int stolenKey ;
    [Tooltip("For ultimate abilities, if needed, its duration.")]
    public int duration;
    [Tooltip("The part of the body that cast this ability. 0 for leftHand, 1 for rightHand.")]
    public int abilityCaster;
    [Tooltip("The position of this ability, relative to its parent (abilityCaster)")]
    public Vector3 abilityPosition;
    [Tooltip("The rotation of this ability, relative to its parent (abilityCaster)")]
    public Vector3 abilityRotation;
    [Tooltip("Align the ability to the view ?")]
    public bool abilityLooksView;
    [Tooltip("The animation style of this ability, mostly for weapons.")]
    public string animationType = "PistolMode"; //SniperMode ;
    [Tooltip("The animation to cast during the ability.")]
    public string animationName = ""; //ArmsRun
    [Tooltip("The speed of the animation to cast during the ability.")]
    public float animationSpeed = 1;
    public bool keepActive = false;
    [Tooltip("Linked health manager, for shield for example.")]
    public Health linkedHealth;

    [Header("Ingame")]
    public bool allowed = true;

    [Tooltip("For abilities with variable cooldown, the cooldown it will use")]
    public int usedCooldown;
    public int actCooldown;
    public int actMunitions;
    public int actDuration;
    public int betweenUsesDelay;

    public bool preCast;
    public bool cast;
    public bool pressed; bool released;
    public int pressDuration;
    public int preCastDelay;
    [Tooltip("For ultimate abilities, is the ability being used ?")]
    public bool casting;

    [Tooltip("Is this in use ? For example, is the grapple shoted and fixed ?")]
    public bool used;

    public PlayerClass main;
    public EntityManager entityManager;

    public Transform crosshair;

    public Collider[] colliders;

    bool did;

    int castD;
    int castRemoveDelay;
    bool visualeffected;

    int animationDelay;

    bool blockeddone;

    [Header("Miscelaneous")]
    public GameObject visualMain;

    [Tooltip("The names of this ability,weapon, or anything. In each languages.")]
    public string[] surnames = new string[15];
    [Tooltip("The descriptions of this ability,weapon, or anything. In each languages.")]
    public string[] descriptions = new string[15];

    public Sprite icon;
    public Sprite secondIcon;
    public Color iconColor = Color.white;

    public Animation[] animationsToStart;
    public Animation[] animationsToLoop;

    public Transform jauge;

    public ParticleSystem[] particleToPlay;

    [Tooltip("The played when button pressed")]
    public AudioSource useSound;
    [Tooltip("The played when ability starts")]
    public AudioSource actSound;

    public GameObject[] munitionsVisualCounter;

    [Header("Visual Effect")]
    public string[] screenEffectsNames;
    public float[] screenEffectsInSpeeds;
    public float[] screenEffectsOutSpeeds;
    public float[] screenEffectsIntensities;
    public int screenEffectsDuration = 5;
    int screeEffectsDelay;

    [Header("References")]

    public MonoBehaviour primaryMode;
    public MonoBehaviour secondaryMode;

    void Start()
    {
        actMunitions = munitions;
        actCooldown = cooldown;
        usedCooldown = cooldown;
        if (main != null)
        {
            crosshair = main.crosshair;
        }
        else if (entityManager != null)
        {
            crosshair = entityManager.crosshair;
        }
        foreach(Animation a in animationsToStart)
        {
            a.Play();
        }
        StopAllCoroutines();
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

    IEnumerator Clock()
    {
        if ( (cast && castD > 2) || casting)
        {
            if (minCooldown > 0 && pressDuration > 0 && maxPressDuration > 0 && pressDuration < maxPressDuration)
            {
                usedCooldown = minCooldown + (int)((float)(cooldown - minCooldown) * (float)(pressDuration / (float)maxPressDuration));
            }
            else
            {
                usedCooldown = cooldown;
            }
            if (casting)
            {
                actCooldown = 0;
            }
        }

        if ((actMunitions < munitions && (!cast) ) && (!ultimate || (ultimate && ((actMunitions <= 0 && abilityType != 2) || (abilityType == 2 && !casting)))))
        {
            if (actCooldown < usedCooldown)
            {
                actCooldown++;
                if (actCooldown >= usedCooldown)
                {
                    if (!ultimate)
                    {
                        actMunitions++;
                        if (actMunitions < munitions)
                        {
                            actCooldown = 0;
                        }
                    }
                    else
                    {
                        if (abilityType != 2)
                        {
                            actMunitions = munitions;
                        }
                        else
                        {
                            actMunitions++;
                            if (actMunitions < munitions)
                            {
                                actCooldown = 0;
                            }
                        }
                    }
                }
            }
            else
            {
                actCooldown = 0;
            }
        }

        if (pressed)
        {
            pressDuration++;
        }
        else
        {
            pressDuration = 0;
        }

        if (ultimate)
        {
            if (actDuration > 0)
            {
                if (!pressed && abilityType == 2)
                {
                    if (casting)
                    {
                        casting = false;
                        actDuration = 0;
                    }
                } 
                actDuration--;
            }
            else
            {
                if (casting)
                {
                    casting = false;
                    actMunitions = 0;
                }
            }
        }

        animationDelay++;
        preCastDelay++;
        screeEffectsDelay++;
        betweenUsesDelay++;

        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Clock());
    }

    void GetKeys()
    {
        if (main != null)
        {
            if (abilityNum == 0)
            {
                pressed = main.GetComponent<Controler>().shooting;
            }
            if ((abilityNum == 1 && (!casting||abilityType==2)) || (casting && stolenKey == 1 && abilityType != 2))
            {
                pressed = main.GetComponent<Controler>().ability1;
            }
            if ((abilityNum == 2 && (!casting || abilityType == 2)) || (casting && stolenKey == 2&& abilityType != 2))
            {
                pressed = main.GetComponent<Controler>().ability2;
            }
            if (abilityNum == 3 && (!casting || abilityType == 2))
            {
                pressed = main.GetComponent<Controler>().ability3;
            }
            if (abilityNum == 5 && (!casting || abilityType == 2))
            {
                pressed = main.GetComponent<Controler>().ability4;
            }
            if (abilityNum == 4)
            {
                if (passiveKey == 0)
                {
                    if (abilityType != 2)
                    {
                        if (released && main.GetComponent<Controler>().jump2ing)
                        {
                            pressed = main.GetComponent<Controler>().jump2ing;
                            released = false;
                        }
                        else if (!main.GetComponent<Controler>().jump2ing)
                        {
                            released = false;
                        }
                        else
                        {
                            pressed = false;
                        }
                        if (main.GetComponent<Controler>().isJumping && !main.GetComponent<Controler>().jump2ing)
                        {
                            released = true;
                        }
                    }
                    else
                    {
                        pressed = main.GetComponent<Controler>().jump2ing;
                    }
                }
            }
            if (casting && stolenKey == 0)
            {
                pressed = main.GetComponent<Controler>().shooting;
            }
        }
        else if (entityManager != null)
        {
            //pressed = entityManager.GetComponent<EntityManager>().attack;
        }
        else
        {
            pressed = false;
        }
    }

    private void Update()
    {
        if (main != null)
        {
            crosshair = main.GetComponent<Controler>().crosshair;
            GetComponent<ObjectInfos>().team = main.GetComponent<ObjectInfos>().team;
        }
        else if (entityManager != null)
        {
            crosshair = entityManager.crosshair;
            GetComponent<ObjectInfos>().team = entityManager.GetComponent<ObjectInfos>().team;
        }

        if (jauge != null)
        {
            float size = actMunitions / (float)munitions;
            size += (actCooldown / (float)cooldown) / (float)munitions;
            if(actMunitions >= munitions)
            {
                size = 1;
            } 
            jauge.localScale = new Vector3(size, jauge.localScale.y, jauge.localScale.z);
        }

        foreach(GameObject a in munitionsVisualCounter)
        {
            a.SetActive(false);
        }
            int l = 0;
        while (l < actMunitions && l < munitionsVisualCounter.Length)
        {
            munitionsVisualCounter[l].SetActive(true);
            l++;
        }

        if (abilityLooksView)
        {
            transform.rotation = Quaternion.LookRotation(main.crosshair.forward);
        }

        foreach (Animation a in animationsToLoop)
        {
            if (!a.isPlaying)
            {
                a.Play();
            }
        }

        if (abilityType != -1)
        {

            GetKeys();

            if (allowed)
            {
                if (cast && castRemoveDelay > 1)
                {
                    if (abilityType != 2)
                    {
                        if (GetComponent<Basic_Gun>() == null)
                        {
                            main.GetComponent<Controler>().zoomCoef /= aimZoom;
                        }
                        cast = false;
                        did = true;
                    }
                    else
                    {
                        if (pressDuration >= maxPressDuration || !pressed)
                        {
                            if (GetComponent<Basic_Gun>() == null)
                            {
                                main.GetComponent<Controler>().zoomCoef /= aimZoom;
                            }
                            cast = false;
                            did = true;
                        }
                    }
                }

                if ( (actMunitions > 0||dontusemunitions) && !did && !cast && !preCast && betweenUsesDelay >= betweenUsesTime)
                {
                    if (((abilityType == 0 || abilityType == 2) && pressed) || (abilityType == 1 && pressDuration > 0 && !pressed))
                    {
                        if (GetComponent<Basic_Gun>() == null)
                        {
                            actMunitions--;
                            if (!ultimate || (ultimate && casting && abilityType != 2))
                            {
                                preCast = true;
                                preCastDelay = 0;
                                main.GetComponent<Controler>().zoomCoef *= aimZoom;
                                /*
                                if (screenShrinkSize < 1)
                                {
                                    GetComponent<AbilityManager>().main.HUD.GetComponent<HUDManager>().ScreenEffects(true, screenShrinkSize, "shrink", screenShrinkingInSpeed, screenShrinkingOutSpeed);
                                }
                                */
                                castD = 0;
                                castRemoveDelay = 0;
                            }
                            if (ultimate && !casting)
                            {
                                casting = true;
                                actDuration = duration;
                            }
                            usedCooldown = cooldown;
                        }
                        else if(GetComponent<Basic_Gun>().actAmmo > 0)
                        {
                            if(abilityType == 1)
                            {
                                GetComponent<Basic_Gun>().Shoot(true);
                            }
                            else
                            {
                                cast = true;
                            }
                        }
                        betweenUsesDelay = 0;
                    }
                }

                if(preCast && preCastDelay >= castDelay)
                {
                    preCast = false;
                    cast = true;
                    if (useSound != null)
                    {
                        useSound.Play();
                    }
                }

                if (casting || cast || preCast)
                {
                    if (!visualeffected)
                    {
                        int i = 0;
                        while (i < screenEffectsNames.Length)
                        {
                            main.HUD.GetComponent<HUDManager>().ScreenEffects(screenEffectsIntensities[i], screenEffectsNames[i], screenEffectsInSpeeds[i]);
                            i++;
                        }
                        if (animationName != "")
                        {
                            foreach (AnimatorControllerParameter parameter in main.animator.parameters)
                            {
                                main.animator.SetBool(parameter.name, false);
                            }
                            main.animator.SetBool(animationName, true);
                            main.animator.speed = animationSpeed;
                        }
                        visualeffected = true;
                        animationDelay = 0;
                    }
                    if (cast || casting)
                    {
                        if (actSound != null)
                        {
                            if (!actSound.isPlaying)
                            {
                                actSound.Play();
                            }
                        }
                        foreach (ParticleSystem a in particleToPlay)
                        {
                            if (!a.isPlaying)
                            {
                                a.Play();
                            }
                        }
                    }
                }
                else if (visualeffected)
                { 
                    if(animationDelay > 3)
                    {
                        if (animationName != "" && main != null)
                        {
                            foreach (AnimatorControllerParameter parameter in main.animator.parameters)
                            {
                                main.animator.SetBool(parameter.name, false);
                            }
                            if (main.actWeapon < main.weapons.Count)
                            {
                                main.animator.SetBool(main.weapons[main.actWeapon].GetComponent<AbilityManager>().animationType, true);
                            }
                            visualeffected = false;
                        }
                    }
                    if (screeEffectsDelay > screenEffectsDuration)
                    {
                        int i = 0;
                        while (i < screenEffectsNames.Length)
                        {
                            main.HUD.GetComponent<HUDManager>().ScreenEffects(0, screenEffectsNames[i], screenEffectsOutSpeeds[i]);
                            i++;
                        }
                    }
                }

                if (!pressed)
                {
                    did = false;
                }

                //if (ultimate)
                //{
                if ((actMunitions != 0 && casting) || used)
                {
                    if (blockedAbilities[0] == true && GetComponent<AbilityManager>().main.weapons.Count > 0)
                    {
                        GetComponent<AbilityManager>().main.weapons[0].GetComponent<AbilityManager>().allowed = false;
                    }
                    if (blockedAbilities[1] == true && GetComponent<AbilityManager>().main.weapons.Count > 1)
                    {
                        GetComponent<AbilityManager>().main.weapons[1].GetComponent<AbilityManager>().allowed = false;
                    }
                    if (blockedAbilities[2] == true && GetComponent<AbilityManager>().main.weapons.Count > 2)
                    {
                        GetComponent<AbilityManager>().main.weapons[2].GetComponent<AbilityManager>().allowed = false;
                    }
                    if (blockedAbilities[3] == true && GetComponent<AbilityManager>().main.ability1 != null)
                    {
                        GetComponent<AbilityManager>().main.ability1.GetComponent<AbilityManager>().allowed = false;
                    }
                    if (blockedAbilities[4] == true && GetComponent<AbilityManager>().main.ability2 != null)
                    {
                        GetComponent<AbilityManager>().main.ability2.GetComponent<AbilityManager>().allowed = false;
                    }
                    if (blockedAbilities[5] == true && GetComponent<AbilityManager>().main.passive != null)
                    {
                        GetComponent<AbilityManager>().main.passive.GetComponent<AbilityManager>().allowed = false;
                    }
                    blockeddone = true;
                }
                else if(blockeddone)
                {
                    //casting = false;
                    if (blockedAbilities[0] == true && GetComponent<AbilityManager>().main.weapons.Count > 0)
                    {
                        GetComponent<AbilityManager>().main.weapons[0].GetComponent<AbilityManager>().allowed = true;
                    }
                    if (blockedAbilities[1] == true && GetComponent<AbilityManager>().main.weapons.Count > 1)
                    {
                        GetComponent<AbilityManager>().main.weapons[1].GetComponent<AbilityManager>().allowed = true;
                    }
                    if (blockedAbilities[2] == true && GetComponent<AbilityManager>().main.weapons.Count > 2)
                    {
                        GetComponent<AbilityManager>().main.weapons[2].GetComponent<AbilityManager>().allowed = true;
                    }
                    if (blockedAbilities[3] == true && GetComponent<AbilityManager>().main.ability1 != null)
                    {
                        GetComponent<AbilityManager>().main.ability1.GetComponent<AbilityManager>().allowed = true;
                    }
                    if (blockedAbilities[4] == true && GetComponent<AbilityManager>().main.ability2 != null)
                    {
                        GetComponent<AbilityManager>().main.ability2.GetComponent<AbilityManager>().allowed = true;
                    }
                    if (blockedAbilities[5] == true && GetComponent<AbilityManager>().main.passive != null)
                    {
                        GetComponent<AbilityManager>().main.passive.GetComponent<AbilityManager>().allowed = true;
                    }
                    blockeddone = false;
                }
                //}
            }
            else
            {
                pressed = false;
                pressDuration = 0;
                cast = false;
                did = false;
            }

            castD++;
            castRemoveDelay++;
        }

        if (doubleWeapon)
        {
            if (main.actWeapon == 0)
            {
                primaryMode.enabled = true;
                secondaryMode.enabled = false;
            }
            else if (main.actWeapon == 1)
            {
                primaryMode.enabled = false;
                secondaryMode.enabled = true;
            }
            else
            {
                primaryMode.enabled = false;
                secondaryMode.enabled = false;
            }
            if (GetComponent<Basic_Gun>() != null)
            {
                bool b = false;
                if (main.actWeapon == 1)
                {
                    b = true;
                }
                GetComponent<Basic_Gun>().weaponAnimator.SetBool("Deployed", b);
                GetComponent<Basic_Gun>().weaponAnimator.speed = 1 / (float)Time.timeScale;
            }
        }
    }

    public void Throw(bool delayedDestroy = true)
    {
        if(main != null)
        {
            main.inventory.Remove(gameObject);
        }
        entityManager = null;
        foreach (Collider a in colliders)
        {
            a.enabled = true;
        }
        transform.parent = null;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        if(GetComponent<DelayedDestroy>()!= null && delayedDestroy) 
        {
            GetComponent<DelayedDestroy>().actLifeTime = GetComponent<DelayedDestroy>().lifeTime;
            GetComponent<DelayedDestroy>().enabled = true;
        }
        if (GetComponent<Basic_Deployable>() != null)
        {
            if (GetComponent<Basic_Deployable>().toDeploy.GetComponent<Collider>() != null)
            {
                GetComponent<Basic_Deployable>().toDeploy.GetComponent<Collider>().enabled = false;
            }
        }
    }

    public void Loot()
    {
        main = null;
        entityManager = null;
        foreach (Collider a in colliders)
        {
            a.enabled = false; 
        }
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll; 
        gameObject.SetActive(false);
        if (GetComponent<DelayedDestroy>() != null)
        {
            GetComponent<DelayedDestroy>().actLifeTime = GetComponent<DelayedDestroy>().lifeTime;
            GetComponent<DelayedDestroy>().enabled = false;
        }
        if (GetComponent<Basic_Deployable>() != null)
        {
            if (GetComponent<Basic_Deployable>().toDeploy.GetComponent<Collider>() != null)
            {
                GetComponent<Basic_Deployable>().toDeploy.GetComponent<Collider>().enabled = true;
            }
        }
    }
}
/*                if (GetComponent<AbilityManager>().main.GetComponent<Controler>().ability1 && abilityNum == 1)
                {
                    GetComponent<AbilityManager>().main.GetComponent<Controler>().ability1 = false;
                }
                if (GetComponent<AbilityManager>().main.GetComponent<Controler>().ability2 && abilityNum == 2)
                {
                    GetComponent<AbilityManager>().main.GetComponent<Controler>().ability2 = false;
                }
                if (GetComponent<AbilityManager>().main.GetComponent<Controler>().ability3 && abilityNum == 3)
                {
                    GetComponent<AbilityManager>().main.GetComponent<Controler>().ability3 = false;
                }

    */