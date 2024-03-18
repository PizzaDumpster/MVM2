//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerGroundCheck : MonoBehaviour
//{
//    [SerializeField] bool isGrounded;
//    [SerializeField] Transform groundCheck;
//    [SerializeField] LayerMask groundLayer;
//    [SerializeField] float groundCheckDistance = 0.025f;

//    void Update()
//    {
//        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
//        Debug.DrawRay(groundCheck.position, Vector2.down * groundCheckDistance, Color.green);
//    }

//    public bool IsGrounded()
//    {
//        return isGrounded;
//    }
//}

using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [SerializeField] bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Vector2 boxSize = new Vector2(1f, 0.1f); // Size of the box collider for ground check

    void Update()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, boxSize, 0f, groundLayer);
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    // Draw the box gizmo in the Scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, boxSize);
    }

    // Adjust the box size in the editor using Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, boxSize);
    }
}

