using UnityEngine;

public class RarePowerup : MonoBehaviour
{
    [SerializeField] private float duration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.ActivateHoming(duration);
            }
            Destroy(gameObject);
        }
    }
}