using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] Vector2 jumpForce;
    [SerializeField] bool movingRight;
    [SerializeField] bool isGrounded; 
    [SerializeField] Transform groundCheck; 
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rb.velocity.x);

        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down,0.05f); 
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
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded) 
        {
            rb.AddForce(jumpForce, ForceMode2D.Impulse);
        }
    }
}
