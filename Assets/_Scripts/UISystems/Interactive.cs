using UnityEngine;
using UnityEngine.Events;

public class OnInteractStart : BaseMessage { }

public class OnInteractFinish : BaseMessage { }
public class Interactive : MonoBehaviour
{
    public UnityEvent onInteract;
    public UnityEvent onFinished;

    public void Interact()
    {
        if (!isActiveAndEnabled)
            return;
        onInteract?.Invoke();
        MessageBuffer<OnInteractStart>.Dispatch();
    }

    public void Finished()
    {
        onFinished?.Invoke();
        MessageBuffer<OnInteractFinish>.Dispatch();
    }

}
