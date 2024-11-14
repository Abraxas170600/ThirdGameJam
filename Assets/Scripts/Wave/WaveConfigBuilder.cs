using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveConfigBuilder
{
    private int enemyCount;
    private float spawnRate;
    private List<Enemy> enemyTypes = new();
    private int difficultyLimitWave;

    public WaveConfigBuilder SetEnemyCount(int count)
    {
        enemyCount = count;
        return this;
    }

    public WaveConfigBuilder SetSpawnRate(float rate)
    {
        spawnRate = rate;
        return this;
    }

    public WaveConfigBuilder AddEnemyType(List<Enemy> type)
    {
        enemyTypes.AddRange(type);
        return this;
    }

    public WaveConfigBuilder SetDifficultyLimitWave(int limitWave)
    {
        difficultyLimitWave = limitWave;
        return this;
    }

    public WaveConfig Build()
    {
        return new(enemyCount, spawnRate, enemyTypes, difficultyLimitWave);
    }
}
