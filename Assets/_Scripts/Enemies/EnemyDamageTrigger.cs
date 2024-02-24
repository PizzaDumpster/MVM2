using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTrigger : DamageTrigger
{
    public Collider2D enemyCollider;

    private void Start()
    {
        enemyCollider = GetComponentInChildren<Collider2D>();
    }
}
