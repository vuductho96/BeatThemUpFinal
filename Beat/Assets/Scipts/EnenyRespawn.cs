using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnenyRespawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxEnemyCount = 10;
    public float respawnInterval = 5f;
    public GameObject[] enemyPool;

    private void Start()
    {
        InitializeEnemyPool();
        StartCoroutine(RespawnEnemies());
    }

    private void InitializeEnemyPool()
    {
        enemyPool = new GameObject[maxEnemyCount];
        for (int i = 0; i < maxEnemyCount; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, transform);
            newEnemy.SetActive(false);
            enemyPool[i] = newEnemy;
        }
    }

    private IEnumerator RespawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnInterval);

            GameObject availableEnemy = GetAvailableEnemy();
            if (availableEnemy != null)
            {
                availableEnemy.SetActive(true);
                availableEnemy.transform.position = GetRandomPosition();
                // Call any initialization or setup functions on the enemy here
            }
        }
    }

    private GameObject GetAvailableEnemy()
    {
        for (int i = 0; i < maxEnemyCount; i++)
        {
            if (!enemyPool[i].activeSelf)
            {
                return enemyPool[i];
            }
        }
        return null;
    }

    private Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(-10f, 10f);
        float randomY = Random.Range(-10f, 10f);
        return new Vector3(randomX, randomY, 0f);
    }
}
