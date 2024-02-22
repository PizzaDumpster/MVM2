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
        
        if(isGrounded)
        {
            groundTimer += Time.deltaTime;
            airTimer = 0;
            jumpCounter = 0;
        }
        else
        {
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
                anim.SetTrigger("idle");
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
                anim.SetTrigger("idle");
            }

        }
        if(Input.GetKeyDown(KeyCode.Space) && jumpCounter == 0 && isGrounded && (groundTimer > 0 || airTimer < 0.5f)) 
        {
            anim.SetTrigger("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCounter++;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetTrigger("attack");
            RaycastHit2D[] enemyHits = Physics2D.CircleCastAll(attackPoint.position, 1f, Vector2.right);
            foreach (RaycastHit2D hit in enemyHits)
            {
                Debug.Log(hit.collider.name);
            }
        }
    }
}
