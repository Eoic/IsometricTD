using UnityEngine;

public class UIEvents : MonoBehaviour
{
    public static UIEvents Instance = null;
    [SerializeField] private RectTransform buldingsPanel;

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
    }
}
