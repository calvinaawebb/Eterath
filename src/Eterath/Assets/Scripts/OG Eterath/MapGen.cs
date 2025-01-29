using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    public enum DrawMode {NoiseMap, ColorMap}
    public DrawMode drawMode;

    public int xSize;
    public int zSize;
    public float scale;

    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool autoUpdate;

    public TerrainType[] regions;

    public double[,] noiseMap;

    public void GenerateMap() 
    {
        noiseMap = NoiseGen.GenerateNoiseMap(xSize, zSize, seed, scale, octaves, persistance, lacunarity, offset);

        Color[] colorMap = new Color[xSize * zSize];
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++) 
            {
                double currentHeight = noiseMap[x,z];
                for (int i = 0; i < regions.Length; i++) 
                {
                    if(currentHeight <= regions[i].height) 
                    {
                        colorMap[z * xSize + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.NoiseMap) 
        {
            display.DrawTexture(TextureGen.TextureFromHeightMap(noiseMap));
        } else if (drawMode == DrawMode.ColorMap) 
        {
            display.DrawTexture(TextureGen.TextureFromColorMap(colorMap, xSize, zSize));
        }
        //display.DrawTexture(noiseMap);
    }

    void OnValidate() 
    {
        if(xSize < 1) 
        {
            xSize = 1;
        }
        if(zSize < 1) 
        {
            zSize = 1;
        }
        if(lacunarity < 1) 
        {
            lacunarity = 1;
        }
        if(octaves < 0) 
        {
            octaves = 0;
        }
    }
}

[System.Serializable]
public struct TerrainType {
    public string name;
    public float height;
    public Color color; 
}
