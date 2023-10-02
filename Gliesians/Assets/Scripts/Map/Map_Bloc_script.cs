using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[CreateAssetMenu(fileName = "Map_bloc", menuName = "Map/Mapbloc", order = 1)]
public class Map_Bloc_script : ScriptableObject
{
    public string surname;

    [Header("0 : Left ; 1 : Right ; : 2 Top ; 3 : Bottom ; 4 : Forward ; 5 : Backward ; 6 : Bridge")]
    public bool[] links = new bool[7] { false, false, false, false ,false,false,false};

    [Header("The architecture of this bloc, for example, a corridor, a place, or a bridge.")]
    public string architecture = "Corridor";

    public float architectureForce = 1;

    [Header("The biomes available for this bloc. The biome determines the ambience, some parts of the decor, and some caracteritics of the mobs.")]
    public string[] biomes = new string[5] { "Base", "Forest" ,"","",""};

    public Sprite bitmap;
    public GameObject block;

    public bool spawnable = true;

    public bool bridgeable = false;

    public List<string> cannotBeOver;

    public int minEtage;
}
[CreateAssetMenu(fileName = "Map_detail", menuName = "Map/Mapdetail", order = 2)]
public class Map_Detail_script : ScriptableObject
{
    public string surname;

    public GameObject[] models;
}

[CreateAssetMenu(fileName = "Biome", menuName = "Map/Biome", order = 1)]
public class Biome_Bloc_script : ScriptableObject
{

    public string surname;

    public Material mainMaterial;

    public PostProcessProfile postProcess;

    public Color mainColor;

    public int height = 2;

    public float etageDensity = 0.25f;

    public int rdcEtage = 0;

    public string mainArchitecture = "Corridor";

}

public class Map_Bloc_test : ScriptableObject
{

    public int[] links = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

    public List<string> architectures ;
    public List<float> architectureForces;

    public List<Biome_Bloc_script> biomes ;
    public int[] biomesSize = new int[4] { 0, 0, 0, 0 };
}