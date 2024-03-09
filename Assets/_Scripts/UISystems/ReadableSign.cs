using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadableSign : MonoBehaviour
{
    UIInput uIInput;

    private void Start()
    {
        if (uIInput == null)
        {
            uIInput = GetComponentInParent<UIInput>();
        }

        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        
        print(uIInput.IsSubmitDown());

        if (uIInput != null)
        {
            if (uIInput.IsSubmitDown())
            {
                print("Submit");
            }
        }
    }
}
