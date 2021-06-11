using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheker : MonoBehaviour
{
	// Start is called before the first frame update
	public Jump jump;
    void Start()
    {
		jump = FindObjectOfType<Jump>();
    }
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Ground")
		{
			jump.ResetJumpCount();
		}
	}
}
