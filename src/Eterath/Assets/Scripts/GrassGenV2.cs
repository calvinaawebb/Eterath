using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassGenV2 : MonoBehaviour
{
    public List<GameObject> grassBlades = new List<GameObject>();
    public GameObject[] meshGens;
    public Transform chunkGenerator;
    public GameObject grassBlade;
    public Vector3[] vertices;
    public DimensionalMapGen mapGen;
    public ChunkGen chunkGen;
    public float grassRotx;
    public float grassRotz;
    // Start is called before the first frame update
    void Start()
    {
        meshGens = new GameObject[chunkGen.chunkAmount * chunkGen.chunkAmount];
        Mesh overallMesh = new Mesh();
        int j = 0;
        foreach (Transform child in chunkGenerator)
        {
            meshGens[j] = child.gameObject;
            j++;
        }
        Debug.Log("j" + j);

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

                if (i == 0)
                {
                    transform.GetComponent<MeshFilter>().sharedMesh = grassBlades[^1].GetComponentsInChildren<MeshFilter>()[0].sharedMesh;
                }
                else 
                {
                    Mesh transistionMesh = new Mesh();
                    CombineInstance[] combine = new CombineInstance[2];
                    combine[0].mesh = grassBlades[^1].GetComponentsInChildren<MeshFilter>()[0].sharedMesh;
                    combine[0].transform = grassBlades[^1].GetComponentsInChildren<MeshFilter>()[0].transform.localToWorldMatrix;
                    combine[1].mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
                    combine[1].transform = gameObject.GetComponent<MeshFilter>().transform.localToWorldMatrix;
                    transistionMesh.CombineMeshes(combine);
                    overallMesh = transistionMesh;
                    transform.GetComponent<MeshFilter>().sharedMesh = overallMesh;
                    Destroy(grassBlades[^1]);
                } 
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
