using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WFC : MonoBehaviour
{
    public int dimensions;
    public Tile[] tileObjects;
    public List<Cell> gridComponents;
    public Cell cellObj;
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    int iterations = 0;
    void Awake()
    {
        gridComponents = new List<Cell>();
        InitializeGrid();
    }

    void InitializeGrid()
    {
        for (int z = 0; z < dimensions; z++)
        {
            for (int x = 0; x < dimensions; x++)
            {
                Cell newCell = Instantiate(cellObj, new Vector3(x * 15, 0, z * 15), Quaternion.identity);
                newCell.CreateCell(false, tileObjects);
                gridComponents.Add(newCell);
            }
        }
        StartCoroutine(CheckOptions());
    }

    IEnumerator CheckOptions()
    {
        List<Cell> tempGrid = new List<Cell>(gridComponents);

        tempGrid.RemoveAll(c => c.filled);
        tempGrid.Sort((t1, t2) => { return t1.tileOptions.Length - t2.tileOptions.Length; });
        int arrLength = tempGrid[0].tileOptions.Length;
        int stopIndex = default;

        for (int i = 1; i < tempGrid.Count; i++)
        {
            if (tempGrid[i].tileOptions.Length > arrLength)
            {
                stopIndex = i;
                break;
            }
        }

        if (stopIndex > 0)
        {
            tempGrid.RemoveRange(stopIndex, tempGrid.Count - stopIndex);
        }

        yield return new WaitForSeconds(0.5f);
        FillCell(tempGrid);
    }

    void FillCell(List<Cell> tempGrid)
    {
        int randIndex = UnityEngine.Random.Range(0, tempGrid.Count);

        Cell cellToCollapse = tempGrid[randIndex];
        cellToCollapse.filled = true;
        Tile selectedTile;
        if (cellToCollapse.tileOptions.Length == 0)
        {
            selectedTile = tileObjects[tileObjects.Length - 1];
        }
        else
        {
            selectedTile = cellToCollapse.tileOptions[UnityEngine.Random.Range(0, cellToCollapse.tileOptions.Length)];
        }
        cellToCollapse.tileOptions = new Tile[] { selectedTile };


        Tile foundTile = cellToCollapse.tileOptions[0];
        Instantiate(foundTile, cellToCollapse.transform.position, Quaternion.identity);

        UpdateGeneration();
    }

    void UpdateGeneration()
    {
        List<Cell> newGenerationCell = new List<Cell>(gridComponents);

        for (int y = 0; y < dimensions; y++)
        {
            for (int x = 0; x < dimensions; x++)
            {
                int index = x + y * dimensions;
                Cell currentCell = gridComponents[index];

                if (!currentCell.filled)
                {
                    List<Tile> options = new List<Tile>(tileObjects);

                    // Check constraints from the cell above
                    if (y > 0)
                    {
                        Cell aboveCell = gridComponents[x + (y - 1) * dimensions];
                        options = CheckConstraints(options, aboveCell.tileOptions, Direction.Up);
                    }

                    // Check constraints from the cell to the right
                    if (x < dimensions - 1)
                    {
                        Cell rightCell = gridComponents[x + 1 + y * dimensions];
                        options = CheckConstraints(options, rightCell.tileOptions, Direction.Right);
                    }

                    // Check constraints from the cell below
                    if (y < dimensions - 1)
                    {
                        Cell belowCell = gridComponents[x + (y + 1) * dimensions];
                        options = CheckConstraints(options, belowCell.tileOptions, Direction.Down);
                    }

                    // Check constraints from the cell to the left
                    if (x > 0)
                    {
                        Cell leftCell = gridComponents[x - 1 + y * dimensions];
                        options = CheckConstraints(options, leftCell.tileOptions, Direction.Left);
                    }

                    // Set the updated tile options for the current cell
                    newGenerationCell[index] = currentCell;
                    newGenerationCell[index].RecreateCell(options.ToArray());
                }
            }
        }

        gridComponents = newGenerationCell;
        iterations++;

        if (iterations < dimensions * dimensions)
        {
            StartCoroutine(CheckOptions());
        }
    }

    List<Tile> CheckConstraints(List<Tile> options, Tile[] constraints, Direction direction)
    {
        List<Tile> validOptions = new List<Tile>(options);

        foreach (Tile constraint in constraints)
        {
            // Based on the direction, determine which side to check
            List<Tile> constraintSides = direction switch
            {
                Direction.Up => constraint.downNeighbours,
                Direction.Right => constraint.leftNeighbours,
                Direction.Down => constraint.upNeighbours,
                Direction.Left => constraint.rightNeighbours,
                _ => new List<Tile>(), // Handle other directions as needed
            };

            // Remove options that are not compatible with the constraint
            validOptions.RemoveAll(option => !constraintSides.Contains(option));
        }

        return validOptions;
    }

    void CheckValidity(List<Tile> optionList, List<Tile> validOption)
    {
        optionList.RemoveAll(tile => !validOption.Contains(tile));
    }
}
