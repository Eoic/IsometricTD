using UnityEngine;

public class Resource : MonoBehaviour
{
    public enum ResourceType { STONE, IRON, WOOD }
    [SerializeField] private ResourceType type = ResourceType.STONE;
    [SerializeField] private int supply = 100;

    public ResourceType Type { get => type; }
    public int Supply { get => supply; }
    public bool IsDepleted { get => (supply <= 0); }

    public int ConsumeResource(int amount)
    {
        if (supply - amount < 0)
        {
            int consumed = supply;
            supply = 0;
            return consumed;
        }

        supply -= amount;
        return amount;
    }
}
