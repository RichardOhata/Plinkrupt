using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class SceneNavigator : MonoBehaviour
{
    public GameObject objectToActivate; // Assign this in the Inspector

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

    public void LoadSceneWithDelay(float delay)
    {
        StartCoroutine(LoadSceneWithDelay("Beta", delay));
    }

    public IEnumerator LoadSceneWithDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadScene(sceneName);
    }

    public void TransitionToGame() {
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true); // Activate the target object
            }
    }
}
