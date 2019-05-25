using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointT : MonoBehaviour, ISpawnPoint
{
    public GameObject[] targets;
    public GameObject enemy;
    void Start()
    {
        //InvokeRepeating("SpawnEnemy", 1, 1);
        Invoke("SpawnEnemy", 1);
    }


    void ISpawnPoint.SpawnEnemy()
    {
        var enemyObj = Instantiate(enemy, transform.position, Quaternion.identity);
        var enm = enemyObj.GetComponent<EnemyControllerT>();
        enm.targetPoints = targets;
    }

}
