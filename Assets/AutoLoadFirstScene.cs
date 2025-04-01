using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoLoadFirstScene : MonoBehaviour
{
    public string firstSceneName = "test123"; // Set this to your first scene's name

    void Start()
    {
        // Check if the first scene is already loaded
        if (!IsSceneLoaded(firstSceneName))
        {
            SceneManager.LoadScene(firstSceneName, LoadSceneMode.Additive);
        }
    }

    bool IsSceneLoaded(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == sceneName)
                return true;
        }
        return false;
    }
}
