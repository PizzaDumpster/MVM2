using UnityEngine;
using UnityEngine.UI;

public class GameOverLoader : MonoBehaviour
{
    public GameObject[] buttons;

    void Awake()
    {
        MessageBuffer<PlayerDeath>.Subscribe(GameOverStarted);
        this.gameObject.SetActive(false);
    }

    void RegisterButtons(GameObject button)
    {
        ButtonRegister pointerEvents = button.GetComponent<ButtonRegister>();
        pointerEvents.onClick.AddListener(() => OnSubmit(button));
    }

    private void OnSubmit(GameObject button)
    {
        // You can now perform actions specific to the selected button
        Debug.Log("Submit button clicked on: " + button.name);
    }

    private void GameOverStarted(PlayerDeath obj)
    {
        this.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
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
