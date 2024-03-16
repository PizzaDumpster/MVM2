using UnityEngine.Events;
using UnityEngine;

public class PlayrDetector : MonoBehaviour
{
    public ObjectStringSO player;

    public UnityEvent onEnter;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == player.objectString)
        {
            print("PlayerEntered");
            onEnter?.Invoke();
        }
    }

}
