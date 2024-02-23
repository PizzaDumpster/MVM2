using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    [SerializeField] int jumpCounter;

    [SerializeField] float coyoteTime = 0.2f;
    [SerializeField] float coyoteTimeCounter;

    [SerializeField] float jumpBufferTime = 0.2f;
    [SerializeField] float jumpBufferCounter; 

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        
        if(isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            rb.sharedMaterial = null;
            groundTimer += Time.deltaTime;
            airTimer = 0;
            jumpCounter = 0;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; 
            rb.sharedMaterial = noStick;
            airTimer += Time.deltaTime;
            groundTimer = 0;
        }

        if (Input.GetKey(KeyCode.D))
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
        if (Input.GetKey(KeyCode.A))
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
        if(jumpBufferCounter > 0 && jumpCounter == 0 && coyoteTimeCounter > 0f) 
        {
            anim.SetTrigger("jump"); 
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpBufferCounter = 0f; 
            jumpCounter++;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            coyoteTimeCounter = 0f; 
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetTrigger("attack");
            RaycastHit2D[] enemyHits = Physics2D.CircleCastAll(attackPoint.position, 1f, Vector2.right);
            foreach (RaycastHit2D hit in enemyHits)
            {
                Debug.Log(hit);
            }
        }
    }
}
