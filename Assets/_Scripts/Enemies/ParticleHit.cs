using UnityEngine;

public class ParticleHit : MonoBehaviour
{
    public GameObject objectToInstantiate;

    public void SpawnObject()
    {
        Instantiate(objectToInstantiate, transform.position, Quaternion.identity);
    }
}
