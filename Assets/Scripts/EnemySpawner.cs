using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefabs;
    public float spawnRate = 1.4f;
    float minLength = -8.5f;
    float maxLength = 8.5f;


    void Awake()
    {
        InvokeRepeating(nameof(IncreaseSpawnRate), 0, 15f);
    }

    public void StartSpawn()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    void Spawn()
    {
        GameObject enemies = Instantiate(enemyPrefabs, transform.position, Quaternion.identity);
        enemies.transform.position += Vector3.left * Random.Range(minLength, maxLength);
    }


    void IncreaseSpawnRate()
    {
        if (spawnRate > 0.2f)
        {
            spawnRate -= 0.2f;
        }

        if (spawnRate == 0.2f)
        {
            CancelInvoke("IncreaseSpawnrate");
        }
    }

    public void StopSpawn()
    {
        CancelInvoke(nameof(Spawn));
    }

}
