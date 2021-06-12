using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheker : MonoBehaviour
{
	// Start is called before the first frame update
	public Jump jump;
    public Player player;

	void Start()
    {
		jump = FindObjectOfType<Jump>();
        player = FindObjectOfType<Player>();

    }
	private void OnTriggerStay2D(Collider2D collision)
	{
        if (collision.tag == "Ground")
        {
            jump.ResetJumpCount();
            if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("slowing Down");
                player.SlowDown();
            }
        }
	}

}
