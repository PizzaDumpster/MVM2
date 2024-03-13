using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class GrassTile : MonoBehaviour
{
    public Tilemap tilemap;
    public List<DestroyedTile> destroyedTiles;
    public void Damage(int amount, Transform transform)
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
            TileBase tile = tilemap.GetTile(cellPosition);
            DestroyedTile taggedEvent = destroyedTiles.Find(e => e.tileDestroyed == tile);
            taggedEvent.OnDestroyed?.Invoke();
            tilemap.SetTile(cellPosition, null);
        }
        else
        {
            print("No tile found at cell position.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.gameObject.CompareTag("PlayerAttack"))
            {
            
            Vector3Int cellPosition = tilemap.WorldToCell(GetComponent<TilemapCollider2D>().ClosestPoint(other.gameObject.transform.position));
            // Check if there's a tile at the cell position
            if (tilemap.GetTile(cellPosition) != null)
            {
                TileBase tile = tilemap.GetTile(cellPosition);
                DestroyedTile taggedEvent = destroyedTiles.Find(e => e.tileDestroyed == tile);
                taggedEvent.OnDestroyed?.Invoke();
                tilemap.SetTile(cellPosition, null);
            }
            else
            {
                print("No tile found at cell position.");
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.gameObject.CompareTag("PlayerAttack"))
        {

            Vector3Int cellPosition = tilemap.WorldToCell(GetComponent<TilemapCollider2D>().ClosestPoint(other.gameObject.transform.position));
            // Check if there's a tile at the cell position
            if (tilemap.GetTile(cellPosition) != null)
            {
                TileBase tile = tilemap.GetTile(cellPosition);
                DestroyedTile taggedEvent = destroyedTiles.Find(e => e.tileDestroyed == tile);
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
