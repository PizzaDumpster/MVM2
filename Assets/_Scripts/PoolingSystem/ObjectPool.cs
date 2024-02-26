using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 10;

    private Queue<GameObject> objectPool = new Queue<GameObject>();

    void Start()
    {
        InitializePool();
    }

    void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newObj = Instantiate(prefab, transform);
            newObj.SetActive(false);
            objectPool.Enqueue(newObj);
        }
    }

    public GameObject GetObject()
    {
        if (objectPool.Count == 0)
        {
            Debug.LogWarning("Pool exhausted, consider increasing pool size.");
            return null;
        }

        GameObject obj = objectPool.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        objectPool.Enqueue(obj);
    }
}
