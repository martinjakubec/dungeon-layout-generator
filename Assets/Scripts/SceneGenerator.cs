using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;


public class SceneGenerator : MonoBehaviour
{
    [SerializeField] int roomWidth;
    [SerializeField] int roomHeight;
    [SerializeField] Vector2Int startPoint;
    [SerializeField] Grid grid;

    private Tilemap baseTilemap;
    private Tilemap wallTilemap;


    [SerializeField] TileBase innerTile;
    [SerializeField] TileBase wallTile;
    [SerializeField] TileBase doorTile;

    [SerializeField] RoomsGenerator rb;

    // Start is called before the first frame update
    void Start()
    {
        baseTilemap = GenerateTilemap("Floor Tilemap");
        wallTilemap = GenerateTilemap("Wall Tilemap");
        PopulateLayout(startPoint, roomWidth, roomHeight);

        AddColidersToTilemap(wallTilemap);
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

    private void AddColidersToTilemap(Tilemap tilemap)
    {
        GameObject gameObject = tilemap.gameObject;
        var collider = gameObject.AddComponent<TilemapCollider2D>();
        collider.usedByComposite = true;
        var compCollider = gameObject.AddComponent<CompositeCollider2D>();
        var rb = gameObject.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;

    }

}
