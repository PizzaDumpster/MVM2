using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    [SerializeField] float speed;
    bool movingLeft;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            movingLeft = true;
            transform.localScale = new Vector3(1, 1, 1);
            rb.velocity = rb.velocity + new Vector2(speed, 0);
            anim.SetTrigger("walk");
        }
        else
        {
            if (movingLeft)
            {
                rb.velocity = new Vector2(0f, 0f);
                anim.SetTrigger("idle");
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            movingLeft = false;
            transform.localScale = new Vector3(-1, 1, 1);
            rb.velocity = rb.velocity + new Vector2(-speed, 0);
            anim.SetTrigger("walk");
        }
        else
        {
            if (!movingLeft)
            {
                rb.velocity = new Vector2(0f, 0f);
                anim.SetTrigger("idle");
            }

        }
    }
}
