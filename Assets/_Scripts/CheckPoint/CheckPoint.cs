using System;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Animator animator;

    public TriggerStringSO activaterTrigger;
    public ObjectStringSO playerObject;
    public SetCheckMark checkMark = new SetCheckMark();

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
            MessageBuffer<SetCheckMark>.Dispatch(checkMark);
        }
    }

    private void ChangeAnimationState(string newState)
    {
            animator.Play(newState);
    }
}