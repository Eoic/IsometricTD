using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private string buildingId;
    [SerializeField] private string buildingName;
    [SerializeField] private int hitPoints;
    [SerializeField] private int stoneCost;
    [SerializeField] private int woodCost;
    [SerializeField] private int ironCost;
    [SerializeField] private int hitPointsCurrent;
    [SerializeField] private bool isDestroyed;

    public string BuildingId { get => buildingId; }
    public string BuildingName { get => buildingName; }
    public int HitPoints { get => hitPoints; }
    public int StoneCost { get => stoneCost; }
    public int WoodCost { get => woodCost; }
    public int IronCost { get => ironCost; }

    public void TakeDamage(int amount)
    {
        if (hitPoints - amount > 0)
            hitPoints -= amount;
        else
        {
            hitPoints = 0;
            isDestroyed = true;
        }
    }
}
