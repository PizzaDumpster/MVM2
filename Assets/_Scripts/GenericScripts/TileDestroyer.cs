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


    public void Damage(int amount)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(transform.position);


        // Check if a collider tagged as "Player" hit the grass tile
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                // Print the position of the player
                print("Player hit the grass tile at position: " + collider.transform.position);
                break;
            }
        }

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        DestroyTile(other);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        DestroyTile(other.collider);

    }

    private void DestroyTile(Collider2D other)
    {
        if (other.gameObject.gameObject.CompareTag("PlayerAttack"))
        {

            Vector3Int cellPosition = tilemap.WorldToCell(GetComponent<TilemapCollider2D>().ClosestPoint(other.gameObject.transform.position));
            // Check if there's a tile at the cell position
            if (tilemap.GetTile(cellPosition) != null)
            {
                DestroyedTile taggedEvent = destroyedTiles.Find(e => e.tileDestroyed == tilemap.GetTile(cellPosition));
                print(taggedEvent.tileDestroyed);
                taggedEvent.OnDestroyed?.Invoke();
                tilemap.SetTile(cellPosition, null);
            }
            else
            {
                print("No tile found at cell position.");
            }
        }
    }
}
