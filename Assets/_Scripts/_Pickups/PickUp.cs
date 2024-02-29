using UnityEngine;


public abstract class PickUp : MonoBehaviour
{
    [HideInInspector] public Collider2D collider2d;
    public ObjectStringSO playerObject;

    [Header("")]
    public AudioClip pickupClip;

    public virtual void Start()
    {
        collider2d = GetComponent<Collider2D>();
    }
}