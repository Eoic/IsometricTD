﻿using UnityEngine;

public class UIEvents : MonoBehaviour
{
    public static UIEvents Instance = null;

    // Building menu
    public RectTransform resourceBuildingsPanel;
    public RectTransform towersPanel;
    public RectTransform buildingsCategory;
    public RectTransform buildingInfo;
    private RectTransform referenceToOpened;

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
        if(Input.GetKeyDown(KeyCode.B))
        {
            // Disable build mode.
            StructureBuilder.Instance.DisableBuildMode();

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
        switch(type)
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
        /*
        buildingInfo.gameObject.SetActive(true);
        var destroyButton = buildingInfo.GetChild(1).GetComponent<Button>();

        if(destroyButton != null && building != null)
        {
            destroyButton.onClick.RemoveAllListeners();
            destroyButton.onClick.AddListener(() => {
                var animator = building.GetComponent<Animator>();
                animator.Play("TowerDemolition");
                Destroy(building);
            });
        }
        */
    }
}