using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class JumpPhysics : MonoBehaviour
{
	public float highJumpFall = 2.5f;
	public float lowJumpFall = 2;
	public Animator animator;
	Rigidbody2D rigidBody2D;

	void Start()
    {
		rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		Falling();
		if (rigidBody2D.velocity.y < 0)
		{
			rigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (highJumpFall - 1) * Time.deltaTime;
		}
		else if (rigidBody2D.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
		{
			rigidBody2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpFall - 1) * Time.deltaTime;

		}
	}
	public void Falling()
	{
		if (rigidBody2D.velocity.y < 0)
		{
			animator.SetBool("isJumping", false);
			animator.SetBool("isFalling", true);
		}
	}
}
