using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SceneReference startScene;

    public void StartScene()
    {
        StartCoroutine(TransitionToScene(startScene.name));
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop playing the game in the Unity Editor
        #else
            Application.Quit(); // Quit the application in a built version
        #endif
    }

    IEnumerator TransitionToScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(startScene.sceneName);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.2f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
