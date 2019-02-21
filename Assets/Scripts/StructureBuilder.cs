using System.Collections.Generic;
using UnityEngine;

public class StructureBuilder : MonoBehaviour
{
    // FIELDS
    [SerializeField] private List<GameObject> buildings;
    [SerializeField] private bool buildModeEnabled;
    [SerializeField] private Constants.Buildings buildingId;
    private GameObject structureBlueprint;

    // PROPERTIES
    public static StructureBuilder Instance { get; set; } = null;
    public List<GameObject> Buildings { get => buildings; set => buildings = value; }
    public bool BuildModeEnabled { get => buildModeEnabled; set => buildModeEnabled = value; }
    public Constants.Buildings BuildingId { get => buildingId; set => buildingId = value; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void CreateStructure(Constants.Buildings key)
    {
        GameObject building = Buildings.Find(item => item.GetComponent<Building>().BuildingId == key);
        Vector3 position = MouseTracker.Instance.GetMousePosition();

        if (building == null || BuildModeEnabled == false || position.Equals(Vector3.negativeInfinity))
            return;
        
        if (!structureBlueprint.GetComponent<Building>().OnValidPosition)
            return;

        Vector3 localScale = building.transform.localScale; 
        position = new Vector3(Mathf.Round(position.x / localScale.x) / localScale.x, 
                               position.y + (localScale.y / 2), 
                               Mathf.Round(position.z / localScale.z) / localScale.z);
        building.transform.position = position;

        GameObject buildingReference = Instantiate(building, position, Quaternion.identity);
        buildingReference.GetComponent<Building>().SetAsBuilt();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            CreateStructure(BuildingId);
        else if (Input.GetMouseButtonDown(1))
            DisableBuildMode();

        if (buildModeEnabled)
            structureBlueprint.transform.position = GetBuildingPosition(structureBlueprint.transform);
    }

    // Binds building to mouse cursor and checks for colisions
    public void EnableBuildMode(Constants.Buildings key)
    {
        if (BuildModeEnabled)
            Destroy(structureBlueprint);

        BuildingId = key;
        GameObject building = Buildings.Find(item => item.GetComponent<Building>().BuildingId == key);
        BuildModeEnabled = true;

        if (building != null)
            structureBlueprint = Instantiate(building, Vector3.zero, Quaternion.identity);
        else
            BuildModeEnabled = false;
    }

    // Removes blueprint and disables build mode
    public void DisableBuildMode()
    {
        BuildModeEnabled = false;
        Destroy(structureBlueprint);
    }
    
    // Returns building position according to its scale and cursor position
    private Vector3 GetBuildingPosition(Transform structure)
    {
        Vector3 cursorPosition = MouseTracker.Instance.GetMousePosition();
        float? elevation = MouseTracker.Instance.GetHitTransform()?.position.y;

        if (cursorPosition.Equals(Vector3.negativeInfinity))
            return structure.position;

        return new Vector3(Mathf.Round(cursorPosition.x / structure.localScale.x) / structure.localScale.x, 
                           cursorPosition.y + (structure.transform.localScale.y / 2), 
                           Mathf.Round(cursorPosition.z / structure.localScale.z) / structure.localScale.z);
    }
}
