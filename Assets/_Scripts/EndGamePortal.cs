using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGamePortal : MonoBehaviour
{
    public float fadeoutTime;
    public float fadeinTime;
    public void EndGame()
    {
        Fade.Instance.DoFade(1, fadeoutTime, ()=> SetUI());
    }

    private static void SetUI()
    {

        MessageBuffer<GameCompleted>.Dispatch();
        Fade.Instance.DoFade(0, 1);
    }
}
