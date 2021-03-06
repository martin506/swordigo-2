using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Moving")]
    public float speed;
    private float staticSpeed;

    [Header("Attack")]
    public Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public bool isAttacking = false;
    public int attackDamage = 40;
    [SerializeField] float attackCD = 0.6f;
    private float nextAttackTime = 0;

    [Header("Cached objectas")]
    public Rigidbody2D rigidBody2D;
    private Animator animator;
    public Jump jump;
    public JumpPhysics jumpPhysics;
    public DeathScreen deathScreen;

    [Header("Hit Points")]
    public int maxHitPoints = 100;
    public int currentHeath;
    bool lifeState = false;
    public HealthBar healthBar;
    public GameObject bloodFX;

    [Header("Quests")]
    public Quest quest;

    [Header("Money")]
    public int money = 0;

    [Header("Experience")]
    public int experience = 0;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip slashSound;
    public AudioClip hurtSound;

	void Start()
    {
        currentHeath = maxHitPoints;
        healthBar.setMaxHealth(maxHitPoints);
		healthBar.setHealth(maxHitPoints);

		animator = GetComponent<Animator>();
		rigidBody2D = GetComponent<Rigidbody2D>();
        staticSpeed = speed;
    }

    void Update()
    {
        Running();
        if (Time.time > nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + attackCD;
            }
        }
    }

    public bool GetIsAttacking()
    {
        return isAttacking;
    }

    public float GetPlayerSpeed()
    {
        return speed;
    }

    public void ChangePlayerSpeed(float playerSpeed)
    {
        speed = playerSpeed;
    }

    private void Attack()
    {
        StartCoroutine("AttackTime");

        // PLay an attack animation
        animator.SetTrigger("Attack");
        animator.SetBool("isJumping", false);

        // play attack sound
        AudioSource.PlayClipAtPoint(slashSound, transform.position);

        StartCoroutine("DetectAndDamage");
    }

    public void KillEnemy(string tag)
    {
        if (quest.isActive)
        {
            quest.goal.EnemyKilled(tag);
            if (quest.goal.isReached())
            {
                money += quest.moneyReward;
                experience += quest.xpRevard;

                quest.Complete();
            }
        }
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

    public void playerTakeDamage(int damage)
    {
        currentHeath -= damage;

        healthBar.setHealth(currentHeath);

        // play hurt animation
        animator.SetTrigger("isHurt");

        // play particles
        Instantiate(bloodFX, transform.position, Quaternion.identity);

        // play hurt sound
        AudioSource.PlayClipAtPoint(hurtSound, transform.position);

        if (currentHeath <= 0)
        {
            // play die animation
            animator.SetBool("isDead", true);

            // pop up the death screen
            deathScreen.ItIsTimeToShowDeathScreen();

            // disable the player
            StartCoroutine("StopDeathAnimation");
        }
    }


    // IEnumerators
    private IEnumerator StopDeathAnimation()
    {
        lifeState = true;
        jump.enabled = false;
        jumpPhysics.enabled = false;
        yield return new WaitForSeconds(1f);
        animator.speed = 0;
    }

    private IEnumerator DetectAndDamage()
    {
        yield return new WaitForSeconds(0.1f);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().takeDamage(attackDamage);
        }
    }

    private IEnumerator AttackTime()
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
    }


    // collision functions
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (lifeState == true)
            {
                GetComponent<Collider2D>().enabled = false;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                this.enabled = false;
            }
        }
    }


    // get functions
    public int GetMoney()
    {
        return money;
    }

    public int GetExperience()
    {
        return experience;
    }

    // data functions
    public void BringBackToLife()
    {
        animator.SetBool("isDead", false);
        this.enabled = true;
        lifeState = false;
        jump.enabled = true;
        jumpPhysics.enabled = true;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Rigidbody2D>().gravityScale = 2;
        animator.speed = 1;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        attackDamage = data.attackDamage;

        maxHitPoints = data.maxHitPoints;
        currentHeath = data.currentHitPoints;

        healthBar.setHealth(currentHeath);

        money = data.money;
        experience = data.experience;

        Vector3 playerPos;
        playerPos.x = data.position[0];
        playerPos.y = data.position[1];
        playerPos.z = data.position[2];

        transform.position = playerPos;

        quest.isActive = data.questIsActive;
        quest.title = data.questTitle;
        quest.description = data.questDescription;
        quest.moneyReward = data.questMoneyReward;
        quest.xpRevard = data.questXPRevard;

        quest.goal.goalType = (GoalType)System.Enum.Parse(  typeof(GoalType), data.questGoalType );
        // YourEnumType parsed_enum = (YourEnumType)System.Enum.Parse( typeof(YourEnumType), your_string );
        quest.goal.requiredAmount = data.questRequiredAmount;
        quest.goal.currentAmount = data.questCurrentAmount;
        quest.goal.objectTag = data.questObjectTag;

    }
}
