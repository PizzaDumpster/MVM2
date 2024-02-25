using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] bool movingRight;
    [SerializeField] bool isGrounded; 
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform attackPoint; 
    [SerializeField] PhysicsMaterial2D noStick;
    [SerializeField] float groundTimer;
    [SerializeField] float airTimer;
    [SerializeField] int availableJumps;
    public int maxJumps; 

    [SerializeField] float coyoteTime = 0.2f;
    [SerializeField] float coyoteTimeCounter;

    [SerializeField] float jumpBufferTime = 0.2f;
    [SerializeField] float jumpBufferCounter;

    private PlayerInputs playerControls;

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
            coyoteTimeCounter = coyoteTime;
            rb.sharedMaterial = null;
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
            anim.SetTrigger("attack");
            RaycastHit2D[] enemyHits = Physics2D.CircleCastAll(attackPoint.position, 1f, Vector2.right);
            foreach (RaycastHit2D hit in enemyHits)
            {
                Debug.Log(hit);
            }
        }
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
