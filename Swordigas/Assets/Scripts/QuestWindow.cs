using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestWindow : MonoBehaviour
{
    public GameObject questWindowSprite;
    public Transform playerPos;
    public Transform duckPos;

    bool isAlreadyActive = false;

    [SerializeField] float ShowingWidth;

    TextCode textCode;

    private void Start()
    {
        textCode = FindObjectOfType<TextCode>();
    }

    private void Update()
    {
        if (textCode.getBoolMove() == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isAlreadyActive)
                {
                    questWindowSprite.SetActive(true);
                    isAlreadyActive = true;
                }
                else
                {
                    questWindowSprite.SetActive(false);
                    isAlreadyActive = false;
                }
            }
        }

        if (Mathf.Abs(Mathf.Abs(playerPos.position.x) - Mathf.Abs(duckPos.position.x)) > ShowingWidth)
        {
            questWindowSprite.SetActive(false);
        }
    }

}
