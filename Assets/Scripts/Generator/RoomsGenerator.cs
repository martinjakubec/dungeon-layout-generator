using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CardinalDirection
{
    N,
    W,
    S,
    E
}

public class Coordinates
{
    public int x;
    public int y;

    public Coordinates(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public class Room
{
    private Room[,] grid;
    private int xPos;
    private int yPos;
    private bool isStartRoom = false;
    private CardinalDirection startDirection;
    private List<CardinalDirection> exitDirections;

    public Room(Room[,] grid, Coordinates spawnCoordinates, bool isStart = false)
    {
        this.grid = grid;
        xPos = spawnCoordinates.x;
        yPos = spawnCoordinates.y;
        CreateExits();
    }

    public Coordinates GetRoomCoordinates()
    {
        return new Coordinates(xPos, yPos);
    }

    public void CreateExits()
    {
        int numberOfExits;
        int random = UnityEngine.Random.Range(0, 100);
        if (random > 90)
        {
            numberOfExits = 3;
        }
        else if(random > 75)
        {
            numberOfExits = 2;
        }
        else
        {
            numberOfExits = 1;
        }

        if (!isStartRoom)
        {

            var directions = Enum.GetValues(typeof(CardinalDirection));
            var usedIndeces

            for(int i = 0; i < numberOfExits; i++)
            {

            }

            foreach(int i in directions)
            {
                Debug.Log(i);
            }
        }
    }
}


public class RoomsGenerator : MonoBehaviour
{
    [SerializeField] int gridWidth;
    [SerializeField] int gridHeigth;

    private Room[,] grid;


    void Start()
    {
        DontDestroyOnLoad(gameObject);
        grid = new Room[gridHeigth, gridWidth];
        SpawnStartRoom();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
    }

    private Coordinates GetNeighborCoordinates(CardinalDirection direction, Coordinates coordinates)
    {

        switch (direction)
        {
            case CardinalDirection.N:
                if (coordinates.x - 1 < 0)
                {
                    return null;
                }
                else
                {
                    return new Coordinates(coordinates.x - 1, coordinates.y);
                }
            case CardinalDirection.S:
                Debug.Log($"{coordinates.x + 1} {coordinates.y}");
                if (coordinates.x + 1 >= grid.GetLength(0))
                {
                    return null;
                }
                else
                {
                    return new Coordinates(coordinates.x + 1, coordinates.y);
                }
            case CardinalDirection.W:
                Debug.Log($"{coordinates.x} {coordinates.y - 1}");
                if (coordinates.y - 1 < 0)
                {
                    return null;
                }
                else
                {
                    return new Coordinates(coordinates.x, coordinates.y - 1);
                }
            case CardinalDirection.E:
                Debug.Log($"{coordinates.x} {coordinates.y + 1}");
                if (coordinates.y + 1 >= grid.GetLength(1))
                {
                    return null;
                }
                else
                {
                    return new Coordinates(coordinates.x, coordinates.y + 1);
                }
            default:
                return null;
        }

    }

    void GenerateRooms()
    {
    }

    void SpawnStartRoom()
    {
        Coordinates startRoomSpawn = GetRandomGridCoordinates();
        grid[startRoomSpawn.x, startRoomSpawn.y] = new Room(grid, startRoomSpawn);
    }

    void SpawnNextRoom()
    {

    }

    Coordinates GetRandomGridCoordinates()
    {
        int randomX = UnityEngine.Random.Range(0, gridHeigth);
        int randomY = UnityEngine.Random.Range(0, gridWidth);
        Coordinates randomSpawn = new Coordinates(randomX, randomY);
        return randomSpawn;
    }

}
