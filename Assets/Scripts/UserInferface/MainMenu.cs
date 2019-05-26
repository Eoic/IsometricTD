using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject levelsScreen;
    public GameObject options;
    public GameObject rankings;

    public Slider musicSlider;
    public Slider soundsSlider;

    public void SelectLevel()
    {
        menuScreen.SetActive(false);
        levelsScreen.SetActive(true);
    }

    public void Options()
    {
        menuScreen.SetActive(false);
        options.SetActive(true);

        if (PlayerPrefs.HasKey("MusicEffects"))
            musicSlider.value = PlayerPrefs.GetFloat("MusicEffects");

        if (PlayerPrefs.HasKey("SoundEffects"))
            soundsSlider.value = PlayerPrefs.GetFloat("SoundEffects");
    }

    public void Quit()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    /*
    IEnumerator LoadNewGame()
    {
        PlayerPrefs.Save();
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
    */

    public void BackToMenu()
    {
        options.SetActive(false);
        levelsScreen.SetActive(false);
        rankings.SetActive(false);
        menuScreen.SetActive(true);
    }

    public void Rankings()
    {
        menuScreen.SetActive(false);
        rankings.SetActive(true);
    }
}
