using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetSelectedButton : MonoBehaviour
{
    public GameObject m_Yes;
    public GameObject m_No;
    public GameObject m_Quit;

    GameObject lastselect;
    // Start is called before the first frame update
    void Start()
    {
        lastselect = new GameObject();
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(m_Yes);
    }
    private void OnDisable()
    {
        EventSystem.current.SetSelectedGameObject(m_Quit);
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastselect);
        }
        else
        {
            lastselect = EventSystem.current.currentSelectedGameObject;
        }
    }
}
