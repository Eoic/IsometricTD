using UnityEngine;

public class Building : MonoBehaviour
{
    public LayerMask collisionMask;
    public Material validLocation;
    public Material invalidLocation;

    private Material defaultMaterial;
    [SerializeField] private Constants.Buildings buildingId;
    [SerializeField] private string buildingName;
    [SerializeField] private int hitPoints;
    [SerializeField] private int stoneCost;
    [SerializeField] private int woodCost;
    [SerializeField] private int ironCost;
    [SerializeField] private int hitPointsCurrent;
    [SerializeField] private bool isDestroyed;
    [SerializeField] private bool onValidPosition = true;
    
    public Constants.Buildings BuildingId { get => buildingId; set => buildingId = value; }
    public string BuildingName { get => buildingName; }
    public int HitPoints { get => hitPoints; }
    public int StoneCost { get => stoneCost; }
    public int WoodCost { get => woodCost; }
    public int IronCost { get => ironCost; }
    public bool OnValidPosition { get => onValidPosition; }

    private void Start()
    {
        defaultMaterial = GetComponent<Renderer>().material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & collisionMask) != 0)
        {
            GetComponent<Renderer>().material = invalidLocation;
            onValidPosition = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GetComponent<Renderer>().material = validLocation;
        onValidPosition = true;
    }
}