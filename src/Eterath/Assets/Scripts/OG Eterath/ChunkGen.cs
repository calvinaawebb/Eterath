using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGen : MonoBehaviour
{
    //public ArrayList<GameObject> chunks = new ArrayList<GameObject>();
    public DimensionalMapGen mapref;
    public GameObject chunk;
    public GameObject tree;
    public GameObject stoneHenge;
    public GameObject DLCstoneHenge;
    public int chunkAmount = 9;
    public Material grass;
    public float treeChance;
    public float hengeChance;
    public float DLCChance;
    // Start is called before the first frame update
    void Start()
    {
        mapref = new DimensionalMapGen();
        transform.position = new Vector3((mapref.xbound * 1.5f)*-1, -50, (mapref.zbound*1.5f)*-1);
        for (int x = 0; x < chunkAmount; x++)
        {
            for (int z = 0; z < chunkAmount; z++)
            {
                GameObject o = Instantiate(chunk, transform);
                o.transform.localPosition = new Vector3((mapref.xbound / mapref.resFactor) * x, 0, (mapref.zbound / mapref.resFactor) * z);
                DimensionalMapGen gen = o.GetComponent<DimensionalMapGen>();
                gen.offset = new Vector2((mapref.xbound * x) / gen.scale, (mapref.zbound * z) / gen.scale);
                gen.GenDMap();
                placeObject(treeChance, tree, 10, gen, x, z);
                placeObject(hengeChance, stoneHenge, 1, gen, x, z);
                placeObject(DLCChance, DLCstoneHenge, 1, gen, x, z);
                /*GameObject p = Instantiate(o, transform);
                p.GetComponent<MeshRenderer>().material = grass;*/
            }
        }
    }

    void placeObject(float frequency, GameObject objPlace, int amount, DimensionalMapGen gen, int chunkX, int chunkZ) 
    {
        // Conditional that decides if the object will spawn based of frequency aka percentage of chunks with this item in it.
        if (Random.Range(0f, 1f) / frequency <= 1) 
        {
            for(int i=0;i<amount;i++) 
            {
                GameObject y = Instantiate(objPlace, transform);
                int randomVert = Random.Range(0, gen.vertices.Length - 1);
                y.transform.localPosition = gen.vertices[Random.Range(0, gen.vertices.Length)];
                y.transform.localPosition = new Vector3(((mapref.xbound / mapref.resFactor) * chunkX) + gen.vertices[randomVert].x, gen.vertices[randomVert].y, ((mapref.zbound / mapref.resFactor) * chunkZ) + gen.vertices[randomVert].z);
                y.transform.eulerAngles = new Vector3(0, Random.Range(-360f, 360f), 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
