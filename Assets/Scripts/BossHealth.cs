using UnityEngine;

public class BossHealth : MonoBehaviour
{
    private int _health;
    private System.Action _onDefeated;

    public void Initialize(int hp, System.Action onDeath)
    {
        _health = hp;
        _onDefeated = onDeath;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            _health--;
            Destroy(other.gameObject);

            if (_health <= 0)
            {
                Destroy(gameObject);
                _onDefeated?.Invoke();
            }
        }
    }
    
}
