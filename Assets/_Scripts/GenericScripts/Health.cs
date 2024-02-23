using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int health;

    public UnityEvent OnDeath;

    public int HealthAmount { get; set; }


}
