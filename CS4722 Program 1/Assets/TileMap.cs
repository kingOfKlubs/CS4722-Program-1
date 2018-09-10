 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public GameObject selectedUnit;

    

    public TileType[] tileTypes;

    int[,] Tiles;
    Node[,] graph;

    int mapSizeX = 5;
    int mapSizeY = 5;

    // Use this for initialization
    void Start()
    {
        selectedUnit.GetComponent<Unit>().tileX = (int)selectedUnit.transform.position.x;
        selectedUnit.GetComponent<Unit>().tileY = (int)selectedUnit.transform.position.y;
        selectedUnit.GetComponent<Unit>().map = this;


        GenerateMapData();
        GeneratePathFinding();
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
                Tiles[x, y] = 0;
            }
        }
    }

    public class Node
    {
        public List<Node> neighbors;
        public int x;
        public int y;

        public Node()
        {
            neighbors = new List<Node>();
        }

        public float DistanceTo(Node n)
        {
            return Vector2.Distance(new Vector2(x, y), new Vector2(n.x, n.y));
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
                GameObject go = Instantiate(tt.CubePrefab, new Vector3(x, y, 0), Quaternion.identity);
                ClickableTile ct = go.GetComponent<ClickableTile>();
                ct.tileX = x;
                ct.tileY = y;
                ct.map = this;
            }
        }
    }

    public Vector3 TileCoordToWorldCoord(int x, int y)
    {
        return new Vector3(x, y, 0);
    }

    //public void MoveSelectedUnitTo(int x, int y)
    //{
    //    selectedUnit.GetComponent<Unit>().tileX = x;
    //    selectedUnit.GetComponent<Unit>().tileY = y;
    //}

    public void GeneratePathTo(int x, int y)
    {
        selectedUnit.GetComponent<Unit>().currentPath = null;

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        List<Node> unvisted = new List<Node>();

        Node source = graph[selectedUnit.GetComponent<Unit>().tileX,selectedUnit.GetComponent<Unit>().tileY];
        Node target = graph[x, y];

        dist[source] = 0;
        prev[source] = null;

        foreach (Node v in graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }
            unvisted.Add(v);
        }

        while (unvisted.Count > 0)
        {
            Node u = null;
            foreach (Node possibleU in unvisted)
            {
                if (u == null || dist[possibleU] < dist[u])
                    u = possibleU;
            }
            if (u == target)
            {
                break;
            }
            unvisted.Remove(u);

            foreach(Node v in u.neighbors)
            {
                float alt = dist[u] + u.DistanceTo(v);
                if(alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        if (prev[target] == null)
        {
            return;
        }

        List<Node> currentPath = new List<Node>();

        Node curr = target;

        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        currentPath.Reverse();

        selectedUnit.GetComponent<Unit>().currentPath = currentPath;
    }
}
