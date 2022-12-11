using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.shieldActive = true;
            player.shieldDelegate(player.shieldActive);
            Destroy(gameObject);
        }
    }
}
