using UnityEngine;
using UnityEngine.UI;

public class UIEvents : MonoBehaviour
{
    public static UIEvents Instance = null;
    [SerializeField] private RectTransform buldingsPanel;
    [SerializeField] private RectTransform pauseOverlay;
    [SerializeField] private RectTransform buildingInfo;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            bool isActive = buldingsPanel.gameObject.activeInHierarchy;
            buldingsPanel.gameObject.SetActive(!isActive);
            buildingInfo.gameObject.SetActive(false);
            StructureBuilder.Instance.DisableBuildMode();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            buildingInfo.gameObject.SetActive(false);
            //pauseOverlay.gameObject.SetActive(!pauseOverlay.gameObject.activeInHierarchy);
        }
    }

    public void DisplayBuildingOptions(Building building)
    {
        buildingInfo.gameObject.SetActive(true);
        var destroyButton = buildingInfo.GetChild(1).GetComponent<Button>();

        if(destroyButton != null && building != null)
        {
            destroyButton.onClick.RemoveAllListeners();
            destroyButton.onClick.AddListener(() => {
                var animator = building.GetComponent<Animator>();
                animator.Play("TowerDemolition");
                //Destroy(building);
            });
        }
    }
}
