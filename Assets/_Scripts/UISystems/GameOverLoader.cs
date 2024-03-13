using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameOverLoader : MonoBehaviour
{
    public GameObject m_Continue;
    public GameObject m_Quit;
    IUIInput playerInput;
    GameObject selectedButton; // Variable to store the currently selected button

    void Awake()
    {
        playerInput = GetComponentInParent<IUIInput>();
        MessageBuffer<PlayerDeath>.Subscribe(GameOverStarted);
        this.gameObject.SetActive(false);

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
            if (selectedButton != null)
            {
                OnButtonClick(selectedButton);
            }
        }

        // Get the currently selected button
        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;
        print(currentSelected);
        // Check if the selected button has changed
        if (currentSelected != selectedButton)
        {
            selectedButton = currentSelected;
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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop playing the game in the Unity Editor
#else
        Application.Quit(); // Quit the application in a built version
#endif
    }
}
