using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using NavMeshPlus.Components;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public int Width;
    public int Height;
    public int RoomsToAdd;
    const int roomHeight = 12;
    const int roomWidth = 20;
    
    public GameObject PlayerToMove;
    public GameObject HeroToMove;

    public NavMeshSurface Surface2D;
    public GameObject[] Rooms;
    public GameObject[] VertWalls;
    public GameObject[] VertDoors;
    public GameObject[] HorzWalls;
    public GameObject[] HorzDoors;

    private bool[,] roomMap;
    private List<Tuple<Vector2Int, Vector2Int>> doors;

    private void Start()
    {
        runGeneration();

        PlayerToMove.transform.position = new Vector3(Width * roomWidth / 2f + 5, Height * roomHeight / 2f - 5, 0);
        HeroToMove.transform.position = new Vector3(Width * roomWidth / 2f + 10, Height * roomHeight / 2f - 10, 0);
    }

    public void runGeneration()
    {
        generateMap();
        generateRooms();
        generateWalls();

        Surface2D.BuildNavMesh();
    }

    private void generateMap()
    {
        roomMap = new bool[Width, Height];
        List<Vector2Int> neighbors = new List<Vector2Int>();
        int x = Width / 2, y = Height /2;

        for (int i = 0; i < RoomsToAdd; i++)
        {
            roomMap[x, y] = true;
            addNeighbors(x, y, neighbors);
            int rand = getRand(neighbors.Count);
            x = neighbors[rand].x;
            y = neighbors[rand].y;
            neighbors.RemoveAt(rand);
        }
        
    }

    private void addNeighbors(int x, int y, List<Vector2Int> neighbors)
    {
        if (x > 0)
        {
            if (!roomMap[x-1, y] && !neighbors.Contains(new Vector2Int(x-1, y)))
            {
                neighbors.Add(new Vector2Int(x - 1, y));
            }
        }
        if (x < Width -1)
        {
            if (!roomMap[x + 1, y] && !neighbors.Contains(new Vector2Int(x + 1, y)))
            {
                neighbors.Add(new Vector2Int(x + 1, y));
            }
        }
        if (y > 0)
        {
            if (!roomMap[x, y - 1] && !neighbors.Contains(new Vector2Int(x, y - 1)))
            {
                neighbors.Add(new Vector2Int(x, y - 1));
            }
        }
        if (y < Height - 1)
        {
            if (!roomMap[x, y + 1] && !neighbors.Contains(new Vector2Int(x, y + 1)))
            {
                neighbors.Add(new Vector2Int(x, y + 1));
            }
        }
    }

    private void generateRooms()
    {
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                if (roomMap[x, y])
                    Instantiate(Rooms[getRand(Rooms.Length)], new Vector3(x * roomWidth, y * roomHeight, 0), Quaternion.identity);
    }

    private void generateWalls()
    {
        int vertWall = 0, horzWall = 0;
        for (int x = 0; x < Width + 1; x++)
        {
            for (int y = 0; y < Height + 1; y++)
            {
                
                wallOrDoor(x, y, ref vertWall, ref horzWall);

                if (y < Height)
                {
                    if (vertWall == 1)
                        Instantiate(VertWalls[getRand(VertWalls.Length)], new Vector3(x * roomWidth, y * roomHeight, 0), Quaternion.identity);
                    else if (vertWall == 2)
                        Instantiate(VertDoors[getRand(VertDoors.Length)], new Vector3(x * roomWidth, y * roomHeight, 0), Quaternion.identity);
                }
                if (x < Width)
                {
                    if (horzWall == 1)
                        Instantiate(HorzWalls[getRand(HorzWalls.Length)], new Vector3(x * roomWidth, y * roomHeight, 0), Quaternion.identity);
                    else if (horzWall == 2)
                        Instantiate(HorzDoors[getRand(HorzDoors.Length)], new Vector3(x * roomWidth, y * roomHeight, 0), Quaternion.identity);
                }
            }
        }
    }

    private void wallOrDoor(int x, int y, ref int vertWall, ref int horzWall)
    {
        vertWall = 0;
        horzWall = 0;

        if (x == Width && y == Height) return;

        if (y < Height && x < Width)
        {
            if (roomMap[x, y])
            {
                vertWall = 1;
                horzWall = 1;
            }
            if (x > 0)
            {
                if (roomMap[x - 1, y])
                {
                    vertWall = 1;
                }
                if (roomMap[x - 1, y] && roomMap[x, y])
                {
                    vertWall = 2;
                }
            }
            if (y > 0)
            {
                if (roomMap[x, y - 1])
                {
                    horzWall = 1;
                }
                if (roomMap[x, y - 1] && roomMap[x, y])
                {
                    horzWall = 2;
                }
            }
        }

        if (y == Height)
            if (roomMap[x, y - 1])
                horzWall = 1;
        if (x == Width)
            if (roomMap[x - 1, y])
                vertWall = 1;

    }

    private int getRand(int range)
    {
        return (int) UnityEngine.Random.Range(0, range);
    }

}
