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

    // Ground Detection
    [Header("Ground Detection")]
    [SerializeField] bool isGrounded;
    [SerializeField] Transform groundCheck;

    // Attack Parameters
    [Header("Attack Parameters")]
    [SerializeField] Transform attackPoint;

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
    public int maxJumps;

    // Coyote Time
    [Header("Coyote Time")]
    [SerializeField] float coyoteTime = 0.2f;
    [SerializeField] float coyoteTimeCounter;

    // Jump Buffer
    [Header("Jump Buffer")]
    [SerializeField] float jumpBufferTime = 0.2f;
    [SerializeField] float jumpBufferCounter;

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
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
        playerControls.Player.Jump.performed -= Jump;
        playerControls.Player.Jump.canceled -= JumpStop;
        playerControls.Player.Attack.performed -= Attack;
        playerControls.Player.Attack.canceled -= AttackStop;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down,0.05f);
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
            coyoteTimeCounter = coyoteTime;
            rb.sharedMaterial = null;
            groundTimer += Time.deltaTime;
            airTimer = 0;
            availableJumps = maxJumps;
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

        if (valueX > 0)
        {
            movingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
            rb.velocity = new Vector2(speed,rb.velocity.y + 0);
            anim.SetTrigger("walk");
        }
        else
        {
            if (movingRight)
            {
                rb.velocity += new Vector2(0f, 0f);
                if(isGrounded)
                {
                    anim.SetTrigger("idle");
                }
                    
            }
        }
        if (valueX < 0)
        {
            movingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
            rb.velocity = new Vector2(-speed, rb.velocity.y + 0);
            anim.SetTrigger("walk");
        }
        else
        {
            if (!movingRight)
            {
                rb.velocity += new Vector2(0f, 0f);
                if (isGrounded)
                {
                    anim.SetTrigger("idle");
                }
                
            }

        }
        if(jumpBufferCounter > 0 && coyoteTimeCounter > 0f) 
        {
            anim.SetTrigger("jump"); 
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
            AudioPlayer.Instance.PlayAudioClip(weapon.currentWeapon.WeaponSFX);
            anim.SetTrigger("attack");

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
}
