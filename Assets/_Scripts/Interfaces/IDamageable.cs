using UnityEngine;

public interface IDamageable
{
    void Damage(int damage = 0, Transform transform = null);
}
