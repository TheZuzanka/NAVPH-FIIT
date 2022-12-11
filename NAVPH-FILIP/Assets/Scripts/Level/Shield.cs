using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var enemy in levelManager.enemies)
            {
                enemy.isBlocked = true;
                enemy.shieldDelegate(enemy.isBlocked);
            }
            Destroy(gameObject);
        }
    }
}
