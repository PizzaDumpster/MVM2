using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private Animator animator;
    private PlayerHealth playerHealth;
    public WeaponEquiped weapon;
    private PlayerInputs playerControls;

    public PlayerState currentState;

    public PlayerState idleState;
    public PlayerState movementState;
    public PlayerState attackState;

    public Animator PlayerAnimator { get { return animator; } set { animator = value; } }
    public Rigidbody PlayerRigidBody { get { return playerRigidbody; } set { playerRigidbody = value; } }

    private IPlayerInput m_Input;

    void Start()
    {
        m_Input = GetComponent<IPlayerInput>();
        PlayerAnimator = GetComponentInChildren<Animator>();
        PlayerRigidBody = GetComponent<Rigidbody>();

        currentState = idleState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }

    public void TransitionToState(PlayerState nextState)
    {
        currentState = nextState;

        // Call EnterState method of the next state
        currentState.EnterState(this);
    }

    public int Movement(float threshold)
    {
        float magnitude = PlayerRigidBody.velocity.magnitude;
        int result = magnitude > threshold ? 1 : 0;

        return result;
    }
}
