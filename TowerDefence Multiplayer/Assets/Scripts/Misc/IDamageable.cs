using UnityEngine;
using System.Collections;

public interface IDamageable
{
    void TakeHit(float damage);

    void TakeDamage(float damage);
}