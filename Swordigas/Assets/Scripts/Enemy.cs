using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    public Animator animator;
    public Transform playerPosition;

    public int maxHealth = 100;
    private int currentHealth;
    [SerializeField] float jumpX;
    [SerializeField] float jumpY;
    bool canYouJump = true;
    bool lookingRight = true;
    [SerializeField] float jumpDelay = 0.5f;
    private float nextJumpTime;
    private bool startJumping;

    private bool lifeState = false;

    public Rigidbody2D rigidBody;

    void Start()
    {
        currentHealth = maxHealth;
        nextJumpTime = 0;
    }

    private void Update()
    {
        CheckPlayerPosition();
        if (startJumping == true)
        {
            Move();
        }
        
    }

    private void CheckPlayerPosition()
    {
        if (Math.Abs(playerPosition.position.x + gameObject.transform.position.x * -1) < 10)
        {
            startJumping = true;
        }
        if (playerPosition.position.x >= gameObject.transform.position.x)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            lookingRight = true;
        }
        else
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            lookingRight = false;
        }
    }

    private void Move()
    {
        if (nextJumpTime <= Time.time)
        {
            if (canYouJump == true)
            {
                canYouJump = false;
                if (lookingRight == true)
                {
                    rigidBody.velocity += new Vector2(jumpX * 1, jumpY);
                }
                else
                {
                    rigidBody.velocity += new Vector2(jumpX * -1, jumpY);
                }
            }
        }
    }

    // rigidBody.velocity += new Vector2(jumpX* side, jumpY);

    public void takeDamage(int damage)
    {
        currentHealth -= damage;

        // play hurt animation
        animator.SetTrigger("hurt");
  
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        lifeState = true;
        Debug.Log("He is dead");

        // play death animation
        animator.SetBool("isDead", true);

        // disable enemy
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (canYouJump == false)
            {
                nextJumpTime = Time.time + jumpDelay;
            }
            canYouJump = true;
            if (lifeState == true)
            {
                GetComponent<Collider2D>().enabled = false;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                this.enabled = false;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canYouJump = true;
            if (lifeState == true)
            {
                GetComponent<Collider2D>().enabled = false;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                this.enabled = false;
            }
        }
    }
}
