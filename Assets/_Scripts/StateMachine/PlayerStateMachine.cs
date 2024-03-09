using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerWallCheck wallCheck;
    private PlayerGroundCheck groundCheck;
    private PlayerDashController dashController;
    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private PlayerHealth playerHealth;
    

    public float speed = 2f;
    public float currentDashCooldown = 0.0f;

    [Header("States")]
    public PlayerState currentState;
    public PlayerState previousState;
    public PlayerState startState;
    public PlayerState deathState;
    
    public Vector2 m_InputAxis;

    private IPlayerInput m_Input;

    [Header("Abilities Unlocked")]
    public List<PowerUpSO> unlockedAbilities;

    public Animator PlayerAnimator { get { return animator; } set { animator = value; } }
    
    public Rigidbody2D PlayerRigidBody { get { return playerRigidbody; } set { playerRigidbody = value; } }

    public IPlayerInput PlayerInput { get { return m_Input; } set { m_Input = value; } }

    public Transform Player { get { return this.transform; } set { value = this.transform; } }

    public PlayerGroundCheck GroundCheck { get { return groundCheck; } }
    public PlayerWallCheck WallCheck { get { return wallCheck; } }

    void Start()
    {
        MessageBuffer<PickedUpPowerUp>.Subscribe(AddAbilitiy);
        MessageBuffer<PlayerRespawn>.Subscribe(RespawnCharacterIn);
        m_Input = GetComponent<IPlayerInput>();
        PlayerAnimator = GetComponentInChildren<Animator>();
        PlayerRigidBody = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();
        wallCheck = GetComponentInChildren<PlayerWallCheck>();
        groundCheck = GetComponentInChildren<PlayerGroundCheck>();
        dashController = GetComponent<PlayerDashController>();

        currentState = startState;
        currentState.EnterState(this);
    }

    private void AddAbilitiy(PickedUpPowerUp obj)
    {
        unlockedAbilities.Add(obj.powerUp);
    }

    private void RespawnCharacterIn(PlayerRespawn obj)
    {
        TransitionToState(startState);
    }

    void Update()
    {
        if (PauseController.Instance.IsPaused) return;
        m_InputAxis = m_Input.GetPrimaryAxis();
        currentState.UpdateState();
        HandleMovement();
        CheckForDeath();
        UpdateDashCooldown();
    }

    private void CheckForDeath()
    {
        if(playerHealth.HealthAmount <= 0)
        {
            PlayerRigidBody.velocity = Vector2.zero;
            TransitionToState(deathState);
        }
    }

    public void TransitionToState(PlayerState nextState)
    {
        previousState = currentState;
        currentState.ExitState();

        currentState = nextState;

        currentState.EnterState(this);
    }

    public void HandleMovement()
    {
        if (currentState.CanDash() || currentState.CanWallJump()) return;
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


    private void UpdateDashCooldown()
    {
        if (currentDashCooldown > 0)
        {
            currentDashCooldown -= Time.deltaTime;
        }
        else
        {
            currentDashCooldown = 0;
        }
    }

    public void StartDashCooldown(float coolDownTime)
    {
        dashController.StartDashCooldown(coolDownTime);
    }

    public bool CanDash()
    {
        return dashController.CanDash;
    }


}
