using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Components
    Animator anim;
    Rigidbody2D rb;
    public WeaponEquiped weapon;

    // Movement Parameters
    [Header("Movement Parameters")]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] bool movingRight;
    [SerializeField] bool isWalking = false;
    [SerializeField] bool isIdle = true;

    //Unlocks
    [Header("Unlockables")]
    [SerializeField] public bool unlockedDoubleJump = false;
    [SerializeField] public bool unlockedDash = false;
    [SerializeField] public bool wallClimbUnlocked = false;

    // Ground Detection
    [Header("Ground Detection")]
    [SerializeField] bool isGrounded;
    [SerializeField] Transform groundCheck;

    // Attack Parameters
    [Header("Attack Parameters")]
    [SerializeField] Transform attackPoint;
    public bool isAttacking = false;

    // Physics Material
    [Header("Physics Material")]
    [SerializeField] PhysicsMaterial2D noStick;

    // Jump Timers
    [Header("Jump Timers")]
    [SerializeField] float groundTimer;
    [SerializeField] float airTimer;

    // Jump Counters
    [Header("Jump Counters")]
    [SerializeField] int availableJumps;
    [SerializeField] const int singleJump = 1;
    [SerializeField] const int doubleJump = 2;
    public bool isJumping = false;

    // Dash
    [Header("Dash")]
    [SerializeField] public bool canDash;
    [SerializeField] int availableDash = 1;
    [SerializeField] const int singleDash = 1;
    [SerializeField] float waitForNextDash = 1;
    [SerializeField] float dashSpeed = 30;
    [SerializeField] float dashTime = 0.1f;
    [SerializeField] bool tryToDash;
    [SerializeField] bool isDashing;
    [SerializeField] Vector2 direction = new Vector2(1, 0);
    private TrailRenderer tr;

    //Gravity Modifiers
    [Header("Gravity Modifiers")]
    [SerializeField] float initialGravityMultiplier;
    [SerializeField] const float noGravityMultiplier = 0f;

    // Coyote Time
    [Header("Coyote Time")]
    [SerializeField] float coyoteTime = 0.2f;
    [SerializeField] float coyoteTimeCounter;

    // Jump Buffer
    [Header("Jump Buffer")]
    [SerializeField] float jumpBufferTime = 0.2f;
    [SerializeField] float jumpBufferCounter;

    private string currentState;

    // Player Input
    private PlayerInputs playerControls;

    // Movement Input
    public float valueX;
    public bool tryToJump;
    public bool tryToAttack;

    private void Awake()
    {
        playerControls = new PlayerInputs();
    }

    private void OnEnable()
    {
        playerControls.Player.Jump.performed += Jump;
        playerControls.Player.Jump.canceled += JumpStop;
        playerControls.Player.Attack.performed += Attack;
        playerControls.Player.Attack.canceled += AttackStop;
        playerControls.Player.Dash.performed += Dash;
        playerControls.Player.Dash.canceled += DashStop;
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
        playerControls.Player.Jump.performed -= Jump;
        playerControls.Player.Jump.canceled -= JumpStop;
        playerControls.Player.Attack.performed -= Attack;
        playerControls.Player.Attack.canceled -= AttackStop;
        playerControls.Player.Dash.performed -= Dash;
        playerControls.Player.Dash.canceled -= DashStop;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        canDash = true;
        initialGravityMultiplier = rb.gravityScale;

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.05f);
        valueX = playerControls.Player.HorizontalMove.ReadValue<float>();

        if (playerControls.Player.Jump.triggered)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (isGrounded)
        {
            isJumping = false;
            coyoteTimeCounter = coyoteTime;
            rb.sharedMaterial = null;
            groundTimer += Time.deltaTime;
            airTimer = 0;
            if (unlockedDoubleJump)
                availableJumps = doubleJump;
            else
                availableJumps = singleJump;
        }
        else if (availableJumps > 0)
        {
            rb.sharedMaterial = noStick;
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
            rb.sharedMaterial = noStick;
            airTimer += Time.deltaTime;
            groundTimer = 0;
        }

        if (!isDashing)
        {
            if (valueX > 0)
            {
                isWalking = true;
                movingRight = true;
                transform.localScale = new Vector3(1, 1, 1);
                rb.velocity = new Vector2(speed, rb.velocity.y + 0);
                direction.x = 1;

            }
            else if (valueX < 0)
            {
                isWalking = true;
                movingRight = false;
                transform.localScale = new Vector3(-1, 1, 1);
                rb.velocity = new Vector2(-speed, rb.velocity.y + 0);
                direction.x = -1;

            }
            else
            {
                isIdle = true; 
                isWalking = false; 
            }
        }

        if (isAttacking)
        {
            ChangeAnimationState("Attack");
        }
        else if (isJumping)
        {
            ChangeAnimationState("Jump");
        }
        else if (isWalking)
        {
            ChangeAnimationState("Walk");
        }
        else if (isIdle)
        {
            ChangeAnimationState("Idle");
        }


        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0f)
        {
            isIdle = false;
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpBufferCounter = 0f;

        }
        if (playerControls.Player.Jump.WasReleasedThisFrame())
        {
            coyoteTimeCounter = 0f;
            availableJumps--;
        }

        if (playerControls.Player.Attack.triggered)
        {
            isIdle = false;
            isAttacking = true;
            AudioPlayer.Instance.PlayAudioClip(weapon.currentWeapon.WeaponSFX);


            // Draw a debug circle
            Debug.DrawRay(attackPoint.position, Vector2.right * 1f, Color.red, 0.5f);

            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, 1f);
            foreach (Collider2D collider in hitColliders)
            {
                if (collider.CompareTag("Player"))
                {
                    continue;
                }

                IDamageable damageable = collider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.Damage(weapon.currentWeapon.WeaponDamage);
                    Debug.Log("Damageable object hit: " + collider.gameObject.name);
                }
            }
        }
        if (unlockedDash)
        {
            if (playerControls.Player.Dash.triggered && canDash)
            {
                StartCoroutine(Dash());
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, 1f);
    }
    private void Jump(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            tryToJump = true;
        }

    }
    private void JumpStop(InputAction.CallbackContext value)
    {
        tryToJump = false;
    }
    private void Attack(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            tryToAttack = true;
        }

    }
    private void AttackStop(InputAction.CallbackContext value)
    {
        tryToAttack = false;
    }
    private void Dash(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            tryToDash = true;
        }

    }
    private void DashStop(InputAction.CallbackContext value)
    {
        tryToDash = false;
    }

    public IEnumerator Dash()
    {
        isDashing = true;
        rb.gravityScale = noGravityMultiplier;
        canDash = false;
        availableDash--;

        rb.velocity = new Vector2(direction.x * dashSpeed, rb.velocity.y);
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        tr.emitting = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.gravityScale = initialGravityMultiplier;
        isDashing = false;
        StartCoroutine(WaitForNextDash());
    }
    public IEnumerator WaitForNextDash()
    {
        yield return new WaitForSeconds(waitForNextDash);
        canDash = true;
        availableDash = singleDash;
    }
    private void ChangeAnimationState(String newState)
    {
        if (currentState != newState)
        {
            anim.Play(newState);
            currentState = newState;
        }
    }

    public void SetAttackFalse()
    {
        isAttacking = false;
    }
}
