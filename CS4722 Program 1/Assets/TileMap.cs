using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{

    public TileType[] tileTypes;

    int[,] Tiles;
    Node[,] graph;

    int mapSizeX = 3;
    int mapSizeY = 3;

    // Use this for initialization
    void Start()
    {
        Tiles = new int[mapSizeX, mapSizeY];

        int x, y;

        for (x = 0; x < mapSizeX; x++)
        {
            for (y = 0; y < mapSizeX; y++)
            {
                Tiles[x, y] = 0;
            }
        }
        GenerateMap();
    }

    class Node
    {
        public List<Node> neighbors;
        public int x;
        public int y;

        public Node()
        {
            neighbors = new List<Node>();
        }
    }

    void GeneratePathFinding()
    {
        graph = new Node[mapSizeX, mapSizeY];
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeX; y++)
            {
                graph[x, y] = new Node();
                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }

                for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeX; y++)
            {
                if (x > 0)
                    graph[x, y].neighbors.Add(graph[x - 1, y]);
                if (x < mapSizeX - 1)
                    graph[x, y].neighbors.Add(graph[x + 1, y]);
                if (y > 0)
                    graph[x, y].neighbors.Add(graph[x, y - 1]);
                if (y < mapSizeY - 1)
                    graph[x, y].neighbors.Add(graph[x, y + 1]);
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
                Instantiate(tt.CubePrefab, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
    }
}
