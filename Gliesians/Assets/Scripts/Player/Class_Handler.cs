using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Class_Handler : MonoBehaviour
{
    public string officialName;

    public string[] surnames = new string[10];//Multilanguages

    public string encodedName;

    public int life = 1000;

    public float speed = 6;
    public float run = 1.4f;
    public float midairSpeed = 3.5f;

    public float gravity = 1;

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
    public Color iconColor = Color.white;
    public Color mainColor = Color.white;
    public Color secondColor = Color.white;
    public Color thirdColor = Color.white;

    [Tooltip("0 : Leader / 1: Protectress / 2: Engineer / 3: Voyager / 4: Pathfinder / 5: Spy / 6: Illusionist / 7: Savior / 8: Medic / 9: Healers")]
    public int role; //0 : Leader / 1: Pathfinder / 2: Spy / 3: Healer / 4: Illusionist / 5: Engineer / 6: Protector / 7: Medic / 8: Savior / 9: Voyager / 10: 
    public string[] roleNames = new string[10]; //Multilanguages

    public int roleNumber; //The number of this hero in the role-list 

    public string[] descriptions = new string[10];//Multi languegas

    public float damageRank; //For 0 to 1 (0% to 100%)
    public float healRank;
    public float protectRank;
    public float sustainRank;
}
