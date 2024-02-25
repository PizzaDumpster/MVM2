using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowItemShooter : MonoBehaviour
{
    public GameObject arrowPrefab; 
    public Transform shootPoint;
    public int poolSize = 10; 
    public float shootInterval = 1f; 

    private List<GameObject> arrowPool = new List<GameObject>();


    void Start()
    {
        InitializePool();
        StartCoroutine(ShootArrowTimer());
    }

    void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity, transform);
            arrow.SetActive(false);
            arrowPool.Add(arrow);
        }
    }

    IEnumerator ShootArrowTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootInterval);
            ShootArrowItem();
        }
    }

    public void ShootArrowItem()
    {
        GameObject arrow = GetInactiveArrowFromPool();

        if (arrow != null)
        {
            arrow.transform.position = shootPoint.position;

            // Set arrow's rotation to match shootPoint's rotation
            arrow.transform.rotation = shootPoint.rotation;

            arrow.SetActive(true);
            arrow.GetComponent<Arrow>().Shoot();
        }
        else
        {
            Debug.Log("No inactive arrow found in the pool.");
        }
    }

    GameObject GetInactiveArrowFromPool()
    {
        foreach (GameObject arrow in arrowPool)
        {
            if (!arrow.activeInHierarchy)
            {
                return arrow;
            }
        }
        return null;
    }
}
