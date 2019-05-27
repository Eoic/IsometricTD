using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class StatisticsManager : MonoBehaviour
{
    public static StatisticsManager instance;

    // UI elements
    public Text waveCount;
    public GameObject gameWinScreen;
    public Text wavesSurvived;
    public Text enemiesKilled;
    public Text structuresBuilt;
    public Text damageTaken;
    public TextMeshProUGUI gameInfoStructuresBuilt;
    public TextMeshProUGUI gameInfoStructuresAllowed;
    public Text timePlayed;
    public InputField playersName;

    public int StructuresBuilt { get; private set; }
    public int StructuresAllowed = 40;
    public int EnemiesKilled { get; private set; }
    public int WavesSurvived { get; private set; }
    public int EnemiesSpawned { get; private set; }
    public int DamageTaken { get; private set; }
    public int MaxWaves { get; private set; }
    public bool WavesEnded { get; private set; } = false;
    public float GameTime { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        waveCount.text = "1";
        gameInfoStructuresAllowed.text = StructuresAllowed.ToString();
        gameInfoStructuresBuilt.text = StructuresBuilt.ToString();
    }

    public void RegisterStructureBuilt()
    {
        StructuresBuilt++;
        gameInfoStructuresBuilt.text = StructuresBuilt.ToString();
    }

    public void RegisterEnemyKilled()
    {
        EnemiesKilled++;

        // All enemies eliminated
        if (WavesEnded && EnemiesSpawned == EnemiesKilled)
        {
            Time.timeScale = 0;
            wavesSurvived.text = WavesSurvived.ToString();
            enemiesKilled.text = EnemiesKilled.ToString();
            structuresBuilt.text = StructuresBuilt.ToString();
            damageTaken.text = DamageTaken.ToString();

            // Player won the game. Set record.
            GameTime = Time.timeSinceLevelLoad;
            var time = TimeSpan.FromSeconds(GameTime);
            var timeString = time.ToString(@"mm\:ss").Replace(":", " min. ") + " sec.";
            timePlayed.text = timeString;
            gameWinScreen.SetActive(true);
        }
    }

    public void RegisterWaveSurvived()
    {
        WavesSurvived++;
        waveCount.text = WavesSurvived.ToString();
    }

    public void RegisterEnemySpawned() => 
        EnemiesSpawned++;

    public void RegisterDamageTaken(int amount) =>
        DamageTaken += amount;

    public void RegisterMaxWaves(int amount) =>
        MaxWaves = amount;

    public void RegisterWavesEnded() =>
        WavesEnded = true;

    public RankingEntry CreateScore()
    {
        var playerName = playersName.text;

        if (playerName.Trim().Length == 0)
            playerName = "Player 1";

        var levelName = SceneManager.GetActiveScene().name;
        int.TryParse(levelName.Substring(levelName.Length - 1), out int level);


        return new RankingEntry(playerName, GameTime, level);
    }
}
