using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemies : MonoBehaviour
{
    public float radius;
    public LayerMask enemyLayer;

    // Should be in other class
    public float shotCooldown = 1f;
    public int damage;

    private Collider[] collisions;

    private void Start()
    {
        InvokeRepeating("Attack", 1f, shotCooldown);
    }

    public void Attack()
    {
        collisions = Physics.OverlapSphere(transform.position, radius, enemyLayer);

        if (collisions.Length > 0)
        {
            int randomEnemy = Random.Range(0, collisions.Length);
            Debug.Log("Rand: " + randomEnemy);
            EnemyController enemy = collisions[randomEnemy].gameObject.GetComponent<EnemyController>();
            enemy.TakeDamage(damage);
        }
    }
}
