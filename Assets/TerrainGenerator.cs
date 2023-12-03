
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int height = 20;
    public int width = 256;
    public int length = 256;

    public float scale = 20f;

    public float offsetX = 100f;
    public float offsetY = 100f;

    void Start()
    {
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);
    }
    public void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
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

}
