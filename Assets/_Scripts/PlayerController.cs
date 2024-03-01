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
    PlayerHealth playerHealth; 
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
    [SerializeField] public bool wallSlidingUnlocked = false;
    [SerializeField] public bool wallJumpingUnlocked = false; 

    // Ground Detection
    [Header("Ground Detection")]
    [SerializeField] bool isGrounded;
    [SerializeField] Transform groundCheck;

    // Attack Parameters
    [Header("Attack Parameters")]
    [SerializeField] Transform attackPoint;
    public bool isAttacking = false;
    public bool isDownwardAttacking = false; 

    // Physics Material
    [Header("Physics Material")]
    [SerializeField] PhysicsMaterial2D noStick;

    // Jump Timers
    [Header("Jump Timers")]
    [SerializeField] float groundTimer;
    public float airTimer;

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
    [SerializeField] float dashTime = 0.15f;
    [SerializeField] bool tryToDash;
    [SerializeField] bool isDashing;
    public Vector2 direction = new Vector2(0, 0);
    private TrailRenderer tr;

    //Wall Slide and Jump
    [Header("WallSlide and WallJump")]
    private bool isWallSliding;
    private float wallSlidingSpeed = 2f; 
    [SerializeField] Transform wallCheck; 
    [SerializeField] LayerMask wallLayer;
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8, 16);



    //Gravity Modifiers
    [Header("Gravity Modifiers")]
    [SerializeField] float initialGravityMultiplier;
    [SerializeField] const float noGravityMultiplier = 0f;
    [SerializeField] const float wallSlideGravityMultiplier = 1f; 

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
    public float valueY; 
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

        ResetPlayer(); 
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
        playerHealth = GetComponent<PlayerHealth>();
        canDash = true;
        initialGravityMultiplier = rb.gravityScale;

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.05f);
        valueX = playerControls.Player.HorizontalMove.ReadValue<float>();
        SetYDircetion();
        DownwardAttack();

        if (wallSlidingUnlocked)
        {
            WallSlide(); 
        }
        if (wallJumpingUnlocked)
        {
            WallJump();
        }
        

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
            isDownwardAttacking = false;
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
                if (!isWallJumping)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    rb.velocity = new Vector2(speed, rb.velocity.y + 0);
                }
                    
                direction.x = 1;

            }
            else if (valueX < 0)
            {
                isWalking = true;
                movingRight = false;
                if (!isWallJumping)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    rb.velocity = new Vector2(-speed, rb.velocity.y + 0);
                }
                   
                direction.x = -1;

            }
            else
            {
                isIdle = true; 
                isWalking = false;
                direction.x = 0; 
            }
            if(valueY > 0)
            {
                direction.y = 1; 
            }
            else if (valueY < 0)
            {
                direction.y = -1;
            }
            else
            {
                direction.y = 0; 
            }
        }

        if(playerHealth.HealthAmount <= 0)
        {
            ChangeAnimationState("Death");
        }
        else if (isDownwardAttacking)
        {
            ChangeAnimationState("DownwardAttack");
        }
        else if (isAttacking)
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

        if (playerControls.Player.Attack.triggered && !isDownwardAttacking)
        {
            isIdle = false;
            isAttacking = true;


            // Draw a debug circle
           // Debug.DrawRay(attackPoint.position, Vector2.right * 1f, Color.red, 0.5f);

            //Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackPoint.position, 1f);
            //foreach (Collider2D collider in hitColliders)
            //{
            //    if (collider.CompareTag("Player"))
            //    {
            //        continue;
            //    }

            //    IDamageable damageable = collider.GetComponent<IDamageable>();
            //    if (damageable != null)
            //    {
            //        damageable.Damage(weapon.currentWeapon.WeaponDamage);
            //        Debug.Log("Damageable object hit: " + collider.gameObject.name);
            //    }
            //}
        }
        if (unlockedDash)
        {
            if (playerControls.Player.Dash.triggered && canDash)
            {
                StartCoroutine(Dash());
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(attackPoint.position, 1f);
    //}
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

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer); 
    }
    private void WallSlide()
    {
        if(IsWalled() && !isGrounded && valueX != 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false; 
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime; 
        }
        if(playerControls.Player.Jump.triggered && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;
            if(transform.localScale.x != wallJumpingDirection)
            {
                movingRight = !movingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            }
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false; 
    }

    public IEnumerator Dash()
    {
        isDashing = true;
        rb.gravityScale = noGravityMultiplier;
        canDash = false;
        availableDash--;

        rb.velocity = new Vector2(direction.x, direction.y).normalized * dashSpeed;
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        tr.emitting = false;
        rb.velocity = new Vector2(0, 0);
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

    private void ResetPlayer()
    {
        canDash = true;
        availableDash = 1;
        isAttacking = false; 
    }
    private void SetYDircetion()
    {
        valueY = playerControls.Player.VerticalMove.ReadValue<float>();
    }
    private void DownwardAttack()
    {
        if(!isGrounded && direction.y == -1 && playerControls.Player.Attack.triggered)
        {
            isDownwardAttacking = true;
        }
    }
}
