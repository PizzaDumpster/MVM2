using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerGroundCheck groundCheck;
    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private PlayerHealth playerHealth;

    public WeaponEquiped weapon;

    public float speed = 2f;

    [Header("States")]
    public PlayerState currentState;
    public PlayerState startState;
    public PlayerState deathState;

    private Vector2 m_InputAxis;

    private IPlayerInput m_Input;
    public Animator PlayerAnimator { get { return animator; } set { animator = value; } }
    public Rigidbody2D PlayerRigidBody { get { return playerRigidbody; } set { playerRigidbody = value; } }

    public IPlayerInput PlayerInput { get { return m_Input; } set { m_Input = value; } }

    public Transform Player { get { return this.transform; } set { value = this.transform; } }

    public PlayerGroundCheck GroundCheck { get { return groundCheck; } }

    void Start()
    {
        m_Input = GetComponent<IPlayerInput>();
        PlayerAnimator = GetComponentInChildren<Animator>();
        PlayerRigidBody = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();
        groundCheck = GetComponentInChildren<PlayerGroundCheck>();

        currentState = startState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        m_InputAxis = m_Input.GetPrimaryAxis();
        currentState.UpdateState();
        HandleMovement();
        CheckForDeath();
    }

    private void CheckForDeath()
    {
        if(playerHealth.HealthAmount <= 0)
        {
            TransitionToState(deathState);
        }
    }

    public void TransitionToState(PlayerState nextState)
    {
        currentState = nextState;

        // Call EnterState method of the next state
        currentState.EnterState(this);
    }

    public void HandleMovement()
    {
        if (PlayerInput.GetPrimaryAxis().x > 0)
        {
            Player.localScale = new Vector3(1, 1, 1);
            PlayerRigidBody.velocity = new Vector2(speed, PlayerRigidBody.velocity.y + 0);
        }
        else if (PlayerInput.GetPrimaryAxis().x < 0)
        {
            Player.localScale = new Vector3(-1, 1, 1);
            PlayerRigidBody.velocity = new Vector2(-speed, PlayerRigidBody.velocity.y + 0);
        }
    }
}
