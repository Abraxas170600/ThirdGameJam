using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

[RequireComponent(typeof(EnemySpawner))]
public class WaveManager : MonoBehaviour
{
    [SerializeField] private int difficultyLimitWave = 10;
    [SerializeField] private List<Enemy> enemyTypes;
    [SerializeField] private UltEvent<int, int> gameValuesEvents;
    [SerializeField] private UltEvent completeWaveEvents;

    private EnemySpawner spawner;
    private int waveNumber = 0;
    private int killsAmount = 1;
    private EnemyTracker enemyTracker;

    private void Start()
    {
        AudioManager.Instance.PlayMusic(EnumSounds.Sound_Gameplay);
        spawner = GetComponent<EnemySpawner>();
        spawner.Initialize(enemyTypes);
        enemyTracker = new(this);
        StartCoroutine(StartNextWave());
    }

    private IEnumerator StartNextWave()
    {
        waveNumber++;
        PlayerPrefs.SetInt("MaxWave", waveNumber);
        completeWaveEvents.Invoke();

        yield return new WaitForSeconds(4f);

        int enemyCount = waveNumber <= difficultyLimitWave ? waveNumber * 3 : difficultyLimitWave * 3;
        float spawnRate = waveNumber <= difficultyLimitWave ? Mathf.Max(1f, 2.5f - (waveNumber * 0.1f)) : Mathf.Max(1f, 2.5f - (difficultyLimitWave * 0.1f));

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
        StartCoroutine(StartNextWave());
    }

    public void NotifyEnemyDestroyed()
    {
        enemyTracker.DecreaseEnemyCount();
        gameValuesEvents.Invoke(killsAmount++, waveNumber);
        PlayerPrefs.SetInt("HighEnemiesKilled", killsAmount);
    }
}
