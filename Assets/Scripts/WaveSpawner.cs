using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner Instance; // So enemies can call EnemyDied()

    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;

    public float timeBetweenEnemies = 0.5f;
    public float timeBetweenWaves = 3f;
    public int enemiesPerWave = 5;
    public int waveIncrease = 2;

    private int currentWave = 1;
    private int enemiesAlive = 0;
    
    private bool _isGameOver = false;

    void Awake()
    {
        Instance = this;
    }
    
    IEnumerator SpawnWave()
    {
        if (_isGameOver) yield break;
        
        Debug.Log("Wave " + currentWave + " starting...");

        for (int i = 0; i < enemiesPerWave; i++)
        {
            if (_isGameOver) yield break; // 🔒 Check during loop
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenEnemies);
        }

        while (enemiesAlive > 0)
        {
            if (_isGameOver) yield break;
            yield return null;
        }

        enemiesPerWave += waveIncrease;
        currentWave++;
        yield return new WaitForSeconds(timeBetweenWaves);

        StartCoroutine(SpawnWave());
        Debug.Log("Wave " + currentWave + " starting...");

        for (int i = 0; i < enemiesPerWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenEnemies);
        }

        while (enemiesAlive > 0)
        {
            yield return null;
        }

        Debug.Log(enemiesAlive);

        enemiesPerWave += waveIncrease;
        currentWave++;
        yield return new WaitForSeconds(timeBetweenWaves);

        StartCoroutine(SpawnWave());
    }

    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        GameObject enemy = Instantiate(
            enemyPrefabs[randomIndex],
            spawnPoints[spawnPointIndex].position,
            Quaternion.identity
        );

        enemiesAlive++;
    }

    public void EnemyDied()
    {
        enemiesAlive--;
    }
    
    public void StartWaves()
    {
        StartCoroutine(SpawnWave());
    }

     public void GameOver()
    {
        StopAllCoroutines(); // Stop spawning new enemies

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy); // Or SetActive(false) for object pooling
        }
    }
}