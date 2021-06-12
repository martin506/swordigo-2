using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Moving")]
    public float speed;
    private float staticSpeed;
	private Vector3 scaleChange = new Vector3(8f, 0f, 0f);

    [Header("Attack")]
    public Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    public LayerMask enemyLayers;

    [Header("Cached objectas")]
    public Rigidbody2D rigidBody2D;
    private Animator animator;


	void Start()
    {
		animator = GetComponent<Animator>();
		rigidBody2D = GetComponent<Rigidbody2D>();
        staticSpeed = speed;
    }

    void Update()
    {
        Running();
        if (Input.GetKeyDown(KeyCode.X))
        {
            Attack();
        }
    }

    public void SlowDown()
    {
        StartCoroutine("ReduceSpeed");
    }

    private void Attack()
    {
        // PLay an attack animation
        animator.SetTrigger("Attack");
        animator.SetBool("isJumping", false);

		// Detect enemies in range of the attack
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Do damage to enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("we hit: " + enemy.name);
        }
    }

    private IEnumerator ReduceSpeed()
    {
        float oldSpeed = staticSpeed;
        speed /= 3;
        yield return new WaitForSeconds(1.1f);
        speed = oldSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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
