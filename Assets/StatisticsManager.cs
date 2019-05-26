using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatisticsManager : MonoBehaviour
{
    public static StatisticsManager instance;

    // UI elements
    public TextMeshProUGUI waveCount;
    public GameObject gameWinScreen;
    public TextMeshProUGUI wavesSurvived;
    public TextMeshProUGUI enemiesKilled;
    public TextMeshProUGUI structuresBuilt;
    public TextMeshProUGUI damageTaken;
    public TextMeshProUGUI gameInfoStructuresBuilt;
    public TextMeshProUGUI gameInfoStructuresAllowed;
    public TextMeshProUGUI timePlayed;
    public InputField playersName;

    public int StructuresBuilt { get; private set; }
    public int StructuresAllowed = 40;
    public int EnemiesKilled { get; private set; }
    public int WavesSurvived { get; private set; }
    public int EnemiesSpawned { get; private set; }
    public int DamageTaken { get; private set; }
    public int MaxWaves { get; private set; }

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
        if (EnemiesSpawned == EnemiesKilled)
        {
            Time.timeScale = 0;
            wavesSurvived.text = WavesSurvived.ToString();
            enemiesKilled.text = EnemiesKilled.ToString();
            structuresBuilt.text = StructuresBuilt.ToString();
            damageTaken.text = DamageTaken.ToString();

            // Player won the game. Set record.
            timePlayed.text = Time.timeSinceLevelLoad.ToString();
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

    public RankingEntry CreateScore()
    {
        var playerName = playersName.text;

        if (playerName.Trim().Length == 0)
            playerName = "Player 1";

        return new RankingEntry(playerName, Time.timeSinceLevelLoad, 1);
    }
}
