using UnityEngine;
using UnityEngine.EventSystems;

public class Building : MonoBehaviour, IPointerClickHandler
{
    public LayerMask collisionMask;
    public Material invalidLocation;
    private Material defaultMaterial;
    private Renderer objectRenderer;
    private LineRenderer line;
    [SerializeField] private int viewCircleSegments;
    [SerializeField] private int viewRange;
    [SerializeField] private string buildingId;
    [SerializeField] private int stoneCost;
    [SerializeField] private int woodCost;
    [SerializeField] private int ironCost;
    [SerializeField] private bool onValidPosition;
    [SerializeField] private bool isBuilt = false;
    
    public string BuildingId { get => buildingId; set => buildingId = value; }
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
        line = gameObject.GetComponent<LineRenderer>();
        line.positionCount = viewCircleSegments + 1;
        CreateViewCircle();
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
    //AudioManager.instance.Play("Building01");

    public void SetAsBuilt()
    {
        objectRenderer.material = defaultMaterial;
        ShowParticleEffects();
        isBuilt = true;
    }

    void ShowParticleEffects()
    {
        var particleSystem = GetComponent<ParticleSystem>();

        if(particleSystem != null)
            particleSystem.Play();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        /*
        if(eventData.button == PointerEventData.InputButton.Left)
            AudioManager.instance.Play("PointerClick");
        */
        // Invoke building options menu on click
        UIEvents.Instance.DisplayBuildingOptions(this);
    }

    private void CreateViewCircle()
    {
        float x, z, angle = 90f;

        for (int i = 0; i < viewCircleSegments + 1; i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * viewRange;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * viewRange;
            line.SetPosition(i, new Vector3(x, 0, z));
            angle += 360f / viewRange;
        }
    }
}