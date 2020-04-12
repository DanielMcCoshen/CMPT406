//using System.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomEditor : MonoBehaviour
{
    public Transform tilemapObject;

    public Tilemap[] children;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            FlipNSMaps();
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            FlipWEMaps();
        }
    }

    void FlipNSMaps()
    {
        children = tilemapObject.GetComponentsInChildren<Tilemap>();
        for (int i = 0; i < children.Length; i++)
        {
            if (i != 1)
            {
                FlipTilemap(children[i], true);
            }

        }
    }

    void FlipWEMaps()
    {
        children = tilemapObject.GetComponentsInChildren<Tilemap>();
        for (int i = 0; i < children.Length; i++)
        {
            if (i != 1)
            {
                FlipTilemap(children[i], false);
            }

        }
    }

    public void FlipTilemap(Tilemap tilemapToFlip, bool rotateNS)
    {
        Tilemap tilemap = tilemapToFlip;
        tilemap.CompressBounds();
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        TileBase[] newTilebase = new TileBase[allTiles.Length];

        //Debug.Log("Flipping tilemap.");

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                //TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                if (tile != null)
                {
                    //Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                }
                else
                {
                    //Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                }

                int indexToAccess = 0;

                if (rotateNS)
                {
                    // Flip N-S
                    //indexToAccess = (newTilebase.Length - 1) - ((x) + ((y * bounds.size.x)));
                    indexToAccess = ((bounds.size.x - x - 1) + (((bounds.size.y - y - 1) * bounds.size.x)));
                }
                else
                {
                    // Flip W-E
                    indexToAccess = ((x) + (((bounds.size.y - y - 1) * bounds.size.x)));

                }

                // Don't change
                //indexToAccess = ((x) + ((y) * bounds.size.x));

                //Debug.Log(indexToAccess);
                try
                {
                    newTilebase[indexToAccess] = tile;
                }
                catch (IndexOutOfRangeException e)
                {
                    Debug.LogError(indexToAccess);
                }
            }
        }
        tilemapToFlip.SetTilesBlock(bounds, newTilebase);
    }
}
