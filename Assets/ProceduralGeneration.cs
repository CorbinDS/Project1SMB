using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    public GameObject[] prefabs;
    public int rows;
    public int columns;
    private int[,] prefabIndexBoard;

    [System.Serializable]
    public class bannedConnections
    {
        public string start;
        public string[] banned;
    }

    [SerializeField] bannedConnections[] rules;

    public Dictionary<string, string[]> badConnections;
    // Start is called before the first frame update
    void Start()
    {
        if (rows == 0) rows = 3;
        if (columns == 0) columns = 3;
        prefabIndexBoard = new int[rows, columns];
        Debug.Log(prefabs[0] + " " + prefabs[0].name);
        // Instantiate(prefabs[0], new Vector3(0, 0, 0), Quaternion.identity);
    }

    void WaveFunctionCollapse()
    {
        bool noneEmpty = true;
        while (noneEmpty)
        {
            int rowIndex = Random.Range(0, rows);
            int colIndex = Random.Range(0, columns);
        }



    }

    void InstantiateMap()
    {
        foreach (var pI in prefabIndexBoard)
        {
            Instantiate(prefabs[pI], new Vector3(0, pI, 0), Quaternion.identity);
        }
    }
}
