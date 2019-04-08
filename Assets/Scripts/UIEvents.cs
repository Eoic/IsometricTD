using UnityEngine;

public class UIEvents : MonoBehaviour
{
    public static UIEvents Instance = null;
    [SerializeField] private RectTransform buldingsPanel;
    [SerializeField] private RectTransform pauseOverlay;

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
            StructureBuilder.Instance.DisableBuildMode();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
            pauseOverlay.gameObject.SetActive(!pauseOverlay.gameObject.activeInHierarchy);
    }
}
