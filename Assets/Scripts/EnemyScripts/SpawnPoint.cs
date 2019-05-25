using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Transform[] path;
    public GameObject enemy;
    public Light waveStateLight;
    public ParticleSystem waveStateParticles;

    public int enemiesPerWave;
    private int enemiesSpawned = 0;
    public float enemySpawnCooldown;

    private Color blueColor;
    private Color redColor;

    void Start()
    {
        blueColor = new Color(0.01f, 0.42f, 0.82f);
        redColor = new Color(0.82f, 0.01f, 0.01f);
    }

    /*
    void Start() => InvokeRepeating("SpawnWave", 2f, waveCooldown);

    void SpawnWave()
    {
        StatisticsManager.instance.RegisterWaveSurvived();
        enemiesPerWave = Mathf.RoundToInt(enemiesPerWave * enemyMultiplierPerWave);
        enemiesSpawned = 0;
        waves--;

        if (waves == 0)
            CancelInvoke("SpawnWave");
        
        InvokeRepeating("SpawnEnemies", 0f, enemySpawnCooldown);
    }
    */

    public void LaunchWave()
    {
        InvokeRepeating("SpawnEnemies", 1f, enemySpawnCooldown);
    }

    void SpawnEnemies()
    {
        if (enemiesSpawned < enemiesPerWave)
        {
            GameObject enemyRef = Instantiate(enemy, transform.position, Quaternion.identity);
            enemyRef.GetComponent<EnemyController>().SetPath(path);
            StatisticsManager.instance.RegisterEnemySpawned();
            enemiesSpawned++;
        }
        else
        {
            //Debug.Log(gameObject.name + " done spawning " + enemiesSpawned + " enemies.");
            CancelInvoke("SpawnEnemies");
            enemiesSpawned = 0;
        }
    }

    public void SetAsActive()
    {
        waveStateLight.color = redColor;
        ParticleSystem.MainModule main = waveStateParticles.main;
        main.startColor = redColor;
    }

    public void SetAsInactive()
    {
        waveStateLight.color = blueColor;
        ParticleSystem.MainModule main = waveStateParticles.main;
        main.startColor = blueColor;
    }
}
