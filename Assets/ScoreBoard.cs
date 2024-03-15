using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    private float startTime; // The time when the game started
    public string time;

    private bool isPaused;
    private float pauseStartTime;
    private float totalPausedTime;

    public int totalDeaths = 0;

    private void Awake()
    {
        MessageBuffer<PauseMessage>.Subscribe(PauseTime);
        MessageBuffer<GameUnPausedMessage>.Subscribe(ResumeTime);
        MessageBuffer<PlayerDeath>.Subscribe(DeathCalculator);
        startTime = Time.time; // Record the start time when the game starts
        totalDeaths = 0;
    }

    private void DeathCalculator(PlayerDeath obj)
    {
        totalDeaths ++;
    }

    private void OnDestroy()
    {
        MessageBuffer<PauseMessage>.Unsubscribe(PauseTime);
        MessageBuffer<GameUnPausedMessage>.Unsubscribe(ResumeTime);
        MessageBuffer<PlayerDeath>.Unsubscribe(DeathCalculator);
    }

    private void ResumeTime(GameUnPausedMessage obj)
    {
        if (isPaused)
        {
            totalPausedTime += Time.time - pauseStartTime;
            isPaused = false;
            UpdateTimeText(); // Update time string format when the game is resumed
        }
    }

    private void PauseTime(PauseMessage obj)
    {
        isPaused = true;
        pauseStartTime = Time.time;
    }


    private void UpdateTimeText()
    {
        float adjustedTime = GetAdjustedElapsedTime();


        int hours = Mathf.FloorToInt(adjustedTime / 3600f);
        int minutes = Mathf.FloorToInt((adjustedTime - hours * 3600f) / 60f);
        int seconds = Mathf.FloorToInt(adjustedTime - hours * 3600f - minutes * 60f);


        time = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    // Method to get the adjusted elapsed time
    private float GetAdjustedElapsedTime()
    {
        // Calculate the elapsed time since the game started, excluding the paused time
        float currentTime = Time.time;
        float elapsedSinceStart = currentTime - startTime;
        float adjustedTime = elapsedSinceStart - totalPausedTime;
        return adjustedTime;
    }

    // Method to get the current time string format
    public string GetCurrentTime()
    {
        UpdateTimeText();
        return time;
    }

    public int GetDeathCount()
    {
        return totalDeaths;
    }
}
