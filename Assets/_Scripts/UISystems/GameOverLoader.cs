using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverLoader : MonoBehaviour
{
    public SceneReference titleScreen;

    public GameObject m_Continue;
    public GameObject m_Quit;


    IUIInput playerInput;
    GameObject selectedButton; // Variable to store the currently selected button
    GameObject lastselect;

    void Awake()
    {
        lastselect = new GameObject();
        playerInput = GetComponentInParent<IUIInput>();
        MessageBuffer<PlayerDeath>.Subscribe(GameOverStarted);
        this.gameObject.SetActive(false);

    }

    private void OnDestroy()
    {
        MessageBuffer<PlayerDeath>.Unsubscribe(GameOverStarted);
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(m_Continue);
    }

    private void Update()
    {
        // Handle controller submit event
        if (playerInput.IsSubmitDown())
        {
            if (lastselect != null)
            {
                OnButtonClick(lastselect);
            }
        }

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastselect);
        }
        else
        {
            lastselect = EventSystem.current.currentSelectedGameObject;
        }

    }

    private void OnButtonClick(GameObject clickedButton)
    {
        Debug.Log("Clicked button: " + clickedButton.name);
    }

    private void GameOverStarted(PlayerDeath obj)
    {
        MessageBuffer<PauseMessage>.Dispatch();
        this.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        MessageBuffer<GameUnPausedMessage>.Dispatch();
        MessageBuffer<PlayerRespawn>.Dispatch();
        this.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        StartCoroutine(TransitionToScene(titleScreen.name));
    }

    IEnumerator TransitionToScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(titleScreen.sceneName);
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
