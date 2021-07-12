using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public Player player;

    public GameObject questWindow;

    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
    }
}
