using UnityEngine;
using UnityEngine.UI;

public class GameSpeedManager : MonoBehaviour
{
    public Button normalSpeedButton;
    public Button fastSpeedButton;

    public void NormalSpeed()
    {
        SwapColors(normalSpeedButton, fastSpeedButton);
        Time.timeScale = 1;
    }

    public void FastSpeed()
    {
        SwapColors(fastSpeedButton, normalSpeedButton);
        Time.timeScale = 2;
    }

    void SwapColors(Button selected, Button other)
    {
        SetTransparency(selected, 1);
        SetTransparency(other, 0);
    }

    void SetTransparency(Button button, float value)
    {
        var colorBlock = button.colors;
        var normalColor = colorBlock.normalColor;
        colorBlock.normalColor = new Color(normalColor.r, normalColor.g, normalColor.b, value);
        button.colors = colorBlock;
    }
}
