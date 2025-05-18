using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _speed = 3f; 
    [SerializeField] private int _powerupID;
    [SerializeField] private AudioClip _clip;
    private Transform _player;
    private bool _magnetActive = false;


    void Start()
    {
        _player = GameObject.FindWithTag("Player")?.transform;
    }
   
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -7)
        {
            Destroy(gameObject);
        }
        
        if (_magnetActive && _player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_clip, transform.position);
            
            if (player != null)
            {
                switch (_powerupID)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                       player.SpeedBoosterActive();
                        break;
                    case 2:
                        player.ShieldsActive();
                        break;
                    case 3:
                        player.AmmoActive();
                        break;
                    case 4:
                        player.HealthActive();
                        break;
                    case 5:
                        player.MagiccallerActive();
                        break;
                    case 6 :
                        player.NegativeActive();
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }
            Destroy(gameObject);
        }

        if (other.tag == "Laser" && other.tag == "Enemy")
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
    
    public void ActivateMagnet()
    {
        _magnetActive = true;
    }

    
}

