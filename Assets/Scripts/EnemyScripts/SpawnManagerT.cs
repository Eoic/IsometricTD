using UnityEngine;

[System.Serializable]
public class WaveT
{
    public int WaveTime;
    public bool[] WhichSpawnsToUse;
    public int[] EnemyCountPerSpawn;
    public EnemyType[] enemyTypes;
}


public class SpawnManagerT : MonoBehaviour
{
    [SerializeField]
    public WaveT[] waves;
    //public float timeBetweenSpawns = 10; //seconds between spawn events
    public GameObject[] spawnPointObjects;
    private SpawnPointT[] spawnPoints;
    //public int[] enemiesPerSpawn;
    public float secondsBetweenSpawns = 1; //seconds between every enemy spawned
    private int currentWaveIdx = 0;
    private float seconds = 0;
    


    void Start()
    {
        spawnPoints = new SpawnPointT[spawnPointObjects.Length];
        for (int i = 0; i < spawnPointObjects.Length; i++)
        {
            spawnPoints[i] = spawnPointObjects[i].GetComponent<SpawnPointT>();
        }

        StatisticsManager.instance.RegisterMaxWaves(waves.Length);
        //SpawnEvent();
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;
        if (currentWaveIdx < waves.Length && seconds >= waves[currentWaveIdx].WaveTime)
        {
            seconds = 0;

            if (currentWaveIdx == waves.Length - 1)
                StatisticsManager.instance.RegisterWavesEnded();
            
            SpawnEvent();

        }
    }

    private void SpawnEvent()
    {
        //if all spawns happened, skip
        if (currentWaveIdx >= waves.Length) {
            
            return;
        }
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i].enemyType = waves[currentWaveIdx].enemyTypes[i];
            if (!waves[currentWaveIdx].WhichSpawnsToUse[i])
                continue; //skip if spawn point disabled
            float secondAcc = secondsBetweenSpawns;
            for (int j = 0; j < waves[currentWaveIdx].EnemyCountPerSpawn[i]; j++)
            {
                spawnPoints[i].Invoke("SpawnEnemy", secondAcc);
                secondAcc += secondsBetweenSpawns;
            }
        }
        currentWaveIdx++;
        StatisticsManager.instance.RegisterWaveSurvived();
    }
}
