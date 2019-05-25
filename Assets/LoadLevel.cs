using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject selectLevelScreen;

    public GameObject loadingScreen;
    public Image loadingProgress;

    public void StartLevel(string levelName) =>
        StartCoroutine(LoadNewGame(levelName));

    IEnumerator LoadNewGame(string levelName)
    {
        PlayerPrefs.Save();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelName);

        // Turn off other windows
        menuScreen.SetActive(false);
        selectLevelScreen.SetActive(false);

        // Show loading screen
        loadingScreen.SetActive(true);

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            loadingProgress.fillAmount = progress;
            yield return null;
        }
    }
}
