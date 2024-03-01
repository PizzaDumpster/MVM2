using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

[System.Serializable]
public struct AnimEvent
{
    public AnimationEventTag eventName;
    public UnityEvent eventTrigger;
}

public class TaggedEventManager : MonoBehaviour
{
    public List<AnimEvent> taggedEvents = new List<AnimEvent>();

    // Called by animation events
    public void TriggerEvent(AnimationEventTag objString)
    {
        AnimEvent taggedEvent = taggedEvents.Find(e => e.eventName == objString);
        if (taggedEvent.eventName != null)
        {
            taggedEvent.eventTrigger.Invoke();
        }
        else
        {
            Debug.LogWarning("No event found for tag: " + objString.tag);
        }
    }
}
