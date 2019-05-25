using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointT : MonoBehaviour
{

    public GameObject enemy;
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1, 1);
    }

  
    void SpawnEnemy()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }
}
