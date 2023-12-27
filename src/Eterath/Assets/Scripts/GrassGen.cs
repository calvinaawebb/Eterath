using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassGen : MonoBehaviour
{
    public GameObject[] grassBlades;
    public GameObject[] meshGens;
    public Transform chunkGenerator;
    public GameObject grassBlade;
    public GameObject player;
    public Vector3[] vertices;
    public PlayerController playerCont;
    public DimensionalMapGen mapGen;
    public ChunkGen chunkGen;

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
    }

    void Update() 
    {
        int shortestDist = 0;
        GameObject shortestDistOBJ;
        for(int k=0;k<meshGens.Length;k++) 
        {
            if(k == 0) 
            {
                shortestDist = 0;
            } 
            else if(Vector3.Distance(meshGens[k].transform.position, player.transform.position) < Vector3.Distance(meshGens[shortestDist].transform.position, player.transform.position)) 
            {
                shortestDist = k;
            }
        }
        //Debug.Log("ok????: " + shortestDist + " grass arry length: " + grassBlades.Length + " vertices array length: " + vertices.Length);
        mapGen = meshGens[shortestDist].GetComponent<DimensionalMapGen>();
        vertices = mapGen.vertices;
        grassBlades = new GameObject[vertices.Length];
        for(int i=0;i<vertices.Length;i++) 
        {
            //Debug.Log("Grass: " + (Vector3.Distance(player.transform.position, vertices[i]) < 100));
            if(Vector3.Distance(player.transform.position, vertices[i]) < 5) 
            {
                grassBlades[i] = GameObject.Instantiate(grassBlade);
                grassBlades[i].transform.position = vertices[i] + meshGens[shortestDist].transform.position;
            }
        }
    }   
}
