using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    public int xSize;
    public int zSize;
    public float scale;

    public void GenerateMap() 
    {
        float[,] noiseMap = NoiseGen.GenerateNoiseMap(xSize, zSize, scale);


        MapDisplay display = FindObjectOfType<MapDisplay>();
        display.DrawNoiseMap(noiseMap);
    }
}
