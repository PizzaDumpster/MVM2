using UnityEngine;
using UnityEngine.Tilemaps;

public class GrassTile : MonoBehaviour, IDamageable
{
    public Tilemap tilemap;

    public void Damage(int amount)
    {
        print("Hit Grass");
        Vector3Int cellPosition = tilemap.WorldToCell(transform.position);

        // Remove the tile at the given position
        tilemap.SetTile(cellPosition, null);
    }
}
