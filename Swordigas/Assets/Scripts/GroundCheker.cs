using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheker : MonoBehaviour
{
	// Start is called before the first frame update
	public Jump jump;
    public Player player;
	public Animator animator;

    private float playerSpeed;
    private float reducedPlayerSpeed;
    private bool attacks;

	void Start()
    {
		jump = FindObjectOfType<Jump>();
        player = FindObjectOfType<Player>();

        playerSpeed = player.GetPlayerSpeed();
        reducedPlayerSpeed = playerSpeed / 5;
    }

    private void Update()
    {
        attacks = player.GetIsAttacking();
    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.tag == "Ground")
        {
            jump.ResetJumpCount();
			animator.SetBool("isFalling", false);
		}
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (attacks == true)
        {
            player.ChangePlayerSpeed(reducedPlayerSpeed);
        }
        else
        {
            player.ChangePlayerSpeed(playerSpeed);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
	{

        player.ChangePlayerSpeed(playerSpeed);
		animator.SetBool("isFalling", true);

	}

}
