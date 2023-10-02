using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Gun : MonoBehaviour
{
    [Header("Main Properties")]

    [Tooltip("The shot profile used by each bullet of each shot.")]
    public Shot_Profile[] shot_profile;

    [Tooltip("The liked improver.")]
    public Basic_Gun_Improver improver;

    [Tooltip("Damages per bullets. So several values will shot several bullets at once. A negative value will heal the target. If using a shot_profile, this will be a coeficient applied to the damages.")]
    public float[] damages = { 1 };
    [Tooltip("The minimum damages per bullets, after the max falloff distance. If using a shot_profile, this will be a coeficient applied to the damages.")]
    public float[] minDamages = { 1 };

    [Tooltip("Max distance. 0 means unlimited range.")]
    public float portee;

    [Tooltip("The travel speed of the bullets. If this is a hitscan shot, the speed will be used as a kick value for the touched target.")]
    public float speed;

    [Tooltip("Dispersion of the shots. A value between 0 and 1")]
    public float dispersion;
    [Tooltip("The quickness with witch the dispersion will be applied to the bullet after being shoted straight")]
    public float dispersionSpeed = 0.5f;
    [Tooltip("The minimal distance from witch the dispersion starts to be applied to the bullet after being shoted straight")]
    public float dispersionDistance = 0;

    [Tooltip("The distance at which the damages start to decrease.")]
    public float falloffStart;
    [Tooltip("The distance at which the damages stop to decrease.")]
    public float falloffEnd;

    [Tooltip("Time between each bullet , in 1/20 of seconds.")]
    public int rate;

    [Tooltip("Time between each shot , in 1/20 of seconds. The duration between the last bullet of a shot and the first of the following shot.")]
    public int cadence;

    [Tooltip("Force the burst, by shooting every bullets of the rate at each press even if the player realese the button earlier.")]
    public bool forceRate = false;

    [Tooltip("Number of bullet in one magazine. Set it to 0 for infinite ammo.")]
    public int ammo;
    public bool useAbilityManagerAmmo = false;
    public int munitionConsommation = 1;

    [Tooltip("Reload duration, in 1/20 of seconds.")]
    public int reload;
    public bool canReload = true;

    [Tooltip("Can you aim with this weapon ?")]
    public bool aimable = true;

    [Tooltip("How this weapon is affected by slowmotion ? 1 for real time, and lower values to increase the rate of fire over time.")]
    public float timePow = 0.5f;
    public float animationTimePow = 0.5f;

    [Tooltip("The zoom while aiming.")]
    public float aimZoom = 1;

    public LayerMask layer;

    [Tooltip("Automatic weapon : 1 ; Other : 0")]
    public int weaponType;
    public Animator weaponAnimator;

    [Header("Ingame")]

    public bool shooting;
    public int shootDuration;
    public bool aim;
    public int aimDuration;

    public GameObject aimed;

    public int actAmmo;

    public GameObject victim;

    public int bulletsCounter;

    public int rateDelay;
    public int cadenceDelay;

    public bool reloading;
    public int reloadDelay;

    public bool infiniteAmmo;

    [Header("Miscelaneous")]

    public string aimAnimationType = "PistolAim";
    public string reloadAnimationType = "PistolReload";
    public float reloadAnimationSpeed = 10;
    public string shootAnimationType = "PistolShoot";
    public float shootAnimationSpeed = 10;
    public AudioSource audiosource;

    float audioIniPitch;

    public Sprite icon;

    public LineRenderer laser;

    public GameObject[] shotEffects;

    public ParticleSystem[] improverLoadEffects;
    public AudioSource improverLoadingSound;
    public bool stopImproverLoadingSoundWhenLoaded;
    public AudioSource improverActiveSound;
    bool improverActiveSoundPlayed;
    public bool stopImproverLoadEffectWhenLoaded = true;
    public ParticleSystem[] improverLoadedEffects;

    [Header("Others")]

    [Tooltip("The transform from where the raycasts are shooted.")]
    public Transform crosshair;

    [Tooltip("If it is a physical bullet (not a hitscan).")]
    public GameObject bulletPrefab;
    public Transform gunCannon;
    public Transform[] gunCannons;

    //public PlayerControls controls;

    public Transform shooter;

    [Header("Debug")]

    public bool printVictim;

    public bool printAim;

    private void Start()
    {
        actAmmo = ammo;
        audioIniPitch = audiosource.pitch;
        if (GetComponent<AbilityManager>().main == null)
        {
            Initialization();
        }
        if (ammo < 0)
        {
            infiniteAmmo = true;
        }
        if (portee <= 0)
        {
            portee = 100000;
        }
        StopAllCoroutines();
        StartCoroutine(Clock());
    }

    void OnEnable()
    {
        if (GetComponent<AbilityManager>().main == null)
        {
            Initialization();
        }
        StopAllCoroutines();
        StartCoroutine(Clock());
    }
    void OnDisable()
    {
        if (GetComponent<AbilityManager>().main != null)
        {
            GetComponent<AbilityManager>().main.animator.SetBool(reloadAnimationType, false);
        }
        StopAllCoroutines();
    }

    void Initialization()
    {
        if (crosshair == null && GetComponent<AbilityManager>() != null)
        {
            crosshair = GetComponent<AbilityManager>().crosshair;
        }
        if (GetComponent<AbilityManager>().main != null)
        {
            shooter = GetComponent<AbilityManager>().main.transform;
        }
        if (GetComponent<AbilityManager>().entityManager != null)
        {
            shooter = GetComponent<AbilityManager>().entityManager.transform;
        }
        if (portee <= 0)
        {
            portee = 100000;
        }
        cadenceDelay = cadence;
        if (ammo < 0)
        {
            infiniteAmmo = true;
        }
        StopAllCoroutines();
        StartCoroutine(Clock());
    }

    //Clock
    IEnumerator Clock()
    {
        rateDelay++;
        cadenceDelay++;
        reloadDelay++;

        if(actAmmo > 0 && cadenceDelay >= cadence && rateDelay >= rate)
        {
            if (aim)
            {
                aimDuration++;
            }

            if (shooting)
            {
                shootDuration++;
            }
        }
        else
        {
            shootDuration = 0;
            aimDuration = 0;
            GetComponent<AbilityManager>().pressDuration = 0;
        }

        yield return new WaitForSeconds(0.05f * Mathf.Pow(Time.timeScale, timePow));
        StartCoroutine(Clock());
    }


    void Update()
    {
        if (useAbilityManagerAmmo)
        {
            actAmmo = GetComponent<AbilityManager>().actMunitions;
        }

        if (GetComponent<AbilityManager>() != null)
        {
            crosshair = GetComponent<AbilityManager>().crosshair;
        }

        if (weaponAnimator != null)
        {

        }

        if (improver != null && actAmmo > 0)
        {
            if (cadenceDelay >= cadence)
            {
                if (rateDelay >= rate)
                {
                    if (improver.type == 1 && aim)
                    {
                        if (aimDuration >= improver.durationMin)
                        {
                            if (improverActiveSound != null)
                            {
                                if (!improverActiveSoundPlayed)
                                {
                                    improverActiveSound.Play();
                                    improverActiveSoundPlayed = true;
                                }
                            }
                            if (stopImproverLoadEffectWhenLoaded)
                            {
                                foreach (ParticleSystem a in improverLoadEffects)
                                {
                                    a.Stop();
                                }
                            }
                            foreach (ParticleSystem a in improverLoadedEffects)
                            {
                                if (!a.isPlaying)
                                {
                                    a.Play();
                                }
                            }
                            if (improverLoadingSound != null && stopImproverLoadingSoundWhenLoaded)
                            {
                                improverLoadingSound.Stop();
                            }
                        }
                        else if(aimDuration == 0)
                        {
                            if (improverLoadingSound != null)
                            {
                                improverLoadingSound.Stop();
                            }
                        }
                        else
                        {
                            foreach (ParticleSystem a in improverLoadEffects)
                            {
                                if (!a.isPlaying)
                                {
                                    a.Play();
                                }
                            }
                            if (improverLoadingSound != null)
                            {
                                if (!improverLoadingSound.isPlaying)
                                {
                                    improverLoadingSound.Play();
                                }
                            }
                            improverActiveSoundPlayed = false;
                        }
                    }
                    else if (improver.type == 2 && shooting)
                    {
                        if (shootDuration >= improver.durationMin)
                        {
                            if (improverActiveSound != null)
                            {
                                if (!improverActiveSoundPlayed)
                                {
                                    improverActiveSound.Play();
                                    improverActiveSoundPlayed = true;
                                }
                            }
                            if (stopImproverLoadEffectWhenLoaded)
                            {
                                foreach (ParticleSystem a in improverLoadEffects)
                                {
                                    a.Stop();
                                }
                            }
                            foreach (ParticleSystem a in improverLoadedEffects)
                            {
                                if (!a.isPlaying)
                                {
                                    a.Play();
                                }
                            }
                            if (improverLoadingSound != null && stopImproverLoadingSoundWhenLoaded)
                            {
                                improverLoadingSound.Stop();
                            }
                        }
                        else if (shootDuration == 0)
                        {
                            if (improverLoadingSound != null)
                            {
                                improverLoadingSound.Stop();
                            }
                        }
                        else
                        {
                            foreach (ParticleSystem a in improverLoadEffects)
                            {
                                if (!a.isPlaying)
                                {
                                    a.Play();
                                }
                            }
                            if (improverLoadingSound != null)
                            {
                                if (!improverLoadingSound.isPlaying)
                                {
                                    improverLoadingSound.Play();
                                }
                            }
                            improverActiveSoundPlayed = false;
                        }
                    }
                    else if (improver.type == 3 && GetComponent<AbilityManager>().pressed)
                    {
                        if (GetComponent<AbilityManager>().pressDuration >= improver.durationMin)
                        {
                            if (improverActiveSound != null)
                            {
                                if (!improverActiveSoundPlayed)
                                {
                                    improverActiveSound.Play();
                                    improverActiveSoundPlayed = true;
                                }
                            }
                            if (stopImproverLoadEffectWhenLoaded)
                            {
                                foreach (ParticleSystem a in improverLoadEffects)
                                {
                                    a.Stop();
                                }
                            }
                            foreach (ParticleSystem a in improverLoadedEffects)
                            {
                                if (!a.isPlaying)
                                {
                                    a.Play();
                                }
                            }
                            if (improverLoadingSound != null && stopImproverLoadingSoundWhenLoaded)
                            {
                                improverLoadingSound.Stop();
                            }
                        }
                        else if (GetComponent<AbilityManager>().pressDuration == 0)
                        {
                            if (improverLoadingSound != null)
                            {
                                improverLoadingSound.Stop();
                            }
                        }
                        else
                        {
                            foreach (ParticleSystem a in improverLoadEffects)
                            {
                                if (!a.isPlaying)
                                {
                                    a.Play();
                                }
                            }
                            if (improverLoadingSound != null)
                            {
                                if (!improverLoadingSound.isPlaying)
                                {
                                    improverLoadingSound.Play();
                                }
                            }
                            improverActiveSoundPlayed = false;
                        }
                    }
                }
                else
                {
                    foreach (ParticleSystem a in improverLoadEffects)
                    {
                        a.Stop();
                    }
                    foreach (ParticleSystem a in improverLoadedEffects)
                    {
                        a.Stop();
                    }
                    if (improverLoadingSound != null)
                    {
                        improverLoadingSound.Stop();
                    }
                }
            }
            else
            {
                foreach (ParticleSystem a in improverLoadEffects)
                {
                    a.Stop();
                }
                foreach (ParticleSystem a in improverLoadedEffects)
                {
                    a.Stop();
                }
                if (improverLoadingSound != null)
                {
                    improverLoadingSound.Stop();
                }
            }
        }
        else
        {
            foreach (ParticleSystem a in improverLoadEffects)
            {
                a.Stop();
            }
            foreach (ParticleSystem a in improverLoadedEffects)
            {
                a.Stop();
            }
            if (improverLoadingSound != null)
            {
                improverLoadingSound.Stop();
            }
        }

        if (audiosource != null)
        {
            audiosource.pitch = audioIniPitch * Mathf.Pow(Time.timeScale, timePow);
        }

        if (GetComponent<AbilityManager>().abilityType != 1)
        {
            shooting = GetComponent<AbilityManager>().pressed;
        }

        if (GetComponent<AbilityManager>().main != null)
        {
            if (GetComponent<AbilityManager>().main.GetComponent<Controler>().canAim && !reloading && aimable)
            {
                aim = GetComponent<AbilityManager>().main.GetComponent<Controler>().aim;
            }
            else
            {
                aim = false;
            }
            if (actAmmo > 0 && !reloading && GetComponent<AbilityManager>().main.GetComponent<Controler>().canReload && !infiniteAmmo && canReload)
            {
                reloading = GetComponent<AbilityManager>().main.GetComponent<Controler>().reloading;
                reloadDelay = 0;
            }
            if (GetComponent<AbilityManager>().main.animator != null)
            {
                if (GetComponent<AbilityManager>().main != null && GetComponent<AbilityManager>().allowed)
                {
                    if (reloading)
                    {
                        GetComponent<AbilityManager>().main.animator.speed = reloadAnimationSpeed / (float)(reload * Mathf.Pow(Time.timeScale, animationTimePow));
                    }
                    GetComponent<AbilityManager>().main.animator.SetFloat("Time", (Mathf.Pow(Time.timeScale, timePow)) / ((reload + 1) * 0.1f));
                    GetComponent<AbilityManager>().main.animator.SetBool(aimAnimationType, aim);
                    GetComponent<AbilityManager>().main.animator.SetBool(reloadAnimationType, reloading);
                    if (weaponType == 1)
                    {
                        //GetComponent<AbilityManager>().main.animator.SetBool("Shoot0", shooting);
                    }
                }
            }

            shooter = GetComponent<AbilityManager>().main.transform;
        }
        else if (GetComponent<AbilityManager>().entityManager != null)
        {
            shooter = GetComponent<AbilityManager>().entityManager.transform;
        }

        if (actAmmo <= 0 && !reloading && !infiniteAmmo && canReload)
        {
            reloading = true;
            reloadDelay = 0;
        }
        if (reloading && !infiniteAmmo)
        {
            if (useAbilityManagerAmmo)
            {
                GetComponent<AbilityManager>().actMunitions = 0;
            }
            actAmmo = 0;
            if (reloadDelay >= reload)
            {
                reloading = false;
                if (GetComponent<AbilityManager>().main != null)
                {
                    GetComponent<AbilityManager>().main.GetComponent<Controler>().reloading = false;
                }
                if (useAbilityManagerAmmo)
                {
                    GetComponent<AbilityManager>().actMunitions = GetComponent<AbilityManager>().munitions;
                }
                else
                {
                    actAmmo = ammo;
                }
            }
        }

        if (aimZoom > 1)
        {
            GetComponent<AbilityManager>().aimZoom = aimZoom;
        }
    }

    void FixedUpdate()
    {
        victim = null;

        Shoot(false);

    }

    public void Shoot(bool abilityTypeIs1)
    {
        if ( ( (shooting || (forceRate && bulletsCounter >0 ))  && (actAmmo > 0 || infiniteAmmo) && GetComponent<AbilityManager>().allowed && !reloading) || abilityTypeIs1)
        {
            if (cadenceDelay >= cadence)
            {
                if (rateDelay >= rate)
                {
                    if (rate == 0)
                    {
                        bulletsCounter = 0;
                    }
                    int i = 0;
                    while ((i < shot_profile.Length && ((rate == 0) || (i < 1))))
                    {
                        Vector3 shotDir = transform.forward;
                        Vector3 shotDisp = transform.forward;

                        if (crosshair != null)
                        {
                            shotDir = crosshair.forward;
                        }
                        if (gunCannon == null)
                        {
                            shotDir = (crosshair.transform.forward).normalized;
                            shotDisp = (shotDir + (crosshair.transform.right * Random.Range(-dispersion, dispersion)) + (crosshair.transform.up * Random.Range(-dispersion, dispersion))).normalized;
                            Debug.DrawRay(crosshair.transform.position, shotDir * portee, Color.red);
                            if (laser != null)
                            {
                                laser.SetPosition(0, crosshair.position);
                            }

                            if (GetComponent<Basic_Interactor>() != null)
                            {
                                GetComponent<Basic_Interactor>().Interact(crosshair.position, shotDir, portee);
                            }

                        }
                        else
                        {
                            Transform t = transform;
                            if (crosshair != null)
                            {
                                t = crosshair;
                            }
                            RaycastHit hit;
                            Ray Rayon = new Ray(t.position, t.forward * 10000);
                            if (Physics.Raycast(Rayon, out hit, portee, layer))
                            {
                                shotDir = (hit.point - gunCannon.position).normalized;
                                aimed = hit.collider.gameObject;
                                if (printAim)
                                {
                                    print(hit.collider.name);
                                }
                            }
                            else
                            {
                                shotDir = t.forward;
                            }
                            shotDisp = (shotDir + (t.transform.right * Random.Range(-dispersion, dispersion)) + (t.transform.up * Random.Range(-dispersion, dispersion)));
                            Debug.DrawRay(gunCannon.transform.position, shotDir * portee, Color.green);
                            if (laser != null)
                            {
                                laser.SetPosition(0, gunCannon.position);
                            }


                            if (GetComponent<Basic_Interactor>() != null)
                            {
                                GetComponent<Basic_Interactor>().Interact(gunCannon.position, shotDir, portee);
                            }

                        }

                        if (bulletPrefab == null)
                        {
                            RaycastHit hit;
                            Ray Rayon = new Ray(gunCannon.position, shotDir * 10000);
                            if (Physics.Raycast(Rayon, out hit, portee, layer))
                            {
                                victim = hit.collider.gameObject;
                                if (printVictim)
                                {
                                    print(victim.name);
                                }
                                if (laser != null)
                                {
                                    laser.gameObject.SetActive(true);
                                    laser.SetPosition(1, hit.point);
                                }

                                if (victim.GetComponent<Health>() != null || victim.GetComponent<HealthBodyPart>() != null)
                                {
                                    int damage = 0;

                                    if (victim.GetComponent<Health>() != null)
                                    {
                                        if (victim.GetComponent<SleepIfPlayersFar>() != null)
                                        {
                                            victim.GetComponent<SleepIfPlayersFar>().unSleep = 2;
                                            victim.GetComponent<SleepIfPlayersFar>().Switch(true);
                                        }
                                    }
                                    if (victim.GetComponent<HealthBodyPart>() != null)
                                    {
                                        victim.GetComponent<HealthBodyPart>().mainHealth.GetComponent<SleepIfPlayersFar>().unSleep = 2;
                                        victim.GetComponent<HealthBodyPart>().mainHealth.GetComponent<SleepIfPlayersFar>().Switch(true);
                                    }

                                    if (shot_profile != null && improver != null)
                                    {
                                        damage = Falloff(damages[bulletsCounter] * shot_profile[bulletsCounter].damages * improver.damages, minDamages[bulletsCounter] * shot_profile[bulletsCounter].damages * improver.damages, transform.position, victim.transform.position, falloffStart, falloffEnd);
                                    }
                                    else if (shot_profile != null)
                                    {
                                        damage = Falloff(damages[bulletsCounter] * shot_profile[bulletsCounter].damages, minDamages[bulletsCounter] * shot_profile[bulletsCounter].damages, transform.position, victim.transform.position, falloffStart, falloffEnd);
                                    }
                                    else
                                    {
                                        damage = Falloff(damages[bulletsCounter], minDamages[bulletsCounter], transform.position, victim.transform.position, falloffStart, falloffEnd);
                                    }

                                    if (shooter.GetComponent<EffectManager>() != null)
                                    {
                                        damage = Mathf.RoundToInt(damage * shooter.GetComponent<EffectManager>().damageBoost);
                                    }
                                    if (victim.GetComponent<EffectManager>() != null)
                                    {
                                        if (improver == null)
                                        {
                                            victim.GetComponent<EffectManager>().ApplyEffects(shot_profile[bulletsCounter].effects, shot_profile[bulletsCounter].values, shot_profile[bulletsCounter].durations, true);
                                        }
                                        else
                                        {
                                            List<string> e = new List<string>(); e.AddRange(shot_profile[bulletsCounter].effects); e.AddRange(improver.effects);
                                            List<float> v = new List<float>(); v.AddRange(shot_profile[bulletsCounter].values); v.AddRange(improver.values);
                                            List<int> d = new List<int>(); d.AddRange(shot_profile[bulletsCounter].durations); d.AddRange(improver.durations);
                                            victim.GetComponent<EffectManager>().ApplyEffects(e.ToArray(), v.ToArray(), d.ToArray(), true);
                                        }
                                    }
                                    else if (victim.GetComponent<HealthBodyPart>() != null)
                                    {
                                        if (victim.GetComponent<HealthBodyPart>().mainHealth.GetComponent<EffectManager>() != null)
                                        {
                                            {
                                                if (improver == null)
                                                {
                                                    victim.GetComponent<HealthBodyPart>().mainHealth.GetComponent<EffectManager>().ApplyEffects(shot_profile[bulletsCounter].effects, shot_profile[bulletsCounter].values, shot_profile[bulletsCounter].durations, true);
                                                }
                                                else
                                                {
                                                    List<string> e = new List<string>(); e.AddRange(shot_profile[bulletsCounter].effects); e.AddRange(improver.effects);
                                                    List<float> v = new List<float>(); v.AddRange(shot_profile[bulletsCounter].values); v.AddRange(improver.values);
                                                    List<int> d = new List<int>(); d.AddRange(shot_profile[bulletsCounter].durations); d.AddRange(improver.durations);
                                                    victim.GetComponent<HealthBodyPart>().mainHealth.GetComponent<EffectManager>().ApplyEffects(e.ToArray(), v.ToArray(), d.ToArray(), true);
                                                }
                                            }
                                        }
                                    }

                                    /*
                                    if (victim.GetComponent<Health>() != null)
                                    {
                                        victim.GetComponent<Health>().damager += damage;
                                    }
                                    */
                                    if (victim.GetComponent<HealthBodyPart>() != null)
                                    {
                                        victim.GetComponent<HealthBodyPart>().Damage(damage, speed * shotDir, hit.point);
                                    }

                                    if (shooter.GetComponent<Entity_Stats>() != null)
                                    {
                                        shooter.GetComponent<Entity_Stats>().Deal(damage);
                                    }

                                }
                            }
                            else
                            {
                                if (laser != null)
                                {
                                    laser.gameObject.SetActive(false);
                                }
                            }
                        }
                        else
                        {
                            Transform canon = gunCannon;
                            if(gunCannons.Length > 0)
                            {
                                canon = gunCannons[bulletsCounter];
                            }
                            GameObject bullet = Instantiate(bulletPrefab, canon.position, crosshair.transform.rotation);
                            bullet.GetComponent<Rigidbody>().velocity = (shotDir * speed);
                            float boost = 1;
                            if (shooter.GetComponent<EffectManager>() != null)
                            {
                                boost = shooter.GetComponent<EffectManager>().damageBoost;
                            }
                            if (bullet.GetComponent<Basic_Bullet>() != null)
                            {
                                bullet.GetComponent<Basic_Bullet>().shooter = shooter.gameObject;
                                bullet.GetComponent<Basic_Bullet>().shoterSpeed = shooter.GetComponent<Rigidbody>().velocity;
                                bullet.GetComponent<Basic_Bullet>().speed = (shotDisp * speed);

                                if (shooter.gameObject.layer == 9) //If the shooter is the player :
                                {
                                    bullet.layer = 11; //Set the bullet layer the PlayerProjectile, so they will not hit the player
                                }
                                if (aimed != null)
                                {
                                    if (aimed.GetComponent<Rigidbody>() != null || aimed.GetComponent<EntityManager>() != null || aimed.GetComponent<HealthBodyPart>() != null)
                                    {
                                        bullet.GetComponent<Basic_Bullet>().target = aimed.transform.position;
                                        bullet.GetComponent<Basic_Bullet>().targeted = true;
                                    }
                                }

                                bullet.GetComponent<Basic_Bullet>().shoterSpeedLoss = dispersionSpeed;
                                bullet.GetComponent<Basic_Bullet>().lossStartDistance = dispersionDistance;
                                bullet.GetComponent<Basic_Bullet>().deathDistance = portee;
                                bullet.GetComponent<Basic_Bullet>().impactKnocbackDir = shotDisp.normalized;

                                bullet.GetComponent<Basic_Bullet>().shot_profile = shot_profile[bulletsCounter];

                                bool imp = true; float coef = 1; bool eff = false;
                                if (improver == null)
                                {
                                    imp = false;
                                }
                                else
                                {
                                    if (improver.type == 1 && aimDuration < improver.durationMin) //TYPE 1
                                    {
                                        imp = false;
                                    }
                                    else
                                    {
                                        eff = true;
                                        coef = ((aimDuration - improver.durationMin) / (float)(improver.durationMax - improver.durationMin));
                                    }

                                    if (improver.type == 2 && shootDuration < improver.durationMin) //TYPE 2
                                    {
                                        imp = false;
                                    }
                                    else
                                    {
                                        eff = true;
                                        coef = ((shootDuration - improver.durationMin) / (float)(improver.durationMax - improver.durationMin));
                                    }

                                    if (improver.type == 3 && GetComponent<AbilityManager>().pressDuration < improver.durationMin) //TYPE 3
                                    {
                                        imp = false;
                                    }
                                    else
                                    {
                                        eff = true;
                                        coef = ((GetComponent<AbilityManager>().pressDuration - improver.durationMin) / (float)(improver.durationMax - improver.durationMin));
                                    }  
                                }

                                if (coef > 1)
                                {
                                    coef = 1;
                                }
                                if (coef < 0)
                                {
                                    coef = 0;
                                }

                                if (imp)
                                {
                                    Shot_Profile s = new Shot_Profile(); s.damages = (int)(shot_profile[bulletsCounter].damages * (1 + ( improver.damages * coef) ));

                                    List<string> e = new List<string>(); e.AddRange(shot_profile[bulletsCounter].effects); if (eff) { e.AddRange(improver.effects); }
                                    List<float> v = new List<float>(); v.AddRange(shot_profile[bulletsCounter].values); if (eff) { v.AddRange(improver.values); }
                                    List<int> d = new List<int>(); d.AddRange(shot_profile[bulletsCounter].durations); if (eff) { d.AddRange(improver.durations); }

                                    s.effects = e.ToArray(); s.values = v.ToArray(); s.durations = d.ToArray();

                                    bullet.GetComponent<Basic_Bullet>().shot_profile = s;

                                    bullet.GetComponent<Basic_Bullet>().impactKnockback *= 1 + ( improver.inerty * coef);
                                    bullet.GetComponent<Basic_Bullet>().impactKnockup *=1+( improver.inerty * coef);

                                    if (improver.autolock)
                                    {
                                        bullet.GetComponent<Basic_Bullet>().autolock = true;
                                    }

                                    if (improver.explosivity && bullet.GetComponent<Basic_Grenade>()!=null)
                                    {
                                        bullet.GetComponent<Basic_Grenade>().enabled = !bullet.GetComponent<Basic_Grenade>().enabled;
                                        if (bullet.GetComponent<Basic_Grenade>().enabled)
                                        {
                                            bullet.GetComponent<Basic_Bullet>().destroyOnImpact = false;
                                        }
                                    }
                                }

                                bullet.GetComponent<Basic_Bullet>().damages = Mathf.RoundToInt(damages[bulletsCounter] * boost * bullet.GetComponent<Basic_Bullet>().shot_profile.damages);
                            }

                            if (bullet.GetComponent<Basic_Grenade>() != null)
                            {
                                bullet.GetComponent<Basic_Grenade>().throwed = true;
                            }

                            if (bullet.GetComponent<ObjectInfos>() != null)
                            {
                                bullet.GetComponent<ObjectInfos>().team = GetComponent<ObjectInfos>().team;
                            }

                            if (GetComponent<AbilityManager>().main == null) //If the shooter is not a player, set the bullet mass very low to avoid the player to be pushed by ennemies bullets (that's annoying)
                            {
                                bullet.GetComponent<Rigidbody>().mass = 0.0001f;
                            }

                        }

                        if (rate == 0)
                        {
                            bulletsCounter++;
                        }

                        i++;

                    }

                    rateDelay = 0;

                    bulletsCounter++;

                    if (audiosource != null)
                    {
                        if (!audiosource.isPlaying)
                        {
                            audiosource.Play();
                        }
                    }

                    foreach (GameObject a in shotEffects)
                    {
                        GameObject t = Instantiate(a, gunCannon.position, Quaternion.LookRotation(crosshair.transform.forward));
                    }

                    if (GetComponent<AbilityManager>().main != null && weaponType == 0)
                    {
                        GetComponent<AbilityManager>().main.animator.speed = shootAnimationSpeed / (float)(cadence * Mathf.Pow(Time.timeScale, animationTimePow));
                        GetComponent<AbilityManager>().main.animator.SetTrigger(shootAnimationType);
                    }

                    if (bulletsCounter >= damages.Length)
                    {
                        cadenceDelay = 0;
                        bulletsCounter = 0;
                    }

                    if (useAbilityManagerAmmo)
                    {
                        GetComponent<AbilityManager>().actMunitions -= munitionConsommation;
                    }
                    else
                    {
                        actAmmo -= munitionConsommation;
                    }
                }
            }
            else
            {
                if (laser != null)
                {
                    laser.gameObject.SetActive(false);
                }
            }

        }
    }


    public static int Falloff(float damages, float minDamages, Vector3 position, Vector3 targetPos, float start, float end)
    {
        Vector3 dist = targetPos - position;

        float damage = damages;

        if (dist.magnitude > start && dist.magnitude < end)
        {
            float dam = damages - minDamages;
            damage = damages - Mathf.RoundToInt(dam * ((dist.magnitude - start) / (float)(end - start)));
        }
        if (dist.magnitude > end)
        {
            damage = minDamages;
        }

        return Mathf.RoundToInt(damage);
    }
}
