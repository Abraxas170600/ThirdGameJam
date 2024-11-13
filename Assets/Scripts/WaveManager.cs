using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

[RequireComponent(typeof(EnemySpawner))]
public class WaveManager : MonoBehaviour
{
    [SerializeField] private int difficultyLimitWave = 10;
    [SerializeField] private List<Enemy> enemyTypes;
    [SerializeField] private UltEvent<int, int> waveEvents;

    private EnemySpawner spawner;
    private int waveNumber = 0;
    private int killsAmount = 1;
    private EnemyTracker enemyTracker;

    private void Start()
    {
        spawner = GetComponent<EnemySpawner>();
        spawner.Initialize(enemyTypes);
        enemyTracker = new(this);

        StartNextWave();
    }

    private void StartNextWave()
    {
        waveNumber++;
        PlayerPrefs.SetInt("MaxWave", waveNumber);

        int enemyCount = waveNumber <= difficultyLimitWave ? waveNumber * 5 : difficultyLimitWave * 5;
        float spawnRate = waveNumber <= difficultyLimitWave ? Mathf.Max(0.5f, 1.6f - (waveNumber * 0.1f)) : Mathf.Max(0.5f, 1.6f - (difficultyLimitWave * 0.1f));

        WaveConfig waveConfig = new WaveConfigBuilder()
            .SetEnemyCount(enemyCount)
            .SetSpawnRate(spawnRate)
            .SetDifficultyLimitWave(difficultyLimitWave)
            .AddEnemyType(enemyTypes)
            .Build();

        if (enemyTypes != null && waveNumber > 1)
        {
            foreach (var enemyType in enemyTypes)
            {
                waveConfig.EnemyTypes.Add(enemyType);
            }
        }

        enemyTracker.SetEnemyCount(waveConfig.EnemyCount);
        spawner.SpawnWave(waveConfig, NotifyEnemyDestroyed);
    }

    public void OnWaveCleared()
    {
        StartNextWave();
    }

    public void NotifyEnemyDestroyed()
    {
        enemyTracker.DecreaseEnemyCount();
        waveEvents.Invoke(killsAmount++, waveNumber);
        PlayerPrefs.SetInt("HighEnemiesKilled", killsAmount);
    }
}
