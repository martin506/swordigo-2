using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Animator animator;

    public int maxHealth = 100;
    private int currentHealth;
    [SerializeField] float jumpX;
    [SerializeField] float jumpY;

    private bool lifeState = false;

    public Rigidbody2D rigidBody;

    void Start()
    {
        currentHealth = maxHealth;
        StartCoroutine("Move");
    }



    private IEnumerator Move()
    {
        float side = 1;
        for (int i = 0; i < 2; i++)
        {
            if (lifeState == false)
            {
                rigidBody.velocity += new Vector2(jumpX * side, jumpY);
                yield return new WaitForSeconds(1f);
                if (i == 1)
                {
                    i = 0;
                    side *= -1;
                }
            }
            else
            {
                i = 2;
            }
        }
    }

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
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        this.enabled = false;
    }
}
