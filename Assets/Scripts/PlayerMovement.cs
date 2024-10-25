using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D _rigidbody;

    [SerializeField]
    [Range(1f, 10f)]
    float _speed;

    [SerializeField]
    [Range(1f, 10f)]
    float _jumpForce;

    [SerializeField]
    Transform _groundCheckTransform;

    [SerializeField]
    LayerMask _groundLayer;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            Jump();

        if (Input.GetKeyDown(KeyCode.L))
            SceneManager.LoadScene(1);

        if (Input.GetKeyDown(KeyCode.P))
            SceneManager.LoadScene(0);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float movementDistance = horizontal * _speed;

        Vector3 newPosition = transform.position + new Vector3(movementDistance, 0, 0);
        //_rigidbody.MovePosition(newPosition);
        _rigidbody.velocity = new Vector2(movementDistance, _rigidbody.velocity.y);
    }

    private void Jump()
    {
        if (!IsGrounded()) return;

        _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheckTransform.position, 0.2f, _groundLayer);
    }
}
