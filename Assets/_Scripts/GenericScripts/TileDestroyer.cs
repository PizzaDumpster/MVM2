using UnityEngine;
using UnityEngine.Tilemaps;

public class TileDestroyer : MonoBehaviour
{
    public Tilemap tilemap;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider belongs to the tilemap
        if (collision.gameObject.CompareTag("Grass"))
        {
            // Output a message to the console
            Debug.Log("Entered the tile collider!");

            // Get the position of the collision in world space
            Vector3 collisionPosition = collision.transform.position;

            // Draw a debug sphere at the collision position
            Debug.DrawRay(collisionPosition, Vector3.up * 0.5f, Color.red, 1f);
            Debug.DrawRay(collisionPosition, Vector3.down * 0.5f, Color.red, 1f);
            Debug.DrawRay(collisionPosition, Vector3.left * 0.5f, Color.red, 1f);
            Debug.DrawRay(collisionPosition, Vector3.right * 0.5f, Color.red, 1f);

            // Get the position of the collision in tile coordinates
            Vector3Int cellPosition = tilemap.WorldToCell(collisionPosition);

            // Remove the tile at the given position
            tilemap.SetTile(cellPosition, null);
        }
    }
}
