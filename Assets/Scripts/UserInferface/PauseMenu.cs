using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public RectTransform pauseOverlay;
    public RectTransform pauseMenu;
    public RectTransform optionsMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePauseMenu(!pauseMenu.gameObject.activeInHierarchy);
    }

    public void TogglePauseMenu(bool state)
    {
        if (optionsMenu.gameObject.activeInHierarchy)
            optionsMenu.gameObject.SetActive(false);

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
        optionsMenu.gameObject.SetActive(true);
        pauseMenu.gameObject.SetActive(false);
    }

    public void Controls()
    {

    }

    public void ExitGame() =>
        Application.Quit();

    #endregion

    #region OptionsMenuActions

    public void BackToPauseMenu()
    {
        pauseMenu.gameObject.SetActive(true);
        optionsMenu.gameObject.SetActive(false);
    }

    #endregion
}
