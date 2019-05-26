using UnityEngine;

public class SpawnManagerT : MonoBehaviour
{
    public float timeBetweenSpawns = 10; //seconds between spawn events
    public GameObject[] spawnPointObjects;
    private SpawnPointT[] spawnPoints;
    public int[] enemiesPerSpawn;
    public float secondsBetweenSpawns = 1; //seconds between every enemy spawned
    private int currentSpawnIdx = 0;
    private float seconds = 0;
    


    void Start()
    {
        spawnPoints = new SpawnPointT[spawnPointObjects.Length];
        for (int i = 0; i < spawnPointObjects.Length; i++)
        {
            spawnPoints[i] = spawnPointObjects[i].GetComponent<SpawnPointT>();
        }
        SpawnEvent();
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;
        if (seconds >= timeBetweenSpawns)
        {
            seconds = 0;
            SpawnEvent();
        }
    }

    private void SpawnEvent()
    {
        //if all spawns happened, skip
        if (currentSpawnIdx >= enemiesPerSpawn.Length)
            return;
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            float secondAcc = secondsBetweenSpawns;
            for (int j = 0; j < enemiesPerSpawn[currentSpawnIdx]; j++)
            {
                spawnPoints[i].Invoke("SpawnEnemy", secondAcc);
                secondAcc += secondsBetweenSpawns;
            }

        }
        currentSpawnIdx++;
    }
}
