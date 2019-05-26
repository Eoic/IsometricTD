using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public RectTransform pauseOverlay;
    public RectTransform pauseMenu;
    public RectTransform optionsMenu;
    public RectTransform controlsMenu;

    // Options sliders
    public Slider musicSlider;
    public Slider soundsSlider;

    private float previousTimescale = 1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePauseMenu(!pauseMenu.gameObject.activeInHierarchy);
    }

    public void TogglePauseMenu(bool state)
    {
        if (optionsMenu.gameObject.activeInHierarchy)
            optionsMenu.gameObject.SetActive(false);

        if (controlsMenu.gameObject.activeInHierarchy)
            controlsMenu.gameObject.SetActive(false);

        if (state)
        {
            previousTimescale = Time.timeScale;
            Time.timeScale = 0;
        }
        else Time.timeScale = previousTimescale;

        pauseOverlay.gameObject.SetActive(state);
        pauseMenu.gameObject.SetActive(state);
    }

    #region PauseMenuActions

    public void Resume() =>
        TogglePauseMenu(false);

    public void RestartLevel() =>
        throw new NotImplementedException();

    public void Options()
    {
        if (PlayerPrefs.HasKey("MusicEffects"))
            musicSlider.value = PlayerPrefs.GetFloat("MusicEffects");

        if (PlayerPrefs.HasKey("SoundEffects"))
            soundsSlider.value = PlayerPrefs.GetFloat("SoundEffects");

        optionsMenu.gameObject.SetActive(true);
        pauseMenu.gameObject.SetActive(false);
    }

    public void Controls()
    {
        pauseMenu.gameObject.SetActive(false);
        controlsMenu.gameObject.SetActive(true);
    }

    public void BackMainMenu() =>
        SceneManager.LoadScene(0);

    public void ExitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    #endregion

    #region OptionsMenuActions

    public void BackToPauseMenu()
    {
        pauseMenu.gameObject.SetActive(true);
        optionsMenu.gameObject.SetActive(false);
        controlsMenu.gameObject.SetActive(false);
    }

    #endregion
}
