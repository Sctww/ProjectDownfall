using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    #region Map Node Fields
    [Header("Map Generation Stuff")]
    public TileType[] tileTypes;
    public int[,] tiles;
    Node[,] graph;
    public int mapSizeX;
    public int mapSizeY;
    #endregion

    void Start()
    {
        GenerateMapData();
        GeneratePathfindingGraph();
        GenerateMapVisuals();
    }

    void GenerateMapData()
    {
        tiles = new int[mapSizeX, mapSizeY];

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                tiles[x, y] = 0;
            }
        }
    }

    public class Node
    {
        public List<Node> neighbours;
        public int x;
        public int y;

        public Node()
        {
            neighbours = new List<Node>();
        }

        public float DistanceTo(Node n)
        {
            return Vector2.Distance(
                new Vector2(x, y),
                new Vector2(n.x, n.y)
                );
        }
    }



    void GeneratePathfindingGraph()
    {
        graph = new Node[mapSizeX, mapSizeY];

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                graph[x, y] = new Node();
                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {

                if (x > 0)
                    graph[x, y].neighbours.Add(graph[x - 1, y]);
                if (x < mapSizeX - 1)
                    graph[x, y].neighbours.Add(graph[x + 1, y]);
                if (y > 0)
                    graph[x, y].neighbours.Add(graph[x, y - 1]);
                if (y < mapSizeY - 1)
                    graph[x, y].neighbours.Add(graph[x, y + 1]);
            }
        }
    }


    void GenerateMapVisuals()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                TileType tt = tileTypes[tiles[x, y]];

                Instantiate(tt.tilePrefab, new Vector3(x, 0, y), Quaternion.identity);
            }
        }
    }

    void GeneratePathTo(int x, int y)
    {
        //currentPath = null;

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        List<Node> unvisited = new List<Node>();

        Node source = graph[0, 0]; //Make the PlayerBase a coordinate here pls
        Node target = graph[0, 0];

        dist[source] = Mathf.Infinity;
        prev[source] = null;

        foreach (Node v in graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }

            unvisited.Add(v);
        }

        while (unvisited.Count > 0)
        {
            Node u = null;
            foreach (Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (u == target)
            {
                break;
            }

            unvisited.Remove(u);

            foreach (Node v in u.neighbours)
            {
                float alt = dist[u] + u.DistanceTo(v);
                if (alt < dist[v])
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

        Node current = target;
        while (current != null)
        {
            currentPath.Add(current);
            current = prev[current];
        }

        currentPath.Reverse();


    }

}