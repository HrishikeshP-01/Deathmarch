using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_WorldBuilder : MonoBehaviour
{
    public F_TileGrid hexGrid;

    [Header("Tiles")]
    public GameObject[] hexTilePrefabs;
    public double[] tileProbs;

    [Header("Tile Variables")]
    public int columns, rows;
    public float radius, xOffset = 0, zOffset = 0;

    private Vector3 rowStarterPosition;
    private double[] tileProbLookUpTable;

    enum GridType { Parallelogram, Rectangular, Hexagonal };
    [SerializeField] GridType gridType;

    private float landTiles = 0, forestTiles = 0, waterTiles = 0;

    private void Start()
    {
        hexGrid = new F_TileGrid();

        hexGrid.columns = columns;
        hexGrid.rows = rows;
        hexGrid.radius = radius;
        hexGrid.XOffset = xOffset;
        hexGrid.ZOffset = zOffset;

        rowStarterPosition = this.transform.position;
        MakeTileLookUpTable();

        switch (gridType)
        {
            case GridType.Parallelogram:
                ParallelogramGrid();
                break;
            case GridType.Rectangular:
                RectangularGrid();
                break;
            case GridType.Hexagonal:
                HexagonalGrid();
                break;
        }
    }

    void ParallelogramGrid()
    {
        Vector3 cellPosition = rowStarterPosition;
        System.Random rnd = new System.Random();

        for (int i = 0; i < hexGrid.columns; i++)
        {
            for (int j = 0; j < hexGrid.rows; j++)
            {
                GameObject selectedTile = TileSelector(rnd.NextDouble());
                GameObject hex = Instantiate(selectedTile, position: cellPosition, Quaternion.Euler(0f, 0f, 0f));
                cellPosition = cellPosition + new Vector3(Mathf.Sqrt(3) * hexGrid.radius + hexGrid.XOffset, 0f, 0f);
            }
            rowStarterPosition = rowStarterPosition + new Vector3(Mathf.Sqrt(3) / 2 * hexGrid.radius + hexGrid.XOffset, 0f, 3 / 2 * hexGrid.radius + hexGrid.ZOffset);
            cellPosition = rowStarterPosition;
        }
    }

    void RectangularGrid()
    {
        Vector3 cellPosition = rowStarterPosition;
        System.Random rnd = new System.Random();

        for (int i = 0; i < hexGrid.columns; i++)
        {
            for (int j = 0; j < hexGrid.rows; j++)
            {
                GameObject selectedTile = TileSelector(rnd.NextDouble());
                Instantiate(selectedTile, position: cellPosition, Quaternion.Euler(0f, 0f, 0f));
                cellPosition = cellPosition + new Vector3(Mathf.Sqrt(3) * hexGrid.radius + hexGrid.XOffset, 0f, 0f);
            }
            if (i % 2 == 0)
            {
                rowStarterPosition = rowStarterPosition + new Vector3(Mathf.Sqrt(3) / 2 * hexGrid.radius + hexGrid.XOffset, 0f, 3 / 2 * hexGrid.radius + hexGrid.ZOffset);
            }
            else
            {
                rowStarterPosition = rowStarterPosition + new Vector3(-(Mathf.Sqrt(3) / 2 * hexGrid.radius + hexGrid.XOffset), 0f, 3 / 2 * hexGrid.radius + hexGrid.ZOffset);
            }
            cellPosition = rowStarterPosition;
        }
    }

    void HexagonalGrid()
    {
        Vector3 cellPosition = rowStarterPosition;
        System.Random rnd = new System.Random();

        int k = 0;
        int cellInRow = hexGrid.columns;
        for (int i = 0; i < cellInRow; i++)
        {
            GameObject selectedTile = TileSelector(rnd.NextDouble());
            Instantiate(selectedTile, position: cellPosition, Quaternion.Euler(0f, 0f, 0f));
            cellPosition += new Vector3(Mathf.Sqrt(3) * hexGrid.radius + hexGrid.XOffset, 0f, 0f);
        }
        cellInRow--;
        int cellsInTopRows = cellInRow;
        Vector3 topRowCellPosition = rowStarterPosition + new Vector3(Mathf.Sqrt(3) / 2 * hexGrid.radius + hexGrid.XOffset, 0f, 3 / 2 * hexGrid.radius + hexGrid.ZOffset);
        cellPosition = topRowCellPosition;
        for (int i = hexGrid.columns / 2; i > 0; i--)
        {
            for (int j = 0; j < cellsInTopRows; j++)
            {
                GameObject selectedTile = TileSelector(rnd.NextDouble());
                Instantiate(selectedTile, position: cellPosition, Quaternion.Euler(0f, 0f, 0f));
                cellPosition += new Vector3(Mathf.Sqrt(3) * hexGrid.radius + hexGrid.XOffset, 0f, 0f);
            }
            topRowCellPosition = topRowCellPosition + new Vector3(Mathf.Sqrt(3) / 2 * hexGrid.radius + hexGrid.XOffset, 0f, 3 / 2 * hexGrid.radius + hexGrid.ZOffset);
            cellPosition = topRowCellPosition;
            cellsInTopRows--;
        }
        int cellsInBottomRows = cellInRow;
        Vector3 bottomRowCellPosition = rowStarterPosition + new Vector3(Mathf.Sqrt(3) / 2 * hexGrid.radius + hexGrid.XOffset, 0f, -(3 / 2 * hexGrid.radius + hexGrid.ZOffset));
        cellPosition = bottomRowCellPosition;
        for (int i = hexGrid.columns / 2; i > 0; i--)
        {
            for (int j = 0; j < cellsInBottomRows; j++)
            {
                GameObject selectedTile = TileSelector(rnd.NextDouble());
                Instantiate(selectedTile, position: cellPosition, Quaternion.Euler(0f, 0f, 0f));
                cellPosition += new Vector3(Mathf.Sqrt(3) * hexGrid.radius + hexGrid.XOffset, 0f, 0f);
            }
            bottomRowCellPosition = bottomRowCellPosition + new Vector3(Mathf.Sqrt(3) / 2 * hexGrid.radius + hexGrid.XOffset, 0f, -(3 / 2 * hexGrid.radius + hexGrid.ZOffset)
                );
            cellPosition = bottomRowCellPosition;
            cellsInBottomRows--;
        }
    }

    public GameObject TileSelector(double x)
    {
        for (int i = 0; i < tileProbLookUpTable.Length; i++)
        {
            if (x <= tileProbLookUpTable[i])
            {
                updateTileCount(hexTilePrefabs[i].GetComponent<F_Tile>().tileType);
                return hexTilePrefabs[i];
            }
        }
        return hexTilePrefabs[0];
    }

    public void MakeTileLookUpTable()
    {
        double tot = 0;
        tileProbLookUpTable = new Double[tileProbs.Length];
        for (int i = 0; i < tileProbs.Length; i++)
        {
            tot += tileProbs[i];
            tileProbLookUpTable[i] = tot;
        }
    }

    void updateTileCount(string tileType)
    {
        switch(tileType)
        {
            case "Land":
                landTiles++;
                break;
            case "Water":
                waterTiles++;
                break;
            case "Forest":
                forestTiles++;
                landTiles++;
                break;
        }
    }
}
