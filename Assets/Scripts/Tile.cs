using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Tile> upNeighbours;
    public List<Tile> rightNeighbours;
    public List<Tile> leftNeighbours;
    public List<Tile> downNeighbours;

}
