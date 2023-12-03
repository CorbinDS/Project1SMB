using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool filled;
    public Tile[] tileOptions;

    public void CreateCell(bool fill, Tile[] tiles)
    {
        filled = fill;
        tileOptions = tiles;
    }

    public void RecreateCell(Tile[] tiles)
    {
        tileOptions = tiles;
    }
}
