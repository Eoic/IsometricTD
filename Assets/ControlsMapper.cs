using UnityEngine;

public class ControlsMapper : MonoBehaviour
{
    public static ControlsMapper Instance;

    // Key buttons
    public ButtonKeyChanger turnCameraLeftBtn;
    public ButtonKeyChanger turnCameraRightBtn;
    public ButtonKeyChanger openBuildMenuBtn;

    // Default keys
    private readonly KeyCode defaultTurnCameraRight = KeyCode.Q;
    private readonly KeyCode defaultTurnCameraLeft = KeyCode.E;
    private readonly KeyCode defaultOpenBuildMenu = KeyCode.B;

    public KeyCode TurnCameraLeft { get; private set; }
    public KeyCode TurnCameraRight { get; private set; }
    public KeyCode OpenBuildMenu { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start() => MapKeys();

    public void MapKeys()
    {
        if (PlayerPrefs.GetString("TurnCameraLeft") != null)
            TurnCameraLeft = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("TurnCameraLeft"));
        else TurnCameraLeft = defaultTurnCameraLeft;

        if (PlayerPrefs.GetString("TurnCameraRight") != null)
            TurnCameraRight = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("TurnCameraRight"));
        else TurnCameraRight = defaultTurnCameraRight;

        if (PlayerPrefs.GetString("BuildMenu") != null)
            OpenBuildMenu = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("BuildMenu"));
        else OpenBuildMenu = defaultOpenBuildMenu;
    }

    public void ResetDefaults()
    {
        PlayerPrefs.SetString("TurnCameraLeft", defaultTurnCameraLeft.ToString());
        PlayerPrefs.SetString("TurnCameraRight", defaultTurnCameraRight.ToString());
        PlayerPrefs.SetString("BuildMenu", defaultOpenBuildMenu.ToString());
        PlayerPrefs.Save();

        turnCameraLeftBtn.SetKey(defaultTurnCameraLeft.ToString());
        turnCameraRightBtn.SetKey(defaultTurnCameraRight.ToString());
        openBuildMenuBtn.SetKey(defaultOpenBuildMenu.ToString());

        TurnCameraLeft = defaultTurnCameraLeft;
        TurnCameraRight = defaultTurnCameraRight;
        OpenBuildMenu = defaultOpenBuildMenu;
    }
}