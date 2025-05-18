using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _speed = 7.0f;
    [SerializeField] private AudioClip _ammoBuzzer;
    private bool _isEnemyLaser = false;

    public int _AmmoCount = 15;

    public bool isPlayerLaser = false; // Used to check ownership

    
    void Update()
    {
        // Move laser depending on owner
        if (_isEnemyLaser)
        {
            MoveDown();
        }
        else
        {
            MoveUp();
        }

        // Auto-destroy if too far from screen
        if (Mathf.Abs(transform.position.y) > 10f)
        {
            Destroy(this.gameObject);
        }
    }

    void MoveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= 8.0f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -8.0f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
        isPlayerLaser = false; // Ensure flag is consistent
    }

    public bool IsEnemyLaser()
    {
        return _isEnemyLaser;
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {// Enemy laser hits player
        if (other.CompareTag("Player") && _isEnemyLaser)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            Destroy(this.gameObject);
        }

        // Player laser hits enemy
        if (other.CompareTag("Enemy") && isPlayerLaser)
        {
            Destroy(other.gameObject); // destroy enemy
            Destroy(this.gameObject);  // destroy laser
        }

        // Laser hits pickup
        if (other.CompareTag("Pickup"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("SuperEnemy"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
//use only 15 lasers to shoot
