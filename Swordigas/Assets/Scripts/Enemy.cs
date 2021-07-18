using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    public Animator animator;
    public Transform playerPosition;

    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] float jumpX;
    [SerializeField] float jumpY;
    bool canYouJump = true;
    bool lookingRight = true;
    [SerializeField] float jumpDelay = 0.5f;
    private float nextJumpTime;
    public bool startJumping;
    private int jumpingState = 0;

    public bool lifeState = false;

    public float attackCD = 0.5f;
    private float nextAttackTime;


    [Header("Deal Damage")]
    private Player player;
    [SerializeField] int slimeDamage = 20;

    [Header("Take Damage")]
    public GameObject bloodFX;

    public Rigidbody2D rigidBody;

    [Header("Save Data")]
    //public int id;
    public DataSaverAndLoader saver;

    void Start()
    {      

        //id = GetInstanceID();
        currentHealth = maxHealth;
        nextJumpTime = 0;

        saver = FindObjectOfType<DataSaverAndLoader>();

        player = FindObjectOfType<Player>();

        playerPosition = player.transform;
    }

    public void resetJumpingState()
    {
        jumpingState = 2;
    }

    private void Update()
    {
        if (saver.GetSaveEnemyData())
        {
            SaveEnemy();
        }

        if (saver.GetLoadEnemyData())
        {
            LoadEnemy();
            if (lifeState == false)
            {
                BringEnemyBackToLife();
            }
            if (lifeState == true)
            {
                Destroy(gameObject);
            }
        }

        if (!lifeState)
        {
            CheckPlayerPosition();
            if (startJumping == true)
            {
                Move();
            }
        }
        
    }

    private void CheckPlayerPosition()
    {
        if (Math.Abs(playerPosition.position.x + gameObject.transform.position.x * -1) < 10)
        {
            startJumping = true;
        }
        if (lookingRight == false)
        {
            if (playerPosition.position.x >= gameObject.transform.position.x)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                lookingRight = true;
                jumpingState = 0;
            }
        }
        if (lookingRight == true)
        {
            if (playerPosition.position.x <= gameObject.transform.position.x)
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
                lookingRight = false;
                jumpingState = 0;
            }
        }
    }

    private void Move()
    {
        if (nextJumpTime <= Time.time)
        {
            if (canYouJump == true)
            {
                canYouJump = false;
                if (jumpingState == 0)
                {
                    if (lookingRight == true)
                    {
                        rigidBody.velocity += new Vector2(jumpX * 1, jumpY);
                    }
                    else
                    {
                        rigidBody.velocity += new Vector2(jumpX * -1, jumpY);
                    }
                }
                if (jumpingState == 1)
                {
                    if (lookingRight == true)
                    {
                        rigidBody.velocity += new Vector2(4f, 7f);
                    }
                    else
                    {
                        rigidBody.velocity += new Vector2(-4f, 7f);
                    }
                    jumpingState = 0;
                }
                if (jumpingState == 2)
                {
                    if (lookingRight == true)
                    {
                        rigidBody.velocity += new Vector2(-1.5f, 1.5f);
                    }
                    else
                    {
                        rigidBody.velocity += new Vector2(1.5f, 1.5f);
                    }
                    jumpingState = 1;
                }
            }
        }
    }

    public void takeDamage(int damage)
    {
        if (lifeState == false)
        {
            currentHealth -= damage;

            animator.SetTrigger("hurt");

            // particles btiches
            Instantiate(bloodFX, transform.position, Quaternion.identity);

            if (currentHealth <= 0)
            {
                Die();

                // goal check
                player.KillEnemy(gameObject.tag);
            }
        }
    }

    private void Die()
    {
        lifeState = true;

        animator.SetBool("isDead", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (lifeState == true)
            {
                GetComponent<Collider2D>().enabled = false;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }

        if (collision.gameObject.tag == "Player")
        {
            player.playerTakeDamage(slimeDamage);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (lifeState == true)
            {
                GetComponent<Collider2D>().enabled = false;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }
    }

    public void CheckGround()
    {
        canYouJump = true;
    }

    public void DelayJump()
    {
        if (canYouJump == false)
        {
            nextJumpTime = Time.time + jumpDelay;
        }
    }

    public void BringEnemyBackToLife()
    {
        animator.SetBool("isDead", false);

        GetComponent<Collider2D>().enabled = true;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        this.enabled = true;
    }

    // Save and Load system 
    public void SaveEnemy()
    {
        SaveSystem.SaveEnemy(this);
    }

    public void LoadEnemy()
    {
        EnemyData data = SaveSystem.LoadEnemy(this);

        //id = data.id;
        currentHealth = data.currentHitPoints;

        Vector3 enemyPos;
        enemyPos.x = data.position[0];
        enemyPos.y = data.position[1];
        enemyPos.z = data.position[2];

        transform.position = enemyPos;

        startJumping = data.startJumping;

        lifeState = data.lifeState;
    }
}
