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
        ResetTable();
        var scores = Rankings.Instance.LoadRankings();
        var filteredScores = scores.Where(score => score.Level == 1).ToList();
        AddScoreRecords(filteredScores);
    }

    public void FetchScores(TMP_Dropdown dropdown)
    {
        ResetTable();
        var scores = Rankings.Instance.LoadRankings();
        var filteredScores = scores.Where(score => score.Level == dropdown.value + 1).ToList();
        AddScoreRecords(filteredScores);
    }

    void AddScoreRecords(List<RankingEntry> filteredScores)
    {
        var orderedScores = filteredScores.OrderBy(score => score.PlayTime).ToList();

        if (orderedScores.Count() > 0)
            for (var i = 0; i < filteredScores.Count(); i++)
                CreateTextElements(24, (i + 1).ToString(), filteredScores[i].Name, ToTimeString(filteredScores[i].PlayTime));
    }

    string ToTimeString(float seconds)
    {
        var time = TimeSpan.FromSeconds(seconds);
        return time.ToString(@"mm\:ss").Replace(":", " min. ") + " sec."; 
    }

    void ResetTable()
    {
        foreach (Transform child in scrollContent)
            Destroy(child.gameObject);

        CreateTextElements(null, "Place", "Name", "Time");
    }

    void CreateTextElements(float? size = null, params string[] textForElements)
    {
        foreach (var text in textForElements)
        {
            var textMesh = new GameObject();
            textMesh.AddComponent<TextMeshProUGUI>().text = text;
            
            if (size != null)
                textMesh.GetComponent<TextMeshProUGUI>().fontSize = size.Value;

            textMesh.transform.SetParent(scrollContent);
        }
    }
}
