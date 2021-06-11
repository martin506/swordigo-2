using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
	[Header("Jumping")]
	[Range(1, 10)]
	public float jumpVelocity;

	void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
		}
	}
}
