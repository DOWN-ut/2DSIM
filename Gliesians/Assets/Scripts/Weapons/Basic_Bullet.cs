using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Bullet : MonoBehaviour
{
    public Shot_Profile shot_profile;
    public int damages;

    public bool notDamager;

    public Vector3 shoterSpeed;
    public bool keepShooterSpeed;
    public Vector3 speed;
    public float shoterSpeedLoss;//The speed at which the shoter speed is erased while the dispersion is applied
    public float lossStartDistance;//The minimum distance from wihich the dispersion starts to apply

    public float deathDistance = 1000;

    public bool impactCineticPush = true;

    public bool impactKnockbackDirectionnal;
    public Vector3 impactKnocbackDir;
    public float impactKnockback;
    public float impactKnockup;

    public bool keepVelocity = true;
    public bool keepGravity;
    public bool lookWay;
    public bool lookHorizontal;//For humanoids
    public bool onlyEntities = false;

    public bool destroyOnImpact = true;

    public GameObject[] detachOnImpact;

    public GameObject bulletImpact;

    public GameObject shooter;

    public bool autolock;
    public Vector3 target;
    public bool targeted;

    Vector3 startPos;

    private void Start()
    {
        if (shooter != null)
        {
            if (shooter.layer == 9) //If the shooter is the player :
            {
                gameObject.layer = 11; //Set the bullet layer the PlayerProjectile, so they will not hit the player
            }
        }
        if (keepShooterSpeed)
        {
            GetComponent<Rigidbody>().velocity += shoterSpeed;
            speed += shoterSpeed;
        }

        if (notDamager)
        {
            shot_profile = null;
            damages = 0;
        }

        startPos = transform.position;
        //GetComponent<Rigidbody>().velocity = speed + shoterSpeed;
    }

    public void OnDestroy()
    {
        foreach (GameObject a in detachOnImpact)
        {
            a.transform.parent = null;
            if (a.GetComponent<DelayedDestroy>() != null)
            {
                a.GetComponent<DelayedDestroy>().enabled = true;
            }
            if (a.GetComponent<ParticleSystem>() != null)
            {
                a.GetComponent<ParticleSystem>().Stop();
            }
        }
    }

    private void FixedUpdate()
    {
        float distance = (startPos - transform.position).magnitude;

        if (targeted && autolock)
        {
            speed = (target - transform.position).normalized * speed.magnitude;
        }

        if (keepVelocity && distance > lossStartDistance)
        {
            float y = GetComponent<Rigidbody>().velocity.y;
            GetComponent<Rigidbody>().velocity = Vector3.MoveTowards(GetComponent<Rigidbody>().velocity, speed, shoterSpeedLoss * Time.deltaTime);
            if (keepGravity)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, y, GetComponent<Rigidbody>().velocity.z);
            }
        }

        if(distance > deathDistance)
        {
            foreach (GameObject a in detachOnImpact)
            {
                a.transform.parent = null;
                if (a.GetComponent<DelayedDestroy>() != null)
                {
                    a.GetComponent<DelayedDestroy>().enabled = true;
                }
                if (a.GetComponent<ParticleSystem>() != null)
                {
                    a.GetComponent<ParticleSystem>().Stop();
                }
            }
            Destroy(gameObject);
        }

        if (lookWay)
        {
            transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity.normalized,transform.right);
            if (lookHorizontal)
            {
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        int damage = damages;
        bool dest = false;

        bool apply = false;

        if (shot_profile.damageTargetTeam == -1)
        {
            apply = true;
        }

        if (other.gameObject.GetComponent<ObjectInfos>() != null)
        {
            if (shot_profile.damageTargetTeam == 0 && GetComponent<ObjectInfos>().team == other.gameObject.GetComponent<ObjectInfos>().team)
            {
                apply = true;
            }

            if (shot_profile.damageTargetTeam == 1 && GetComponent<ObjectInfos>().team != other.gameObject.GetComponent<ObjectInfos>().team)
            {
                apply = true;
            }
        }

        if (other.gameObject.GetComponent<EffectManager>() != null)
        {
            other.gameObject.GetComponent<EffectManager>().ApplyEffects(shot_profile.effects, shot_profile.values, shot_profile.durations, true, shot_profile.stackable, shot_profile.targetTeam, GetComponent<ObjectInfos>().team);
            damage = Mathf.RoundToInt(damage / (float)other.gameObject.GetComponent<EffectManager>().resistanceBoost);
        }

        if (other.gameObject.GetComponent<Health>() != null)
        {
            if (apply)
            {
                if (other.gameObject.GetComponent<SleepIfPlayersFar>() != null)
                {
                    other.gameObject.GetComponent<SleepIfPlayersFar>().unSleep = 2;
                    other.gameObject.GetComponent<SleepIfPlayersFar>().Switch(true);
                }
                //if (other.gameObject.GetComponent<Health>().isPlayer)
                {
                    other.gameObject.GetComponent<Health>().damager += damage;
                    if (shooter.GetComponent<Entity_Stats>() != null)
                    {
                        shooter.GetComponent<Entity_Stats>().Deal(damage);
                    }
                }
            }
            dest = true;
        }
        else if (other.gameObject.GetComponent<HealthBodyPart>() != null)
        {
            if (other.gameObject.GetComponent<ObjectInfos>() != null)
            {
                if (shot_profile.damageTargetTeam == 0 && GetComponent<ObjectInfos>().team == other.gameObject.GetComponent<HealthBodyPart>().mainHealth.GetComponent<ObjectInfos>().team)
                {
                    apply = true;
                }

                if (shot_profile.damageTargetTeam == 1 && GetComponent<ObjectInfos>().team != other.gameObject.GetComponent<HealthBodyPart>().mainHealth.GetComponent<ObjectInfos>().team)
                {
                    apply = true;
                }
            }
            other.gameObject.GetComponent<HealthBodyPart>().mainHealth.GetComponent<SleepIfPlayersFar>().unSleep = 2;
            other.gameObject.GetComponent<HealthBodyPart>().mainHealth.GetComponent<SleepIfPlayersFar>().Switch(true);
            if (other.gameObject.GetComponent<HealthBodyPart>().mainHealth.GetComponent<EffectManager>() != null)
            {
                other.gameObject.GetComponent<HealthBodyPart>().mainHealth.GetComponent<EffectManager>().ApplyEffects(shot_profile.effects, shot_profile.values, shot_profile.durations, true, shot_profile.stackable, shot_profile.targetTeam, GetComponent<ObjectInfos>().team);
                damage = Mathf.RoundToInt(damage / (float)other.gameObject.GetComponent<HealthBodyPart>().mainHealth.GetComponent<EffectManager>().resistanceBoost);
            }
            List<string> ef = new List<string>(); ef.AddRange(shot_profile.effects);
            if (apply)
            {               
                other.gameObject.GetComponent<HealthBodyPart>().Damage(damage, speed * GetComponent<Rigidbody>().mass, other.GetContact(0).point,null,impactCineticPush,ef.Contains("blow") );
                if (shooter != null)
                {
                    if (shooter.GetComponent<Entity_Stats>() != null)
                    {
                        shooter.GetComponent<Entity_Stats>().Deal(damage);
                    }
                }
            }
            dest = true;
        }
        else
        {
            if (bulletImpact != null)
            {
                GameObject temp = Instantiate(bulletImpact, other.GetContact(0).point, transform.rotation);
                temp.transform.rotation = Quaternion.LookRotation(-other.GetContact(0).normal);
            }
        }

        if (!onlyEntities || dest)
        {
            //Vector3 dir = GetComponent<Rigidbody>().velocity.normalized;
            Vector3 dir = speed.normalized;
            if (impactKnockbackDirectionnal)
            {
                dir = impactKnocbackDir;
            }
            if (other.gameObject.GetComponent<HealthBodyPart>() != null)
            {
                other.gameObject.GetComponent<HealthBodyPart>().mainHealth.GetComponent<Rigidbody>().AddForce((dir * impactKnockback) + (other.gameObject.GetComponent<HealthBodyPart>().mainHealth.transform.up * impactKnockup), ForceMode.Impulse);
            }
            else if (other.gameObject.GetComponent<Rigidbody>() != null)
            {
                other.gameObject.GetComponent<Rigidbody>().AddForce((dir * impactKnockback) + (other.transform.up * impactKnockup), ForceMode.Impulse);
            }
            foreach (GameObject a in detachOnImpact)
            {
                a.transform.parent = null;
                if (a.GetComponent<DelayedDestroy>() != null)
                {
                    a.GetComponent<DelayedDestroy>().enabled = true;
                }
                if (a.GetComponent<ParticleSystem>() != null)
                {
                    a.GetComponent<ParticleSystem>().Stop();
                }
            }
            if (destroyOnImpact)
            {
                Destroy(gameObject);
            }
        }
    }
}
