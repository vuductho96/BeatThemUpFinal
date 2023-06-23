using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnenyRespawn : MonoBehaviour
{
    public class Enemy : MonoBehaviour
    {
        public string enemyName;

        public void Initialize(string name)
        {
            enemyName = name;
            Debug.Log(enemyName + " spawned!");
        }

        public void DestroyEnemy()
        {
            Debug.Log(enemyName + " destroyed!");
            gameObject.SetActive(false);
        }
    }

    public class EnemyManager : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public int maxEnemyCount = 10;
        public float respawnInterval = 5f;
        private Enemy[] enemyPool;

        private void Start()
        {
            InitializeEnemyPool();
            StartCoroutine(RespawnEnemies());
        }

        private void InitializeEnemyPool()
        {
            enemyPool = new Enemy[maxEnemyCount];
            for (int i = 0; i < maxEnemyCount; i++)
            {
                GameObject newEnemy = Instantiate(enemyPrefab, transform);
                newEnemy.SetActive(false);
                enemyPool[i] = newEnemy.GetComponent<Enemy>();
            }
        }

        private IEnumerator RespawnEnemies()
        {
            while (true)
            {
                yield return new WaitForSeconds(respawnInterval);

                Enemy availableEnemy = GetAvailableEnemy();
                if (availableEnemy != null)
                {
                    availableEnemy.gameObject.SetActive(true);
                    availableEnemy.Initialize("Enemy" + (EnemyCount() + 1));
                    availableEnemy.transform.position = GetRandomPosition();
                }
            }
        }

        private Enemy GetAvailableEnemy()
        {
            for (int i = 0; i < maxEnemyCount; i++)
            {
                if (!enemyPool[i].gameObject.activeSelf)
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

        private int EnemyCount()
        {
            int count = 0;
            for (int i = 0; i < maxEnemyCount; i++)
            {
                if (enemyPool[i].gameObject.activeSelf)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
