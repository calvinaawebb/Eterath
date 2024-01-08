using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassGen : MonoBehaviour
{
    public List<GameObject> grassBlades = new List<GameObject>();
    public GameObject[] meshGens;
    public GameObject placeHolder;
    public Transform chunkGenerator;
    public GameObject grassBlade;
    public GameObject player;
    public Vector3[] vertices;
    public PlayerController playerCont;
    public DimensionalMapGen mapGen;
    public ChunkGen chunkGen;
    public float rendDist = 5;
    public List<int> shortestDists = new List<int>();
    public float grassRotx;
    public float grassRotz;
    //public Mesh overallMesh = new Mesh();

    void Start() 
    {
        meshGens = new GameObject[chunkGen.chunkAmount * chunkGen.chunkAmount];
        player = playerCont.player;
        int j = 0;
        foreach(Transform child in chunkGenerator) 
        {
            meshGens[j] = child.gameObject;
            j++;
        }
        Debug.Log("j" + j);
        //grassBlades = new GameObject[100000];
        //placeHolder = grassBlades[0];
        for (int idx = 0; idx < meshGens.Length; idx++) 
        {
            Debug.Log(meshGens[idx]);
            mapGen = meshGens[idx].GetComponent<DimensionalMapGen>();
            Vector3[] verticesIdx = mapGen.vertices;
            for (int i = 0; i < verticesIdx.Length; i++)
            {
                grassRotx = Random.Range(-30f, 30f);
                grassRotz = Random.Range(-30f, 30f);
                grassBlades.Add(GameObject.Instantiate(grassBlade));
                grassBlades[^1].transform.position = verticesIdx[i] + meshGens[idx].transform.position;
                grassBlades[^1].transform.eulerAngles = new Vector3(grassRotx + Random.Range(-5f, 5f), Random.Range(-360f, 360f), grassRotz + Random.Range(-5f, 5f));
                grassBlades[^1].transform.localScale = new Vector3(1f, 0.5f, 1f);

                /*CombineInstance[] combine = new CombineInstance[2];
                combine[0].mesh = grassBlades[^1].GetComponent<MeshFilter>().sharedMesh;
                combine[0].transform = grassBlades[^1].GetComponent<MeshFilter>().transform.localToWorldMatrix;
                combine[1].mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
                combine[1].transform = gameObject.GetComponent<MeshFilter>().transform.localToWorldMatrix;
                overallMesh.CombineMeshes(combine);
                transform.GetComponent<MeshFilter>().sharedMesh = overallMesh;
                Destroy(grassBlades[^1]);*/
                /*if (grassBlades.Count == 0)
                {
                    grassBlades.Add(GameObject.Instantiate(grassBlade));
                    grassBlades[^1].transform.position = verticesIdx[i] + meshGens[idx].transform.position;
                    grassBlades[^1].transform.eulerAngles = new Vector3(grassRotx + Random.Range(-5f, 5f), Random.Range(-360f, 360f), grassRotz + Random.Range(-5f, 5f));
                    grassBlades[^1].transform.localScale = new Vector3(1f, 0.5f, 1f);
                }
                else 
                {
                    Debug.Log("Count: " + grassBlades.Count);
                    Debug.Log("i: " + i);
                    if (!(grassBlades.Contains(grassBlades[i-1])))
                    {
                        grassBlades.Add(GameObject.Instantiate(grassBlade));
                        grassBlades[^1].transform.position = verticesIdx[i] + meshGens[idx].transform.position;
                        grassBlades[^1].transform.eulerAngles = new Vector3(grassRotx + Random.Range(-5f, 5f), Random.Range(-360f, 360f), grassRotz + Random.Range(-5f, 5f));
                        grassBlades[^1].transform.localScale = new Vector3(1f, 0.5f, 1f);
                        *//*grassRotx += Random.Range(-5f, 5f);
                        grassRotz += Random.Range(-5f, 5f);*//*
                    }
                } */
            }
        }
    }

    void Update() 
    {
        /*GameObject shortestDistOBJ;
        for (int k = 0; k < meshGens.Length; k++)
        {
            if (k == 0)
            {
                if (!(shortestDists.Contains(k)))
                {
                    shortestDists.Add(0);
                }
                else
                {
                    shortestDists.Remove(0);
                    shortestDists.Insert(0, 0);
                }
            }
            else if (Vector3.Distance(meshGens[k].transform.position, player.transform.position) < Vector3.Distance(meshGens[shortestDists[0]].transform.position, player.transform.position))
            {
                if (!(shortestDists.Contains(k)))
                {
                    shortestDists.Insert(0, k);
                }
                else
                {
                    shortestDists.Remove(0);
                    shortestDists.Insert(0, k);
                }
            }
            if (meshGens[k].GetComponent<Renderer>().isVisible)
            {
                grassGeneration(k);
            }
        }*/
        /*for (int o = 0; o < 6; o++) 
        {
            grassGeneration(shortestDists[o]);
        }*/
        //grassGeneration(shortestDists[0]);
        //Debug.Log("ok????: " + shortestDist + " grass arry length: " + grassBlades.Length + " vertices array length: " + vertices.Length);
    }

    /*void grassGeneration(int idx) 
    {
        mapGen = meshGens[idx].GetComponent<DimensionalMapGen>();
        Vector3[] verticesIdx = mapGen.vertices;
        Debug.Log("grass blade: " + placeHolder);
        Debug.Log("grass blade check: " + (grassBlades[0] == placeHolder));
        for (int i = 0; i < verticesIdx.Length; i++)
        {
            //Debug.Log("Grass: " + (Vector3.Distance(player.transform.position, vertices[i]) < 100));
            if (grassBlades[i] == placeHolder)
            {
                if (Vector3.Distance(player.transform.position, (verticesIdx[i] + meshGens[idx].transform.position)) < 5)
                {
                    //Vector3 alteredDomain = new Vector3((vertices[i].x/2) + player.transform.position.x/3, vertices[i].y, (vertices[i].z/2) + player.transform.position.z/3);
                    grassBlades[i] = GameObject.Instantiate(grassBlade);
                    grassBlades[i].transform.position = verticesIdx[i] + meshGens[idx].transform.position;
                    grassBlades[i].transform.eulerAngles = new Vector3(0, Random.Range(-360f, 360f), 0);
                    grassBlades[i].transform.localScale = new Vector3(1f, 1f, 1f);
                }
            }
            else
            {
                if (Vector3.Distance(player.transform.position, (verticesIdx[i] + meshGens[idx].transform.position)) > 5)
                {
                    Destroy(grassBlades[i]);
                }
            }
        }
    }*/

    /* MK.1 Grass Gen:
    void grassGeneration(int idx) 
    {
        mapGen = meshGens[idx].GetComponent<DimensionalMapGen>();
        Vector3[] verticesIdx = mapGen.vertices;
        Debug.Log("grass blade: " + placeHolder);
        Debug.Log("grass blade check: " + (grassBlades[0] == placeHolder));
        for (int i = 0; i < verticesIdx.Length; i++)
        {
            //Debug.Log("Grass: " + (Vector3.Distance(player.transform.position, vertices[i]) < 100));
            if (grassBlades[i] == placeHolder)
            {
                if (Vector3.Distance(player.transform.position, (verticesIdx[i] + meshGens[idx].transform.position)) < 5)
                {
                    //Vector3 alteredDomain = new Vector3((vertices[i].x/2) + player.transform.position.x/3, vertices[i].y, (vertices[i].z/2) + player.transform.position.z/3);
                    grassBlades[i] = GameObject.Instantiate(grassBlade);
                    grassBlades[i].transform.position = verticesIdx[i] + meshGens[idx].transform.position;
                    grassBlades[i].transform.eulerAngles = new Vector3(0, Random.Range(-360f, 360f), 0);
                    grassBlades[i].transform.localScale = new Vector3(1f, 1f, 1f);
                }
            }
            else
            {
                if (Vector3.Distance(player.transform.position, (verticesIdx[i] + meshGens[idx].transform.position)) > 5)
                {
                    Destroy(grassBlades[i]);
                }
            }
        }
    }*/
}
