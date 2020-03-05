using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomEditor : MonoBehaviour {

    [SerializeField]
    Tilemap ground;
    Tilemap walls;
    Tilemap danger;

    void Start () {
        FlipTilemap(ground);
    }

    public void FlipTilemap(Tilemap tilemapToFlip) {
        Tilemap tilemap = tilemapToFlip;
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        TileBase[] newTilebase = new TileBase[allTiles.Length];

        Debug.Log("Flipping tilemap.");

        for (int x = 0; x < bounds.size.x; x++) {
            for (int y = 0; y < bounds.size.y; y++) {
                TileBase tile = allTiles[x + y * bounds.size.x];
                //TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                if (tile != null) {
                    Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                } else {
                    Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                }
                int indexToAccess = (newTilebase.Length)-(x + (y * bounds.size.x));
                Debug.Log(indexToAccess);
                newTilebase[indexToAccess-1] = tile;
            }
        }
        tilemapToFlip.SetTilesBlock(bounds, newTilebase);
    }
}
