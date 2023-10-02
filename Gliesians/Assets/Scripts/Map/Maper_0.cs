using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class Maper_0 : MonoBehaviour {

    [Header("Parameters")]

    public Map_Bloc_script[] usableBlocs;
    public Map_Bloc_script[] wallBlocs;
    public int wallBlockProb;
    public Map_Bloc_script[] voidBlocs;
    public Biome_Bloc_script biome;
    public float maxEtageDensity;
    public float minEtageDensity;
    public int minHeight;
    public int maxHeight;
    public int biomeCoherence;
    public string[] aviableArchitectures = new string[3] { "Corridor", "Place", "Room" };
    public float architectureCoherence;
    public float architectureForce;
    //public GameObject[] mapDetails;
    //public float mapDetailsFrequence = 1;
    public Spawn_Profile[] spawners;
    public EntitySpawner spawnerPrefab;
    public float spawnersHeight = 1.25f;
    public float spawnerDensity = 0.1f;
    public EntitySpawner[] lootboxes;
    public float lootboxesHeight = 1f;
    public float lootboxesDensity = 0.15f;

    public bool showBitMap;
    public float bitMapDistance;

    public bool regenerate;

    [Header("Dimensions of the map, in blocs")]
    public int width;
    public int length;
    public int height;

    [Header("Dimensions of each bloc, in units")]
    public float blocWidth;
    public float blocLength;
    public float blocHeight;
    public float resize;
    public float blockModelResize;

    [Header("Ingame")]

    public Map_Bloc_script[,,] map;

    public GameObject[,,] blocks;

    public GameObject navMap;

    public PartyManager manager;

    public List<Transform> mainCheckpoints;// The transforms placed in the center of each mapblock

    public bool navMeshGened;

    public float size;

    public bool generated;

    // Use this for initialization
    void Awake()
    {
        if (navMap == null)
        {
            navMap = new GameObject("navMap");
        }

        manager = FindObjectOfType<PartyManager>();

        map = new Map_Bloc_script[length, height, width];
        blocks = new GameObject[length, height, width];

        biome = GenerateBiome();
        height = biome.height;

        Generate(false);

        if (regenerate)
        {
            Generate(true);
        }
    }

    
    private void Start()
    {
        //var surface = GetComponent(typeof(NavMeshSurface)) as NavMeshSurface;
        //surface.BuildNavMesh();
        //navMap.AddComponent<NavMeshSurface>();
        //navMap.GetComponent<NavMeshSurface>().collectObjects = CollectObjects.Children;

    }

    private void Update()
    {
        
        if (!navMeshGened)
        {
            //navMap.GetComponent<NavMeshSurface>().BuildNavMesh();
            //Lightmapping.Bake();
            navMeshGened = true;
        }
        
    }

    public void Delete()
    {
        foreach (GameObject a in blocks)
        {
            Destroy(a);
        }
        map = new Map_Bloc_script[length, height, width];
        blocks = new GameObject[length, height, width];
        generated = false;
    }

    public void Generate(bool adapt)
    {       
        int l = 0; //For the length generation     
        int h = 0; //For the height generation
        int w = 0; //For the width generation

        if (map.GetLength(0) != length || map.GetLength(1) != height || map.GetLength(2) != width)
        {
            Map_Bloc_script[,,] newmap = map;
            map = new Map_Bloc_script[length, height, width];
            GameObject[,,] newblocks = blocks;
            foreach (GameObject a in blocks)
            {
                Destroy(a);
            }
            blocks = new GameObject[length, height, width];

            while (l < newmap.GetLength(0))
            {
                while (h < newmap.GetLength(1))
                {
                    while (w < newmap.GetLength(2))
                    {
                        map[l, h, w] = newmap[l, h, w];
                        blocks[l, h, w] = newblocks[l, h, w];
                        w++;
                    }
                    h++;
                }
                l++;
            }
        }

        l = 0;
        h = 0;
        w = 0;

        while (l < length)
        {
            while (h < height)
            {
                while (w < width)
                {
                    if (map[l, h, w] == null || adapt)
                    {
                        List<Map_Bloc_script> usables = new List<Map_Bloc_script>();
                        Map_Bloc_test voisins = new Map_Bloc_test();

                        bool ok = true;
                        bool needbridge = false;

                        if (h > 0)
                        {
                            bool midair = true;

                            if (map[l, h - 1, w] != null)
                            {
                                midair = false;
                            }
                            else
                            {
                                needbridge = true;
                            }
                            if (l < length - 1)
                            {
                                if(map[l+1, h, w] != null)
                                {
                                    midair = false;
                                }
                            }
                            if (l >0)
                            {
                                if (map[l - 1, h, w] != null)
                                {
                                    midair = false;
                                }
                            }
                            if (w < width - 1)
                            {
                                if (map[l , h, w+1] != null)
                                {
                                    midair = false;
                                }
                            }
                            if (w>0)
                            {
                                if (map[l, h, w - 1] != null)
                                {
                                    midair = false;
                                }
                            }

                            if (midair)
                            {
                                ok = false;
                            }

                            midair = true;

                            foreach(Map_Bloc_script a in voidBlocs)
                            {
                                if (map[l, h - 1, w] != a)
                                {
                                    midair = false;
                                }
                                else
                                {
                                    needbridge = true;
                                }
                                if (l < length - 1)
                                {
                                    if (map[l + 1, h, w] != a)
                                    {
                                        midair = false;
                                    }
                                }
                                if (l > 0)
                                {
                                    if (map[l - 1, h, w] != a)
                                    {
                                        midair = false;
                                    }
                                }
                                if (w < width - 1)
                                {
                                    if (map[l, h, w + 1] != a)
                                    {
                                        midair = false;
                                    }
                                }
                                if (w > 0)
                                {
                                    if (map[l, h, w - 1] != a)
                                    {
                                        midair = false;
                                    }
                                }

                                if (midair)
                                {
                                    ok = false;
                                }
                            }
                        }
                        if (ok)
                        {
                            int usablesLenExt = 1;
                            if (w == 0 && h == 0 && l == 0)
                            {
                                voisins.links = new int[6] { 1, 1, 0, 0, 1, 0 };
                                voisins.architectures = new List<string> {biome.mainArchitecture};
                                voisins.biomes = new List<Biome_Bloc_script>();
                                voisins.biomes.Add(biome);
                                usablesLenExt = 0; //Ne pas mettre de bloc plein (mur) si on est sur la premiere case
                            }
                            else
                            {
                                voisins = GetVoisins(l, h, w, adapt);
                            }

                            usables.AddRange(GetUsables(voisins,biome.mainArchitecture,needbridge,h,w,l)); 

                            int i = wallBlockProb * usablesLenExt;
                            if (!needbridge)
                            {
                                while (i > 0)
                                {
                                    usables.Add(wallBlocs[Random.Range(0, wallBlocs.Length - 1)]); 
                                    i--;
                                }
                            }
                            if (h > biome.rdcEtage)
                            {
                                i = ((Mathf.RoundToInt(10 / (float)biome.etageDensity) / 10) - 1) * usables.Count;
                                while (i > 0)
                                {
                                    usables.Add(voidBlocs[Random.Range(0, voidBlocs.Length - 1)]);
                                    i--;
                                }
                            }

                            /*
                            List<Biome_Bloc_script> ubiomes = new List<Biome_Bloc_script>();
                            int ol = 0;
                            while (ol < voisins.biomes.Count)
                            {
                                ubiomes.Add(voisins.biomes[ol]);
                                ol++;
                            }
                            foreach (Biome_Bloc_script a in usableBiomes)
                            {
                                ubiomes.Add(a);
                            }
                            biome = ubiomes[Random.Range(0, ubiomes.Count)];
                            */
                        }
                        else
                        {
                            usables.Add(voidBlocs[Random.Range(0, voidBlocs.Length - 1)]);
                        }

                        bool good = true;
                        if (adapt && map[l, h, w] != null)
                        {
                            foreach (Map_Bloc_script a in usables)
                            {
                                if (a.surname == map[l, h, w].surname)
                                {
                                    good = false;
                                }
                            }
                        }

                        if (good)
                        {
                            if (usables.Count > 0)
                            {
                                map[l, h, w] = usables[Random.Range(0, usables.Count)];
                            }
                            else
                            {
                                map[l, h, w] = voidBlocs[Random.Range(0, voidBlocs.Length)];
                            }
                        }
                    }
                    if (map[l, h, w] != null)
                    {
                        if (blocks[l, h, w] != null)
                        {
                            Destroy(blocks[l, h, w].gameObject);
                        }
                        GameObject temp = new GameObject();
                        GameObject temp3 = new GameObject();
                        if (showBitMap)
                        {
                            temp3.AddComponent<SpriteRenderer>().sprite = map[l, h, w].bitmap;
                        }
                        if (map[l, h, w].block != null)
                        {
                            GameObject temp2 = Instantiate(map[l, h, w].block, temp.transform.position, map[l, h, w].block.transform.rotation, temp.transform);
                            temp2.transform.localScale *= blockModelResize;
                            temp2.GetComponent<MapBlock>().biomeScript = biome;
                            temp2.GetComponent<MapBlock>().biome = temp2.GetComponent<MapBlock>().biomeScript.surname;
                            temp2.GetComponent<MapBlock>().maper = this;
                              temp2.isStatic = true;
                            if (showBitMap)
                            {
                                temp3.GetComponent<SpriteRenderer>().color = temp2.GetComponent<MapBlock>().biomeScript.mainColor;
                            }
                        }
                        temp3.transform.parent = temp.transform;
                        temp3.transform.rotation = transform.rotation;
                        //temp.transform.position = transform.position + (new Vector3(w * blocWidth, h * blocHeight, -l * blocLength) * size);
                        temp.transform.position = transform.position + ((-transform.forward * blocWidth * w) + (transform.right * blocLength * l) + (transform.up * blocHeight * h) * size);
                        temp.transform.rotation = transform.rotation;
                        temp.transform.localScale *= resize;
                        temp3.transform.localScale = new Vector3(blocWidth, blocHeight, blocLength)*blockModelResize;
                        temp3.transform.position = new Vector3(temp3.transform.position.x, -((h+1)*bitMapDistance), temp3.transform.position.z);
                        temp3.transform.rotation = Quaternion.Euler(0, 0, 0);
                        blocks[l, h, w] = temp;
                        if (map[l, h, w].spawnable)  
                        {
                            //Placing spawners
                            int ran = (int)Random.Range(0, spawners.Length / (float)spawnerDensity);
                            if (ran < spawners.Length)
                            {
                                GameObject spa = Instantiate(spawnerPrefab.gameObject, blocks[l, h, w].transform.position + new Vector3(0, spawnersHeight, 0), Quaternion.Euler(0, 0, 0), blocks[l, h, w].transform);
                                spa.GetComponent<EntitySpawner>().spawns.Add(spawners[ran]);
                            }
                            //Placing lootboxes                     
                            ran = (int)Random.Range(0, lootboxes.Length / (float)lootboxesDensity);
                            if (ran < lootboxes.Length)
                            {
                                blocks[l, h, w].transform.GetChild(0).GetComponent<MapBlock>().lootboxes.Add(lootboxes[ran].gameObject);
                                blocks[l, h, w].transform.GetChild(0).GetComponent<MapBlock>().altitude = lootboxesHeight;
                            }
                            //blocks[l, h, w].transform.GetChild(0).GetComponent<MapBlock>().size = resize * blockModelResize;
                            /*
                            //Details
                            ran = (int)Random.Range(0, mapDetails.Length / (float)mapDetailsFrequence);
                            if(ran < mapDetails.Length)
                            {
                                GameObject deta = Instantiate(mapDetails[ran], blocks[l, h, w].transform.position, mapDetails[ran].transform.rotation, navMap.transform);//blocks[l, h, w].transform
                            }
                            */
                        }
                        blocks[l, h, w].transform.GetChild(0).GetChild(0).parent = navMap.transform;
                        blocks[l, h, w].transform.GetChild(0).GetComponent<MapBlock>().manager = manager;
                        mainCheckpoints.Add(blocks[l, h, w].transform.GetChild(0).GetComponent<MapBlock>().centralCheckpoint);
                    }
                    w++;
                }
                h++;
                w = 0;
            }
            l++;
            h = 0;
        }
        generated = true;
    }

    Map_Bloc_script[] GetUsables(Map_Bloc_test voisins,string mainArchitecture,bool nothingUnder,int etage,int wi,int le)
    {
        List<Map_Bloc_script> ff = new List<Map_Bloc_script>();

        int u = 0;
        while (u < usableBlocs.Length)
        {
            int prob = 1;

            int o = 0;
            while (o < voisins.links.Length)
            {
                if (voisins.links[o] == 1 && usableBlocs[u].links[o] != true)
                {
                    prob = 0;
                }
                if (voisins.links[o] == -1 && usableBlocs[u].links[o] != false)
                {
                    prob = 0;
                }
                o++;
            }
            if (voisins.architectures.Contains(usableBlocs[u].architecture))
            {
                prob = (int)(prob * architectureCoherence);
                /*
                int p = -1;
                foreach(string a in voisins.architectures)
                {
                    if (a == usableBlocs[u].architecture)
                    {
                        p = voisins.architectures.IndexOf(a);
                    }
                }
                if (p >= 0)
                {
                    prob = (int)(prob * voisins.architectureForces[p]);
                }
                */
            }
            if (usableBlocs[u].architecture == mainArchitecture)
            {
                prob = (int)(prob * architectureForce);
            }

            if(nothingUnder && !usableBlocs[u].bridgeable)
            {
                prob = 0;
            }

            if(usableBlocs[u].minEtage > etage)
            {
                prob = 0;
            }

            if(etage > 0)
            {
                if (usableBlocs[u].cannotBeOver.Contains(map[le, etage - 1, wi].architecture))
                {
                    prob = 0;
                }
            }

            while (prob > 0)
            {
                ff.Add(usableBlocs[u]);
                prob--;
            }

            u++;
        }

        return ff.ToArray();
    }

    Biome_Bloc_script GenerateBiome()
    {
        Biome_Bloc_script bioma = new Biome_Bloc_script();

        bioma.mainColor = biome.mainColor;

        bioma.mainMaterial = biome.mainMaterial;

        bioma.postProcess = biome.postProcess;

        List<string> archis = new List<string>(); archis.AddRange(aviableArchitectures);
        foreach(string a in archis.ToArray())
        {
            if (a == "Void")
            {
                archis.Remove(a);
            }
        }

        bioma.mainArchitecture = aviableArchitectures[Random.Range(0, archis.Count - 1)];

        bioma.height = Random.Range(minHeight, maxHeight);

        bioma.rdcEtage = Random.Range(0, height/2);

        bioma.etageDensity = Random.Range(minEtageDensity, maxEtageDensity);

        return bioma;
    }

    List<Biome_Bloc_script> BiomeCoherer(int l,int h,int w)
    {
        List<Biome_Bloc_script> biomes = new List<Biome_Bloc_script>();

        int biomeSize = 0;

        if (w > 0)
        {
            Biome_Bloc_script bio = blocks[l, h, w - 1].transform.GetChild(0).GetComponent<MapBlock>().biomeScript;
            int ww = w-1;
            while (ww > 1 && blocks[l, h, ww].transform.GetChild(0).GetComponent<MapBlock>().biomeScript == bio)
            {
                biomeSize++;
                ww--;
            }
            int wwo = biomeCoherence / (biomeSize+1);
            while (wwo > 0)
            {
                biomes.Add(bio);
                wwo--;
            }
        }

        if (h > 0)
        {
            Biome_Bloc_script bio = blocks[l, h-1, w].transform.GetChild(0).GetComponent<MapBlock>().biomeScript;
            int hh = h-1;
            while (hh > 1 && blocks[l, hh, w].transform.GetChild(0).GetComponent<MapBlock>().biomeScript == bio)
            {
                biomeSize++;
                hh--;
            }
            int hho = biomeCoherence / (biomeSize+1);
            while (hho > 0)
            {
                biomes.Add(bio);
                hho--;
            }
        }

        if (l > 0)
        {
            Biome_Bloc_script bio = blocks[l-1, h, w ].transform.GetChild(0).GetComponent<MapBlock>().biomeScript;
            int ll = l-1;
            while (ll > 1 && blocks[ll, h, w].transform.GetChild(0).GetComponent<MapBlock>().biomeScript == bio)
            {
                biomeSize++;
                ll--;
            }
            int llo = biomeCoherence / (biomeSize+1);
            while (llo > 0)
            {
                biomes.Add(bio);
                llo--;
            }
        }

        return biomes;
    }

    //Return an array which contains all the necesary links that the bloc needs to have
    Map_Bloc_test GetVoisins(int l, int h, int w, bool adapt)
    {
        int[] links = { 0, 0, 0, 0, 0, 0};
        List<Biome_Bloc_script> biomes = new List<Biome_Bloc_script>();
        List<string> architectures = new List<string>();
        List<float> architecturesForces = new List<float>();

        List<Biome_Bloc_script> bio = BiomeCoherer(l, h, w);
        foreach (Biome_Bloc_script a in bio)
        {
            biomes.Add(a);
        }

        //Right link test on the previous bloc
        if (w > 0)
        {
            if (map[l, h, w - 1] != null)
            {
                if (map[l, h, w - 1].links[1])
                {
                    links[0] = 1;
                }
                architectures.Add(map[l, h, w - 1].architecture);
                architecturesForces.Add(map[l, h, w - 1].architectureForce);
            }
        }

        //Top link test on the under bloc
        if (h > 0)
        {
            if (map[l, h - 1, w] != null)
            {
                if (map[l, h - 1, w].links[2])
                {
                    links[3] = 1;
                }
                architectures.Add(map[l, h-1, w].architecture);
                architecturesForces.Add(map[l, h-1, w ].architectureForce);
            }
        }

        //Forward link test on the previous bloc
        if (l > 0)
        {
            if (map[l - 1, h, w] != null)
            {
                if (map[l - 1, h, w].links[4])
                {
                    links[5] = 1;
                }
                architectures.Add(map[l-1, h, w].architecture);
                architecturesForces.Add(map[l-1, h, w].architectureForce);
            }
        }
        else
        {
            //Force a forward link on the last bloc of a line if there's no one on the line
            if (w >= width - 1)
            {
                Map_Bloc_script[] temp = GetPartOfMap(0, width - 1, l, h);

                if (TestLink(temp, new int[] { 0, 0, 0, 0, 1, 0 }) == false)
                {
                    links[4] = 1;
                }
            }
        }

        //Left link test on the next bloc
        if (w < width - 1)
        {
            if (map[l, h, w + 1] != null)
            {
                if (map[l, h, w + 1].links[0])
                {
                    links[1] = 1;
                }
                architectures.Add(map[l, h, w+1].architecture);
                architecturesForces.Add(map[l, h, w + 1].architectureForce);
            }
        }

        //Bottom link test on the above bloc
        if (h < height - 1)
        {
            if (map[l, h + 1, w] != null)
            {
                if (map[l, h + 1, w].links[3])
                {
                    links[2] = 1;
                }
                architectures.Add(map[l, h+1, w].architecture);
                architecturesForces.Add(map[l, h+1, w].architectureForce);
            }
        }

        //Backward link test on the next bloc
        if (l < length - 1)
        {
            if (map[l + 1, h, w] != null)
            {
                if (map[l + 1, h, w].links[5])
                {
                    links[4] = 1;
                }
                architectures.Add(map[l+1, h, w].architecture);
                architecturesForces.Add(map[l+1, h, w].architectureForce);
            }
        }

        /*
        //Test how many bridge there is before this bloc
        if(w > 0)
        {
            int i = 0;
            while (i < w)
            {
                if(map[l, h, i].links[6])
                {
                    links[6] += 1;
                    i++;
                }
                else
                {
                    i = w;
                }
            }
        }
        */

        Map_Bloc_test block = new Map_Bloc_test() ;
        block.links = links;
        block.biomes = biomes;
        block.architectures = architectures;
        block.architectureForces = architecturesForces;

        return block;
    }

    Map_Bloc_script[] GetPartOfMap(int start,int end,int l,int h)
    {
        Map_Bloc_script[] a = new Map_Bloc_script[end - start];

        int i = start;
        while (i < end)
        {
            a[i] = map[l, h, i];
            i++;
        }

        return a;
    }

    bool TestLink(Map_Bloc_script[] blocksToTest,int[] linksToTest)
    {
        bool b = true;

        int i = 0;
        while (i < linksToTest.Length)
        {
            int o = 0;
            while (o < blocksToTest.Length)
            {
                if (linksToTest[i] == 1 && blocksToTest[o].links[i] != true)
                {
                    b = false;
                }
                if (linksToTest[i] == -1 && blocksToTest[o].links[i] != false)
                {
                    b = false;
                }
                o++;
            }
            i++;
        }

        return b;
    }

}

/*
[CustomEditor(typeof(Maper_0))]
public class Maper_0_Editor : Editor
{
    Maper_0 myscript;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        myscript = (Maper_0)target;
        if (GUILayout.Button("Update Map"))
        {
            myscript.Generate(myscript.generated);
            myscript.Generate(myscript.generated);
        }
        if (GUILayout.Button("Delete Map"))
        {
            myscript.Delete();
        }
    }
}
*/
