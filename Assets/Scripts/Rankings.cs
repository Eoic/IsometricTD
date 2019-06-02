using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Rankings : MonoBehaviour
{
    public static Rankings Instance;
    private List<RankingEntry> entries;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        entries = LoadRankings();
    }

    // Save score on game win
    public void AddScore()
    {
        Debug.Log("Serializing player score...");
        var entry = StatisticsManager.instance.CreateScore();
        entries.Add(entry);
        SaveRankings();
    }

    public List<RankingEntry> LoadRankings()
    {
        var rankings = new List<RankingEntry>();
        var binaryFormatter = new BinaryFormatter();

        // Load rankings if they exist.
        if (File.Exists(Application.persistentDataPath + "/rankings.save"))
        {
            var fileStream = File.Open(Application.persistentDataPath + "/rankings.save", FileMode.Open);
            rankings = (List<RankingEntry>)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
        }

        return rankings;
    }

    public void SaveRankings()
    {
        var binaryFormatter = new BinaryFormatter();
        var fileStream = File.Create(Application.persistentDataPath + "/rankings.save");
        binaryFormatter.Serialize(fileStream, entries);
        fileStream.Close();
    }
}
