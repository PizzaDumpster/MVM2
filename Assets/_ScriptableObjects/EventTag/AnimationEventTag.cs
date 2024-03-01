using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TaggedEvent", menuName = "Tagged Event")]
public class AnimationEventTag : ScriptableObject
{
    public string tag;
    public UnityEvent unityEvent;
}
