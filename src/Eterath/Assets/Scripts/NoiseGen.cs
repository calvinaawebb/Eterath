using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NoiseTest;

public static class NoiseGen
{
    public static double[,] GenerateNoiseMap(int xSize, int zSize, int seed, double scale, int octaves, double persistance, double lacunarity, Vector2 offset) 
    {
        OpenSimplexNoise noise2d = new OpenSimplexNoise(seed);
        double[,] noiseMap = new double[xSize+1,zSize+1];

        System.Random prng = new System.Random(seed);
        Vector2[] octavesOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++) 
        {
            double offsetX = prng.Next(-100000, 100000) + offset.x;
            double offsetZ = prng.Next(-100000, 100000) + offset.y;
            octavesOffsets[i] = new Vector2((float)offsetX, (float)offsetZ);
        }

        if (scale <= 0) 
        {
            scale = 0.001f;
        }

        double maxNoiseHeight = double.MinValue;
        double minNoiseHeight = double.MaxValue;

        double halfWidth = xSize / 2;
        double halfHeight = zSize / 2;

        for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                double amplitude = 1;
                double frequency = 1;
                double noiseHeight = 0f;
                for (int i = 0; i < octaves; i++) 
                {
                    //double sampleX = (x-halfWidth) / scale * frequency + octavesOffsets[i].x;
                    //double sampleZ = (z-halfHeight) / scale * frequency + octavesOffsets[i].y;

                    double sampleX = ((x-halfWidth) / scale + octavesOffsets[i].x) * frequency;
                    double sampleZ = ((z-halfHeight) / scale + octavesOffsets[i].y) * frequency;

                    double perlinValue = noise2d.Evaluate(sampleX, sampleZ) * 2 - 1;
                    //double perlinValue = (double)Mathf.PerlinNoise((float)sampleX, (float)sampleZ) * 2 - 1;
                    //cool: double perlinValue = (double)Mathf.PerlinNoise(Mathf.PerlinNoise((float)sampleX, (float)sampleZ), Mathf.PerlinNoise((float)sampleX, (float)sampleZ)) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }
                if(noiseHeight > maxNoiseHeight) {
                    maxNoiseHeight = noiseHeight;
                } else if (noiseHeight < minNoiseHeight) {
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[x,z] = noiseHeight;
            }
        }

        for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                //noiseMap[x,z] = Lerp(minNoiseHeight,maxNoiseHeight,noiseMap[x,z]);
            }
        }
        return noiseMap;
    }

    public static double Lerp(double firstFloat, double secondFloat, double by)
    {
        return (by - firstFloat) / (secondFloat - firstFloat);
    }
}
