using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
	public Animator animator;
	private float increasedPlayerSpeed;
	private float playerSpeed;
	Player player;

    public GameObject dirtFX;
    public Transform dirtTransform;
	
	public BoxCollider2D boxCollider;
	// Size scale
	const float ySize = 0.1819278f;
	const float xSize = 0.3144184f;
	const float ySizeDefault = 0.3165592f;
	const float xSizeDefault = 0.1785374f;

	// Offset scale
	const float xOffset = -0.008369446f;
	const float yOffset = -0.12f;
	const float xOffsetDefault = -0.008369446f;
	const float yOffsetDefault = -0.05316057f;


	// Start is called before the first frame update
	void Start()
    {
		player = FindObjectOfType<Player>();

		playerSpeed = player.GetPlayerSpeed();

		boxCollider = GetComponent<BoxCollider2D>();

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
            animator.SetTrigger("Sliding");
		}
	}

    public void instantiateDirtParticles()
    {
        Instantiate(dirtFX, dirtTransform.position, dirtTransform.rotation);
    }

	public void BoxColliderShrink()
	{
		boxCollider.size = new Vector2(xSize, ySize);
		boxCollider.offset = new Vector2(xOffset, yOffset);
		Debug.Log("keicia");
	}
	public void BoxColliderNormalize()
	{
		boxCollider.size = new Vector2(xSizeDefault, ySizeDefault);
		boxCollider.offset = new Vector2(xOffsetDefault, yOffsetDefault);
		Debug.Log("keicia");
	}
}
