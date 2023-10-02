using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class PlayerClass : MonoBehaviour
{
    [Header("Parameters")]
    public Class_Handler Class;

    public float lootDistance; 

    [Header("Ingame")]
    public int actWeapon = 3;

    public List<GameObject> weapons;
    public GameObject ability1; 
    public GameObject ability2;
    public GameObject ability3;
    public GameObject ultimate;
    public GameObject passive;
    public GameObject passive2;

    public List<GameObject> inventory;
    public int quickAction;
    public bool handleAction;
    bool actioned;
    bool actionEquip;
    public bool changingWeapon;

    public GameObject HUD;

    public int animatorWaitOneFrame;

    public bool initialise;

    bool interac;

    [Header("References")]
    public Animator animator;
   
    public Transform rightHand;
    public Transform leftHand;

    public LineRenderer aimLine;
    public Transform aimLineEnd; 
     
    public Transform crosshair;

    public Transform weaponManager;

    public Transform HUDManager;

    [Header("Visual Effects")]
    public PostProcessVolume shrinkScreenEffect;
    public PostProcessVolume uncolorScreenEffect; 
    public PostProcessVolume saturScreenEffect;
    public PostProcessVolume flashScreenEffect;

    void Awake()
    {
        GetComponent<Health>().life = Class.life;
        GetComponent<Controler>().speed = Class.speed;
        GetComponent<Controler>().speedWhileJump = Class.midairSpeed;
        GetComponent<Controler>().runing = Class.run;
        GetComponent<Controler>().jump = Class.jump; 
        GetComponent<Controler>().dodge = Class.dodge;

        GetComponent<Physicbody>().gravityMagnitude *= Class.gravity;

        GameObject temp; Transform paren = rightHand;
        if (Class.primaryWeapon != null) 
        {
            if (Class.primaryWeapon.GetComponent<AbilityManager>().abilityCaster == 0)
            {
                paren = leftHand;
            }
            else if (Class.primaryWeapon.GetComponent<AbilityManager>().abilityCaster == 1)
            {  
                paren = rightHand;
            }
            temp = Instantiate(Class.primaryWeapon, paren);
            temp.transform.localPosition = Class.primaryWeapon.GetComponent<AbilityManager>().abilityPosition;
            temp.transform.localRotation = Quaternion.Euler(Class.primaryWeapon.GetComponent<AbilityManager>().abilityRotation);
            temp.GetComponent<AbilityManager>().main = GetComponent<PlayerClass>(); temp.SetActive(false); temp.GetComponent<AbilityManager>().crosshair = crosshair; weapons.Add(temp);
        }
        if (weapons[0].GetComponent<AbilityManager>().doubleWeapon)
        {
            weapons.Add(weapons[0]);
        }
        else if (Class.secondaryWeapon != null)
        {
            if (Class.secondaryWeapon.GetComponent<AbilityManager>().abilityCaster == 0)
            {
                paren = leftHand;
            }
            else if (Class.secondaryWeapon.GetComponent<AbilityManager>().abilityCaster == 1)
            {
                paren = rightHand;
            }
            temp = Instantiate(Class.secondaryWeapon, paren);
            temp.transform.localPosition = Class.secondaryWeapon.GetComponent<AbilityManager>().abilityPosition;
            temp.transform.localRotation = Quaternion.Euler(Class.secondaryWeapon.GetComponent<AbilityManager>().abilityRotation);
            temp.GetComponent<AbilityManager>().main = GetComponent<PlayerClass>(); temp.SetActive(false); temp.GetComponent<AbilityManager>().crosshair = crosshair; weapons.Add(temp);
        }
        if (Class.ability1 != null)
        {
            if (Class.ability1.GetComponent<AbilityManager>().abilityCaster == 0)
            {
                paren = leftHand;
            }
            else if (Class.ability1.GetComponent<AbilityManager>().abilityCaster == 1)
            {
                paren = rightHand;
            }
            ability1 = Instantiate(Class.ability1, paren);
            ability1.transform.localPosition = Class.ability1.GetComponent<AbilityManager>().abilityPosition;
            ability1.transform.localRotation = Quaternion.Euler(Class.ability1.GetComponent<AbilityManager>().abilityRotation);
            ability1.GetComponent<AbilityManager>().main = GetComponent<PlayerClass>(); ability1.GetComponent<AbilityManager>().crosshair = crosshair;
        }
        if (Class.ability2 != null)
        { 
            if (Class.ability2.GetComponent<AbilityManager>().abilityCaster == 0)  
            {
                paren = leftHand; 
            }
            else if (Class.ability2.GetComponent<AbilityManager>().abilityCaster == 1) 
            {
                paren = rightHand;
            }
            ability2 = Instantiate(Class.ability2, paren);
            ability2.transform.localPosition = Class.ability2.GetComponent<AbilityManager>().abilityPosition;
            ability2.transform.localRotation = Quaternion.Euler(Class.ability2.GetComponent<AbilityManager>().abilityRotation);
            ability2.GetComponent<AbilityManager>().main = GetComponent<PlayerClass>(); ability2.GetComponent<AbilityManager>().crosshair = crosshair;
        }
        if (Class.ability3 != null)
        {
            if (Class.ability3.GetComponent<AbilityManager>().abilityCaster == 0)
            {
                paren = leftHand;
            }
            else if (Class.ability3.GetComponent<AbilityManager>().abilityCaster == 1)
            {
                paren = rightHand;
            }
            ability3 = Instantiate(Class.ability3, paren);
            ability3.transform.localPosition = Class.ability3.GetComponent<AbilityManager>().abilityPosition;
            ability3.transform.localRotation = Quaternion.Euler(Class.ability3.GetComponent<AbilityManager>().abilityRotation);
            ability3.GetComponent<AbilityManager>().main = GetComponent<PlayerClass>(); ability3.GetComponent<AbilityManager>().crosshair = crosshair;
        }
        if (Class.ultimate != null)
        {
            if (Class.ultimate.GetComponent<AbilityManager>().abilityCaster == 0)
            {
                paren = leftHand;
            }
            else if (Class.ultimate.GetComponent<AbilityManager>().abilityCaster == 1)
            {
                paren = rightHand;
            }
            ultimate = Instantiate(Class.ultimate, paren);
            ultimate.transform.localPosition = Class.ultimate.GetComponent<AbilityManager>().abilityPosition;
            ultimate.transform.localRotation = Quaternion.Euler(Class.ultimate.GetComponent<AbilityManager>().abilityRotation);
            ultimate.GetComponent<AbilityManager>().main = GetComponent<PlayerClass>(); ultimate.GetComponent<AbilityManager>().crosshair = crosshair;
        }
        if (Class.passive != null)
        {
            if (Class.passive.GetComponent<AbilityManager>().abilityCaster == 0)
            {
                paren = leftHand;
            }
            else if (Class.passive.GetComponent<AbilityManager>().abilityCaster == 1)
            {
                paren = rightHand;
            }
            passive = Instantiate(Class.passive, paren);
            passive.transform.localPosition = Class.passive.GetComponent<AbilityManager>().abilityPosition;
            passive.transform.localRotation = Quaternion.Euler(Class.passive.GetComponent<AbilityManager>().abilityRotation);
            passive.GetComponent<AbilityManager>().main = GetComponent<PlayerClass>(); passive.GetComponent<AbilityManager>().crosshair = crosshair;
        }
        if (Class.passive2 != null)
        {
            if (Class.passive2.GetComponent<AbilityManager>().abilityCaster == 0)
            {
                paren = leftHand;
            }
            else if (Class.passive2.GetComponent<AbilityManager>().abilityCaster == 1)
            {
                paren = rightHand;
            }
            passive2 = Instantiate(Class.passive2, paren);
            passive2.transform.localPosition = Class.passive2.GetComponent<AbilityManager>().abilityPosition;
            passive2.transform.localRotation = Quaternion.Euler(Class.passive2.GetComponent<AbilityManager>().abilityRotation);
            passive2.GetComponent<AbilityManager>().main = GetComponent<PlayerClass>(); passive2.GetComponent<AbilityManager>().crosshair = crosshair;
        }
        if (Class.HUD != null)
        {
            HUD = Instantiate(Class.HUD, HUDManager); HUD.GetComponent<HUDManager>().main = this;
        }
        GetComponent<Health>().jauge = HUD.GetComponent<HUDManager>().healthBar;

        GetComponent<Controler>().equipementIndex = 0;
        //actWeapon = weapons.Count + 1;
    }

    private void Start()
    {
        actWeapon = weapons.Count;
        initialise = true;
        WeaponChange();
    }

    private void Update()
    {
        foreach(GameObject a in inventory)
        {
            if(a == null)
            {
                inventory.Remove(a);
            }
        }

        bool interacted = false;
        if (GetComponent<Controler>().quickActioning)
        {
            if (!interac)
            {
                interacted = Interact();
                interac = interacted;
            }
        }
        else
        {
            interac = false;
        }

        if (inventory.Count > 0)
        {
            if (quickAction >= inventory.Count)
            {
                quickAction = inventory.Count - 1;
            }
            if ( (GetComponent<Controler>().quickActioning || actionEquip) && !interac)
            {
                Action();
            }
            else if (actioned)
            {
                actioned = false;
            }
        }
        if (!GetComponent<Controler>().aim)
        {
            WeaponChange();
            if (GetComponent<Controler>().quickActioning)
            {
                Loot();
            }
        }

        if (GetComponent<Controler>().inventoring)
        {
            HUD.GetComponent<HUDManager>().inventory.SetActive(true);
            GetComponent<Player_TimeControl>().exter = true;
        }
        else
        {
            HUD.GetComponent<HUDManager>().inventory.SetActive(false);
            GetComponent<Player_TimeControl>().exter = false;
        }
        
        if (changingWeapon)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("ChangeWeapon"))
            {
                if (GetComponent<Controler>().equipementIndex < weapons.Count)
                {
                    animator.speed = weapons[GetComponent<Controler>().equipementIndex].GetComponent<AbilityManager>().unsheatheSpeed / (float)Time.timeScale;
                }
                else
                {
                    animator.speed = 2f / (float)Time.timeScale;
                }
            }
            else
            {
                changingWeapon = false;
            }
        }
        
        foreach(GameObject a in weapons.ToArray())
        {
            if(a.GetComponent<AbilityManager>().abilityCaster == 0)
            {
                a.transform.parent = leftHand;
            }
            else if(a.GetComponent<AbilityManager>().abilityCaster == 1)
            {
                a.transform.parent = rightHand;
            }
            a.transform.localRotation = Quaternion.Euler(a.GetComponent<AbilityManager>().abilityRotation);
            a.transform.localPosition = a.GetComponent<AbilityManager>().abilityPosition;
        }

        animatorWaitOneFrame++;
    }

    bool Interact()
    {
        bool inted = false;

        RaycastHit hit;
        Ray Rayon = new Ray(crosshair.transform.position, crosshair.forward);
        if (Physics.Raycast(Rayon, out hit, lootDistance))
        {
            if (hit.collider.gameObject.GetComponent<Button>() != null)
            {
                hit.collider.gameObject.GetComponent<Button>().activated = !hit.collider.gameObject.GetComponent<Button>().activated;
                inted = true;
            }
        }

        return inted;
    }

    void Loot()
    {
        RaycastHit hit;
        Ray Rayon = new Ray(crosshair.transform.position, crosshair.forward);
        if (Physics.Raycast(Rayon, out hit, lootDistance))
        {
            if (hit.collider.GetComponent<AbilityManager>() != null && !weapons.Contains(hit.collider.gameObject))
            {
                if ((inventory.Count < Class.inventorySize && hit.collider.GetComponent<AbilityManager>().abilityNum != 0) || (hit.collider.GetComponent<AbilityManager>().abilityNum == 0))
                {
                    hit.collider.GetComponent<AbilityManager>().Loot();
                    if (hit.collider.GetComponent<AbilityManager>().abilityCaster == 0)
                    { 
                        hit.collider.transform.parent = leftHand;
                    }
                    else if (hit.collider.GetComponent<AbilityManager>().abilityCaster == 1)
                    {
                        hit.collider.transform.parent = rightHand;
                    }
                    hit.collider.transform.localPosition = hit.collider.GetComponent<AbilityManager>().abilityPosition;
                    hit.collider.transform.localRotation = Quaternion.Euler(hit.collider.GetComponent<AbilityManager>().abilityRotation);
                    hit.collider.GetComponent<AbilityManager>().main = this;
                    if (hit.collider.GetComponent<AbilityManager>().abilityNum == 0)  
                    {
                        hit.collider.gameObject.SetActive(true);
                        hit.collider.GetComponent<AbilityManager>().crosshair = crosshair;
                        if (weapons.Count >= 3)
                        {
                            weapons[2].GetComponent<AbilityManager>().Throw();
                            weapons.Remove(weapons[2]);
                        }
                        weapons.Add(hit.collider.gameObject);
                        GetComponent<Controler>().equipementIndex = weapons.Count-1;
                    }
                    else
                    {
                        inventory.Add(hit.collider.gameObject);
                    }
                }
            }
        }
    }

    void Action()
    {
        if (!actioned)
        {
            if (!inventory[quickAction].activeSelf)
            {
                actionEquip = true;
                GetComponent<Controler>().equipementIndex = weapons.Count;
                if (actWeapon == GetComponent<Controler>().equipementIndex)
                {
                    foreach (GameObject a in inventory)
                    {
                        if (a.GetComponent<AbilityManager>().keepActive)
                        {
                            if (a.GetComponent<AbilityManager>().visualMain != null)
                            {
                                a.GetComponent<AbilityManager>().visualMain.SetActive(false);
                            }
                        }
                        else
                        {
                            a.SetActive(false);
                        }
                        a.GetComponent<AbilityManager>().allowed = false;
                    }
                    inventory[quickAction].SetActive(true);
                    if (inventory[quickAction].GetComponent<AbilityManager>().visualMain != null)
                    {
                        inventory[quickAction].GetComponent<AbilityManager>().visualMain.SetActive(true);
                    }
                    inventory[quickAction].GetComponent<AbilityManager>().allowed = true;
                    handleAction = true;
                    actioned = true;
                    actionEquip = false;
                }
            }
        }
    }

    public void WeaponChange()
    {
        if (GetComponent<Controler>().equipementIndex > weapons.Count)
        {
            GetComponent<Controler>().equipementIndex = 0;
        }
        if (GetComponent<Controler>().equipementIndex < 0)
        {
            GetComponent<Controler>().equipementIndex = weapons.Count;
        }
        if (GetComponent<Controler>().equipementIndex != actWeapon || initialise) //GetComponent<Controler>().equipementIndex != actWeapon+=
        {
            GetComponent<Controler>().reloading = false;
            bool ok = true;
            if (ok)
            {
                if (GetComponent<Controler>().equipementIndex < weapons.Count)
                {
                    animator.speed = weapons[GetComponent<Controler>().equipementIndex].GetComponent<AbilityManager>().unsheatheSpeed / (float)Time.timeScale;
                }
                else
                {
                    animator.speed = 2f / (float)Time.timeScale;
                }
                foreach (GameObject a in weapons)
                {
                    a.GetComponent<AbilityManager>().allowed = false;
                }
                changingWeapon = true;
                if(animatorWaitOneFrame > 5)
                {
                    animatorWaitOneFrame = 0;
                }
                if(animatorWaitOneFrame > 0 && animatorWaitOneFrame <= 1)
                {
                    if (GetComponent<Controler>().equipementIndex < weapons.Count && actWeapon < weapons.Count){
                        if (weapons[GetComponent<Controler>().equipementIndex] != weapons[actWeapon])
                        {
                            foreach (AnimatorControllerParameter parameter in animator.parameters)
                            {
                                animator.SetBool(parameter.name, false);
                            }
                        }
                    }
                    else
                    {
                        foreach (AnimatorControllerParameter parameter in animator.parameters)
                        {
                            animator.SetBool(parameter.name, false);
                        }
                    }
                }
                if (animatorWaitOneFrame > 1)
                {
                    if (GetComponent<Controler>().equipementIndex < weapons.Count && actWeapon < weapons.Count)
                    {
                        if (weapons[GetComponent<Controler>().equipementIndex] == weapons[actWeapon])
                        {
                            foreach (AnimatorControllerParameter parameter in animator.parameters)
                            {
                                animator.SetBool(parameter.name, false);
                            }
                        }
                        animator.SetBool(weapons[GetComponent<Controler>().equipementIndex].GetComponent<AbilityManager>().animationType, true);
                    }
                    else if(GetComponent<Controler>().equipementIndex < weapons.Count)
                    {
                        animator.SetBool(weapons[GetComponent<Controler>().equipementIndex].GetComponent<AbilityManager>().animationType, true);
                    }
                    else if (inventory.Count > 0) 
                    {
                        animator.SetBool(inventory[quickAction].GetComponent<AbilityManager>().animationType, true);
                    } 
                    else
                    {
                        //actWeapon = GetComponent<Controler>().equipementIndex;
                    }    
                }
                //else if (animatorWaitOneFrame > 1)
                //{
                //    animator.SetBool("ChangeWeapon", false);
                //}
                if (animatorWaitOneFrame > 2)
                {
                    if (!animator.IsInTransition(0))//animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Idle")        animator.GetCu rrentAnimatorStateInfo(0).normalizedTime > animator.GetCurrentAnimatorClipInfo(0)[0].clip.length / 2f
                    {
                        actWeapon = GetComponent<Controler>().equipementIndex; 
                        foreach (GameObject a in inventory)
                        { 
                            if (a != null)
                            {
                                if (a.GetComponent<AbilityManager>().keepActive)
                                {
                                    if (a.GetComponent<AbilityManager>().visualMain != null)
                                    {
                                        a.GetComponent<AbilityManager>().visualMain.SetActive(false);
                                    }
                                }
                                else
                                {
                                    a.SetActive(false);
                                }
                                a.GetComponent<AbilityManager>().allowed = false;
                            }
                        }
                    }
                }
                if (GetComponent<Controler>().equipementIndex == actWeapon)
                {
                    handleAction = false;
                    int i = 0;
                    while (i <= weapons.Count)
                    {
                        if (i < weapons.Count)
                        {
                            if (i != actWeapon)
                            {
                                if (weapons[i].GetComponent<AbilityManager>().keepActive)
                                {
                                    if (weapons[i].GetComponent<AbilityManager>().visualMain != null)
                                    {
                                        weapons[i].GetComponent<AbilityManager>().visualMain.SetActive(false);
                                    }
                                }
                                else
                                {
                                    weapons[i].SetActive(false);
                                }
                                weapons[i].GetComponent<AbilityManager>().allowed = false;
                            }
                        }
                        i++;
                    }
                    if (actWeapon < weapons.Count)
                    {
                        weapons[actWeapon].SetActive(true);
                        weapons[actWeapon].GetComponent<AbilityManager>().allowed = true;
                        if (weapons[actWeapon].GetComponent<AbilityManager>().visualMain != null)
                        {
                            weapons[actWeapon].GetComponent<AbilityManager>().visualMain.SetActive(true);
                        }
                    }
                    initialise = false;
                }
            }
            else
            {
                actWeapon = GetComponent<Controler>().equipementIndex;
            }
        }
    }
}
/*

[CreateAssetMenu(fileName = "Player_Class", menuName = "Player/Class", order = 1)]
public class Class_Handler : ScriptableObject
{
    public string officialName;
    public string surname;
     
    public int life = 1000;

    public float speed = 6;
    public float run = 1.4f;
    public float midairSpeed = 0.1f;

    public float jump = 10;
    public float dodge = 15;  

    public int numberOfWeapon = 2;
    public int inventorySize = 10;

    public GameObject primaryWeapon;
    public GameObject secondaryWeapon;

    public GameObject ability1;
    public GameObject ability2;
    public GameObject ability3;
    public GameObject ultimate;  
    public GameObject passive;
    public GameObject passive2;

    public GameObject HUD;

    public Sprite icon;

    [Tooltip("0 : Leader / 1: Pathfinder / 2: Spy / 3: Healer / 4: Illusionist / 5: Engineer / 6: Protector / 7: Medic / 8: Savior / 9: Voyager")]
    public int role; //0 : Leader / 1: Pathfinder / 2: Spy / 3: Healer / 4: Illusionist / 5: Engineer / 6: Protector / 7: Medic / 8: Savior / 9: Voyager / 10: 

    public float damageRank; //For 0 to 1 (0% to 100%)
    public float healRank;
    public float protectRank;
    public float sustainRank;
}
*/
