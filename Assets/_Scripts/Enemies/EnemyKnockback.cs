using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    [SerializeField] float speed; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(this.isActiveAndEnabled)
        {
            StartCoroutine(KnockBack(other));
        }
        
    }

    IEnumerator KnockBack(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector2 direction = (other.transform.position - transform.position).normalized;
          
            other.gameObject.GetComponent<Rigidbody2D>().velocity = direction * speed;
           
            yield return null; 
        }
        
    }
}
