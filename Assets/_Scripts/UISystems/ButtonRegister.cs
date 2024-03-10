using UnityEngine.Events;
using UnityEngine;

public class ButtonRegister : MonoBehaviour
{
    public UnityEvent onClick;

    public void Submit()
    {
        onClick?.Invoke();
    }
}
