using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public UnityEvent OnDeath;

    public int HealthAmount { get { return health; } set { health = Mathf.Clamp(value, 0, maxHealth); } }


}
