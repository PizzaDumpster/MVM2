using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    public ObjectPool pool;

    public void ReturnToPool()
    {
        pool.ReturnObject(gameObject);
    }
}
