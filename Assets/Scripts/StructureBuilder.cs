using System.Collections.Generic;
using UnityEngine;

public class StructureBuilder : MonoBehaviour
{
    // FIELDS
    public List<GameObject> buildings;
    public bool buildModeEnabled;
    public string buildingId;
    private GameObject structureBlueprint;

    // PROPERTIES
    public static StructureBuilder Instance { get; set; } = null;
    public bool BuildModeEnabled { get => buildModeEnabled; set => buildModeEnabled = value; }
    public string BuildingId { get => buildingId; set => buildingId = value; }
    public List<GameObject> Buildings { get => buildings; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        buildModeEnabled = false;
        buildingId = "NONE";
    }

    public void CreateStructure(string key)
    {
        // Find building with provided key
        GameObject building = buildings.Find(item => item.GetComponentInChildren<Building>().BuildingId == key);
        Vector3 position = MouseTracker.Instance.GetMousePosition();

        // Game manager does not have reference to bilding with this key
        if (building == null || BuildModeEnabled == false || position.Equals(Vector3.negativeInfinity))
        {
            Debug.LogWarning("Building with id " + key + " was not found.");
            return;
        }
        
        // Building is not on valid surface
        if (!structureBlueprint.GetComponentInChildren<Building>().OnValidPosition)
            return;

        // Not enough resources to build this building.
        if (!ResourceManager.Instance.ConsumeBuildingResources(building.GetComponentInChildren<Building>()))
        {
            Debug.LogWarning("Not enough resources.");
            return;
        }

        // Place building
        Vector3 localScale = building.transform.localScale; 
        position = new Vector3(Mathf.Round(position.x / localScale.x) / localScale.x, position.y, Mathf.Round(position.z / localScale.z) / localScale.z);
        building.transform.position = position;
        GameObject buildingReference = Instantiate(building, position, building.transform.rotation);
        buildingReference.GetComponentInChildren<Building>().SetAsBuilt();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && buildModeEnabled)
            CreateStructure(BuildingId);
        else if (Input.GetMouseButtonDown(1))
            DisableBuildMode();

        if (buildModeEnabled)
            structureBlueprint.transform.position = GetBuildingPosition(structureBlueprint.transform);
    }

    // Binds building to mouse cursor and checks for colisions
    public void EnableBuildMode(string key)
    {
        if (BuildModeEnabled)
            Destroy(structureBlueprint);

        BuildingId = string.Copy(key);
        GameObject building = buildings.Find(item => item.GetComponentInChildren<Building>().BuildingId.Equals(key));
        BuildModeEnabled = true;

        if (building != null)
            structureBlueprint = Instantiate(building, MouseTracker.Instance.GetMousePosition(), building.transform.rotation);
        else
            BuildModeEnabled = false;
    }

    // Removes blueprint and disables build mode
    public void DisableBuildMode()
    {
        BuildModeEnabled = false;
        BuildingId = "NONE";
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
                           cursorPosition.y, Mathf.Round(cursorPosition.z / structure.localScale.z) / structure.localScale.z);
    }
}
