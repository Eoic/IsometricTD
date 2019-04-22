using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
                                  .Find(item => item.GetComponentInChildren<Building>()
                                  .BuildingId == buildingKey);

            // If building was found, display its info.
            if (buildingGameObj != null)
            {
                Building building = buildingGameObj.GetComponentInChildren<Building>();
                buildingInfoPanel.GetChild(0).GetComponent<Text>().text = building.Name;
                buildingInfoPanel.GetChild(1).GetComponent<Text>().text = building.WoodCost.ToString();
                buildingInfoPanel.GetChild(3).GetComponent<Text>().text = building.StoneCost.ToString();
                buildingInfoPanel.GetChild(5).GetComponent<Text>().text = building.IronCost.ToString();

                // TODO: If tower script was found, additionally display tower damage per seconds
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buildingInfoPanel.gameObject.SetActive(false);
    }
}
