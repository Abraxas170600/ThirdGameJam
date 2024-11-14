using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    private int enemiesRemaining;
    private readonly WaveManager waveManager;

    public EnemyTracker(WaveManager manager)
    {
        waveManager = manager;
    }

    public void SetEnemyCount(int count)
    {
        enemiesRemaining = count;
    }

    public void DecreaseEnemyCount()
    {
        enemiesRemaining--;
        if (enemiesRemaining <= 0)
        {
            waveManager.OnWaveCleared();
        }
    }
}
