using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseGen
{
    public static float[,] GenerateNoiseMap(int xSize, int zSize, float scale) 
    {
        float[,] noiseMap = new float[xSize,zSize];

        if (scale <= 0) 
        {
            scale = 0.001f;
        }

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                float sampleX = x / scale;
                float sampleZ = z / scale;
                float perlinValue = Mathf.PerlinNoise(sampleX, sampleZ);
                noiseMap[x, z] = perlinValue;
            }
        }
        return noiseMap;
    }
}
