using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class ShowInfoOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform buildingInfoPanel;
    public string buildingKey;

    public void OnPointerEnter(PointerEventData eventData)
    {
        buildingInfoPanel.gameObject.SetActive(true);

        if (buildingKey != null)
        {
            // Find reference to building.
            GameObject buildingGameObj = StructureBuilder.Instance.Buildings
                                  .Find(item =>
                                  item.GetComponentInChildren<Building>().BuildingId == buildingKey
                                      );

            // If building was found, display its info.
            if (buildingGameObj != null)
            {
                Building building = buildingGameObj.GetComponentInChildren<Building>();
                buildingInfoPanel.GetChild(0).GetComponent<TextMeshProUGUI>().text = building.Name;
                buildingInfoPanel.GetChild(1).GetComponent<TextMeshProUGUI>().text = building.WoodCost.ToString();
                buildingInfoPanel.GetChild(2).GetComponent<TextMeshProUGUI>().text = building.StoneCost.ToString();
                buildingInfoPanel.GetChild(3).GetComponent<TextMeshProUGUI>().text = building.IronCost.ToString();

                // TODO: If tower script was found, additionally display tower damage per seconds
                var tower = building.GetComponentInChildren<ShootEnemies>();

                // 9, 10
                if (tower != null)
                {
                    ToggleAdditionalInfo(true);
                    buildingInfoPanel.GetChild(9).GetComponent<TextMeshProUGUI>().text = tower.damage.ToString();
                    buildingInfoPanel.GetChild(10).GetComponent<TextMeshProUGUI>().text = tower.radius.ToString();
                }
                else ToggleAdditionalInfo(false);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buildingInfoPanel.gameObject.SetActive(false);
    }

    public void ToggleAdditionalInfo(bool state)
    {
        buildingInfoPanel.GetChild(7).gameObject.SetActive(state);
        buildingInfoPanel.GetChild(8).gameObject.SetActive(state);
        buildingInfoPanel.GetChild(9).gameObject.SetActive(state);
        buildingInfoPanel.GetChild(10).gameObject.SetActive(state);
    }
}
