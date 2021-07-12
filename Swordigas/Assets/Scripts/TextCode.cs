using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCode : MonoBehaviour
{
    public Transform playerTransform;
    public Transform questGiver;

    public SpriteRenderer spriteRenderer;

    float basicYPos;
    float yPos;

    float yPosMax;
    float yPosMin;

    [SerializeField] float floatingHeight;
    [SerializeField] float speed;

    bool moveUp = true;

    [SerializeField] float WorkingWidth;

    bool move = false;

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
        CheckPlayerPos();

        if (move == true)
        {
            MoveUpAndDown();
        }


    }

    private void MoveUpAndDown()
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

    private void CheckPlayerPos()
    {
        float playerXPos = playerTransform.transform.position.x;
        float questGiverXPos = questGiver.transform.position.x;

        if (Mathf.Abs(Mathf.Abs(playerXPos) - Mathf.Abs(questGiverXPos)) < WorkingWidth)
        {
            move = true;
            spriteRenderer.enabled = true;
        }
        else
        {
            move = false;
            spriteRenderer.enabled = false;
        }
    }

    public bool getBoolMove()
    {
        return move;
    }
}
