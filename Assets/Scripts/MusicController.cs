using UnityEngine;

public class MusicController : MonoBehaviour
{
    //Every theme song must start with "Theme" 
    int themeCount;
    int themeNr = 1;
    string themeName = "Theme";
    string currentTheme => themeName + themeNr.ToString();

    private void Start()
    {
        int themecount = 0;

        foreach (var item in AudioManager.instance.sounds)
            if (item.name.StartsWith("Theme"))
                themecount++;
        
        themeCount = themecount;
    }

    public void NextSong()
    {
        AudioManager.instance.Stop(currentTheme);
        if (themeNr < themeCount)
            themeNr += 1;
        else
            themeNr = 1;
        AudioManager.instance.Play(currentTheme);
    }
}
