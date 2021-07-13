using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    public int requiredAmount;
    public int currentAmount;
    public string objectTag;

    public void EnemyKilled(string tag)
    {
        if (goalType == GoalType.Kill)
        {
            if (tag == objectTag)
            {
                currentAmount++;
            }
        }
    }

    public void ItemGathered(string tag)
    {
        if (goalType == GoalType.Gathering)
        {
            if (tag == objectTag)
            {
                currentAmount++;
            }
        }
    }

    public bool isReached()
    {
        return (currentAmount >= requiredAmount);
    }
}

public enum GoalType
{
    Kill,
    Gathering
}