using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;

public class LevelGenerator : MonoBehaviour
{
    public int Width;
    public int Height;
    public int RoomsToAdd;
    const int roomHeight = 12;
    const int roomWidth = 20;
    
    public Transform PlayerToMove;
    public NavMeshAgent HeroToMove;

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
        spawnStuff();
    }

    public void runGeneration()
    {
        generateMap();
        generateRooms();
        generateWalls();

        Surface2D.BuildNavMesh();
    }

    public void spawnStuff()
    {
        PlayerToMove.position = new Vector3(Width * roomWidth / 2f, Height * roomHeight / 2f - 5, -0.01f);
        Vector2Int farthest = getFarthestRoom();
        HeroToMove.Warp(new Vector2(farthest.x * roomWidth / 2f + 3, farthest.y * roomHeight / 2f - 5));
    }

    public Vector2Int getFarthestRoom()
    {
        Vector2Int farthest = new Vector2Int(Width / 2, Height / 2);
        float farthestDist = 0;
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (!roomMap[x, y]) continue;

                float currDist = (farthest.x - x) * (farthest.x - x) + (farthest.y - y) * (farthest.y - y);
                if (currDist > farthestDist)
                {
                    farthestDist = currDist;
                    farthest = new Vector2Int(x, y);
                }
            }
        }
        return farthest;
    }

    private void generateMap()
    {
        roomMap = new bool[Width, Height];
        doors = new List<Tuple<Vector2Int, Vector2Int>>();
        List<Vector2Int> neighbors = new List<Vector2Int>();
        List<Vector2Int> neighborsParents = new List<Vector2Int>();
        Vector2Int current = new Vector2Int(Width / 2, Height / 2);

        for (int i = 0; i < RoomsToAdd; i++)
        {
            roomMap[current.x, current.y] = true;
            addNeighbors(current, neighbors, neighborsParents);
            int rand = getRand(neighbors.Count);
            current = neighbors[rand];
            // Don't question this
            doors.Add(Tuple.Create(current, neighborsParents[rand]));
            doors.Add(Tuple.Create(neighborsParents[rand], current));
            neighbors.RemoveAt(rand);
            neighborsParents.RemoveAt(rand);
        }
        
    }

    private void addNeighbors(Vector2Int current, List<Vector2Int> neighbors, List<Vector2Int> neighborsParents)
    {
        int x = current.x;
        int y = current.y;
        if (x > 0)
        {
            if (!roomMap[x-1, y] && !neighbors.Contains(new Vector2Int(x-1, y)))
            {
                neighbors.Add(new Vector2Int(x - 1, y));
                neighborsParents.Add(new Vector2Int(x, y));
            }
        }
        if (x < Width -1)
        {
            if (!roomMap[x + 1, y] && !neighbors.Contains(new Vector2Int(x + 1, y)))
            {
                neighbors.Add(new Vector2Int(x + 1, y));
                neighborsParents.Add(new Vector2Int(x, y));
            }
        }
        if (y > 0)
        {
            if (!roomMap[x, y - 1] && !neighbors.Contains(new Vector2Int(x, y - 1)))
            {
                neighbors.Add(new Vector2Int(x, y - 1));
                neighborsParents.Add(new Vector2Int(x, y));
            }
        }
        if (y < Height - 1)
        {
            if (!roomMap[x, y + 1] && !neighbors.Contains(new Vector2Int(x, y + 1)))
            {
                neighbors.Add(new Vector2Int(x, y + 1));
                neighborsParents.Add(new Vector2Int(x, y));
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
                var door = Tuple.Create(new Vector2Int(x - 1, y), new Vector2Int(x, y));
                if (roomMap[x - 1, y] && roomMap[x, y] && doors.Contains(door))
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
                var door = Tuple.Create(new Vector2Int(x, y - 1), new Vector2Int(x, y));
                if (roomMap[x, y - 1] && roomMap[x, y] && doors.Contains(door))
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
