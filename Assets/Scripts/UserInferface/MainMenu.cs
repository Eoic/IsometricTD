using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject loadingScreen;
    public Image loadingProgress;

    public void NewGame()
    {
        StartCoroutine(LoadNewGame());
    }

    public void SelectLevel()
    {

    }

    public void Options()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }

    // ---

    IEnumerator LoadNewGame()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("GameLevel2");
        menuScreen.SetActive(false);
        loadingScreen.SetActive(true);

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            loadingProgress.fillAmount = progress;
            yield return null;
        }
    }
}
