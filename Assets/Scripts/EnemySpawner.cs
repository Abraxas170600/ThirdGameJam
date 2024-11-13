using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Pool Settings")]
    [SerializeField] private int initialEnemyCount = 10;
    private readonly List<Enemy> enemyPool = new();

    [Header("Spawn Settings")]
    public Transform[] spawnPoints;

    [Header("Enemy Type")]
    private List<Enemy> enemyTypes = new();

    public void Initialize(List<Enemy> enemyTypes)
    {
        SetEnemyTypes(enemyTypes);
        CreateEnemyPool(initialEnemyCount);
    }

    private void CreateEnemyPool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int enemyTypeIndex = UnityEngine.Random.Range(0, enemyTypes.Count - 1);
            Enemy enemy = Instantiate(enemyTypes[enemyTypeIndex], Vector3.zero, Quaternion.identity);
            enemy.gameObject.SetActive(false);
            enemyPool.Add(enemy);
        }
    }

    public void SpawnWave(WaveConfig config, Action notifyEnemyDestroyed)
    {
        StartCoroutine(SpawnEnemies(config, notifyEnemyDestroyed));
    }

    private IEnumerator SpawnEnemies(WaveConfig config, Action notifyEnemyDestroyed)
    {
        for (int i = 0; i < config.EnemyCount; i++)
        {
            int spawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
            Enemy enemy = GetEnemyFromPool();
            if (enemy != null)
            {
                enemy.transform.position = spawnPoints[spawnPointIndex].position;
                enemy.gameObject.SetActive(true);
                if (enemy.DefeatEvent == null)
                {
                    enemy.DefeatEvent += notifyEnemyDestroyed;
                }
            }
            yield return new WaitForSeconds(config.SpawnRate);
        }
    }

    private Enemy GetEnemyFromPool()
    {
        for (int i = 0; i < enemyPool.Count; i++)
        {
            if (!enemyPool[i].gameObject.activeInHierarchy)
            {
                return enemyPool[i];
            }
        }
        CreateEnemyPool(1);
        return enemyPool[^1];
    }
    public void SetEnemyTypes(List<Enemy> enemyTypes)
    {
        this.enemyTypes = enemyTypes;
    }
}
