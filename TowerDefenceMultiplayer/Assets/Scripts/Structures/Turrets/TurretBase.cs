using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBase : Structure {

    #region Variables
    [Header("Attributes")]
    protected float upgradeCost = 20f;
    protected float currentRepairCost = 0f;
    public int level = 1;
    public string enemyTag = "EnemyUnits";
    public float range = 20f;
    public float turnSpeed = 5f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    [SerializeField]
    protected Transform target;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    [Header("Graphics")]
    public Transform turretHead;

    #endregion

    public override void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    public void Update()
    {
        if (target == null)
        {
            return;
        }
        AimAtTarget();
        Shoot();
    }

    public void Shoot()
    {
        if (fireCountdown <= 0f)
        {
            fireCountdown = 1f / fireRate;

            GameObject BulletInstance = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation) as GameObject;
            Destroy(BulletInstance, 2f);
        }
        fireCountdown -= Time.deltaTime;
    }

    public void AimAtTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(turretHead.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
        turretHead.rotation = Quaternion.Euler(90, rotation.y, 0);
    }

    public void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        } else {
            target = null;
        }

    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public float getRepairCost()//Calculates the repair cost
    {
        healthDifference = currentHealth - baseHealth;
        currentRepairCost = baseCost + healthDifference;
        return currentRepairCost;
    }

    public void levelUp()//Adds 1 to the level
    { //TODO: Finish the leveling System
      //TODO: make it cost money to do!
        level++;
    }

    public int getLevel() //Returns the level
    {
        return level;
    }
}
