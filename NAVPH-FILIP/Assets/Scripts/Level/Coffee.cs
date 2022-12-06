using UnityEngine;

public class Coffee : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.SetSpeed(1.2f * 3f, 1.2f * 5f);
            player.SetCoffeeTimer();
            Destroy(gameObject);
        }
    }
}
