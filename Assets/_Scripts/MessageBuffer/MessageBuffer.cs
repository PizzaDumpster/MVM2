using System;

public static class MessageBuffer<T>
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

    public static void Dispatch(T message = default)
    {
        SendMessage(message);
    }
}
