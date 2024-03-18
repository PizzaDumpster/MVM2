using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameCompleted : BaseMessage { }
public class CompletedGame : MonoBehaviour
{
    public SceneReference menuScene;
    public ScoreBoard scoreBoard;

    [Header("")]
    public GameObject onQuit;
    public GameObject onMainMenu;

    [Header("")]
    public TextMeshProUGUI deathCount;
    public TextMeshProUGUI time;

    GameObject lastselect;

    void Start()
    {
        lastselect = new GameObject();
        EventSystem.current.SetSelectedGameObject(onMainMenu);
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(onMainMenu);
    }

    void Awake()
    {
        MessageBuffer<GameCompleted>.Subscribe(GameCompletion);
        this.gameObject.SetActive(false);
    }

    public void OnDestroy()
    {
        MessageBuffer<GameCompleted>.Unsubscribe(GameCompletion);
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

    private void GameCompletion(GameCompleted obj)
    {
        MessageBuffer<PauseMessage>.Dispatch();

        deathCount.text = scoreBoard.GetDeathCount().ToString();
        time.text = scoreBoard.GetCurrentTime();

        this.gameObject.SetActive(true);

        EventSystem.current.SetSelectedGameObject(onMainMenu);
    }

    public void MenuScene()
    {
        Debug.Log("MenuScene method called"); // Add debug log statement
        StartCoroutine(TransitionToScene(menuScene.sceneName));
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
        Debug.Log("Transitioning to scene: " + sceneName); // Add debug log statement

        yield return new WaitForSecondsRealtime(1f);

        // Load the new scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                PauseController.Instance.Resume();
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
    }

}
