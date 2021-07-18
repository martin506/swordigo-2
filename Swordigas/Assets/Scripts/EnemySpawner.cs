using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject slimePrefab;
    public int randomNumber;

    void Start()
    {
        SpawnEnemy();
    }

    void Update()
    {
        
    }

    public void SpawnEnemy()
    {
        randomNumber = Random.Range(1, 3);
        if (randomNumber == 1)
        {
            Instantiate(slimePrefab, transform.position, Quaternion.identity);
        }
    }
}
