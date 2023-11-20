using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class ProceduralTerrainGenOutline : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    Vector3 origin;
    int[] triangles;

    public string biome;
    public int x = 1;
    public int z = 1;
    public float y;
    public Material color;

    public float amplitude;
    public float frequency;
    public int N_OCTAVES;
    public float scale;
    public float sampleX;
    public float sampleZ;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = color;

        CreateShape(x*100, z*100, origin = new Vector3(0, 0, 0));
        //CreateShape(x*2, z*2, origin = new Vector3(0+x+10, 0, 0+z+10));
        //CreateShape(x * 4, z * 4, origin = new Vector3(0 + x + 20, 0, 0 + z + 20));
        UpdateMesh();
    }

    void CreateShape(int xSize , int zSize, Vector3 location) 
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        int i = 0;
        for (int z = 0; z <= zSize; z++) 
        {
            for (int x = 0; x <= xSize; x++) 
            {
                N_OCTAVES = 20;
                frequency = .15f;
                amplitude = 1f;
                sampleX = x / scale * frequency;
                sampleZ = z / scale * frequency;
                y = location.y;
                for (int j = 0; j < N_OCTAVES; j++)
                {
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleZ) * 2 - 1;
                    y += perlinValue * amplitude;
                    amplitude *= 0.5f;
                    frequency *= 2.0f;
                }
                //y = Mathf.PerlinNoise(x * .15f, z * .15f) * 1f;
                if (x > 5 + location.x && x < xSize-5 + location.x && z > 5 + location.z && z < zSize - 5 + location.z)
                {
                    N_OCTAVES = 5;
                    frequency = .15f;
                    amplitude = 2.5f;
                    y = location.y;
                    for (int j = 0; j < N_OCTAVES; j++)
                    {
                        y += Mathf.PerlinNoise(frequency * x, frequency * z) * amplitude;
                        amplitude *= 0.5f;
                        frequency *= 2.0f;
                    }
                }
                if (x > 15 + location.x && x < xSize - 15 + location.x && z > 15 + location.z && z < zSize - 15 + location.z)
                {
                    N_OCTAVES = 5;
                    frequency = 1f;
                    amplitude = 1f;
                    y = location.y;
                    for (int j = 0; j < N_OCTAVES; j++)
                    {
                        y += Mathf.PerlinNoise(frequency * x, frequency * z) * amplitude;
                        amplitude *= 0.5f;
                        frequency *= 2.0f;
                    }
                }
                //float y = Mathf.PerlinNoise(x * .15f, z * .15f) * Mathf.PerlinNoise(x * .25f, z * .25f) * 2f;
                vertices[i] = new Vector3(x, y, z) + location;
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

        /*{
            new Vector3 (0,0,0),
            new Vector3 (0,0,1),
            new Vector3 (1,0,0),
            new Vector3 (1,0,1)
        };

        triangles = new int[]
        {
            0,1,2,
            1,3,2
        };*/

    }

    void UpdateMesh() 
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
