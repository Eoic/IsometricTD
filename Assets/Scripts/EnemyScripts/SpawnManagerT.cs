using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerT : MonoBehaviour
{
    public GameObject[] spawnPointObjects;
    private ISpawnPoint[] spawnPoints;
    public int enemiesPerSpawn;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = new ISpawnPoint[spawnPointObjects.Length];
        for (int i = 0; i < spawnPointObjects.Length; i++)
        {
            spawnPoints[i] = spawnPointObjects[i].GetComponent<ISpawnPoint>();
        }
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            for (int j = 0; j < enemiesPerSpawn; j++)
            {
                spawnPoints[j].SpawnEnemy();
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
