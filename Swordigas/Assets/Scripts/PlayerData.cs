using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int attackDamage;

    public int maxHitPoints;
    public int currentHitPoints;

    public int money;
    public int experience;

    public float[] position;

    public bool questIsActive;

    public string questTitle;
    public string questDescription;

    public int questMoneyReward;
    public int questXPRevard;

    public string questGoalType;

    public int questRequiredAmount;
    public int questCurrentAmount;

    public string questObjectTag;

    public PlayerData (Player player)
    {
        attackDamage = player.attackDamage;
        maxHitPoints = player.maxHitPoints;
        currentHitPoints = player.currentHeath;

        money = player.money;
        experience = player.experience;

        position = new float[3];

        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        questIsActive = player.quest.isActive;
        questTitle = player.quest.title;
        questDescription = player.quest.description;
        questMoneyReward = player.quest.moneyReward;
        questXPRevard = player.quest.xpRevard;

        questGoalType = player.quest.goal.goalType.ToString();
        questRequiredAmount = player.quest.goal.requiredAmount;
        questCurrentAmount = player.quest.goal.currentAmount;
        questObjectTag = player.quest.goal.objectTag;
    }
}
