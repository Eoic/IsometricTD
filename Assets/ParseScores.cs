using UnityEngine;
using TMPro;
using System;
using System.Linq;
using System.Collections.Generic;

public class ParseScores : MonoBehaviour
{
    public RectTransform scrollContent;

    private void Start()
    {
        // Default values
        ResetVisibleRecords();
        var scores = Rankings.Instance.LoadRankings();
        var filteredScores = scores.Where(score => score.Level == 1);
        AddScoreRecords(filteredScores);
    }

    public void FetchScores(TMP_Dropdown dropdown)
    {
        ResetVisibleRecords();
        var scores = Rankings.Instance.LoadRankings();
        var filteredScores = scores.Where(score => score.Level == dropdown.value + 1);
        AddScoreRecords(filteredScores);
    }

    void AddScoreRecords(IEnumerable<RankingEntry> filteredScores)
    {
        var orderedScores = filteredScores.OrderBy(score => score.PlayTime).ToList();

        if (orderedScores.Count() > 0)
        {
            foreach (var score in filteredScores)
            {
                var entry = new GameObject();
                entry.AddComponent<TextMeshProUGUI>();
                var scoreText = score.Name + " " + ToTimeString(score.PlayTime);
                entry.GetComponent<TextMeshProUGUI>().text = scoreText;
                entry.transform.SetParent(scrollContent.transform);
            }
        }
        else
        {
            var entry = new GameObject();
            entry.AddComponent<TextMeshProUGUI>();
            entry.GetComponent<TextMeshProUGUI>().text = "No scores available";
            entry.transform.SetParent(scrollContent.transform);
        }
    }

    void ResetVisibleRecords()
    {
        foreach (Transform child in scrollContent)
            GameObject.Destroy(child.gameObject);
    }

    string ToTimeString(float seconds)
    {
        var time = TimeSpan.FromSeconds(seconds);
        return time.ToString(@"mm\:ss").Replace(":", " min. ") + " sec."; 
    }
}
