using System.Collections.Generic;
using UnityEngine;

public class StructureBuilder : MonoBehaviour
{
    // FIELDS
    [SerializeField] private List<GameObject> buildings;
    [SerializeField] private bool buildModeEnabled;
    [SerializeField] private Constants.Buildings buildingId;
    private GameObject structureToPlace;
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

        position = new Vector3(position.x, building.transform.localScale.y / 2, position.z);
        building.transform.position = position;
        Instantiate(building, position, Quaternion.identity);
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
            structureBlueprint = Instantiate(building, GetBuildingPosition(building.transform), Quaternion.identity);
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

        if (cursorPosition.Equals(Vector3.negativeInfinity))
            return new Vector3(structure.position.x, structure.transform.localScale.y / 2, structure.position.z);

        return new Vector3(cursorPosition.x, structure.transform.localScale.y / 2, cursorPosition.z);
    }
}
