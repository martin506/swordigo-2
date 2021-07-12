using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
	public Animator animator;
	private float increasedPlayerSpeed;
	private float playerSpeed;
	Player player;
	// Start is called before the first frame update
	void Start()
    {
		player = FindObjectOfType<Player>();

		playerSpeed = player.GetPlayerSpeed();

		increasedPlayerSpeed = playerSpeed * 2;

	}

    // Update is called once per frame
    void Update()
    {
		Sliding();
    }
	public void Sliding()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			animator.SetTrigger("Slide");
			player.ChangePlayerSpeed(increasedPlayerSpeed);
		}
	}
}
