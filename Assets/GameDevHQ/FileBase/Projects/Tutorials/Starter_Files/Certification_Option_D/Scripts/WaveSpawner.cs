using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Wave
{
    public string waveName;
    public int enemyTotal;
    public GameObject[] enemyTypes;
    public float spawnInteval;
}

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves;
    private Wave _currentWave;
    private int _waveNumber;
    private bool _canSpawn = true;
    private float _nextSpawnTime;

    private void Start()
    {
        UIManager.instance.WaveCount(_waveNumber + 1);
    }

    private void Update()
    {
        _currentWave = waves[_waveNumber];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (totalEnemies.Length == 0 && !_canSpawn && _waveNumber +1 != waves.Length)
        {
                NextWave();
            if (_waveNumber < 13)
                UIManager.instance.WaveCount(_waveNumber + 1);
            else if (_waveNumber >= 13)
                UIManager.instance.FinalBoss();
        }

    }

    private void SpawnWave()
    {
        if (_canSpawn == true && _nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = _currentWave.enemyTypes[Random.Range(0, _currentWave.enemyTypes.Length)];
            Instantiate(randomEnemy, transform.position, Quaternion.identity);
            _currentWave.enemyTotal--;
            _nextSpawnTime = Time.time + _currentWave.spawnInteval;
            if (_currentWave.enemyTotal == 0)
            {
                _canSpawn = false;
            }
        }
    }

    private void NextWave()
    {
        _waveNumber++;
        _canSpawn = true;
    }
}
