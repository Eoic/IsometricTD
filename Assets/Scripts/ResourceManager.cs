using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    public TextMeshProUGUI woodText;
    public TextMeshProUGUI stoneText;
    public TextMeshProUGUI ironText;

    [SerializeField] private int wood;
    [SerializeField] private int stone;
    [SerializeField] private int iron;

    private const int resourceLimitMax = 50_000;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        UpdateResourceDisplay();
    }

    public void AddWood(int amount)
    {
        if (wood + amount <= resourceLimitMax)
        {
            wood += amount;
            woodText.text = wood.ToString();
        }
    }

    public void AddStone(int amount)
    {
        if (stone + amount <= resourceLimitMax)
        {
            stone += amount;
            stoneText.text = stone.ToString();
        }
    }

    public void AddIron(int amount)
    {
        if (iron + amount <= resourceLimitMax)
        {
            iron += amount;
            ironText.text = iron.ToString();
        }
    }

    public bool ConsumeBuildingResources(Building building)
    {
        if (building == null)
        {
            Debug.LogError("Building reference is null.");
            return false;
        }

        if (building.WoodCost < 0 || building.StoneCost < 0 || building.IronCost < 0)
            return false;

        if(wood - building.WoodCost >= 0 && stone - building.StoneCost >= 0 && iron - building.IronCost >= 0)
        {
            wood -= building.WoodCost;
            stone -= building.StoneCost;
            iron -= building.IronCost;
            UpdateResourceDisplay();
            return true;
        }

        return false;
    }

    public void UpdateResourceDisplay()
    {
        woodText.text = wood.ToString().TrimEnd();
        stoneText.text = stone.ToString().TrimEnd();
        ironText.text = iron.ToString().TrimEnd();
    }
}
