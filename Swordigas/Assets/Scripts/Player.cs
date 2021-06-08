using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private Vector3 scaleChange = new Vector3(8f, 0f, 0f);

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            Vector2 newTransform = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            transform.position = newTransform;
            transform.localScale = new Vector3(4f, 4f, 1f);
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            Vector2 newTransform = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            transform.position = newTransform;
            transform.localScale = new Vector3(-4f, 4f, 1f);
        }
    }
}
