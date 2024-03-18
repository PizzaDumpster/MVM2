using UnityEngine;

public class DisableOnCollision : MonoBehaviour
{
    public LayerMask collisionLayer; // Layer to detect collisions with
    public LayerMask passThroughLayer; // Layer to pass through without disabling
    public bool disableOnEnter = true; // Whether to disable on collision enter or exit

    public int DamageAmount;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (disableOnEnter && ShouldDisable(collision.gameObject))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!disableOnEnter && ShouldDisable(collision.gameObject))
        {
            Destroy(this.gameObject);
        }
    }

    private bool ShouldDisable(GameObject other)
    {
        // Check if the collided object is on the specified layer
        bool isCollisionLayer = collisionLayer == (collisionLayer | (1 << other.layer));
        // Check if the collided object is on the pass-through layer
        bool isPassThroughLayer = passThroughLayer == (passThroughLayer | (1 << other.layer));

        // Return true if the collided object is on the specified layer
        // and not on the pass-through layer
        return isCollisionLayer && !isPassThroughLayer;
    }
}
