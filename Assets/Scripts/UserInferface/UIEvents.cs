using UnityEngine;
using UnityEngine.UI;

public class UIEvents : MonoBehaviour
{
    public static UIEvents Instance = null;

    // Building menu
    public RectTransform resourceBuildingsPanel;
    public RectTransform towersPanel;
    public RectTransform buildingsCategory;
    public RectTransform buildingInfo;

    private RectTransform referenceToOpened;
    private Building selectedBuilding = null;

    private static class BuildingTypes
    {
        public const string ResourceBuildings = "RESOURCES";
        public const string TowerBuildings = "TOWERS";
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(ControlsMapper.Instance.OpenBuildMenu))
        {
            // Disable build mode.
            StructureBuilder.Instance.DisableBuildMode();
            buildingInfo.gameObject.SetActive(false);

            if (selectedBuilding != null)
                selectedBuilding.ToggleViewRange(false);

            // Close opened panel and close buildings category.
            if (buildingsCategory.gameObject.activeInHierarchy)
            {
                if (referenceToOpened == null)
                {
                    TogglePanel(towersPanel, false);
                    TogglePanel(resourceBuildingsPanel, false);
                }
                else TogglePanel(referenceToOpened, false);

                buildingsCategory.gameObject.SetActive(false);
            }
            // Open buildings category and previously opened or first panel
            else
            {
                buildingsCategory.gameObject.SetActive(true);

                if (referenceToOpened != null)
                    TogglePanel(referenceToOpened, true);
                else
                {
                    TogglePanel(towersPanel, true);
                    referenceToOpened = towersPanel;
                }
            }
        }
    }

    // Opens menu panel of specified type
    public void OpenBuildingsPanel(string type)
    {
        buildingInfo.gameObject.SetActive(false);

        switch (type)
        {
            case BuildingTypes.ResourceBuildings:
                TogglePanel(towersPanel, false);
                TogglePanel(resourceBuildingsPanel, true);
                referenceToOpened = resourceBuildingsPanel;
                break;
            case BuildingTypes.TowerBuildings:
                TogglePanel(resourceBuildingsPanel, false);
                TogglePanel(towersPanel, true);
                referenceToOpened = towersPanel;
                break;
            default:
                Debug.LogWarning("Could not find panel.");
                break;
        }
    }

    // Opens or closes menu panel
    void TogglePanel(RectTransform panel, bool mode)
    {
        if (panel == null)
        {
            Debug.LogError("Could not open UI panel.");
            return;
        }

        panel.gameObject.SetActive(mode);
    }

    public void DisplayBuildingOptions(Building building)
    {
        if (building.IsBuilt)
        {
            if (selectedBuilding != null)
                selectedBuilding.ToggleViewRange(false);

            GameAudioManager.instance.Play("BuildingClick");
            selectedBuilding = building;

            // Disable overlaping elements first
            resourceBuildingsPanel.gameObject.SetActive(false);
            towersPanel.gameObject.SetActive(false);
            buildingsCategory.gameObject.SetActive(false);

            buildingInfo.gameObject.SetActive(true);
            var destroyButton = buildingInfo.GetChild(1).GetComponent<Button>();

            if (destroyButton != null && building != null)
            {
                destroyButton.onClick.RemoveAllListeners();
                destroyButton.onClick.AddListener(() =>
                {
                    var animator = building.GetComponent<Animator>();
                    Destroy(building);
                });
            }
        }
    }

    public void Deselect()
    {
        if (selectedBuilding != null)
        {
            selectedBuilding.ToggleViewRange(false);
            buildingInfo.gameObject.SetActive(false);
            selectedBuilding = null;
        }        
    }
}