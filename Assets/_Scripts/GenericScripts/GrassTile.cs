using UnityEngine;
using UnityEngine.Tilemaps;

public class GrassTile : MonoBehaviour, IDamageable
{
    public Tilemap tilemap;

    public void Damage(int amount)
    {
        print("Hit Grass");
        Vector3Int cellPosition = tilemap.WorldToCell(transform.position);
        print("GrassTile Position: " + transform.position);
        print("Tilemap Position: " + tilemap.transform.position);
        print("Cell Position: " + cellPosition);

        // Check if there's a tile at the cell position
        if (tilemap.GetTile(cellPosition) != null)
        {
            // Remove the tile at the given position
            tilemap.SetTile(cellPosition, null);
        }
        else
        {
            print("No tile found at cell position.");
        }
    }
}
