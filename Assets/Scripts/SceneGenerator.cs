using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;


public class SceneGenerator : MonoBehaviour
{
    int roomWidth;
    int roomHeight;
    [SerializeField] Vector2Int startPoint;
    [SerializeField] Grid grid;

    [SerializeField] int minRoomHeight = 10;
    [SerializeField] int maxRoomHeight = 20;

    private Tilemap baseTilemap;
    private Tilemap wallTilemap;


    [SerializeField] TileBase innerTile;
    [SerializeField] TileBase wallTile;
    [SerializeField] TileBase doorTile;

    void Start()
    {
        roomHeight = UnityEngine.Random.Range(minRoomHeight, maxRoomHeight);
        roomWidth = roomHeight / 9 * 16;
        SetupCamera();
        SetupPlayer();

        baseTilemap = GenerateTilemap("Floor Tilemap");
        wallTilemap = GenerateTilemap("Wall Tilemap");
        PopulateLayout(startPoint, roomWidth, roomHeight);
        AddColidersToTilemap(wallTilemap, CustomTag.Obstacle);

        AddDoorsToMap();
    }

    private Tilemap GenerateTilemap(string tilemapName)
    {
        var go = new GameObject(tilemapName);
        var tm = go.AddComponent<Tilemap>();
        var tr = go.AddComponent<TilemapRenderer>();
        tm.transform.SetParent(grid.transform);
        tm.tileAnchor = new Vector3(0.5f, 0.5f);

        return tm;
    }

    private void PopulateLayout(Vector2Int startPoint, int width, int height)
    {

        for (int i = startPoint.x; i < width + startPoint.x; i++)
        {
            for (int j = startPoint.y; j < height + startPoint.y; j++)
            {
                Vector3Int tilePosition = new Vector3Int(i, j, 0);
                if (i == startPoint.x || i == startPoint.x + width - 1 || j == startPoint.y || j == startPoint.y + height - 1)
                {
                    wallTilemap.SetTile(tilePosition, wallTile);
                }
                baseTilemap.SetTile(tilePosition, innerTile);
            }
        }
    }

    private void AddColidersToTilemap(Tilemap tilemap, string tag)
    {
        GameObject tilemapGameObject = tilemap.gameObject;
        var collider = tilemapGameObject.AddComponent<TilemapCollider2D>();
        collider.usedByComposite = true;
        var compCollider = tilemapGameObject.AddComponent<CompositeCollider2D>();
        var rb = tilemapGameObject.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        tilemapGameObject.tag = tag;
    }

    private void AddDoorsToMap()
    {
        Tilemap doorTilemap = GenerateTilemap("DoorTilemap");
        int sideMid = (roomWidth - 2) / 2;
        int doorWidth;
        if (roomWidth < 6)
        {
            doorWidth = 2;
        }
        else
        {
            doorWidth = 4;
        }

        for (int i = sideMid - (doorWidth / 2); i < sideMid + (doorWidth / 2); i++)
        {
            doorTilemap.SetTile(new Vector3Int(i, startPoint.y + roomHeight - 1, 0), doorTile);
        }
        AddColidersToTilemap(doorTilemap, CustomTag.Door);
    }

    private void SetupCamera()
    {
        Camera camera = Camera.main;
        camera.transform.position = new Vector3(roomWidth / 2, roomHeight / 2f, 0);
        camera.orthographicSize = roomHeight / 2f;

    }

    private void SetupPlayer()
    {
        GameObject player = GameObject.Find("Player");
        player.transform.position = new Vector3(roomWidth / 2, roomHeight / 2, 0);
    }
}
