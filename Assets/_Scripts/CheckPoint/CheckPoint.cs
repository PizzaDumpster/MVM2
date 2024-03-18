using System;
using UnityEngine;
using UnityEngine.Events;

public class CheckPoint : MonoBehaviour
{
    private Animator animator;
    
    [Header("")]
    public TriggerStringSO activaterTrigger;
    public ObjectStringSO playerObject;
    public SetCheckMark checkMark = new SetCheckMark();

    [Header("")]
    public UnityEvent onIgnited;


    private bool checkpointActivated;

    public AudioClip checkpointActivatedAudio;

    private void Start()
    {
        checkMark.checkPoint = this.transform;
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == playerObject.objectString)
        {
            ChangeAnimationState(activaterTrigger.triggerString);   
            if (!checkpointActivated)
            {
                AudioPlayer.Instance.PlayAudioClip(checkpointActivatedAudio);
                checkpointActivated = true;
                CheckPointSet();
                onIgnited?.Invoke();
            }
            
        }
    }

    private void ChangeAnimationState(string newState)
    {
            animator.Play(newState);
    }

    public void CheckPointSet()
    {
        MessageBuffer<SetCheckMark>.Dispatch(checkMark);
    }
}