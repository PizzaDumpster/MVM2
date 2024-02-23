using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UILoader : MonoBehaviour
{
    public List<GameObject> UI_Objects = new List<GameObject>();
    public Transform UITransform;
    void Start()
    {
        foreach(GameObject UiObject in UI_Objects)
        {
            Instantiate(UiObject, UITransform);
        }
    }


}
