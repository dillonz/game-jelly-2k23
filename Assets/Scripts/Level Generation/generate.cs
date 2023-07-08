using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generate : MonoBehaviour
{
    public int Width;
    public int Height;
    public int WalkLength;
    public int NumWalks;
    const int roomHeight = 12;
    const int roomWidth = 20;

    public GameObject[] Rooms;
    public GameObject[] VertWalls;
    public GameObject[] VertDoors;
    public GameObject[] HorzWalls;
    public GameObject[] HorzDoors;

    private bool[,] roomMap;


    private void Start()
    {
        runGeneration();
    }

    public void runGeneration()
    {
        //generateMap();
        //printMap();
        
        generateRooms();
        generateWalls();
    }

    private void generateMap()
    {
        roomMap = new bool[Width, Height];

        int x = 0, y = 0;
        for (int i = 0; i < NumWalks; i++)
        {
            for (int j = 0; j < WalkLength; j++)
            {
                roomMap[x, y] = true;
                nextStep(ref x, ref y);
            }
            x = getRand(roomWidth);
            y = getRand(roomHeight);
        }
    }

    private void nextStep(ref int x, ref int y)
    {
        bool[] possiblities = new bool[] { x > 0, x < Width, y > 0, y < Height };
        int rand;
        do
        {
            rand = getRand(4);
        } while (!possiblities[rand]);
        
        if (rand == 0)
            x -= 1;
        if (rand == 1)
            x += 1;
        if (rand == 2)
            y -= 1;
        if (rand == 3)
            y += 1;
    }

    private void printMap()
    {
        for (int i = 0 ;i < Width;i++) 
        { 
            for (int j = 0; j < Height; j++) 
            {
                Debug.Log(roomMap[i, j] ? "X" : " ");
            }
        }
    }

    private void generateRooms()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Instantiate(Rooms[getRand(Rooms.Length)], new Vector3(x * roomWidth, y * roomHeight, 0), Quaternion.identity);
            }
        }
    }

    private void generateWalls()
    {
        for (int x = 0; x < Width + 1; x++)
        {
            for (int y = 0; y < Height + 1; y++)
            {
                
                bool isVertWall = (x == 0 || x == Width); // ? true : Random.value > 0.5;
                bool isHorzWall = (y == 0 || y == Height); // ? true : Random.value > 0.5;
                if (y < Height)
                {
                    if (isVertWall)
                        Instantiate(VertWalls[getRand(VertWalls.Length)], new Vector3(x * roomWidth, y * roomHeight, 0), Quaternion.identity);
                    else
                        Instantiate(VertDoors[getRand(VertDoors.Length)], new Vector3(x * roomWidth, y * roomHeight, 0), Quaternion.identity);
                }
                if (x < Width)
                {
                    if (isHorzWall)
                        Instantiate(HorzWalls[getRand(HorzWalls.Length)], new Vector3(x * roomWidth, y * roomHeight, 0), Quaternion.identity);
                    else
                        Instantiate(HorzDoors[getRand(HorzDoors.Length)], new Vector3(x * roomWidth, y * roomHeight, 0), Quaternion.identity);
                }
            }
        }
    }

    private int getRand(int range)
    {
        return (int) Random.Range(0, range);
    }

}
