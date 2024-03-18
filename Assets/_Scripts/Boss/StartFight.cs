using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartFight : MonoBehaviour
{
    public ObjectStringSO player;
    public bool isFighting;

    public UnityEvent fightStarted;
    public UnityEvent fightEnded;
    void Start()
    {
        MessageBuffer<BossFightEnded>.Subscribe(FightsOver);
    }

    private void FightsOver(BossFightEnded obj)
    {
        fightEnded?.Invoke();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(player.objectString) && !isFighting)
        {
            isFighting = true;
            fightStarted?.Invoke();
        }
    }
}
