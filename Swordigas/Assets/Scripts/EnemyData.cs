using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public int id;

    public float[] position;

    public int currentHitPoints;

    public bool startJumping;
    public bool lifeState;

    public EnemyData(Enemy enemy)
    {
        id = enemy.id;

        position = new float[3];
        position[0] = enemy.transform.position.x;
        position[1] = enemy.transform.position.y;
        position[2] = enemy.transform.position.z;

        currentHitPoints = enemy.currentHealth;

        startJumping = enemy.startJumping;

        lifeState = enemy.lifeState;
    }
}
