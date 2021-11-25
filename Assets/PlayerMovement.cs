using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private bool facingRight = true;
    public Animator animator;
    public Transform Firepoint;

    Vector2 movement;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

   
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        if (movement.x == 0 && movement.y == 0)
        {
            animator.SetBool("isrunning", false);
        }
        else
        {
            animator.SetBool("isrunning", true);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        if (facingRight == false && movement.x > 0)
        {
            Flip();
        }
        else if (facingRight == true && movement.x < 0)
        {
            Flip();
            
        }

        void Flip()
        {
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
            Firepoint.Rotate(180f, 0f, 0f);
            
        }
    }
}
