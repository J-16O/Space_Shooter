using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class GameManager : MonoBehaviour
{
   [SerializeField]
   private bool _isGameOver;
   [SerializeField]
   private Text _levelText;
   public int currentLevel = 1;
   [SerializeField]
   private bool spawning = false;
   private bool asteroidDestroyed = false;
   [SerializeField] private GameObject _enemyPrefab;
   private BossManager _bossManager;
   [SerializeField] private Text _littleLevelText;
   public SpawnManager spawnManager;
   [SerializeField] private GameObject _enemy;


   void Start()
   {
      OnAsteroidDestroyed();
   }

   void OnAsteroidDestroyed()
   {
      asteroidDestroyed = true;
      StartCoroutine(StartLevel(currentLevel));
   }
   
   IEnumerator StartLevel(int level)
   {
         spawning = true;
         _levelText.text = "LEVEL " + level; 
         _levelText.gameObject.SetActive(true); 
         yield return new WaitForSeconds(2f);
         _levelText.gameObject.SetActive(false);
         
         _littleLevelText.text = "Level " + level;
      
         if (level <= 3)
         {
            int  enemyCount = Random.Range(5, 7); 
            if (level == 2) enemyCount = Random.Range(8, 10);
            else if (level == 3) enemyCount = Random.Range(11, 13);
            for (int i = 0; i < enemyCount; i++)
            {
               Vector3 spawnPos = new Vector3(Random.Range(-8f, 8f), 6f, 0f);
               Instantiate(_enemy, spawnPos, Quaternion.identity);
               yield return new WaitForSeconds(1f);
            }
            spawning = false;
         }
         else if (currentLevel == 4)
         { 
           
            _bossManager.StartBossSequence();
         }
      
   }
   
   
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
      {
         SceneManager.LoadScene(1);
      }

      if (Input.GetKeyDown(KeyCode.Escape))
      {
         Application.Quit();
      }
      BossComplete();
      
      
   }

   public void BossComplete()
   {
         if (asteroidDestroyed && !spawning && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
         {
            currentLevel++; // ✅ Always increase level FIRST

            if (currentLevel <= 3)
            {
               StartCoroutine(StartLevel(currentLevel));
            }
            else if (currentLevel == 4)
            {
               _levelText.text = "BOSS COMING...";
               _levelText.gameObject.SetActive(true); // ✅ Show the message
               StartCoroutine(StartBossDelay());
            }
         }

   }
   
   
   IEnumerator StartBossDelay()
   {
      yield return new WaitForSeconds(2f);
      _levelText.gameObject.SetActive(false);
      _bossManager.StartBossSequence();
   }
   
   
  public void GameOver()
   {
      Debug.Log("GameManager::GameOver");
      _isGameOver = true;
  }
}
//solved

// asteroid damage
// laser enemy damage 
// boss is not coming

