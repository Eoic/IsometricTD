[System.Serializable]
public class RankingEntry
{
    public RankingEntry(string name, float playTime, int level)
    {
        Name = name;
        PlayTime = playTime;
        Level = level;
    }

    public string Name { get; private set; }
    public float PlayTime { get; private set; }
    public int Level { get; private set; }
}