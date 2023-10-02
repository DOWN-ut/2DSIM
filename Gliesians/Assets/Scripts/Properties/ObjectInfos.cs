using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfos : MonoBehaviour {

	[Header("Types and Tags")]

	public bool isMiror;

	public bool isButton;

    public int team;

    public int interester;//Ennemies are atttracted by sounds

    [Header("Properties")]

    public bool destroyableByShields;

    public bool walkable;

    public bool cineticStealable;

    public bool railSystemAble;

	[Header("Ingame Values")]

	public bool ported ;

    public bool dead;

}
