using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{


    public SceneReference startScene;

    public GameObject startButton;
    public GameObject quitButton;

    GameObject lastselect;

    void Start()
    {
        lastselect = new GameObject();
        EventSystem.current.SetSelectedGameObject(startButton);
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(startButton);
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastselect);
        }
        else
        {
            lastselect = EventSystem.current.currentSelectedGameObject;
        }
    }

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
        // Fade out before transitioning to the new scene
        Fade.Instance.DoFade(1f, .5f); // Fade out completely over 1 second

        // Wait for the fade out to complete
        yield return new WaitForSeconds(1f);

        // Load the new scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(startScene.sceneName);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                // Fade in after the scene is fully loaded
                
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
        
        Fade.Instance.DoFade(0f, 1f); // Fade in completely over 1 second
    }

}
