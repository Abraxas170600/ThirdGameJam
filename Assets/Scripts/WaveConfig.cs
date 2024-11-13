using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveConfig : MonoBehaviour
{
    public int EnemyCount { get; private set; }
    public float SpawnRate { get; private set; }
    public List<Enemy> EnemyTypes { get; private set; }
    public int DifficultyLimitWave { get; private set; }

    public WaveConfig(int enemyCount, float spawnRate, List<Enemy> enemyTypes, int difficultyLimitWave)
    {
        EnemyCount = enemyCount;
        SpawnRate = spawnRate;
        EnemyTypes = enemyTypes;
        DifficultyLimitWave = difficultyLimitWave;
    }
}
