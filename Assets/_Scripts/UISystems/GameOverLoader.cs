using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        MessageBuffer<PlayerDeath>.Subscribe(GameOverStarted);
        this.gameObject.SetActive(false);
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
