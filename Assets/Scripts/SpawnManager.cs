using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] _powerups;
    private GameObject enemyPrefab;
    private GameObject ammoPrefab;
    private GameObject magiccalerPrefab;
    
    [SerializeField] private bool _stopSpawning = false;
   

    private float minEnemySpawnTime = 2f;
    private float maxEnemySpawnTime = 5f;

    private float minPickupSpawnTime = 4f;
    private float maxPickupSpawnTime = 8f;

    [SerializeField] private float nextEnemySpawnTime;
    [SerializeField] private float nextPickupSpawnTime;
    
    [SerializeField] private GameObject bossPrefab;
    public GameManager gameManager;
    
   
    
    void Start()
    {
        ScheduleNextEnemySpawn();
        ScheduleNextPickupSpawn();
        if (gameManager.currentLevel > 3 || _stopSpawning) return;
    }
    public void Update()
       {
           if (Time.time >= nextEnemySpawnTime)
           {
               SpawnEnemy();
               ScheduleNextEnemySpawn();
           }
   
           if (Time.time >= nextPickupSpawnTime)
           {
               SpawnPickup();
               ScheduleNextPickupSpawn();
           }
       }
    
    
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }
    

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false)
        {

            Vector3 posTospawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerup = Random.Range(0, 8);
            Instantiate(_powerups[randomPowerup], posTospawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, GetRandomPosition(), Quaternion.identity);
    }
    
    void SpawnPickup()
    {
        // Choose a pickup type based on weighted probabilities
        float chance = Random.Range(0f, 1f);

        
        if (chance < 0.8f)  // 80% chance for ammo
        {
            Instantiate(ammoPrefab, GetRandomPosition(), Quaternion.identity);
        }
        else if (chance < 0.05f)  // 5% chance for magiccaller
        {
            Instantiate(magiccalerPrefab, GetRandomPosition(), Quaternion.identity);
        }
    }
    
    void ScheduleNextEnemySpawn()
    {
        nextEnemySpawnTime = Time.time + Random.Range(minEnemySpawnTime, maxEnemySpawnTime);
        Debug.Log("Next enemy spawn at: " + nextEnemySpawnTime);
    }
    
    void ScheduleNextPickupSpawn()
    {
        nextPickupSpawnTime = Time.time + Random.Range(minPickupSpawnTime, maxPickupSpawnTime);
    }
    
    
    Vector3 GetRandomPosition()
    {
        float x = Random.Range(-8f, 8f);  
        float y = Random.Range(4f, 6f);
        return new Vector3(x, y, 0f);
    }
    
    public void SpawnBoss()
    {
        OnPlayerDeath(); // Stop all regular spawns
        Instantiate(bossPrefab, new Vector3(0, 6f, 0), Quaternion.identity); // Or whatever spawn position
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
    
}

