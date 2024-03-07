using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;
using System.Collections.Generic;

[System.Serializable]
public struct DestroyedTile
{
    public Tile tileDestroyed;
    public UnityEvent OnDestroyed;
}

public class TileDestroyer : MonoBehaviour, IDamageable
{
    public Tilemap tilemap;
    public List<DestroyedTile> destroyedTiles;
    public float maxCollisionRadius; // Maximum radius for overlap circle

    public void Damage(int amount, Transform passedTranform)
    {
        print("Is this called?");
        Vector3Int cellPosition = tilemap.WorldToCell(passedTranform.position);

        // Iterate over cells within the overlap circle with limited radius
        for (int x = -Mathf.CeilToInt(maxCollisionRadius); x <= Mathf.CeilToInt(maxCollisionRadius); x++)
        {
            for (int y = -Mathf.CeilToInt(maxCollisionRadius); y <= Mathf.CeilToInt(maxCollisionRadius); y++)
            {
                // Calculate the distance between the current cell and the center cell
                float distance = Mathf.Sqrt(x * x + y * y);

                // Check if the current cell is within the maximum collision radius
                if (distance <= maxCollisionRadius)
                {
                    Vector3Int currentCell = new Vector3Int(cellPosition.x + x, cellPosition.y + y, cellPosition.z);

                    // Check if there's a tile at the current cell
                    TileBase tile = tilemap.GetTile(currentCell);
                    if (tile != null)
                    {
                        // A tile is found within the overlap circle
                        print("Tile found at position: " + currentCell);
                        DestroyedTile taggedEvent = destroyedTiles.Find(e => e.tileDestroyed == tile);
                        taggedEvent.OnDestroyed?.Invoke();
                        tilemap.SetTile(currentCell, null); // Set the tile at currentCell to null
                    }
                }
            }
        }
    }
}
