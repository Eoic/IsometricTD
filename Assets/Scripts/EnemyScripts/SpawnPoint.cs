using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Transform[] path;
    public GameObject enemy;
    public float spawnInterval;
    public int waves;

    void Start() => InvokeRepeating("SpawnEnemies", 2f, spawnInterval);

    void SpawnEnemies()
    {
        waves--;

        if (waves == 0)
            CancelInvoke("SpawnEnemies");

        GameObject enemyRef = Instantiate(enemy, transform.position, Quaternion.identity);
        enemyRef.GetComponent<EnemyController>().SetPath(path);
    }
}
