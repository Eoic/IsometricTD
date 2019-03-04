using UnityEngine;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour, IPointerClickHandler
{
    public LayerMask collisionMask;
    public Material invalidLocation;
    private Material defaultMaterial;
    private Renderer objectRenderer;
    [SerializeField] private Constants.Buildings buildingId;
    [SerializeField] private string buildingName;
    [SerializeField] private int stoneCost;
    [SerializeField] private int woodCost;
    [SerializeField] private int ironCost;
    [SerializeField] private bool onValidPosition;
    [SerializeField] private bool isBuilt = false;
    
    public Constants.Buildings BuildingId { get => buildingId; set => buildingId = value; }
    public string BuildingName { get => buildingName; }
    public int StoneCost { get => stoneCost; }
    public int WoodCost { get => woodCost; }
    public int IronCost { get => ironCost; }
    public bool OnValidPosition { get => onValidPosition; }

    private void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        defaultMaterial = objectRenderer.material;
    }

    private void Start()
    {
        onValidPosition = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & collisionMask) != 0 && !isBuilt)
        {
            objectRenderer.material = invalidLocation;
            onValidPosition = false;
        }
    }
    
    private void OnCollisionStay(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & collisionMask) != 0 && onValidPosition && !isBuilt)
        {
            objectRenderer.material = invalidLocation;
            onValidPosition = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!isBuilt)
        {
            objectRenderer.material = defaultMaterial;
            onValidPosition = true;
        }
    }

    public void SetAsBuilt()
    {
        objectRenderer.material = defaultMaterial;
        AudioManager.instance.Play("Building01");
        isBuilt = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
            AudioManager.instance.Play("PointerClick");
    }
}