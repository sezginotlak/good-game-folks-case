using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigidBody;

    [SerializeField]
    [Range(1f, 10f)]
    float speed;

    [SerializeField]
    [Range(1f, 25f)]
    float jumpForce;

    [SerializeField]
    Transform groundCheckTransform;

    [SerializeField]
    LayerMask groundLayer;

    Animator animator;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();   
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            Jump();
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float movementDistance = horizontal * speed;

        animator.SetFloat("speed", Mathf.Abs(movementDistance));

        if (movementDistance > 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        else if (movementDistance < 0)
            transform.localScale = Vector3.one;
        rigidBody.velocity = new Vector2(movementDistance, rigidBody.velocity.y);
    }

    private void Jump()
    {
        if (!IsGrounded()) return;

        rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckTransform.position, 0.2f, groundLayer);
    }
}
