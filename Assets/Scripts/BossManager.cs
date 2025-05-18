using UnityEngine;

public class BossManager : MonoBehaviour
{
    
    public GameObject bossPrefab;
    public Transform spawnPoint; // Center of the screen
    public int bossHealth = 10;
    
    private GameObject bossInstance;
    
    public void StartBossSequence()
    {
        // Call this AFTER "BOSS is coming" text is shown
        Invoke(nameof(SpawnBoss), 5f);
    }
    
    void SpawnBoss()
    {
        bossInstance = Instantiate(bossPrefab, spawnPoint.position, Quaternion.identity);
        BossHealth healthScript = bossInstance.AddComponent<BossHealth>();
        healthScript.Initialize(bossHealth, OnBossDefeated);
    }

    void OnBossDefeated()
    {
        Debug.Log("Boss defeated! Game over!");
        // Add logic here like ending the game, showing a win screen, etc.
        // Example: SceneManager.LoadScene("WinScene");
    }
}

