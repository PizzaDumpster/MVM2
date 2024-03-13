using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadableSign : MonoBehaviour
{
    public IUIInput uIInput;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    private void Update()
    {

        if (uIInput == null)
        {
            uIInput = GetComponentInParent<IUIInput>();
        }

        if (uIInput != null)
        {
            print(uIInput.IsSubmitDown());
            if (uIInput.IsSubmitDown())
            {
                print("Submit");
            }
        }
    }
}
