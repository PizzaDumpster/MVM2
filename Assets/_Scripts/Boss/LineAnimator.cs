using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAnimator : MonoBehaviour
{

    private LineRenderer lineRenderer;
    [SerializeField] private Texture[] lineTexture;
    int animationStep;
    [SerializeField] float fps = 30f;
    float fpsCounter; 

    // Start is called before the first frame update
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        fpsCounter += Time.deltaTime;
        if (fpsCounter >= 1f / fps)
        {
            animationStep++;
            if(animationStep == lineTexture.Length)
            {
                animationStep = 0;
            }

            lineRenderer.material.SetTexture("_MainTex", lineTexture[animationStep]);
            

            fpsCounter = 0f;
        }
    }
}
