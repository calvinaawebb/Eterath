using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class DimensionalMapGen : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    Vector3 origin;
    int[] triangles;

    public string biome;
    public int xbound = 100;
    public int zbound = 100;
    public double y;
    public Material color;

    public float amplitude;
    public float frequency;
    public float amplitude_val;
    public float frequency_val;
    public int N_OCTAVES;
    public float scale;
    public float sampleX;
    public float sampleZ;
    
    public int xSize;
    public int zSize;

    public int octaves;
    [Range(0,1)]
    public double persistance;
    public double lacunarity;

    public int seed;
    public Vector2 offset;

    public bool autoUpdate;

    public TerrainType[] regions;

    public double[,] noiseMap;

    // Start is called before the first frame update
    public void GenDMap()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = color;

        xSize = xbound;
        zSize = zbound;

        CreateShape(origin = new Vector3(0, 0, 0));
        //CreateShape(x*2, z*2, origin = new Vector3(0+x+10, 0, 0+z+10));
        //CreateShape(x * 4, z * 4, origin = new Vector3(0 + x + 20, 0, 0 + z + 20));
        UpdateMesh();
    }

    public void CreateShape(Vector3 location) 
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        noiseMap = NoiseGen.GenerateNoiseMap(xSize, zSize, seed, scale, octaves, persistance, lacunarity, offset);

        Debug.Log(noiseMap.Length);
        Debug.Log(vertices.Length);

        int i = 0;
        for (int z = 0; z <= zSize; z++) 
        {
            for (int x = 0; x <= xSize; x++) 
            {
                frequency = frequency_val;
                amplitude = amplitude_val;
                y = location.y;
                // if (noiseMap[x,z] >= 0.9) 
                // {
                //     y += noiseMap[x,z] * amplitude * ((1.5f-(0.5f-noiseMap[x,z])));
                // } else 
                // {
                //     y += noiseMap[x,z] * amplitude;
                // }
                // Excentuate divits in terrain: y += noiseMap[x,z] * amplitude * (1-noiseMap[x,z]*2);
                y += noiseMap[x,z] * amplitude; //* (1+noiseMap[x,z]);
                vertices[i] = new Vector3(x, (float)y, z) + location;
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;
        for (int z = 0; z < zSize; z++) 
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }  
    }

    public void UpdateMesh() 
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        if (scale <= 0)
        {
            scale = 0.0001f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
