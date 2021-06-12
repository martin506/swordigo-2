using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
	[Header("Jumping")]
	[Range(1, 10)]
	public float jumpVelocity = 10f;
	public Animator animator;
	public float jumpCount = 2f;
	Rigidbody2D rigidBody2D;

	private void Start()
	{
		rigidBody2D = GetComponent<Rigidbody2D>();
	}
	void Update()
    {
		if (jumpCount > 0)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				animator.SetBool("isJumping", true);
				GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
				jumpCount--;
			}
			if (rigidBody2D.velocity.y < 2 && rigidBody2D.velocity.y > -1)
			{
				animator.SetBool("isFalling", false);
			}
		}
	}
	public void ResetJumpCount()
	{
		jumpCount = 2;
	}
}
