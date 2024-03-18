using UnityEngine;
using UnityEngine.EventSystems;

public class SetSelectedButton : MonoBehaviour
{
    public GameObject m_Yes;
    public GameObject m_No;
    public GameObject m_Quit;

    GameObject lastselect;

    void Start()
    {
        lastselect = new GameObject();
    }

    private void OnEnable()
    {
        if (EventSystem.current != null && m_Yes != null)
            EventSystem.current.SetSelectedGameObject(m_Yes);
    }

    private void OnDisable()
    {
        if (EventSystem.current != null && m_Quit != null)
            EventSystem.current.SetSelectedGameObject(m_Quit);
    }

    private void OnDestroy()
    {
        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(null);
    }

    void Update()
    {
        if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastselect);
        }
        else
        {
            lastselect = EventSystem.current.currentSelectedGameObject;
        }
    }
}
