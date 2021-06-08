using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Moving")]
    public float speed;
    private Vector3 scaleChange = new Vector3(8f, 0f, 0f);

    [Header("Jumping")]
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] float jumpCD = 1f;
    private float jumpTime;

    [Header("Cached objectas")]
    public Rigidbody2D rigidBody2D;
    private Animator animator;

    void Start()
    {
        jumpTime = Time.time;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Running();
        Jump();
    }

    private void Jump()
    {
        if (Time.time > jumpTime + jumpCD)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidBody2D.velocity += new Vector2(0f, jumpHeight);
                jumpTime = Time.time;
            }
        }
    }

    private void Running()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            Vector2 newTransform = new Vector2(speed, rigidBody2D.velocity.y);
            rigidBody2D.velocity = newTransform;
            transform.localScale = new Vector3(4f, 4f, 1f);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            Vector2 newTransform = new Vector2(-speed, rigidBody2D.velocity.y);
            rigidBody2D.velocity = newTransform;
            transform.localScale = new Vector3(-4f, 4f, 1f);
        }
        else
        {
            rigidBody2D.velocity = new Vector2(0f, rigidBody2D.velocity.y);
        }

        if (rigidBody2D.velocity.x != 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
