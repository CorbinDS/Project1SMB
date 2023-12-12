using UnityEngine;
using System.Collections.Generic;
public class TerrainGenerator : MonoBehaviour
{
    public int height = 20;
    public int width = 256;
    public int length = 256;

    public float scale = 20f;

    public float offsetX = 100f;
    public float offsetY = 100f;

    public GameObject coinPrefab;
    public float minHeight;
    public float minDistance = 5f; // Minimum distance between coins


    private List<Vector3> placedCoins;
    void Start()
    {
        placedCoins = new List<Vector3>();
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);

        PlacePrefabs(terrain.terrainData);

    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, height, length);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, length];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }
        return heights;
    }

    float CalculateHeight(int x, int y)
    {

        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / length * scale + offsetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }

    void PlacePrefabs(TerrainData tData)
    {
        Vector3 terrainSize = tData.size;
        int heightmapWidth = tData.heightmapResolution;
        int heightmapLength = tData.heightmapResolution;

        for (int x = 0; x < heightmapWidth; x++)
        {
            for (int y = 0; y < heightmapLength; y++)
            {
                float normX = x / (float)heightmapWidth;
                float normY = y / (float)heightmapLength;

                float height = tData.GetHeight(x, y);

                if (height > minHeight)
                {
                    Vector3 worldPosition = new Vector3(normX * terrainSize.x, height + 1, normY * terrainSize.z);

                    if (FarEnough(worldPosition))
                    {
                        GameObject coin = Instantiate(coinPrefab, worldPosition, Quaternion.identity);
                        // CollectibleController coinCont = coin.GetComponent<CollectibleController>();
                        // coinCont.scoreManager = FindObjectOfType<ScoreManager>();
                        placedCoins.Add(worldPosition);
                    }
                }
            }
        }
    }

    bool FarEnough(Vector3 newPos)
    {
        foreach (Vector3 existingPosition in placedCoins)
        {
            if (Vector3.Distance(newPos, existingPosition) < minDistance)
            {
                return false;
            }
        }
        return true;
    }

}
