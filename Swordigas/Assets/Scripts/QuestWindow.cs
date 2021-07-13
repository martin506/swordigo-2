using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestWindow : MonoBehaviour
{
    public Quest quest;
    public Player player;

    public GameObject questWindowSprite;
    public Transform playerPos;
    public Transform duckPos;

    bool isAlreadyActive = false;

    [SerializeField] float ShowingWidth;

    TextCode textCode;

    public Text titleText;
    public Text descriptionText;
    public Text goldText;
    public Text experienceText;

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
                    isAlreadyActive = true;
                    OpenQuestWindow();

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

    private void OpenQuestWindow()
    {
        questWindowSprite.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        goldText.text = quest.moneyReward.ToString();
        experienceText.text = quest.xpRevard.ToString();
    }

    public void AcceptQuest() {
        quest.isActive = true;
        questWindowSprite.SetActive(false);
        player.quest = quest;
    }
}
