using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour
{
    
    public TileType[] tileTypes;

    int[,] Tiles;
    Node[,] graph;

    public int mapSizeX = 5;
    public int mapSizeY = 5;

    // Use this for initialization
    void Start()
    {
        GenerateMapData();
        GenerateMap();
    }
        

    void GenerateMapData()
    {
        Tiles = new int[mapSizeX, mapSizeY];

        int x, y;

        for (x = 0; x < mapSizeX; x++)
        {
            for (y = 0; y < mapSizeX; y++)
            {
                Tiles[x, y] = Random.Range(0, 2);
            }
        }
    }
    void GenerateMap()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeX; y++)
            {
                TileType tt = tileTypes[Tiles[x, y]];
                GameObject go = Instantiate(tt.CubePrefab, new Vector3(x, y, 0), Quaternion.identity);
               // ClickableTile ct = go.GetComponent<ClickableTile>();
                //ct.tileX = x;
                //ct.tileY = y;
                //ct.map = this;
            }
        }
    }
}
