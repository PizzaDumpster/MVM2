using UnityEngine;

public class CursorSetter : MonoBehaviour
{
    public static CursorSetter Instance { get; private set; }

    public bool Lock;
    public bool Visible;
    public bool UnlockForUI = true;
    public bool VisibleForUI = true;

    private int m_InUICount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        m_InUICount = 1;
        SetInUI(false);
    }

    public void SetInUI(bool inUI)
    {
        if (!inUI)
        {
            --m_InUICount;

            if (m_InUICount == 0)
            {
                Debug.Log("Setting cursor to locked: " + Lock);
                Cursor.lockState = Lock ? CursorLockMode.Locked : CursorLockMode.None;
                Debug.Log("Setting cursor visibility: " + Visible);
                Cursor.visible = Visible;
            }
        }
        else
        {
            Debug.Log("Setting cursor to unlocked for UI: " + UnlockForUI);
            Cursor.lockState = UnlockForUI ? CursorLockMode.None : CursorLockMode.Locked;
            Debug.Log("Setting cursor visibility for UI: " + VisibleForUI);
            Cursor.visible = VisibleForUI;
            ++m_InUICount;
        }
    }

}
