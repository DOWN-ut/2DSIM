using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public PlayerClass main;

    public Transform healthBar;

    public Image speedIcon;
    public Image freezeIcon;
    public Image burnIcon;
    public Image damagesIcon;
    public Image shieldIcon;
    public Image invisibleIcon;

    public Image primaryWeaponIcon; public Text primaryWeaponAmmo;
    public Image secondaryWeaponIcon; public Text secondaryWeaponAmmo;
    public Image ability1Icon; public Text ability1Ammo; public Text ability1Cooldown; public GameObject[] ability1AmmoJauge;
    public Image ability2Icon; public Text ability2Ammo; public Text ability2Cooldown; public GameObject[] ability2AmmoJauge;
    public Image ability3Icon; public Text ability3Ammo; public Text ability3Cooldown; public GameObject[] ability3AmmoJauge;
    public Image ultimateIcon; public Text ultimateAmmo; public Text ultimateCooldown; public Image ultimateFond;
    public Image passiveIcon; public Text passiveAmmo; public Text passiveCooldown;
    public Image passive2Icon; public Text passive2Ammo; public Text passive2Cooldown;

    public GameObject inventory;
    public List<Image> inventorytItems;

    public Image foundWeaponIcon; public Text foundWeaponAmmo;

    public Color ultimateFondColor;

    List<string> screenEffects = new List<string>(0);
    List<float> screenEffectsSpeeds = new List<float>(0);
    List<float> screenEffectsGoals = new List<float>(0);

    void Start()
    {
        ultimateFondColor = ultimateFond.color;
    }

    public void ScreenEffects(float goal, string effectName = "shrink", float speed = 0.05f, int index = 0, bool continuous = false)
    {
        if (!screenEffects.Contains(effectName))
        {
            screenEffects.Add(effectName); screenEffectsSpeeds.Add(speed); screenEffectsGoals.Add(goal);
        }
        Vector2 size = new Vector2(0, 0);
        if (effectName == "shrink")
        {
            size = new Vector2(main.shrinkScreenEffect.weight, 0);
        }
        else if (effectName == "uncolor")
        {
            size = new Vector2(main.uncolorScreenEffect.weight, 0);
        }
        else if (effectName == "sature")
        {
            size = new Vector2(main.saturScreenEffect.weight, 0);
        }
        else if (effectName == "flash")
        {
            size = new Vector2(main.flashScreenEffect.weight, 0);
        }
        if (size.x != goal)
        {
            size = Vector2.MoveTowards(size, new Vector2(goal, 0), speed);
        }
        else
        {
            screenEffects.RemoveAt(index); screenEffectsSpeeds.RemoveAt(index); screenEffectsGoals.RemoveAt(index);
        }

        if (effectName == "shrink")
        {
            main.shrinkScreenEffect.weight = size.x;
        }
        else if (effectName == "uncolor")
        {
            main.uncolorScreenEffect.weight = size.x;
        }
        else if (effectName == "sature")
        {
            main.saturScreenEffect.weight = size.x;
        }
        else if (effectName == "flash")
        {
            size = new Vector2(main.flashScreenEffect.weight, 0);
        }
    }

    public void InventoryChoose(int choice)
    {
        if(choice < main.inventory.Count)
        {
            if (main.inventory[choice] != null)
            {
                GameObject temp = main.inventory[0];
                main.inventory[0] = main.inventory[choice];
                main.inventory[choice] = temp;
            }
        }
    }

    public void Stack()
    {

        List<string> nams = new List<string>();

        foreach (GameObject a in main.inventory.ToArray())
        {
            if (!nams.Contains(a.GetComponent<AbilityManager>().prenom))
            {
                nams.Add(a.GetComponent<AbilityManager>().prenom);
            }
        }

        List<List<GameObject>> ne = new List<List<GameObject>>(nams.Count); int prud = 0;
        while(ne.Count < nams.Count && prud < 250)
        {
            List<GameObject> t = new List<GameObject>();
            ne.Add(t);
            prud++;
        }

        foreach (GameObject a in main.inventory.ToArray())
        {
            foreach(string b in nams)
            {
                if(a.GetComponent<AbilityManager>().prenom == b && nams.IndexOf(b) < ne.Count && nams.IndexOf(b) >= 0)
                {
                    ne[nams.IndexOf(b)].Add(a);
                }
            }
        }

        main.inventory = new List<GameObject>();
        
        foreach(List<GameObject> a in ne)
        {
            main.inventory.AddRange(a);
        }
    }

    void Update()
    {
        int g = 0;
        while (g < screenEffects.Count)
        {
            ScreenEffects(screenEffectsGoals[g], screenEffects[g], screenEffectsSpeeds[g], g);
            g++;
        }

        foreach (Image a in inventorytItems.ToArray())
        {
            if (inventorytItems.IndexOf(a) < main.inventory.Count)
            {
                if (main.inventory[inventorytItems.IndexOf(a)] != null)
                {
                    a.sprite = main.inventory[inventorytItems.IndexOf(a)].GetComponent<AbilityManager>().icon;
                    a.color = main.inventory[inventorytItems.IndexOf(a)].GetComponent<AbilityManager>().iconColor;
                }
                else
                {
                    a.sprite = null;
                    a.color = new Color(0, 0, 0, 0);
                }
            }
            else
            {
                a.sprite = null;
                a.color = new Color(0, 0, 0, 0);
            }
        }

        if (main.weapons[0].GetComponent<Basic_Gun>().useAbilityManagerAmmo)
        {
            primaryWeaponAmmo.text = main.weapons[0].GetComponent<AbilityManager>().actMunitions.ToString();
        }
        else
        {
            primaryWeaponAmmo.text = main.weapons[0].GetComponent<Basic_Gun>().actAmmo.ToString();
        }
        primaryWeaponIcon.sprite = main.weapons[0].GetComponent<AbilityManager>().icon;
        primaryWeaponIcon.color = main.weapons[0].GetComponent<AbilityManager>().iconColor;
        if (main.weapons.Count >= 2)
        {
            if (main.weapons[1] != main.weapons[0])
            {
                if (main.weapons[1].GetComponent<Basic_Gun>().useAbilityManagerAmmo)
                {
                    secondaryWeaponAmmo.text = main.weapons[1].GetComponent<AbilityManager>().actMunitions.ToString();
                }
                else
                {
                    secondaryWeaponAmmo.text = main.weapons[1].GetComponent<Basic_Gun>().actAmmo.ToString();
                }
                secondaryWeaponIcon.sprite = main.weapons[1].GetComponent<AbilityManager>().icon;
            } 
            else
            {
                primaryWeaponIcon.sprite = main.weapons[0].GetComponent<AbilityManager>().icon;
                secondaryWeaponIcon.sprite = main.weapons[1].GetComponent<AbilityManager>().secondIcon;
                if (main.actWeapon == 0)
                {
                    primaryWeaponIcon.gameObject.SetActive(true);
                    secondaryWeaponIcon.gameObject.SetActive(false);
                }
                else if(main.actWeapon == 1)
                {
                    primaryWeaponIcon.gameObject.SetActive(false);
                    secondaryWeaponIcon.gameObject.SetActive(true);
                }
            }
            if (main.weapons.Count >= 3) 
            {
                if (main.weapons[2].GetComponent<Basic_Gun>() != null)
                {
                    foundWeaponAmmo.text = main.weapons[2].GetComponent<Basic_Gun>().actAmmo.ToString();
                }
                else if(main.weapons[2].GetComponent<AbilityManager>().linkedHealth != null)
                {
                    foundWeaponAmmo.text = main.weapons[2].GetComponent<AbilityManager>().linkedHealth.actLife.ToString();
                }
                else
                {
                    foundWeaponAmmo.text = "";
                }
                foundWeaponIcon.sprite = main.weapons[2].GetComponent<AbilityManager>().icon;
                foundWeaponIcon.color = new Color(1, 1, 1, 0.5f);
                foundWeaponIcon.gameObject.SetActive(true);
            }
            else
            {
                foundWeaponAmmo.text = "";
                foundWeaponIcon.gameObject.SetActive(false);
            }
        }
        else
        {
            secondaryWeaponAmmo.text = "";
            secondaryWeaponIcon.gameObject.SetActive(false);
            foundWeaponAmmo.text = "";
            foundWeaponIcon.gameObject.SetActive(false);
        }
        /*
        else
        {
            if(main.actWeapon == 0)
            {
                primaryWeaponIcon.gameObject.SetActive(true);
                secondaryWeaponIcon.gameObject.SetActive(false);
            }
            else if(main.actWeapon == 1)
            {
                primaryWeaponIcon.gameObject.SetActive(false);
                secondaryWeaponIcon.gameObject.SetActive(true);
            }
        }
        */

        if (main.ability1 != null)
        {
            if (main.ability1.GetComponent<AbilityManager>().munitions <= 1)
            {
                ability1Ammo.text = "";
            }
            else
            {
                ability1Ammo.text = main.ability1.GetComponent<AbilityManager>().actMunitions.ToString();
                if (ability1AmmoJauge.Length == main.ability1.GetComponent<AbilityManager>().munitions)
                {
                    int i = 0;
                    while (i < main.ability1.GetComponent<AbilityManager>().munitions)
                    {
                        if (i < main.ability1.GetComponent<AbilityManager>().actMunitions)
                        {
                            ability1AmmoJauge[i].SetActive(true);
                        }
                        else
                        {
                            ability1AmmoJauge[i].SetActive(false);
                        }
                        i++;
                    }
                }
            }
            ability1Icon.sprite = main.ability1.GetComponent<AbilityManager>().icon;
            ability1Icon.color = main.ability1.GetComponent<AbilityManager>().iconColor;
            ability1Cooldown.text = ((main.ability1.GetComponent<AbilityManager>().usedCooldown - main.ability1.GetComponent<AbilityManager>().actCooldown) / (int)10).ToString();
            if (int.Parse(ability1Cooldown.text) <= 0 && main.ability1.GetComponent<AbilityManager>().allowed) { ability1Cooldown.text = ""; ability1Icon.color = new Color(1, 1, 1, 0.9f); } else { ability1Icon.color = new Color(1, 1, 1, 0.1f); };
        }
        else
        {
            ability1Icon.color = new Color(0, 0, 0, 0);
            ability1Ammo.text = "";
            ability1Cooldown.text = "";
        }

        if (main.ability2 != null)
        {
            if (main.ability2.GetComponent<AbilityManager>().munitions <= 1)
            {
                ability2Ammo.text = "";
            }
            else
            {
                ability2Ammo.text = main.ability2.GetComponent<AbilityManager>().actMunitions.ToString();
                if (ability2AmmoJauge.Length == main.ability2.GetComponent<AbilityManager>().munitions)
                {
                    int i = 0;
                    while (i < main.ability2.GetComponent<AbilityManager>().munitions)
                    {
                        if (i < main.ability2.GetComponent<AbilityManager>().actMunitions)
                        {
                            ability2AmmoJauge[i].SetActive(true);
                        }
                        else
                        {
                            ability2AmmoJauge[i].SetActive(false);
                        }
                        i++;
                    }
                }
            }
            ability2Icon.sprite = main.ability2.GetComponent<AbilityManager>().icon;
            ability2Icon.color = main.ability2.GetComponent<AbilityManager>().iconColor;
            ability2Cooldown.text = ((main.ability2.GetComponent<AbilityManager>().usedCooldown - main.ability2.GetComponent<AbilityManager>().actCooldown) / (int)10).ToString();
            if (int.Parse(ability2Cooldown.text) <= 0 && main.ability2.GetComponent<AbilityManager>().allowed) { ability2Cooldown.text = ""; ability2Icon.color = new Color(1, 1, 1, 0.9f); } else { ability2Icon.color = new Color(1, 1, 1, 0.1f); };
        }
        else
        {
            ability2Icon.color = new Color(0, 0, 0, 0);
            ability2Ammo.text = "";
            ability2Cooldown.text = "";
        }

        if (main.ability3 != null)
        {
            if (main.ability3.GetComponent<AbilityManager>().munitions <= 1)
            {
                ability3Ammo.text = "";
            }
            else
            {
                ability3Ammo.text = main.ability3.GetComponent<AbilityManager>().actMunitions.ToString();
                if (ability3AmmoJauge.Length == main.ability3.GetComponent<AbilityManager>().munitions)
                {
                    int i = 0;
                    while (i < main.ability3.GetComponent<AbilityManager>().munitions)
                    {
                        if (i < main.ability3.GetComponent<AbilityManager>().actMunitions)
                        {
                            ability3AmmoJauge[i].SetActive(true);
                        }
                        else
                        {
                            ability3AmmoJauge[i].SetActive(false);
                        }
                        i++;
                    }
                }
            }
            ability3Icon.sprite = main.ability3.GetComponent<AbilityManager>().icon;
            ability3Icon.color = main.ability3.GetComponent<AbilityManager>().iconColor;
            ability3Cooldown.text = ((main.ability3.GetComponent<AbilityManager>().usedCooldown - main.ability3.GetComponent<AbilityManager>().actCooldown) / (int)10).ToString();
            if (int.Parse(ability3Cooldown.text) <= 0 && main.ability3.GetComponent<AbilityManager>().allowed) { ability3Cooldown.text = ""; ability3Icon.color = new Color(1, 1, 1, 0.9f); } else { ability3Icon.color = new Color(1, 1, 1, 0.1f); };
        }
        else
        {
            ability3Icon.color = new Color(0, 0, 0, 0);
            ability3Ammo.text = "";
            ability3Cooldown.text = "";
        }

        if (main.ultimate != null)
        {
            if (main.ultimate.GetComponent<AbilityManager>().munitions > 1 && main.ultimate.GetComponent<AbilityManager>().casting)
            {
                ultimateAmmo.text = main.ultimate.GetComponent<AbilityManager>().actMunitions.ToString();
            }
            else
            {
                ultimateAmmo.text = "";
            }
            ultimateCooldown.text = ((main.ultimate.GetComponent<AbilityManager>().usedCooldown - main.ultimate.GetComponent<AbilityManager>().actCooldown) / (int)10).ToString();
            if (int.Parse(ultimateCooldown.text) <= 0 && main.ultimate.GetComponent<AbilityManager>().allowed) { ultimateCooldown.text = ""; ultimateIcon.color = new Color(1, 1, 1, 0.9f); } else { ultimateIcon.color = new Color(1, 1, 1, 0.1f); };
            if (ultimateCooldown.text != "") { ultimateFond.color = new Color(1, 1, 1, 0.01f); }
            else { ultimateFond.color = ultimateFondColor; ultimateIcon.color = new Color(1, 1, 1, 1f); }
            if (main.ultimate.GetComponent<AbilityManager>().casting)
            {
                ultimateIcon.color = new Color(1, 1, 1, 1f);
                ultimateFond.color = new Color(0, 0, 0, 0.9f);
            }
            ultimateIcon.sprite = main.ultimate.GetComponent<AbilityManager>().icon;
            ultimateIcon.color = main.ultimate.GetComponent<AbilityManager>().iconColor;
        }
        else
        {
            ultimateIcon.color = new Color(0, 0, 0, 0);
            ultimateCooldown.text = "";
            ultimateAmmo.text = "";
        }

        if (main.passive != null)
        {
            if (main.passive.GetComponent<AbilityManager>().munitions <= 1)
            {
                passiveAmmo.text = "";
            }
            else
            {
                passiveAmmo.text = main.passive.GetComponent<AbilityManager>().actMunitions.ToString();
            }
            passiveIcon.sprite = main.passive.GetComponent<AbilityManager>().icon;
            passiveIcon.color = main.passive.GetComponent<AbilityManager>().iconColor;
            passiveCooldown.text = ((main.passive.GetComponent<AbilityManager>().usedCooldown - main.passive.GetComponent<AbilityManager>().actCooldown) / (int)10).ToString();
            if (int.Parse(passiveCooldown.text) <= 0 && main.passive.GetComponent<AbilityManager>().allowed) { passiveCooldown.text = ""; passiveIcon.color = new Color(1, 1, 1, 0.9f); } else { passiveIcon.color = new Color(1, 1, 1, 0.1f); };
        }
        else
        {
            passiveIcon.color = new Color(0, 0, 0, 0);
            passiveAmmo.text = "";
            passiveCooldown.text = "";
        }

        if (main.passive2 != null)
        {
            if (main.passive2.GetComponent<AbilityManager>().munitions <= 1)
            {
                passive2Ammo.text = "";
            }
            else
            {
                passive2Ammo.text = main.passive2.GetComponent<AbilityManager>().actMunitions.ToString();
            }
            passive2Icon.sprite = main.passive2.GetComponent<AbilityManager>().icon;
            passive2Icon.color = main.passive2.GetComponent<AbilityManager>().iconColor;
            passive2Cooldown.text = ((main.passive2.GetComponent<AbilityManager>().usedCooldown - main.passive2.GetComponent<AbilityManager>().actCooldown) / (int)10).ToString();
            if (int.Parse(passive2Cooldown.text) <= 0 && main.passive2.GetComponent<AbilityManager>().allowed) { passive2Cooldown.text = ""; passive2Icon.color = new Color(1, 1, 1, 0.9f); } else { passive2Icon.color = new Color(1, 1, 1, 0.1f); };
        }
        else
        {
            passive2Icon.color = new Color(0, 0, 0, 0);
            passive2Ammo.text = "";
            passive2Cooldown.text = "";
        }

        burnIcon.gameObject.SetActive(main.GetComponent<EffectManager>().burned); 
        freezeIcon.gameObject.SetActive(main.GetComponent<EffectManager>().freezed);
        speedIcon.gameObject.SetActive(main.GetComponent<EffectManager>().speeded);
        shieldIcon.gameObject.SetActive(main.GetComponent<EffectManager>().shielded);
        damagesIcon.gameObject.SetActive(main.GetComponent<EffectManager>().damaged);
        if (main.GetComponent<EffectManager>().translucidity > 1)
        {
            invisibleIcon.gameObject.SetActive(true);
        }
        else
        {
            invisibleIcon.gameObject.SetActive(false);
        }
    }
}
