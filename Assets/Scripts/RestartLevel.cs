using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    public void Restart(string name) =>
        StartCoroutine(LoadLevel(name));

    IEnumerator LoadLevel(string name)
    {
        PlayerPrefs.Save();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(name);

        while (!asyncOperation.isDone)
            yield return null;
    }
}
