using UnityEngine;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour, IPointerClickHandler
{
    [Range(1, 50)] public int viewRangeSize = 1;
    public LayerMask collisionMask;
    public Material invalidLocation;
    public string buildingName;
    public Transform viewRange;

    private Material defaultMaterial;
    private Renderer objectRenderer;

    [SerializeField] private string buildingId;
    [SerializeField] private int stoneCost;
    [SerializeField] private int woodCost;
    [SerializeField] private int ironCost;
    [SerializeField] private bool onValidPosition;
    [SerializeField] private bool isBuilt = false;

    public bool IsBlueprint = true;
    
    public string BuildingId { get => buildingId; set => buildingId = value; }
    public string Name { get => buildingName; }
    public int StoneCost { get => stoneCost; }
    public int WoodCost { get => woodCost; }
    public int IronCost { get => ironCost; }
    public bool OnValidPosition { get => onValidPosition; }

    public bool IsBuilt { get => isBuilt; }

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
        ShowParticleEffects();
        isBuilt = true;

        if (viewRange != null)
            viewRange.gameObject.SetActive(false);

        GameAudioManager.instance.Play("Build");
    }

    void ShowParticleEffects()
    {
        var particleSystem = GetComponent<ParticleSystem>();

        if(particleSystem != null)
            particleSystem.Play();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            ToggleViewRange(true);
            UIEvents.Instance.DisplayBuildingOptions(this);
        }
    }

    public void ToggleViewRange(bool state)
    {
        if (viewRange != null)
            viewRange.gameObject.SetActive(state);
    }
}