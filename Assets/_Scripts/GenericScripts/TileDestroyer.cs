using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;
using System.Collections.Generic;

[System.Serializable]
public struct DestroyedTile
{
    public Tile tileDestroyed;
    public int tileHealth;
    public UnityEvent OnDestroyed;
}

public class TileDestroyer : MonoBehaviour, IDamageable
{
    public Tilemap tilemap;
    public List<DestroyedTile> destroyedTiles;
    public float maxCollisionRadius; // Maximum radius for overlap circle

    public void Damage(int amount, Transform passedTranform)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(passedTranform.position);

        for (int x = -Mathf.CeilToInt(maxCollisionRadius); x <= Mathf.CeilToInt(maxCollisionRadius); x++)
        {
            for (int y = -Mathf.CeilToInt(maxCollisionRadius); y <= Mathf.CeilToInt(maxCollisionRadius); y++)
            {
                float distance = Mathf.Sqrt(x * x + y * y);

                if (distance <= maxCollisionRadius)
                {
                    Vector3Int currentCell = new Vector3Int(cellPosition.x + x, cellPosition.y + y, cellPosition.z);

                    TileBase tile = tilemap.GetTile(currentCell);
                    if (tile != null)
                    {
                        print("Tile found at position: " + currentCell);
                        DestroyedTile taggedEvent = destroyedTiles.Find(e => e.tileDestroyed == tile);
                        taggedEvent.OnDestroyed?.Invoke();
                        taggedEvent.tileHealth -= amount;
                        if(taggedEvent.tileHealth <= 0)
                        {
                            tilemap.SetTile(currentCell, null);
                        }

                    }
                }
            }
        }
    }
}
