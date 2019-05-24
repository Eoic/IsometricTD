using System;
using UnityEngine;

public class ShootEnemies : MonoBehaviour
{
    public GameObject projectileBlueprint;
    public Transform projectileSpawn;

    public float radius;
    public LayerMask enemyLayer;

    // Should be in other class
    public float shotCooldown;
    public int damage;

    private Collider[] collisions;

    private void Start() =>
        InvokeRepeating("Attack", 2f, shotCooldown);

    public void Attack()
    {
        collisions = Physics.OverlapSphere(transform.position, radius, enemyLayer);

        if (collisions.Length > 0)
        {
            // Select enemy to fire at
            int randomEnemy = UnityEngine.Random.Range(0, collisions.Length);
            EnemyController enemy = collisions[randomEnemy].gameObject.GetComponent<EnemyController>();

            // Create new projectile
            if (projectileBlueprint != null)
                Instantiate(projectileBlueprint, projectileSpawn.position, Quaternion.identity).GetComponent<Projectile>().InitializeFields(damage, enemy.transform.position);
        }
    }
}
