using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Waves
{
    public bool[] SpawnPoints;
}

public class WaveManager : MonoBehaviour
{
    public Waves[] waves;
    public SpawnPoint[] spawnPoints;
    public float waveCooldown;
    public int wavesToLaunch;

    private int wavesLaunched = 0;

    private void Start() => InvokeRepeating("SpawnWave", 2f, waveCooldown);

    void SpawnWave()
    {
        // All waves completed.
        if (wavesToLaunch == wavesLaunched)
        {
            Debug.Log("Done");
            CancelInvoke("SpawnWave");
            return;
        }

        // Activate spawn points for current wave
        var wave = waves[wavesLaunched];

        for (int i = 0; i < wave.SpawnPoints.Length; i++)
        {
            if (wave.SpawnPoints[i])
            {
                spawnPoints[i].SetAsActive();
                spawnPoints[i].LaunchWave();
            }
            else spawnPoints[i].SetAsInactive();
        }

        // Register wave launch
        wavesLaunched++;
        StatisticsManager.instance.RegisterWaveSurvived();
    }
}
