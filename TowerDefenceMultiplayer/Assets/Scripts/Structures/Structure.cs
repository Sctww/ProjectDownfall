using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Structure : NetworkBehaviour, IDamageable
{

    #region Variables

    public int ID = 0;
    public bool godMode = false;
    protected bool destroyed;
    protected float baseCost = 50;
    protected float baseHealth = 50f;
    protected float currentHealth;
    protected float healthDifference;

    public GameObject destroyEffect;

    #endregion

    // Damageable Interface Methods
    public virtual void Start()
    {
        if (godMode == true)
        {
            currentHealth = Mathf.Infinity;
        }
        else
        {
            currentHealth = baseHealth;
        }
    }
    public virtual void TakeHit(float damage)
    {
        TakeDamage(damage);
    }
    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0 && !destroyed)
        {
            Die();
        }
    }

    //Structure specific Methods
    [ContextMenu("Self Destruct")]
    protected void Die()
    {
        destroyed = true;
        Instantiate(destroyEffect, this.transform.position, this.transform.rotation);
        Destroy(this);
    }
}