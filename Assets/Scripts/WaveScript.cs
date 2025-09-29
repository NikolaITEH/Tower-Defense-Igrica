using UnityEngine;

[System.Serializable]
public class WaveScript
{
    public EnemySpawnEntry[] enemies;
    public float rate;
}

[System.Serializable]
public class EnemySpawnEntry
{
    public GameObject enemyPrefab;
    public int count;
}
