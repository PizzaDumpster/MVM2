using UnityEngine;
using UnityEngine.Tilemaps;

public class TileClickDetector : MonoBehaviour
{
    public Tilemap tilemap; // Assign the tilemap in the inspector
    public Tile newTile; // Assign the new tile in the inspector

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);

            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit.collider != null && hit.collider.GetComponent<Tilemap>() == tilemap)
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.CompareTag("Grass"))
                {
                    TileChangeData tileChangeData = new TileChangeData(cellPosition, newTile, Color.white, Matrix4x4.identity);
                    tilemap.SetTile(tileChangeData, true);
                }
                else
                {
                    Debug.Log("Clicked collider is not tagged as 'Grass'.");
                }
            }
        }
    }
}
