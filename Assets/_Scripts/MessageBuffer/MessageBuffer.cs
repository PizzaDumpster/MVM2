using System;

public static class MessageBuffer<T> where T: BaseMessage, new()
{
    public static event Action<T> MessageReceived;

    public static void SendMessage(T message)
    {
        MessageReceived?.Invoke(message);
    }

    public static void Subscribe(Action<T> callback)
    {
        MessageReceived += callback;
    }

    public static void Unsubscribe(Action<T> callback)
    {
        MessageReceived -= callback;
    }

    public static void Dispatch(T message)
    {
        SendMessage(message);
    }
}
