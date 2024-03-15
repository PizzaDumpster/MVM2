using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crates : MonoBehaviour, IDamageable
{

    public UnityEvent onHit;
    public void Damage(int damage = 0, Transform transform = null)
    {
        onHit?.Invoke();
    }
}
