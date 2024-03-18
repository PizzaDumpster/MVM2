using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivated : MonoBehaviour
{

    public void FightStarted()
    {
        MessageBuffer<BossFightStarted>.Dispatch();
    }

    public void FightEnded()
    {
        MessageBuffer<BossFightEnded>.Dispatch();
    }
        
}
