using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCode : MonoBehaviour
{
    public Transform playerTransform;

    float basicYPos;
    float yPos;

    float yPosMax;
    float yPosMin;

    [SerializeField] float floatingHeight;
    [SerializeField] float speed;

    bool moveUp = true;

    private void Start()
    {
        basicYPos = transform.position.y;
        yPos = transform.position.y;
        yPosMax = basicYPos += floatingHeight;
        yPosMin = basicYPos -= floatingHeight;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= yPosMax)
        {
            moveUp = false;
        }
        if (transform.position.y <= yPosMin)
        {
            moveUp = true;
        }

        if (moveUp)
        {
            yPos += speed;
        }
        else
        {
            yPos -= speed;
        }

        transform.position = new Vector2(transform.position.x, yPos);
    }
}
