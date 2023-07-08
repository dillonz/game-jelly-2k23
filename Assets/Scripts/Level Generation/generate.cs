using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generate : MonoBehaviour
{
    public int width;
    public int height;
    const int roomHeight = 12;
    const int roomWidth = 20;

    public GameObject[] rooms;
    public GameObject vertWall;
    public GameObject vertDoor;
    public GameObject horzWall;
    public GameObject horzDoor;

    private void Start()
    {
        runGeneration();
    }

    public void runGeneration()
    {
        generateRooms();
        generateWalls();
    }

    private void generateRooms()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int randRoom = (int)Random.Range(0, rooms.Length);
                Instantiate(rooms[randRoom], new Vector3(x * roomWidth, y * roomHeight, 0), Quaternion.identity);
            }
        }
    }

    private void generateWalls()
    {
        for (int x = 0; x < width + 1; x++)
        {
            for (int y = 0; y < height + 1; y++)
            {
                
                bool isVertWall = (x == 0 || x == width); // ? true : Random.value > 0.5;
                bool isHorzWall = (y == 0 || y == height); // ? true : Random.value > 0.5;
                if (y < height)
                {
                    if (isVertWall)
                        Instantiate(vertWall, new Vector3(x * roomWidth, y * roomHeight, 0), Quaternion.identity);
                    else
                        Instantiate(vertDoor, new Vector3(x * roomWidth, y * roomHeight, 0), Quaternion.identity);
                }
                if (x < width)
                {
                    if (isHorzWall)
                        Instantiate(horzWall, new Vector3(x * roomWidth, y * roomHeight, 0), Quaternion.identity);
                    else
                        Instantiate(horzDoor, new Vector3(x * roomWidth, y * roomHeight, 0), Quaternion.identity);
                }
            }
        }
    }

}
