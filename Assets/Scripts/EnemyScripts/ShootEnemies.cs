using System;
using UnityEngine;

public class ShootEnemies : MonoBehaviour
{
    public GameObject projectileBlueprint;
    public Transform projectileSpawn;
    //public GameObject weapon;           // Cannon turret.

    public float radius;
    public LayerMask enemyLayer;

    // Should be in other class
    public float shotCooldown;
    public int damage;

    private Collider[] collisions;
    private AudioSource audioSource;

    private void Start()
    {
        InvokeRepeating("Attack", 2f, shotCooldown);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void Attack()
    {
        collisions = Physics.OverlapSphere(transform.position, radius, enemyLayer);

        if (collisions.Length > 0)
        {
            // Select enemy to fire at
            int randomEnemy = UnityEngine.Random.Range(0, collisions.Length);
            EnemyController enemy = collisions[randomEnemy].gameObject.GetComponent<EnemyController>();
            EnemyControllerT enemyT = collisions[randomEnemy].gameObject.GetComponent<EnemyControllerT>();

            // Create new projectile
            if (projectileBlueprint != null)
            {
                audioSource.Play();
                if(enemy != null)
                    Instantiate(projectileBlueprint, projectileSpawn.position, Quaternion.identity).GetComponent<Projectile>().InitializeFields(damage, enemy.transform.position);
                else
                    Instantiate(projectileBlueprint, projectileSpawn.position, Quaternion.identity).GetComponent<Projectile>().InitializeFields(damage, enemyT.transform.position);
            }
        }
    }
}